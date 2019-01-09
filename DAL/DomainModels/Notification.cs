using Newtonsoft.Json;

namespace DAL.DomainModels
{

    public partial class Notification :BaseModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
        
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int Status { get; set; }

        public int? User_Id { get; set; }

        public int? Entity_Id { get; set; }
        public int Type { get; set; }

        public int? Driver_Id { get; set; }

        public int? AdminNotification_Id { get; set; }


        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Driver Driver { get; set; }


        [JsonIgnore]

        public virtual AdminNotifications AdminNotification { get; set; }
    }
}
