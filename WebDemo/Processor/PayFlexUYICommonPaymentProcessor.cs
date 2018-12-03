using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDemo.Processor
{
    public class PayFlexUYICommonPaymentProcessor : IPaymentUYIProcessor
    {
        public PaymentResponse Pay(Payment payment)
        {
            #region Pos Configuration     
            string strHostAddress = payment.ServiceUrl;

            #endregion

            var cardInfo = payment.CreditCard;
            var selectedCurrency = (int)Enum.Parse(typeof(Currency), payment.CurrencyId);
            var selectedTransactionType = Enum.Parse(typeof(PaymentTransactionType), payment.PaymentTransactionType);

            var postData = new StringBuilder();        
            postData.AppendFormat("{0}={1}&", "TransactionId", payment.TransactionId);
            postData.AppendFormat("{0}={1}&", "ClientMerchantCode", payment.PaymentUyi.ClientMerchantCode);
            postData.AppendFormat("{0}={1}&", "Password", payment.PaymentUyi.Password);
            postData.AppendFormat("{0}={1}&", "CardHolderIp", payment.CreditCard.CardHolderIp);
            postData.AppendFormat("{0}={1}&", "CardHolderEmail", payment.CreditCard.CardHoldersEmail);
            postData.AppendFormat("{0}={1}&", "CardHolderName", payment.CreditCard.CardHolderName);
            postData.AppendFormat("{0}={1}&", "Apply3DS", payment.PaymentUyi.Apply3DS);
            postData.AppendFormat("{0}={1}&", "SupportHalfSecure", payment.PaymentUyi.SupportHalfSecure);
            postData.AppendFormat("{0}={1}&", "ReturnUrl", payment.ReturnUrl);
            postData.AppendFormat("{0}={1}&", "Amount", payment.PaymentTotal);
            postData.AppendFormat("{0}={1}&", "AmountCode", selectedCurrency);
            postData.AppendFormat("{0}={1}&", "TransactionType", selectedTransactionType);
            postData.AppendFormat("{0}={1}&", "IsSaveCard", payment.PaymentUyi.IsSaveCard);
            postData.AppendFormat("{0}={1}&", "IsHideSaveCard", payment.PaymentUyi.IsHideSaveCard);
            postData.AppendFormat("{0}={1}&", "HideAmount", payment.PaymentUyi.HideAmount);
            postData.AppendFormat("{0}={1}&", "CustomerId", payment.PaymentUyi.CustomerId);
            postData.AppendFormat("{0}={1}&", "IsSmsUsage", payment.PaymentUyi.IsSmsUsage);
            postData.AppendFormat("{0}={1}&", "TelephoneNumber", payment.CreditCard.PhoneNumber);
            postData.AppendFormat("{0}={1}&", "Token", payment.PaymentUyi.Token);
            postData.AppendFormat("{0}={1}&", "InstallmentCount", payment.InstallmentCount);
            postData.AppendFormat("{0}={1}&", "OrderID", payment.OrderId);
            postData.AppendFormat("{0}={1}", "OrderDescription", payment.OrderDescription);

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