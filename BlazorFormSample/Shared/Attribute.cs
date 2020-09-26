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
    }
}
