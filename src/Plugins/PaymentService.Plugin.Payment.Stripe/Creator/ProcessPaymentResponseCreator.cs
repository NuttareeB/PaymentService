using PaymentService.Core.Models;
using PaymentService.Plugin.Payment.Stripe.Builder;
using Stripe;

namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    public class ProcessPaymentResponseCreator : IProcessPaymentResponseCreator
    {
        private IProcessPaymentResponseBuilder _processPaymentResponseBuilder;

        public ProcessPaymentResponseCreator(IProcessPaymentResponseBuilder processPaymentResponseBuilder)
        {
            _processPaymentResponseBuilder = processPaymentResponseBuilder;
        }

        public ProcessPaymentResponse CreateResponse(StripeCharge chargeResp)
        {
            return _processPaymentResponseBuilder
                .Create()
                .Status(chargeResp.Status)
                .ChargeId(chargeResp.Id)
                .Value();
        }
    }
}
