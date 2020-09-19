using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace BlazorFormSample.Shared
{
    public class GameSystem
    {
        [Key]
        [Column("GameSystemId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

    }
}
