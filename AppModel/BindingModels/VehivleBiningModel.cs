using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class VehicleBindingModel
    {
        public int Id { get; set; }
        public string ImagesToRemoveIds { get; set; }
        public int Company_Id { get; set; }
        public int Model_Id { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int? RideType_Id { get; set; }

        public DateTime RegistrationExpiry { get; set; }
        public string Number { get; set; }

        public List<IFormFile> RegistrationCopyImages { get; set; }
        public List<IFormFile> CarPhotos { get; set; }
    }
}
