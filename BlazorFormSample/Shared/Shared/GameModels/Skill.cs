using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared.GameModels
{
    public class Skill : IEntity
    {
        [Key]
        [Column("SkillId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }
        public SkillGroup SkillGroup { get; set; }

        [Required]
        public Guid SkillGroupId { get; set; }
    }
}
