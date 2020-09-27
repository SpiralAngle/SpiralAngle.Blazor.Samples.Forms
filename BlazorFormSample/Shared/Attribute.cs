using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorFormSample.Shared
{
    public class Attribute : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }

        public Decimal ModifiedMinimum { get; set; }

        public Decimal Minimum { get; set; }

        public Decimal Maximum { get; set; }

        public Decimal ModifiedMaximum { get; set; }
    }
}
