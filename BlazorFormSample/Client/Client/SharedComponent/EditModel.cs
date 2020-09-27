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

        public event Action ModelDeleted;
        public event Action ModelSaved;
        public event Action EditCanceled;
        public event Action DeleteCanceled;
        public event Action EditStarted;

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
            EditCanceled?.Invoke();
        }

        public void CancelDelete()
        {
            DeleteCanceled?.Invoke();
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
                ModelDeleted?.Invoke();
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
            ModelSaved?.Invoke();
        }

        public void StartEdit()
        {
            EditStarted?.Invoke();
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
