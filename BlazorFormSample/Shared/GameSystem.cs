using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace BlazorFormSample.Shared
{
    public class GameSystem : IEntity
    {
        [Key]
        [Column("GameSystemId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Version { get; set; }

        [Required]
        public IList<Race> Races { get; set; }

        [Required]
        public IList<Role> Roles { get; set; }

        [Required]
        public IList<SkillGroup> SkillGroups { get; set; }

        public string Description { get; set; }
        public string Publisher { get; set; }

        public GameSystem()
        {
            Races = new List<Race>();
            Roles = new List<Role>();
            SkillGroups = new List<SkillGroup>();
        }
    }
}
