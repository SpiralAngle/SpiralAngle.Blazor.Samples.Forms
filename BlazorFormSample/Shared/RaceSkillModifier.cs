using System;
using System.ComponentModel.DataAnnotations;


namespace BlazorFormSample.Shared
{
    public class RaceSkillModifier : IEntity
    {
        [Key]
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
