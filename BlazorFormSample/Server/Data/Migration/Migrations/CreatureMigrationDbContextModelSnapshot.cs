﻿// <auto-generated />
using System;
using BlazorFormSample.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    [DbContext(typeof(CreatureMigrationDbContext))]
    partial class CreatureMigrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlazorFormSample.Shared.Attribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GameSystemId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.HasIndex("GameSystemId1");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Creature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Charisma")
                        .HasColumnType("int");

                    b.Property<int>("Constitution")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dexterity")
                        .HasColumnType("int");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Wisdom")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.HasIndex("RaceId");

                    b.HasIndex("RoleId");

                    b.ToTable("Creatures");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.CreatureSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatureSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Rank")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.HasIndex("SkillId");

                    b.ToTable("CreatureSkills");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.GameSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameSystems");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.ItemInventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ItemInventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatureId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.HasIndex("CreatureId1");

                    b.HasIndex("ItemId");

                    b.HasIndex("ItemId1");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Race", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GameSystemId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.HasIndex("GameSystemId1");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.RaceAttributeModifier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Modifier")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RaceId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("RaceId");

                    b.HasIndex("RaceId1");

                    b.ToTable("RaceAttributeModifiers");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.RaceSkillModifier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Modifier")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RaceId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.HasIndex("RaceId1");

                    b.HasIndex("SkillId");

                    b.ToTable("RaceSkillModifiers");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GameSystemId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.HasIndex("GameSystemId1");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SkillGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SkillGroupId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.SkillGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SkillGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameSystemId");

                    b.ToTable("SkillGroups");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Attribute", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", null)
                        .WithMany()
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany("Attributes")
                        .HasForeignKey("GameSystemId1");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Creature", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany()
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.CreatureSkill", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.Creature", null)
                        .WithMany()
                        .HasForeignKey("CreatureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Item", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany()
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.ItemInventory", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.Creature", null)
                        .WithMany()
                        .HasForeignKey("CreatureId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BlazorFormSample.Shared.Creature", null)
                        .WithMany("InventoryItems")
                        .HasForeignKey("CreatureId1");

                    b.HasOne("BlazorFormSample.Shared.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Race", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", null)
                        .WithMany()
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany("Races")
                        .HasForeignKey("GameSystemId1");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.RaceAttributeModifier", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.Attribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Race", null)
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Race", null)
                        .WithMany("RaceAttributeModifiers")
                        .HasForeignKey("RaceId1");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.RaceSkillModifier", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.Race", null)
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.Race", null)
                        .WithMany("RaceSkillModifiers")
                        .HasForeignKey("RaceId1");

                    b.HasOne("BlazorFormSample.Shared.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Role", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", null)
                        .WithMany()
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany("Roles")
                        .HasForeignKey("GameSystemId1");
                });

            modelBuilder.Entity("BlazorFormSample.Shared.Skill", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.SkillGroup", "SkillGroup")
                        .WithMany("Skills")
                        .HasForeignKey("SkillGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorFormSample.Shared.SkillGroup", b =>
                {
                    b.HasOne("BlazorFormSample.Shared.GameSystem", "GameSystem")
                        .WithMany("SkillGroups")
                        .HasForeignKey("GameSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
