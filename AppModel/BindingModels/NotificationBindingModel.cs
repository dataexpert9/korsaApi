using Component.Utility;
using System.ComponentModel.DataAnnotations;

namespace AppModel.BindingModels
{
    public class NotificationBindingModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int? User_Id { get; set; }
        public int? Entity_Id { get; set; }
        public int Type { get; set; }
        public int? Driver_Id { get; set; }

    }

    public class AdminNotificationBindingModel
    {
        [Required(ErrorMessage = "Title is Required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is Required.")]
        public string Description { get; set; }
        public TargetAudienceType TargetAudienceType { get; set; }
        public string Text { get; set; }
        public int CityId { get; set; }
    }
}