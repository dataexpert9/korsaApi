using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
   public class NotificationDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Text_Ar { get; set; }

        public string Title { get; set; }

        public string Title_Ar { get; set; }

        public string ImageUrl { get; set; }

        public int Status { get; set; }

        public int? User_ID { get; set; }

        public int? Entity_ID { get; set; }


        public int? DeliveryMan_ID { get; set; }

        public int? AdminNotification_Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class NotificationDTOList
    {
        public List<NotificationDTO> NotificationsList { get; set; }
    }
}
