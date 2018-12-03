using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDemo.Processor
{
    public class PayFlexUYIVposProcessor : IPaymentUYIProcessor
    {
        public PaymentResponse Pay(Payment payment)
        {
            #region Pos Configuration            
            string strHostAddress = payment.ServiceUrl;
            #endregion

            var cardInfo = payment.CreditCard;
            var selectedCurrency = (int)Enum.Parse(typeof(Currency), payment.CurrencyId);

            var postData = new StringBuilder();
            
            postData.AppendFormat("<{0}>", "TransactionRequest");
            postData.AppendFormat("<{0}>{1}</{0}>", "ClientMerchantCode", payment.PaymentUyi.ClientMerchantCode);
            postData.AppendFormat("<{0}>{1}</{0}>", "Password", payment.PaymentUyi.Password);
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionType", payment.PaymentTransactionType);
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyAmount", payment.PaymentTotal);
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyCode", selectedCurrency);
            postData.AppendFormat("<{0}>{1}</{0}>", "Pan", payment.CreditCard.Number);
            postData.AppendFormat("<{0}>{1}</{0}>", "Cvv", payment.CreditCard.CVV);
            postData.AppendFormat("<{0}>{1}</{0}>", "Expiry", payment.CreditCard.Expiry);
            postData.AppendFormat("<{0}>{1}</{0}>", "CardHoldersClientIp", payment.PaymentUyi.CardHoldersClientIp);
            postData.AppendFormat("<{0}>{1}</{0}>", "CardHoldersEmail", payment.CreditCard.CardHoldersEmail);
            postData.AppendFormat("<{0}>{1}</{0}>", "CustomerId", payment.PaymentUyi.CustomerId);
            postData.AppendFormat("</{0}>", "TransactionRequest");

            byte[] postByteArray = Encoding.UTF8.GetBytes(postData.ToString());

            WebRequest webRequest = WebRequest.Create(strHostAddress);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";
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