using BlazorFormSample.Server.Data;
using BlazorFormSample.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.Tests.Utility
{
    public class CreatureDbContextFixture : IDisposable
    {
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
                            var gameSystemId = new Guid("10000000-0000-0000-0000-000000000000");
                            var skillGroupId = new Guid("10000000-1000-0000-0000-000000000000");
                            GameSystem system1 = new GameSystem
                            {
                                Id =  gameSystemId,
                                Name = "Name",
                                Version = "Version",
                                Roles = new List<Role> { new Role { Name = "ro1", GameSystemId = gameSystemId} },
                                Races = new List<Race> { new Race { Name = "ra1", GameSystemId = gameSystemId } },
                                SkillGroups = new List<SkillGroup>
                                {
                                    new SkillGroup
                                    {
                                        Id = skillGroupId,
                                        Name = "sg1",
                                        GameSystemId = gameSystemId,
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
