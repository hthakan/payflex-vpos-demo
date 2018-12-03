
using System;
using System.Configuration;

namespace WebDemo
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

    public class PaymentUyi
    {
        public string ClientMerchantCode { get; set; }
        public string Password { get; set; }
        public string CardHoldersClientIp { get; set; }
        public string CustomerId { get; set; }
        public byte Apply3DS { get; set; }
        public byte SupportHalfSecure { get; set; }
        public byte IsSaveCard { get; set; }
        public byte IsHideSaveCard { get; set; }
        public byte IsSmsUsage { get; set; }
        public bool HideAmount { get; set; }
        public string Token { get; set; }

    }

    #region Enums

    public enum PaymentType
    {
        VPos
    }

    public enum PaymentStatus
    {
        Success,
        UnSuccess
    }

    public enum PaymentTransactionType
    {
        Sale,
        Auth,
        Vft,
        Point,
        Cancel,
        Refund,
        Capture,
        Reversal,
        CampaignSearch,
        CardTest,
        BatchClosedSuccessSearch,
        SurchargeSearch,
        VFTSale,
        VFTSearch,
        PointSearch,
        PointSale,
        Credit
    }
    #endregion

    public class CreditCard
    {
        public string Number { get; set; }
        public string CardHolderName { get; set; }
        public string CVV { get; set; }
        public string Expiry
        {
            get { return $"{ExpireYear}{ExpireMonth}"; }
        }
        public string BrandNumber { get; set; }
        public string BrandName { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CardHoldersEmail { get; set; }
        public string CardHolderIp { get; set; }
        public string PhoneNumber { get; set; }
    }
}