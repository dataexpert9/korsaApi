using Component.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModel.BindingModels
{
    public class PaymentMethodBindingModel
    {
        public PaymentMethods PaymentMethodType  { get; set; }
    }
    public class AddCreditCardBindingModel
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]

        public DateTime ExpiryDate { get; set; }
        [Required]

        public string CVV { get; set; }
        [Required]

        public string Name { get; set; }
        public string CardTypeName { get; set; }
        public UserTypes UserType { get; set; }

    }
}
