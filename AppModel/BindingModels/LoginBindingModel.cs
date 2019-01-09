using Component.Utility;
using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public class LoginBindingModel
    {
        [Required]    
        public string CellNumOrUsername { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ExternalLoginBindingModel
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public SocialLoginType SocialLoginType { get; set; }
    }
}
