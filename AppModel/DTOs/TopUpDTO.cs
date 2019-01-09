using Component.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppModel.DTOs
{
   public class TopUpDTO : BaseModelDTO
    {
        public TopUpDTO()
        {
            Account = new AccountDTO();
        }
        public int Id { get; set; }
        public double Amount { get; set; }
        public string ImageUrl { get; set; }
        public List<TopUpMediaDTO> Receipts { get; set; }
        public int User_Id { get; set; }
        public virtual UserDTO User { get; set; }
        public TopUpStatus Status { get; set; }
        public PaymentMethods PaymentType { get; set; }

        public int Account_Id { get; set; }
        public virtual AccountDTO Account { get; set; }
    }

    public class TopUpListDTO
    {
        public List<TopUpDTO> BankTopUps { get; set; }
    }

    public class TopUpMediaDTO : BaseModelDTO
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
        public int? TopUp_Id { get; set; }
    }
}
