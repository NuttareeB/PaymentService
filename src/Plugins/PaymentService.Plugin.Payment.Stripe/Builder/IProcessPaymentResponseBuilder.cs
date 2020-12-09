using PaymentService.Core.Models;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public interface IProcessPaymentResponseBuilder
    {
        ProcessPaymentResponseBuilder Create();
        ProcessPaymentResponseBuilder Status(string status);
        ProcessPaymentResponseBuilder ChargeId(string chargeId);
        ProcessPaymentResponse Value();
    }
}