using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
  public class FavouriteLocation : BaseModel
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }
        public string Address { get; set; }

        public int User_Id { get; set; }
        public User User { get; set; }
    }
}
