using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DomainModels
{
    public class InvitedFriend : BaseModel
    {
        public int Id { get; set; }

        public int InvitedUser_Id { get; set; }
        public virtual User InvitedUser { get; set; }

        public int Referrer_Id { get; set; }
        public virtual User Referrer { get; set; }

    }
}
