using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
   public class VehicleDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime RegistrationExpiry { get; set; }
        public string Company { get; set; }
        public string Classification { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Capacity { get; set; }
        public int Company_Id { get; set; } 
        public int Model_Id { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int Driver_Id { get; set; }
        public int RideType_Id { get; set; }


        public virtual ICollection<VehicleMediaDTO> Medias { get; set; }
    }
    public class VehicleMediaDTO
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
    }

    public class VehicleDTOList
    {
        public List<VehicleDTO> Vehicles { get; set; }
    }

    public class BackupViewModel
    {
        public string BackUpString { get; set; }
    }

    public class MailingDTO
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Type { get; set; }
    }


    }
