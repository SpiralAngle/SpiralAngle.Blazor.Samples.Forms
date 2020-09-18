using System.ComponentModel.DataAnnotations;

namespace BlazorFormSample.Shared
{
    public class InventoryItem
    {
        [Required]
        public Item Item { get; set; }

        [Required]
        [Range(0, double.MaxValue)]        
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Weight { get => (Item?.Weight ?? 0) * Quantity; }
    }
}
