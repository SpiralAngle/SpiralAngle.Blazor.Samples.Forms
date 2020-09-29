using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorFormSample.Shared.GameModels
{
    public class Attribute : IEntity
    {
        [Key]
        [Column("AttributeId")]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^(?! )[\\S\\s]*(?<! )$")]
        public string Name { get; set; }

        public GameSystem GameSystem { get; set; }

        [Required]
        public Guid GameSystemId { get; set; }

        [CustomValidation(typeof(Attribute), "ValidateModifiedMinimum")]
        public decimal ModifiedMinimum { get; set; }

        [CustomValidation(typeof(Attribute), "ValidateMinimum")]
        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        [CustomValidation(typeof(Attribute), "ValidateModifiedMaximum")]
        public decimal ModifiedMaximum { get; set; }

        [Required]
        public decimal Order { get; set; }

        public static ValidationResult ValidateModifiedMinimum(decimal value, ValidationContext validationContext)
        {
            var attribute = validationContext.ObjectInstance as Attribute;
            if (value > attribute.Minimum)
            {
                return new ValidationResult("Modified Minimum must be less than or equal to Minimum", new[] { nameof(ModifiedMinimum), nameof(Minimum) });
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateModifiedMaximum(decimal value, ValidationContext validationContext)
        {
            var attribute = validationContext.ObjectInstance as Attribute;
            if (value < attribute.Maximum)
            {
                return new ValidationResult("Modified Maximum must be greater than or equal to Maximum", new[] { nameof(ModifiedMaximum), nameof(Maximum) });
            }
            return ValidationResult.Success;
        }


        public static ValidationResult ValidateMinimum(decimal value, ValidationContext validationContext)
        {
            var attribute = validationContext.ObjectInstance as Attribute;
            if (value > attribute.Maximum)
            {
                return new ValidationResult("Minimum must be less than or equal to Maximum", new[] { nameof(Minimum), nameof(Maximum) });
            }
            return ValidationResult.Success;
        }
    }
}
