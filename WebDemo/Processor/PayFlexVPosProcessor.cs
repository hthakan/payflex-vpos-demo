using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDemo.Processor
{
    public class PayFlexVPosProcessor : IPaymentProcessor
    {
        public PaymentResponse Pay(Payment payment)
        {
            #region Pos Configuration            
            string strPassword = "0";
            string strMerchantID = "0";
            string strHostAddress = "https://";
            string strTerminalNo = "";
            #endregion

            var cardInfo = payment.CreditCard;
            var selectedCurrency = (int)Enum.Parse(typeof(Currency), payment.CurrencyId);

            var postData = new StringBuilder();
            postData.AppendFormat("{0}", "<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            postData.AppendFormat("<{0}>", "VposRequest");
            postData.AppendFormat("<{0}>{1}</{0}>", "MerchantId", strMerchantID);
            postData.AppendFormat("<{0}>{1}</{0}>", "Password", strPassword);
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionId", payment.TransactionId);
            postData.AppendFormat("<{0}>{1}</{0}>", "TerminalNo", strTerminalNo);
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionType", "Sale");           
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyAmount", payment.PaymentTotal);
            postData.AppendFormat("<{0}>{1}</{0}>", "SurchargeAmount", payment.PaymentTotal);
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyCode", selectedCurrency);
            postData.AppendFormat("<{0}>{1}</{0}>", "PointAmount", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "PointCode", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "NumberOfInstallments", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "BrandName", payment.CreditCard.BrandName);
            postData.AppendFormat("<{0}>{1}</{0}>", "Pan", cardInfo.Number);
            postData.AppendFormat("<{0}>{1}</{0}>", "Expiry", cardInfo.Expiry);
            postData.AppendFormat("<{0}>{1}</{0}>", "Cvv", cardInfo.CVV);
            postData.AppendFormat("<{0}>{1}</{0}>", "SecurityCode", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "ReferenceTransactionId", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "CardHoldersName", payment.CreditCard.CardHolderName);
            postData.AppendFormat("<{0}>{1}</{0}>", "ClientIp", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "OrderId", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "OrderDescription", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "RecurringFrequencyType", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "RecurringFrequency", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "RecurringInstallmentCount", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "CAVV", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "ECI", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "CustomItems", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "DeviceType", "3");
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionDeviceSource", "0");
            postData.AppendFormat("<{0}>{1}</{0}>", "Extract", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "ExpSign", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "MpiTransactionId", "");
            postData.AppendFormat("<{0}>{1}</{0}>", "Location", "1");
            postData.AppendFormat("<{0}>{1}</{0}>", "MerchantType", "0");
            postData.AppendFormat("<{0}>{1}</{0}>", "SubMerchantId", "");
            postData.AppendFormat("</{0}>", "VposRequest");

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