using System;

namespace DAL.DomainModels
{
    public class UserTrip :BaseModel
    {
        public int Id { get; set; }
        public short Rating { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
        public int Trip_Id { get; set; }
        public Trip Trip { get; set; }
        public DateTime RequestTime { get; set; }
        public bool isScheduled { get; set; }
    }
}
