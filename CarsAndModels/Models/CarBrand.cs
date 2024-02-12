using System.ComponentModel.DataAnnotations;

namespace CarsAndModels.Models
{
    public class CarBrand
    {
        [Key]
        public int BrandId { get; set; }

        [Required(ErrorMessage ="Brand name is required")]
        [MaxLength(100)]
        [Display(Name="Name")]
        public string BrandName { get; set; } = string.Empty;

        // Navigatie-eigenschap naar de modellen van het merk
        public ICollection<CarModel>? Models { get; set; }
    }
}
