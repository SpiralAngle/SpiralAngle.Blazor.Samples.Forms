using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.GameModels
{
    public class RaceSkillModifier : IEntity
    {
        [Key]
        [Column("RaceSkillModifierId")]
        public Guid Id { get; set; }

        public Skill Skill { get; set; }

        [Required]
        public Guid SkillId { get; set; }

        [Required]
        public decimal Modifier { get; set; }

        [Required]
        public Guid RaceId { get; set; }
    }
}
