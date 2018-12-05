using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
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
}
