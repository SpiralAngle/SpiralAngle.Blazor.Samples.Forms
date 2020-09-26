using BlazorFormSample.Server.Data;
using BlazorFormSample.Server.Shared;
using BlazorFormSample.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.GameSystemApi
{
    public class GameSystemProvider : IEntityProvider<GameSystem>
    {
        private readonly CreatureDbContext _context;

        public GameSystemProvider(CreatureDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(GameSystem entity)
        {
            SetIdIfNeeded(entity);
            SetChildIds(entity);
            _context.GameSystems.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameSystem = await GetGameSystemAsync(id);
            if (gameSystem == null)
            {
                return false;
            }

            foreach (var role in gameSystem.Roles)
            {
                _context.Roles.Remove(role);
            }
            foreach (var race in gameSystem.Races)
            {
                _context.Races.Remove(race);
            }
            foreach (var skillGroup in gameSystem.SkillGroups)
            {
                RemoveEntity(skillGroup);
            }

            _context.GameSystems.Remove(gameSystem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<GameSystem> GetAsync(Guid id)
        {
            return await GetGameSystemAsync(id);
        }

        public async Task<IEnumerable<GameSystem>> GetListAsync()
        {
            return await _context.GameSystems.ToListAsync();
        }

        public async Task<bool> UpdateAsync(GameSystem entity)
        {

            SetChildIds(entity);
            var existing = await GetGameSystemAsync(entity.Id);
            foreach (var role in existing.Roles)
            {
                if (entity.Roles.All(x => x.Id != role.Id))
                {
                    RemoveEntity(role);                    
                }
            }
            foreach (var race in existing.Races)
            {
                if (entity.Races.All(x => x.Id != race.Id))
                {
                    RemoveEntity(race);                    
                }
            }

            foreach (var skillGroup in existing.SkillGroups)
            {
                var incomingGroup = entity.SkillGroups.FirstOrDefault(x => x.Id == skillGroup.Id);
                if (incomingGroup == null)
                {
                    RemoveEntity(skillGroup);
                }
                else
                {
                    foreach (var skill in skillGroup.Skills)
                    {
                        if (incomingGroup.Skills.All(x => x.Id != skill.Id))
                        {
                            _context.Entry(skill).State = EntityState.Deleted;
                        }
                    }
                }
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        private void SetChildIds(GameSystem gameSystem)
        {
            foreach (var race in gameSystem.Races)
            {
                SetIdIfNeeded(race);
                if (race.GameSystemId == default)
                {
                    race.GameSystemId = gameSystem.Id;
                    _context.Races.Add(race);
                }
                else
                {
                    _context.Entry(race).State = EntityState.Modified;
                }
            }

            foreach (var role in gameSystem.Roles)
            {
                SetIdIfNeeded(role);
                if (role.GameSystemId == default)
                {
                    role.GameSystemId = gameSystem.Id;
                    _context.Roles.Add(role);
                }
                else
                {

                    _context.Entry(role).State = EntityState.Modified;
                }
            }

            foreach (var skillGroup in gameSystem.SkillGroups)
            {
                SetIdIfNeeded(skillGroup);

                foreach (var skill in skillGroup.Skills)
                {
                    SetIdIfNeeded(skill);
                    if (skill.SkillGroupId == default)
                    {
                        skill.SkillGroupId = skillGroup.Id;
                        _context.Skills.Add(skill);
                    }
                    else
                    {
                        _context.Entry(skill).State = EntityState.Modified;
                    }
                }
                if (skillGroup.GameSystemId == default)
                {
                    skillGroup.GameSystemId = gameSystem.Id;
                    _context.SkillGroups.Add(skillGroup);
                }
                else
                {
                    _context.Entry(skillGroup).State = EntityState.Modified;
                }
            }
        }

        private static void SetIdIfNeeded(IEntity entity)
        {
            entity.Id = entity.Id == default ? Guid.NewGuid() : entity.Id;
        }

        private void RemoveEntity<T>(T entity) where T : IEntity
        {         
            var listProperties = entity.GetType().GetProperties().Where(p => p.PropertyType != typeof(string) && p.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)));
            foreach (var collection in listProperties)
            {
                var coll = collection.GetValue(entity) as IEnumerable;
                
                    foreach (var item in coll)
                    {
                        RemoveEntity(item as IEntity);
                    }                
            }
            _context.Entry(entity).State = EntityState.Deleted;
        }

        private bool GameSystemExists(Guid id)
        {
            return _context.GameSystems.Any(e => e.Id == id);
        }

        private async Task<GameSystem> GetGameSystemAsync(Guid id)
        {
            return await _context.GameSystems.AsNoTracking()
                .Include(x => x.Roles).AsNoTracking()
                .Include(x => x.SkillGroups).ThenInclude(x => x.Skills)
                .Include(x => x.Races).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
