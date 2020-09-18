using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BlazorFormSample.Shared
{
    public class Creature
    {
        [Key]
        public Guid CreatureId { get; set; }

        [Required]
        [RegularExpression("^(?! )[A-Za-z0-9 \']*(?<! )$")]
        public string Name{ get; set; }
        
        [Required]
        [RegularExpression("^(?! )[A-Za-z0-9 \']*(?<! )$")]
        public string Race { get; set; }

        [Required]
        [RegularExpression("^(?! )[A-Za-z0-9 \']*(?<! )$")]
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
