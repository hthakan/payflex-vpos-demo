using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
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
