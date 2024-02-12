using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarsAndModels.Models
{
    public class CarModel
    {
        [Key]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Model name is required")]
        [MaxLength(100)]
        [Display(Name="Name")]
        public string ModelName { get; set; } = string.Empty;

        [Display(Name="Image")]
        public string? ImageUrl { get; set; }

        // Foreign key naar het merk waartoe dit model behoort
        [ForeignKey("CarBrand")]
        public int BrandId { get; set; }

        // Navigatie-eigenschap naar het merk van dit model
        public CarBrand? CarBrand { get; set; } 
    }
}
