using BlazorFormSample.Server.Data;
using BlazorFormSample.Server.Shared;
using BlazorFormSample.Shared.GameModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using System;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.GameSystemApi
{
    public class GameSystemProvider : EntityProviderBase<GameSystem>
    {
        public GameSystemProvider(CreatureDbContext context): base(context)
        {
        }

        protected override async Task<GameSystem> GetEntityAsync(Guid id)
        {
            return await Context.GameSystems.AsNoTracking()
                .Include(x => x.Roles).AsNoTracking()
                .Include(x => x.SkillGroups).ThenInclude(x => x.Skills)
                .Include(x => x.Races).AsNoTracking()
                .Include(x => x.Attributes).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
