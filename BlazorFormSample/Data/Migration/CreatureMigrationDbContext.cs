using BlazorFormSample.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorFormSample.Data.Migrations
{
    public class CreatureMigrationDbContext : CreatureDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BlazorFormSample");
    }
}
