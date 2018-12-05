using System;

namespace PayFlex.Client
{
    public class PaymentManager
    {
        private IPaymentProcessor _paymentProcessor;

        public PaymentResponse Pay(Payment payment)
        {
            try
            {
                switch (payment.PaymentType)
                {
                    case PaymentType.VPos:
                        var selectProcessor = payment.ActivePayment;

                        if (selectProcessor == "MPI")
                        {
                            _paymentProcessor = PaymentProcessorFactory.CreatePaymentProcessor(VposPaymentSupplier.MPI);
                        }

                        if (selectProcessor == "VPOS")
                        {
                            _paymentProcessor = PaymentProcessorFactory.CreatePaymentProcessor(VposPaymentSupplier.VPos);
                        }

                        if (selectProcessor == "COMMONPAYMENT")
                        {
                            _paymentProcessor = PaymentProcessorFactory.CreatePaymentProcessor(VposPaymentSupplier.CommonPayment);
                        }

                        if (selectProcessor == "UYI_VPOS")
                        {
                            _paymentProcessor = PaymentProcessorFactory.CreatePaymentProcessor(VposPaymentSupplier.UYIVpos);
                        }

                        if (selectProcessor == "UYI_COMMONPAYMENT")
                        {
                            _paymentProcessor = PaymentProcessorFactory.CreatePaymentProcessor(VposPaymentSupplier.UYICommonPayment);
                        }

                        break;
                }

                var result = _paymentProcessor.Pay(payment);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}