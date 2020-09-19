using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared
{
    public class Creature
    {
        [Key]
        [Column("CreatureId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        [Required]
        public GameSystem GameSystem { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Race { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Class { get; set; }

        [Required]
        [Range(0, 100)]
        public int Strength { get; set; }

        [Required]
        [Range(0, 100)]
        public int Dexterity { get; set; }

        [Required]
        [Range(0, 100)]
        public int Constitution { get; set; }

        [Required]
        [Range(0, 100)]
        public int Intelligence { get; set; }

        [Required]
        [Range(0, 100)]
        public int Wisdom { get; set; }

        [Required]
        [Range(0, 100)]
        public int Charisma { get; set; }

        public string Description { get; set; }

        public IList<ItemInventory> InventoryItems { get; set; }

        public Creature()
        {
            InventoryItems = new List<ItemInventory>();
        }
    }
}
