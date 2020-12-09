using PaymentService.Core.Models;
using PaymentService.Plugin.Payment.Stripe.Builder;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public class RefundResponseCreator : IRefundResponseCreator
    {
        private IRefundResponseBuilder _refundResponseBuilder;

        public RefundResponseCreator(IRefundResponseBuilder refundResponseBuilder)
        {
            _refundResponseBuilder = refundResponseBuilder;
        }

        public RefundResponse CreateResponse(StripeRefund refundResp)
        {
            return _refundResponseBuilder
                .Create()
                .RefundId(refundResp.Id)
                .ChargeId(refundResp.ChargeId)
                .Amount(refundResp.Amount)
                .Status(refundResp.Status)
                .Value();
        }
    }
}
