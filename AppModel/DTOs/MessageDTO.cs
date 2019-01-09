using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool isUserSender { get; set; }
    }
    public class MessageDTOList
    {
        public List<MessageDTO> Messages { get; set; }
    }

    public class NexmoCodeDTO
    {
        public string Code { get; set; }
    }
}
