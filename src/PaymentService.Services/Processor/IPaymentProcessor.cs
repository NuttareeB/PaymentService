using PaymentService.Core.Models;

namespace PaymentService.Services.Processor
{
    public interface IPaymentProcessor
    {
        ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request);
        RefundResponse Refund(RefundRequest request);
    }
}