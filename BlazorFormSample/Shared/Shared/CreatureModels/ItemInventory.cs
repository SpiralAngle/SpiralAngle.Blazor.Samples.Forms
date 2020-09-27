using BlazorFormSample.Shared.GameModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.CreatureModels
{
    public class ItemInventory : IEntity
    {
        [Key]
        [Column("ItemInventoryId")]
        public Guid Id { get; set; }

        [Required]
        public Item Item { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Weight { get => (Item?.Weight ?? 0) * Quantity; }
    }
}
