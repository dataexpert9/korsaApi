using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
   public class UserDriverMessaging : BaseModel
    {
        public int Id { get; set; }

        [ForeignKey("Message")]
        public int Message_Id { get; set; }
        public Message Message { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }
        [ForeignKey("Driver")]
        public int Driver_Id { get; set; }
        public Driver Driver { get; set; }
    }
}
