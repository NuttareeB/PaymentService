using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public interface IRefundRequestBuilder
    {
        RefundRequestBuilder Amount(int amount);
        RefundRequestBuilder Create();
        RefundRequestBuilder Reason(string reason);
        StripeRefundCreateOptions Value();
    }
}