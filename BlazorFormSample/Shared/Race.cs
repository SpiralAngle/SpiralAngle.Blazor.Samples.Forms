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

        [Required]
        public IList<RaceAttributeModifier> RaceAttributeModifiers { get; set; }

        [Required]
        public IList<RaceSkillModifier> RaceSkillModifiers { get; set; }

        public Race()
        {
            RaceAttributeModifiers = new List<RaceAttributeModifier>();
            RaceSkillModifiers = new List<RaceSkillModifier>();
        }

    }
}
