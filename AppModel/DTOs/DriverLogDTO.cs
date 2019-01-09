using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class DriverLogDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public LogType Type { get; set; }
        public DateTime OnlineTime { get; set; }
        public DateTime OfflineTime { get; set; }

        public DriverDTO Driver { get; set; }
        public int TotalRides { get; set; }
    }

    public class DriverLogDTOList
    {
        public DriverLogDTOList()
        {
            Logs = new List<DriverLogDTO>();
        }
        public List<DriverLogDTO> Logs { get; set; }
    }
}
