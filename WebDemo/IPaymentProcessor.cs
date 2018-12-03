namespace WebDemo
{
    public interface IPaymentProcessor
    {
        PaymentResponse Pay(Payment input);
    }
}
