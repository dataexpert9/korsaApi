using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
    public class BaseModelDTO
    {
        public BaseModelDTO()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
