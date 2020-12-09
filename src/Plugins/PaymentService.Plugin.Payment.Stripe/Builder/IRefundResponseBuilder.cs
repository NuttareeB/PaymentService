using PaymentService.Core.Models;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public interface IRefundResponseBuilder
    {
        RefundResponseBuilder Amount(int amount);
        RefundResponseBuilder ChargeId(string chargeId);
        RefundResponseBuilder Create();
        RefundResponseBuilder RefundId(string refundId);
        RefundResponseBuilder Status(string status);
        RefundResponse Value();
    }
}