using BlazorFormSample.Server.Data;
using BlazorFormSample.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.Shared
{
    public abstract class EntityProviderBase<TEntity> : IEntityProvider<TEntity> where TEntity : class, IEntity, new()
    {
        protected CreatureDbContext Context { get; }

        public EntityProviderBase(CreatureDbContext context)
        {
            Context = context;
        }

        public virtual async Task<Guid> AddAsync(TEntity entity)
        {
            SetIdIfNeeded(entity);
            SetChildren(entity);
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var gameSystem = await GetEntityAsync(id);
            if (gameSystem == null)
            {
                return false;
            }
            RemoveEntity(gameSystem);
            await Context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await GetEntityAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            SetChildren(entity);
            var existing = await GetEntityAsync(entity.Id);
            RemoveOrphans(existing, entity);

            Context.Entry(entity).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSystemExists(entity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private static (Type type, IEnumerable<PropertyInfo> listProperties) GetEntityProperties<T>(T entity) where T : IEntity
        {
            Type type = entity.GetType();
            return (type, type.GetProperties().Where
            (p =>
            p.PropertyType != typeof(string)
            && p.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
            && p.PropertyType.GenericTypeArguments.All(gta => gta.GetInterfaces().Contains(typeof(IEntity)))
            ));
        }

        private static void SetIdIfNeeded(IEntity entity)
        {
            entity.Id = entity.Id == default ? Guid.NewGuid() : entity.Id;
        }

        private void SetChildren<T>(T entity) where T : IEntity
        {
            var (type, listProperties) = GetEntityProperties(entity);
            foreach (var collection in listProperties)
            {

                var itemCollection = collection.GetValue(entity) as IEnumerable;
                var parentIdName = $"{type.Name}Id";
                PropertyInfo parentIdProperty = null;
                foreach (IEntity item in itemCollection)
                {
                    parentIdProperty ??= item.GetType().GetProperty(parentIdName);
                    SetIdIfNeeded(item);
                    var parentId = (Guid)parentIdProperty.GetValue(item);
                    if (parentId == default)
                    {
                        parentIdProperty.SetValue(item, entity.Id);
                        Context.Entry(item).State = EntityState.Added;
                    }
                    else
                    {
                        Context.Entry(item).State = EntityState.Modified;
                    }
                    SetChildren(item);
                }
            }
        }

        private void RemoveOrphans<T>(T existing, T incoming) where T : IEntity
        {
            var (type, listProperties) = GetEntityProperties(existing);
            foreach (var collection in listProperties)
            {
                var existingCollection = collection.GetValue(existing) as IEnumerable;
                var incomingCollection = collection.GetValue(incoming) as IEnumerable;

                foreach (IEntity existingItem in existingCollection)
                {
                    IEntity foundItem = null;
                    foreach (IEntity incomingItem in incomingCollection)
                    {
                        if (existingItem.Id == incomingItem.Id)
                        {
                            foundItem = incomingItem;
                            break;
                        }
                    }
                    if (foundItem == null)
                    {
                        RemoveEntity(existingItem);
                    }
                    else
                    {
                        RemoveOrphans(existingItem, foundItem);
                    }
                }
            }
        }

        private void RemoveEntity<T>(T entity) where T : IEntity
        {
            var (type, listProperties) = GetEntityProperties(entity);
            foreach (var collection in listProperties)
            {
                var coll = collection.GetValue(entity) as IEnumerable;

                foreach (var item in coll)
                {
                    RemoveEntity(item as IEntity);
                }
            }
            Context.Entry(entity).State = EntityState.Deleted;
        }

        private bool GameSystemExists(Guid id)
        {
            return Context.GameSystems.Any(e => e.Id == id);
        }

        protected abstract Task<TEntity> GetEntityAsync(Guid id);
    }
}
