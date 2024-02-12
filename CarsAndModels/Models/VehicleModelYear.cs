using System.ComponentModel.DataAnnotations;

namespace CarsAndModels.Models
{
    public class VehicleModelYear
    {
        public int id { get; set; }
        public int year { get; set; }
        [StringLength(50)]
        public string make { get; set; }
        [StringLength(50)]
        public string model { get; set; }
    }
}
