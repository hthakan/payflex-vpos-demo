using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
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

    public enum Currency
    {
        TRY = 949,
        USD = 840,
        EUR = 978,
        JPY = 392,
        GBP = 826,
    }
}
