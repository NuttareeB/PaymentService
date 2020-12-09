using PaymentService.Core.Models;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public interface IProcessPaymentRequestCreator
    {
        StripeChargeCreateOptions CreateRequest(ProcessPaymentRequest request);
    }
}