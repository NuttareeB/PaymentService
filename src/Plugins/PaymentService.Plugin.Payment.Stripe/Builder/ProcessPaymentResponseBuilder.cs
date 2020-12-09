using PaymentService.Core.Models;

namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    public class ProcessPaymentResponseBuilder : IProcessPaymentResponseBuilder
    {
        private ProcessPaymentResponse _response;

        public ProcessPaymentResponseBuilder Create()
        {
            _response = new ProcessPaymentResponse();
            return this;
        }

        public ProcessPaymentResponseBuilder Status(string status)
        {
            _response.Status = status;
            return this;
        }

        public ProcessPaymentResponseBuilder ChargeId(string chargeId)
        {
            _response.ChargeId = chargeId;
            return this;
        }

        public ProcessPaymentResponse Value()
        {
            return _response;
        }
    }
}
