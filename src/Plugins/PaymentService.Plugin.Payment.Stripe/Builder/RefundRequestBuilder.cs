using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public class RefundRequestBuilder : IRefundRequestBuilder
    {
        private StripeRefundCreateOptions _request;

        public RefundRequestBuilder Create()
        {
            _request = new StripeRefundCreateOptions();
            return this;
        }

        public RefundRequestBuilder Amount(int amount)
        {
            _request.Amount = amount;
            return this;
        }

        public RefundRequestBuilder Reason(string reason)
        {
            _request.Reason = reason;
            return this;
        }

        public StripeRefundCreateOptions Value()
        {
            return _request;
        }
    }
}
