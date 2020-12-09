using PaymentService.Core.Models;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public class RefundResponseBuilder : IRefundResponseBuilder
    {
        private RefundResponse _response;

        public RefundResponseBuilder Create()
        {
            _response = new RefundResponse();
            return this;
        }

        public RefundResponseBuilder Status(string status)
        {
            _response.Status = status;
            return this;
        }

        public RefundResponseBuilder ChargeId(string chargeId)
        {
            _response.ChargeId = chargeId;
            return this;
        }

        public RefundResponseBuilder RefundId(string refundId)
        {
            _response.RefundId = refundId;
            return this;
        }

        public RefundResponseBuilder Amount(int amount)
        {
            _response.Amount = amount;
            return this;
        }

        public RefundResponse Value()
        {
            return _response;
        }
    }
}
