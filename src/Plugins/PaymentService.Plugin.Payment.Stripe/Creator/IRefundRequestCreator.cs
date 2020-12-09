using PaymentService.Core.Models;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public interface IRefundRequestCreator
    {
        StripeRefundCreateOptions CreateRequest(RefundRequest request);
    }
}