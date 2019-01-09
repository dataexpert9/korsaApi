using System.Collections.Generic;

namespace AppModel.BindingModels
{
    public class PaymentConfirmationBindingModel
    {
        public string TransactionId { get; set; }
        public int? SubscriptionPackage_Id { get; set; }
        public double Amount { get; set; }
    }


    public class PaypalVerificationModel
    {
        public string Id { get; set; }
        public string Create_time { get; set; }
        public string Update_time { get; set; }
        public string State { get; set; }
        public string Intent { get; set; }
        public List<PaypalTransactionModel> Transactions { get; set; }
    }
    public class PaypalTransactionModel
    {
        public PaypalTransactionModel()
        {
            Amount = new PaypalAmountModel();
        }
        public PaypalAmountModel Amount { get; set; }
    }

    public class PaypalTransactionInputModel
    {
        public PaypalTransactionInputModel()
        {
            amount = new PaypalAmountInputModel();
        }
        public PaypalAmountInputModel amount { get; set; }
        public string description { get; set; } = "Payment by vaulted credit card.";
    }


    public class PaypalAmountModel
    {
        public double Total { get; set; }
        public string Currency { get; set; }
    }

    public class PaypalAmountInputModel
    {
        public string total { get; set; } = "6.70";
        public string currency { get; set; } = "USD";
    }


    public class PaypalAccessTokenModel
    {
        public string access_token { get; set; }
    }

    public class MakePaypalPaymentModel
    {
        public MakePaypalPaymentModel()
        {
            payer = new PaypalPayerModel();
            transactions = new List<PaypalTransactionInputModel>();
        }
        public PaypalPayerModel payer { get; set; }
        public string intent { get; set; } = "sale";
        public List<PaypalTransactionInputModel> transactions { get; set; }
    }


    public class MakePaymentResponseModel
    {
        public string id { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public string state { get; set; }
    }

    public class PaypalPayerModel
    {
        public PaypalPayerModel()
        {
            funding_instruments = new List<PaypalFundingModel>();
        }
        public string payment_method { get; set; } = "credit_card";

        public List<PaypalFundingModel> funding_instruments { get; set; }
    }

    public class PaypalFundingModel
    {
        public PaypalCreditCardModel credit_card_token { get; set; }
    }
    public class PaypalCreditCardModel
    {
        public string credit_card_id { get; set; }

        public string external_customer_id { get; set; }
    }

}
