using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.CustomModels
{
    public class NotificationModel
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int NotificationId { get; set; }

        public int Type { get; set; }

        public int EntityId { get; set; }

        public int? DeliveryMan_Id { get; set; }
    }
}
