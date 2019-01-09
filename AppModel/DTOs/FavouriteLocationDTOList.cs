using AppModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{

    public class FavouriteLocationDTO
    {
        public int Id { get; set; }
        public Locations Location { get; set; }
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }

        public string Address { get; set; }

        public int User_Id { get; set; }
    }


    public class FavouriteLocationDTOList
    {
        public List<FavouriteLocationDTO> FavouriteLocationsList { get; set; }

    }
}
