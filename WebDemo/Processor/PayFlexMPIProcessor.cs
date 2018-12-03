using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDemo.Processor
{
    public class PayFlexMPIProcessor : IPaymentProcessor
    {
        public PaymentResponse Pay(Payment payment)
        {
            #region Pos Configuration            
            string strPassword = "0";
            string strMerchantID = "0";
            string strHostAddress = payment.ServiceUrl;
            string strMerchantType = "0";
            #endregion

            var cardInfo = payment.CreditCard;
            var selectedCurrency = (int)Enum.Parse(typeof(Currency), payment.CurrencyId);

            var postData = new StringBuilder();
            postData.AppendFormat("{0}={1}&", "MerchantId", strMerchantID);
            postData.AppendFormat("{0}={1}&", "MerchantPassword", strPassword);
            postData.AppendFormat("{0}={1}&", "TransactionType", "Sale");
            postData.AppendFormat("{0}={1}&", "VerifyEnrollmentRequestId", payment.TransactionId);
            postData.AppendFormat("{0}={1}&", "Pan", cardInfo.Number);
            postData.AppendFormat("{0}={1}&", "Expiry", cardInfo.Expiry);
            postData.AppendFormat("{0}={1}&", "PurchaseAmount", payment.OrderDescription);
            postData.AppendFormat("{0}={1}&", "Currency", selectedCurrency);
            postData.AppendFormat("{0}={1}&", "BrandName", payment.CreditCard.BrandName);
            postData.AppendFormat("{0}={1}&", "SessionInfo", "");
            postData.AppendFormat("{0}={1}&", "SuccessUrl", payment.SuccessUrl);
            postData.AppendFormat("{0}={1}&", "FailUrl", payment.FailUrl);
            postData.AppendFormat("{0}={1}&", "InstallmentCount", payment.InstallmentCount);
            postData.AppendFormat("{0}={1}&", "IsRecurring", "");
            postData.AppendFormat("{0}={1}&", "RecurringFrequency", "");
            postData.AppendFormat("{0}={1}&", "RecurringEndDate", "");
            postData.AppendFormat("{0}={1}&", "MerchantType", strMerchantType);
            postData.AppendFormat("{0}={1}", "SubMerchantId", "");

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