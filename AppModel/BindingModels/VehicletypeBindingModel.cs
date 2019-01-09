using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using static Utility.ImageHelper;

namespace AppModel.BindingModels
{
   public  class RideTypeBindingModel
    {
        public RideTypeBindingModel()
        {
            Culture = CultureType.English;
        }
        public int Id { get; set; }
        public int PersonsCapacity { get; set; }
        public int CapacityInKGs { get; set; }
        public float FarePerKM { get; set; }

        public ImageModel DefaultImage { get; set; }
        public ImageModel SelectedImage { get; set; }
        public double FarePerMin { get; set; }

        public float BasicCharges { get; set; }
        public string Name { get; set; }
        public string AboutRideType { get; set; }
        public CultureType Culture { get; set; }
    }
}
