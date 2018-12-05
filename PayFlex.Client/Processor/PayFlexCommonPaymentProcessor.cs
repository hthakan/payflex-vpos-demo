using System;
using System.IO;
using System.Net;
using System.Text;

namespace PayFlex.Client.Processor
{
    public class PayFlexCommonPaymentProcessor : IPaymentProcessor
    {
        public PaymentResponse Pay(Payment payment)
        {
            #region Pos Configuration            
            string strMerchantPassword = "0";
            string strHostMerchantID = "0";
            string strMerchantType = "0";
            string strHostSubMerchantId = "";
            string strHostAddress = payment.ServiceUrl;

            #endregion

            var cardInfo = payment.CreditCard;
            var selectedCurrency = (int)Enum.Parse(typeof(Currency), payment.CurrencyId);
            var selectedTransactionType = Enum.Parse(typeof(PaymentTransactionType), payment.PaymentTransactionType);

            var postData = new StringBuilder();
            postData.AppendFormat("{0}={1}&", "HostMerchantId", strHostMerchantID);
            postData.AppendFormat("{0}={1}&", "AmountCode", selectedCurrency);
            postData.AppendFormat("{0}={1}&", "Amount", payment.PaymentTotal);
            postData.AppendFormat("{0}={1}&", "MerchantPassword", strMerchantPassword);
            postData.AppendFormat("{0}={1}&", "TransactionId", payment.TransactionId);
            postData.AppendFormat("{0}={1}&", "OrderID", payment.OrderId);
            postData.AppendFormat("{0}={1}&", "OrderDescription", payment.OrderDescription);
            postData.AppendFormat("{0}={1}&", "InstallmentCount", payment.InstallmentCount);
            postData.AppendFormat("{0}={1}&", "TransactionType", selectedTransactionType);
            postData.AppendFormat("{0}={1}&", "IsSecure", payment.IsSecure);
            postData.AppendFormat("{0}={1}&", "AllowNotEnrolledCard", payment.AllowNotEnrolledCard);
            postData.AppendFormat("{0}={1}&", "SuccessUrl", payment.SuccessUrl);
            postData.AppendFormat("{0}={1}&", "FailUrl", payment.FailUrl);
            postData.AppendFormat("{0}={1}&", "BrandNumber", payment.CreditCard.BrandNumber);
            postData.AppendFormat("{0}={1}&", "CVV", payment.CreditCard.CVV);
            postData.AppendFormat("{0}={1}&", "PAN", payment.CreditCard.Number);
            postData.AppendFormat("{0}={1}&", "ExpireMonth", payment.CreditCard.ExpireMonth);
            postData.AppendFormat("{0}={1}&", "ExpireYear", payment.CreditCard.ExpireYear);
            postData.AppendFormat("{0}={1}&", "RequestLanguage", payment.RequestLanguage);
            postData.AppendFormat("{0}={1}&", "MerchantType", strMerchantType);
            postData.AppendFormat("{0}={1}", "HostSubMerchantId", strHostSubMerchantId);


            byte[] postByteArray = Encoding.UTF8.GetBytes(postData.ToString());

            WebRequest webRequest = WebRequest.Create(strHostAddress);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postByteArray.Length;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Tls;

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(postByteArray, 0, postByteArray.Length);
            dataStream.Close();

            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var bankResponse = new PaymentResponse();
            bankResponse.Response = reader.ReadToEnd();
            bankResponse.IsSuccessful = true;
            reader.Close();
            dataStream.Close();
            webResponse.Close();

            return bankResponse;
        }
    }
}