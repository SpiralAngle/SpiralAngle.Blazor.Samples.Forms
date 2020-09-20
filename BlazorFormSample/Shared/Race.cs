using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorFormSample.Shared
{
    public class Race : IEntity
    {
        [Key]
        [Column("RaceId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }
        
        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }
    }
}
