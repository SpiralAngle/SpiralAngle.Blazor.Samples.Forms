using BlazorFormSample.Client.Shared;
using BlazorFormSample.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public interface IEditModel<TEntity> where TEntity : class, IEntity
    {
        IService<TEntity> Service { get; }

        EditContext EditContext { get; }

        TEntity Model { get; }

        bool ReadOnly { get; }

        bool WasFromNew { get; }

        bool ConfirmDelete { get; }

        bool HasChanges { get; }

        bool HideConfirmDelete { get; }

        bool HideDelete { get; }

        event Action<TEntity> OnModelSet;
        event Action<TEntity> ModelDeleted;
        event Action<TEntity> ModelSaved;
        event Action<TEntity> EditCanceled;
        event Action<TEntity> DeleteCanceled;
        event Action<TEntity> EditStarted;

        Task InitializeEditorAsync(Guid id);

        Task CancelAsync();

        void CancelDelete();

        void Delete();

        Task DeleteConfirmedAsync();

        void StartEdit();

        Task SaveAsync();
    }
}
