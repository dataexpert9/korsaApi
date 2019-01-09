using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class SupportConversationDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConversationDate { get; set; }
        public int userType { get; set; }
    }
    public class SupportConversationDTOList
    {
        public List<SupportConversationDTO> Conversations { get; set; }
    }
    
}
