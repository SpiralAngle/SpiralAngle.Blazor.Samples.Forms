using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared
{
    public class SkillGroup
    {

        [Key]
        [Column("SkillGroupId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[A-Za-z0-9 \']*(?<! )$")]
        public string Name { get; set; }
    }
}
