namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    using global::Stripe;
    using PaymentService.Core.Models;
    using PaymentService.Plugin.Payment.Stripe.Builder;
    using System;

    public class RefundRequestCreator : IRefundRequestCreator
    {
        private IRefundRequestBuilder _refundRequestBuilder;

        public RefundRequestCreator(IRefundRequestBuilder refundRequestBuilder)
        {
            _refundRequestBuilder = refundRequestBuilder;
        }

        public StripeRefundCreateOptions CreateRequest(RefundRequest request)
        {
            return _refundRequestBuilder
                .Create()
                .Amount(Decimal.ToInt32(request.Amount))
                .Reason(request.Reason)
                .Value();
        }
    }
}
