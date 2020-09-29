using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public abstract class EntityListBase<TEntity>:ComponentBase where TEntity : class, IEntity, new()
    {
        [Inject]
        protected IService<TEntity> ApiService { get; set; }

        protected IEnumerable<TEntity> Entities;

        protected override async Task OnInitializedAsync()
        {
            await LoadItems();
        }

        private async Task LoadItems()
        {
            Entities = await ApiService.GetListAsync();
        }


        protected abstract string GetEntityLink(TEntity entity);        
    }
}
