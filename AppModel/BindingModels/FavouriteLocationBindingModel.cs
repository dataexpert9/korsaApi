using AppModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.BindingModels
{
    public class FavouriteLocationBindingModel
    {
        public Locations Location { get; set; }
        public string PlaceId { get; set; }
        public string Address { get; set; }
        public string FormattedAddress { get; set; }
    }

    public class SupportConversationBindingModel
    {
        public int EntityId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConversationDate { get; set; }
        public int userType { get; set; }
    }
}
