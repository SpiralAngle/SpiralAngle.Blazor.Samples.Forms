using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.GameModels
{
    public class Item : IEntity
    {
        [Key]
        [Column("ItemId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }
    }
}
