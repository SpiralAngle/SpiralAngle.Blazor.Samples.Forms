using BlazorFormSample.Server.CreatureApi;
using BlazorFormSample.Server.GameSystemApi;
using BlazorFormSample.Server.Tests.GameSystemApi;
using BlazorFormSample.Server.Tests.Utility;
using BlazorFormSample.Shared.CreatureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorFormSample.Server.Tests.CreatureApi
{
    [Collection("ProviderTests")]
    public class CreatureProviderTests
    {
        public CreatureDbContextFixture Fixture { get; }

        public CreatureProviderTests(CreatureDbContextFixture fixture) => Fixture = fixture;
        // public CreatureProviderTests() => Fixture = new CreatureDbContextFixture();

        [Fact]
        public async Task Add_Adds()
        {
            Guid CreatureId = new Guid("10000000-2000-0000-0000-000000000000");
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    CreatureProvider provider = new CreatureProvider(context);
                    GameSystemProvider gameProvder = new GameSystemProvider(context);
                    var gameSystem = await gameProvder.GetAsync(CreatureDbContextFixture.GameSystemId);
                    Creature creature = new Creature
                    {
                        GameSystemId = CreatureDbContextFixture.GameSystemId,
                        Name = "Bob",
                        RaceId = gameSystem.Races.First().Id,
                        RoleId = gameSystem.Roles.First().Id
                    };

                    var newId = await provider.AddAsync(creature);

                    var retrievedCreature = await provider.GetAsync(newId);

                    Assert.Equal(creature.Name, retrievedCreature.Name);
                }
            }
        }
        [Fact]
        public async Task GetList_Gets_List_With_GameSystem_Race_And_Role()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    Creature creature1 = new Creature
                    {
                        Id = CreatureDbContextFixture.CreatureId,
                        Name = "Name2",
                        RaceId = CreatureDbContextFixture.RaceId,
                        RoleId = CreatureDbContextFixture.Roleid,
                        GameSystemId = CreatureDbContextFixture.GameSystemId
                    };
                    context.Add(creature1);

                    context.SaveChanges();

                    var provider = new CreatureProvider(context);

                    var creatureList = await provider.GetListAsync();

                    Assert.Equal(CreatureDbContextFixture.GameSystemId, creatureList.First().GameSystem.Id);
                    Assert.Equal(CreatureDbContextFixture.RaceId, creatureList.First().Race.Id);
                    Assert.Equal(CreatureDbContextFixture.Roleid, creatureList.First().Role.Id);
                }
                await transaction.RollbackAsync();
            }
        }
    }
}
