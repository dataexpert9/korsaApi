using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DomainModels
{
    public class DriverLog : BaseModel
    {
        public int Id { get; set; }
        public int Driver_Id { get; set; }
        public DateTime OnlineTime { get; set; }
        public DateTime OfflineTime { get; set; }
        public LogType Type { get; set; }
        public Driver Driver { get; set; }
    }
}
