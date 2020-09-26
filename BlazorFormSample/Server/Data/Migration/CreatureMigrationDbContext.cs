using BlazorFormSample.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorFormSample.Data.Migrations
{
    public class CreatureMigrationDbContext : CreatureDbContext
    {
        public CreatureMigrationDbContext(DbContextOptions<CreatureMigrationDbContext> options) : base(options)
        {

        }
    }
}
