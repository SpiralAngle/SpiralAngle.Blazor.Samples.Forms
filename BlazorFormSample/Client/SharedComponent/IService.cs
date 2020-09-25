using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<Guid> AddItemAsync(TEntity item);
        Task DeleteItemAsync(TEntity item);
        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<Guid> UpdateItemAsync(TEntity item);
    }
}
