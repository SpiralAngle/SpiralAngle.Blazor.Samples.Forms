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
        public DbSet<Attribute> Attributes{ get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceAttributeModifier> RaceAttributeModifiers { get; set; }
        public DbSet<RaceSkillModifier> RaceSkillModifiers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<GameSystem> GameSystems { get; set; }

        public CreatureDbContext(DbContextOptions<CreatureDbContext> options) : base(options)
        { }
        protected CreatureDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Creature>(entity =>
           {
               entity.HasOne(c => c.GameSystem)
                    .WithMany()
                    .HasForeignKey(c => c.GameSystemId)
                    .OnDelete(DeleteBehavior.NoAction);

               entity.HasOne(c => c.Race)
                    .WithMany()
                    .HasForeignKey(c => c.RaceId)
                    .OnDelete(DeleteBehavior.NoAction);

               entity.HasOne(c => c.Role)
                    .WithMany()
                    .HasForeignKey(c => c.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);
           });

            modelBuilder.Entity<Item>(b =>
           {
               b.HasOne(c => c.GameSystem)
                   .WithMany()
                   .HasForeignKey(c => c.GameSystemId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
           });

            modelBuilder.Entity<ItemInventory>(b =>
            {
                b.HasOne<Creature>()
                    .WithMany()
                    .HasForeignKey("CreatureId")
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne<Item>()
                    .WithMany()
                    .HasForeignKey(ii => ii.ItemId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasOne(c => c.SkillGroup)
                     .WithMany(c => c.Skills)
                     .HasForeignKey(c => c.SkillGroupId)
                     .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CreatureSkill>(entity =>
            {
                entity.HasOne(c => c.Skill)
                     .WithMany()
                     .HasForeignKey(c => c.SkillId)
                     .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<Creature>()
                     .WithMany()
                     .HasForeignKey(c => c.CreatureId)
                     .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Attribute>(b =>
            {
                b.HasOne<GameSystem>()
                    .WithMany()
                    .HasForeignKey("GameSystemId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<Race>(b =>
            {
                b.HasOne<GameSystem>()
                    .WithMany()
                    .HasForeignKey("GameSystemId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasOne<GameSystem>()
                    .WithMany()
                    .HasForeignKey("GameSystemId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity<RaceAttributeModifier>(entity =>
            {
                entity.HasOne<Race>()
                    .WithMany()
                    .HasForeignKey(ram => ram.RaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });


            modelBuilder.Entity<RaceSkillModifier>(entity =>
            {
                entity.HasOne<Race>()
                    .WithMany()
                    .HasForeignKey(rsm => rsm.RaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
