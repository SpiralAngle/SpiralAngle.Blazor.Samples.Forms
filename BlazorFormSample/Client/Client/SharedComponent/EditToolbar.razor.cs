using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    // partial is needed to add generic constraints.
    // https://github.com/dotnet/aspnetcore/issues/8433
    public partial class EditToolbar<TEntity> where TEntity: class,IEntity
    {
        [Parameter]
        public IEditModel<TEntity> EditModel { get; set; }
    }
}
