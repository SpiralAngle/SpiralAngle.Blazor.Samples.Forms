using BlazorFormSample.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Server.Data
{
    public class CreatureDbContext : DbContext
    {
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<ItemInventory> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BlazorFormSample");
    }
}
