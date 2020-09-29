using BlazorFormSample.Shared.GameModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.CreatureModels
{
    public class Creature : IEntity
    {
        [Key]
        [Column("CreatureId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        public GameSystem GameSystem { get; set; }

        [Required]

        public Guid GameSystemId { get; set; }

        public Race Race { get; set; }

        [Required]
        public Guid RaceId { get; set; }

        public Role Role { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public IList<CreatureAttribute> Attributes { get; set; }

        public IList<CreatureSkill> Skills { get; set; }

        public string Description { get; set; }

        public IList<ItemInventory> InventoryItems { get; set; }

        public Creature()
        {
            InventoryItems = new List<ItemInventory>();
            Attributes = new List<CreatureAttribute>();
            Skills = new List<CreatureSkill>();
        }
    }
}
