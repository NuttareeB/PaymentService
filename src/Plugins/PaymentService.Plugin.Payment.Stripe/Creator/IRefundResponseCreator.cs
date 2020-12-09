using PaymentService.Core.Models;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public interface IRefundResponseCreator
    {
        RefundResponse CreateResponse(StripeRefund refundResp);
    }
}