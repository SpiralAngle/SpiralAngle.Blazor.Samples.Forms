using System;
using System.ComponentModel.DataAnnotations;


namespace BlazorFormSample.Shared
{
    public class RaceAttributeModifier : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Attribute Attribute { get; set; }

        [Required]
        public Guid AttributeId { get; set; }

        [Required]
        public decimal Modifier { get; set; }

        public Race Race { get; set; }

        [Required]
        public Guid RaceId { get; set; }
    }
}
