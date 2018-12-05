
using System;
using System.Configuration;

namespace PayFlex.Client
{
    public class Payment
    {
        public Guid TransactionId { get; set; }
        public Guid? OrderId { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal PaymentTotal { get; set; }
        public string CurrencyId { get; set; }
        public string OrderDescription { get; set; }
        public string InstallmentCount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentTransactionType { get; set; }
        public string ActivePayment
        {
            get
            {
                var supplier = ConfigurationManager.AppSettings["VposPaymentSupplier"];

                if (string.IsNullOrWhiteSpace(supplier))
                {
                    throw new ArgumentNullException("VposPaymentSupplier does not containg a definition for Config");
                }

                return supplier;
            }
        }
        public string IsSecure { get; set; }
        public string AllowNotEnrolledCard { get; set; }
        public string ServiceUrl { get; set; }
        public string CPayRegisterUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string RequestLanguage { get; set; }
        public PaymentUyi PaymentUyi { get; set; }       
    }
}