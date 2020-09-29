using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public abstract class EntityEditorBase<TEntity> : ComponentBase, IDisposable where TEntity : class, IEntity, new()
    {
        [Parameter]
        public string EntityId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IService<TEntity> EntityService { get; set; }

        protected EditModel<TEntity> EditModel { get; private set; }

        public abstract string BaseUri { get; }

        private bool WasFromNew;
        private bool _readOnly = true;
        
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                InvokeAsync(StateHasChanged);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            ReadOnly = true;
            WasFromNew = new Uri(NavigationManager.Uri).Segments.Last() == "new";
            EditModel = new EditModel<TEntity>(EntityService);
            EditModel.ModelDeleted += ModelDeleted;
            EditModel.ModelSaved += ModelSaved;
            EditModel.EditCanceled += EditCanceled;
            EditModel.EditStarted += EditStarted;
            await EditModel.InitializeEditorAsync(WasFromNew ? default : new Guid(EntityId));
            ReadOnly = !WasFromNew;

            //await base.OnParametersSetAsync();
        }


        private void ModelSaved(TEntity model)
        {
            ReadOnly = true;
            if (WasFromNew)
            {
                NavigationManager.NavigateTo($"{BaseUri}/{EditModel.Model.Id}");
            }
        }

        private void ModelDeleted(TEntity model)
        {
            ReadOnly = true;
            NavigationManager.NavigateTo(BaseUri);
        }

        private void EditCanceled(TEntity model)
        {
            ReadOnly = true;
            if (WasFromNew)
            {
                NavigationManager.NavigateTo(BaseUri);
            }
        }

        private void EditStarted(TEntity model)
        {
            ReadOnly = false;
        }

        public virtual void Dispose()
        {
            if (EditModel != null)
            {
                EditModel.ModelDeleted -= ModelDeleted;
                EditModel.ModelSaved -= ModelSaved;
                EditModel.EditCanceled -= EditCanceled;
                EditModel.EditStarted -= EditStarted;
                EditModel.Dispose();
            }
        }
    }
}
