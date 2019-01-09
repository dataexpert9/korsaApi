using Component.Utility;
using System.Collections.Generic;

namespace DAL.DomainModels
{
    public class TopUp :BaseModel
    {
        public TopUp()
        {
            Receipts = new HashSet<TopUpMedia>();
        }
        public int Id { get; set; }
        public double Amount { get; set; }
        public string ImageUrl { get; set; }
        public TopUpStatus Status { get; set; }
        public PaymentMethods PaymentType { get; set; }
        public int User_Id { get; set; }
        public virtual User User { get; set; }

        public int? Account_Id { get; set; }
        public virtual Account Account { get; set; }

        public virtual ICollection<TopUpMedia> Receipts { get; set; }

    }
}
