using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public class EditModel<TEntity> : IEditModel<TEntity>, IDisposable where TEntity : class, IEntity, new()
    {
        public IService<TEntity> Service { get; private set; }

        public EditContext EditContext { get; private set; }

        public TEntity Model { get; private set; }

        public bool ReadOnly { get; set; }

        public bool WasFromNew { get; private set; }

        public bool ConfirmDelete { get; private set; }

        public bool HasChanges { get => EditContext.IsModified(); }

        public bool HideConfirmDelete
        {
            get
            {
                if (WasFromNew)
                    return true;
                return !ConfirmDelete;
            }
        }

        public bool HideDelete
        {
            get
            {
                return WasFromNew || ConfirmDelete;
            }
        }

        public event Action<TEntity> OnModelSet;
        public event Action<TEntity> ModelDeleted;
        public event Action<TEntity> ModelSaved;
        public event Action<TEntity> EditCanceled;
        public event Action<TEntity> DeleteCanceled;
        public event Action<TEntity> EditStarted;

        public EditModel(IService<TEntity> service)
        {
            Service = service;
        }

        public async Task InitializeEditorAsync(Guid id)
        {
            WasFromNew = id == default;
            if (WasFromNew)
            {
                Model = new TEntity();
                ReadOnly = false;
            }
            else
            {
                WasFromNew = false;
                Model = await Service.GetAsync(id);
                ReadOnly = true;
            }
            if (EditContext != null)
            {
                EditContext.OnFieldChanged -= FieldChanged;
            }
            EditContext = new EditContext(Model);
            EditContext.OnFieldChanged += FieldChanged;
            OnModelSet?.Invoke(Model);
        }

        private void FieldChanged(object sender, FieldChangedEventArgs e)
        {
            ConfirmDelete = false;
        }

        public async Task CancelAsync()
        {
            if (!WasFromNew)
            {                
                await InitializeEditorAsync(Model.Id);
            }
            EditCanceled?.Invoke(Model);
        }

        public void CancelDelete()
        {
            DeleteCanceled?.Invoke(Model);
            ConfirmDelete = false;
        }

        public void Delete()
        {
            ConfirmDelete = true;
        }

        public async Task DeleteConfirmedAsync()
        {
            if (!WasFromNew && ConfirmDelete)
            {
                await Service.DeleteItemAsync(Model);
                ModelDeleted?.Invoke(Model);
            }
            ConfirmDelete = false;
        }

        public  async Task SaveAsync()
        {
            Guid id;
            if (!EditContext.Validate())
                return;
            if (WasFromNew)
            {
                id = (await Service.AddItemAsync(Model));                
            }
            else
            {
                await Service.UpdateItemAsync(Model);
                id = Model.Id;
            }
            await InitializeEditorAsync(id);
            ModelSaved?.Invoke(Model);
        }

        public void StartEdit()
        {
            EditStarted?.Invoke(Model);
            ReadOnly = false;            
        }


        public void Dispose()
        {
            if (EditContext != null)
            {
                EditContext.OnFieldChanged -= FieldChanged;
            }
        }
    }
}
