using PaymentService.Core.Models;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public interface IProcessPaymentResponseCreator
    {
        ProcessPaymentResponse CreateResponse(StripeCharge chargeResp);
    }
}