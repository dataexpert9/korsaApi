using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class AdminDTO
    {
        public AdminDTO()
        {
            AdminTokens = new List<AdminTokensDTO>();
            Token = new TokenDTO();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public short Role { get; set; }

        public string AccountNo { get; set; }

        public int? Store_Id { get; set; }

        public short? Status { get; set; }

        public bool IsDeleted { get; set; }

        public TokenDTO Token { get; set; }

        public bool ImageDeletedOnEdit { get; set; }

        public string ImageUrl { get; set; }

        public virtual List<AdminTokensDTO> AdminTokens { get; set; }
    }
    public class AdminTokensDTO
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public int Admin_Id { get; set; }

        public bool IsActive { get; set; }
    }
    public class TokenDTO
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class DashboardStatsDTO
    {

        public int TotalUsers { get; set; }

        public int TotalDrivers { get; set; }

        public int TotalRides { get; set; }
        public int TotalVehicles { get; set; }


        public List<DeviceStatsDTO> DeviceUsage { get; set; }


    }
    public class DeviceStatsDTO
    {
        public int Count { get; set; }
        public int Percentage { get; set; }
    }

}
