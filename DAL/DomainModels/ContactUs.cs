using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public class ContactUs : BaseModel
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey("User")]
        public int? User_Id { get; set; }
        public User User { get; set; }
        [ForeignKey("Driver")]
        public int? Driver_Id { get; set; }
        public Driver Driver { get; set; }

    }
}
