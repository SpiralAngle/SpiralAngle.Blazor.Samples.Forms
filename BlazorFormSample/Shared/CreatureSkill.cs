using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared
{
    public class CreatureSkill : IEntity
    {
        [Key]
        [Column("CreatureSkillId")]
        public Guid Id { get; set; }

        [Required]
        public Guid CreatureId { get; set; }

        [Required]
        public Skill Skill { get; set; }

        [Required]
        public Guid SkillId { get; set; }

        [Required]
        public Decimal Rank { get; set; }
    }
}
