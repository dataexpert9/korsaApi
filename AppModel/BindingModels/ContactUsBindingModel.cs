using Component.Utility;
using System.ComponentModel.DataAnnotations;

namespace LIMO.BindingModels
{
    public class ContactUsBindingModel
    {
        [Required]
        [StringLength(2000)]
        public string Message { get; set; }
        public UserTypes UserType { get; set; }
    }
}
