using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class UserCode
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Promocode_Id { get; set; }
        public DateTime UsageDate { get; set; }
        public virtual Promocode Promocode { get; set; }
        public virtual User User { get; set; }

    }
}
