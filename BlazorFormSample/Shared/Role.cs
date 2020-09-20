using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFormSample.Shared
{
    public class Role : IEntity
    {
        [Key]
        [Column("RoleId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }
    }
}
