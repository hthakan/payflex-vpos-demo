namespace PayFlex.Client
{
    public interface IPaymentProcessor
    {
        PaymentResponse Pay(Payment input);
    }
}
