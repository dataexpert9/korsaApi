using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class SupportConversation : BaseModel
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConversationDate { get; set; }
        public int userType { get; set; }
    }
}
