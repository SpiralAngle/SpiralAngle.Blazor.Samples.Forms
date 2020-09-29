using BlazorFormSample.Server.Data;
using BlazorFormSample.Shared.GameModels;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace BlazorFormSample.Server.Tests.Utility
{
    [CollectionDefinition("ProviderTests")]
    public class DatabaseCollection : ICollectionFixture<CreatureDbContextFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public class CreatureDbContextFixture : IDisposable
    {
        public static Guid GameSystemId => new Guid("10000000-0000-0000-0000-000000000000");
        public static Guid SkillGroupId => new Guid("10000000-1000-0000-0000-000000000000");

        private static readonly object _lock = new object();
        private static bool _databaseInitialized;
        private bool disposedValue;

        public bool IsReady
        {
            get
            {
                return _databaseInitialized;
            }
        }

        public CreatureDbContextFixture()
        {
            Connection = new SqliteConnection("Filename=:memory:");
            // note: this order of opening the connection before Seed() is called is critical for in memory SQL Lite
            // as SQL Lite dbs go away when last connection is lost.
            Connection.Open();
            Seed();
        }

        public DbConnection Connection { get; }

        public CreatureDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new CreatureDbContext(new DbContextOptionsBuilder<CreatureDbContext>().UseSqlite(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        public void Seed()
        {
            if (!_databaseInitialized)
            {
                lock (_lock)
                {
                    if (!_databaseInitialized)
                    {
                        using (var context = CreateContext())
                        {
                            context.Database.EnsureDeleted();
                            context.Database.EnsureCreated();

                            GameSystem system1 = new GameSystem
                            {
                                Id = GameSystemId,
                                Name = "Name1",
                                Version = "Version1",
                                Roles = new List<Role> { new Role { Name = "ro1", GameSystemId = GameSystemId } },
                                Races = new List<Race> { new Race { Name = "ra1", GameSystemId = GameSystemId } },
                                Attributes = new List<BlazorFormSample.Shared.GameModels.Attribute> {
                                    new BlazorFormSample.Shared.GameModels.Attribute { Name ="at1", GameSystemId = GameSystemId } },
                                SkillGroups = new List<SkillGroup>
                                {
                                    new SkillGroup
                                    {
                                        Id = SkillGroupId,
                                        Name = "sg1",
                                        GameSystemId = GameSystemId,
                                        Skills = new List<Skill>
                                        {
                                            new Skill { Name= "sk1" }
                                        }
                                    }
                                }
                            };

                            context.Add(system1);
                            context.SaveChanges();
                        }
                        _databaseInitialized = true;
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    using (Connection) { }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

    }
}
