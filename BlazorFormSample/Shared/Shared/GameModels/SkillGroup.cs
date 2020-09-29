using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace BlazorFormSample.Shared.GameModels
{
    public class SkillGroup : IEntity
    {

        [Key]
        [Column("SkillGroupId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }

        public IList<Skill> Skills { get; set; }

        public SkillGroup()
        {
            Skills = new List<Skill>();
        }
    }
}
