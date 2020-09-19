using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorFormSample.Shared
{
    public class CreatureSkill
    {
        [Key]
        public Guid CreatureSkillId { get; set; }

        [Required]
        public Skill Skill { get; set; }

        [Required]
        public Decimal Rank { get; set; }
    }
}
