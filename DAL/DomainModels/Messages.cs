using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class Message :BaseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [ForeignKey("Driver")]
        public int? Driver_Id { get; set; }
        public Driver Driver { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }
        public bool isUserSender { get; set; }
    }
    public class MessageML
    {
        public int Id { get; set; }
    }
    
}
