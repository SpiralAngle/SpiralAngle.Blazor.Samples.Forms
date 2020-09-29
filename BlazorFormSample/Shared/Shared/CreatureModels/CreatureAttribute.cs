using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorFormSample.Shared.CreatureModels
{
    public class CreatureAttribute : IEntity
    {
        [Key]
        [Column("CreatureAttribute")]
        public Guid Id { get; set; }

        [Required]
        public Guid CreatureId { get; set; }

        public Creature Creature { get; set; }

        [Required]
        public Guid AttributeId { get; set; }

        public GameModels.Attribute Attribute { get; set; }

        [Required]
        public Decimal Value { get; set; }
    }
}
