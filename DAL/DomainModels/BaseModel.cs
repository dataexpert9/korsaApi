using System;

namespace DAL.DomainModels
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
