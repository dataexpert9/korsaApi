using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class MessageBindingModel
    {
        [Required]
        public string Text { get; set; }
        public int Driver_Id { get; set; }
        public int User_Id { get; set; }
        [Required]
        public bool isUserSender { get; set; }
    }

    public class GetMessagesBindingModel
    {
        public int Id { get; set; }
        public bool isUser { get; set; }
    }
}

