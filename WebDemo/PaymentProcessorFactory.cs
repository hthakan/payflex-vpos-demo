using System;
using WebDemo.Processor;

namespace WebDemo
{
    public class PaymentProcessorFactory
    {
        public static IPaymentProcessor CreatePaymentProcessor(VposPaymentSupplier supplier)
        {

            switch (supplier)
            {
                case VposPaymentSupplier.MPI:
                    return new PayFlexMPIProcessor();
                case VposPaymentSupplier.VPos:
                    return new PayFlexVPosProcessor();
                case VposPaymentSupplier.CommonPayment:
                    return new PayFlexCommonPaymentProcessor();
                case VposPaymentSupplier.UYIVpos:
                    return new PayFlexUYIVposProcessor();
                case VposPaymentSupplier.UYICommonPayment:
                    return new PayFlexUYICommonPaymentProcessor();
                default:
                    throw new ArgumentOutOfRangeException("supplier", supplier, null);
            }
        }
    }

    public enum VposPaymentSupplier
    {
        MPI,
        VPos,
        CommonPayment,
        UYIVpos,
        UYICommonPayment
    }
}