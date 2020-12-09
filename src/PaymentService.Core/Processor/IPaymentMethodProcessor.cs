namespace PaymentService.Core.Processor
{
    using PaymentService.Core.Models;

    public interface IPaymentMethodProcessor
    {
        ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request);
        RefundResponse Refund(RefundRequest request);
    }
}
