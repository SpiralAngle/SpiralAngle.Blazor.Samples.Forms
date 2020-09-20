﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared
{
    public class Skill : IEntity
    {
        [Key]
        [Column("SkillId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        [Required]
        public SkillGroup SkillGroup { get; set; }

        [Required]
        public Guid SkillGroupId { get; set; }
    }
}
