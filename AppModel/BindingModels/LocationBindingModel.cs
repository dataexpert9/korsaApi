using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public class LocationBindingModel
    {
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
