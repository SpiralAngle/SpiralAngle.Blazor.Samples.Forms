using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.GameModels
{
    public class RaceAttributeModifier : IEntity
    {
        [Key]
        [Column("RaceAttributeModifierId")]
        public Guid Id { get; set; }

        public Attribute Attribute { get; set; }

        [Required]
        public Guid AttributeId { get; set; }

        [Required]
        public decimal Modifier { get; set; }

        [Required]
        public Guid RaceId { get; set; }
    }
}
