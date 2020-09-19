using BlazorFormSample.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorFormSample.Server.Data
{
    public class CreatureDbContext : DbContext
    {
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<ItemInventory> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CreatureSkill> CreatureSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillGroup> SkillGroups { get; set; }
        public DbSet<GameSystem> GameSystems { get; set; }

        public CreatureDbContext(DbContextOptions<CreatureDbContext> options) : base(options)
        { }
    }
}
