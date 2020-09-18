using System.ComponentModel.DataAnnotations;

namespace BlazorFormSample.Shared
{
    public class Item
    {
        [Required]
        [RegularExpression("^(?! )[A-Za-z0-9 \']*(?<! )$")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Weight { get; set; }
    }
}
