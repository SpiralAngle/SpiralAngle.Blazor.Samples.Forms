using BlazorFormSample.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.Shared
{
    public interface IEntityProvider<TEntity> where TEntity : class, IEntity, new()
    {
        Task<Guid> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<bool> UpdateAsync(TEntity entity);
    }
}
