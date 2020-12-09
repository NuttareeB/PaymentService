namespace PaymentService.Plugin.Payment.Stripe
{
    using global::Stripe;
    using PaymentService.Core.Models;
    using PaymentService.Core.Processor;
    using PaymentService.Plugin.Payment.Stripe.Creator;

    public class PaymentMethodProcessor : IPaymentMethodProcessor
    {
        private IProcessPaymentRequestCreator _processPaymentRequestCreator;
        private IProcessPaymentResponseCreator _processPaymentResponseCreator;
        private IRefundRequestCreator _refundRequestCreator;
        private IRefundResponseCreator _refundResponseCreator;

        public PaymentMethodProcessor(
            IProcessPaymentRequestCreator processPaymentRequestCreator,
            IProcessPaymentResponseCreator processPaymentResponseCreator,
            IRefundRequestCreator refundRequestCreator,
            IRefundResponseCreator refundResponseCreator)
        {
            _processPaymentRequestCreator = processPaymentRequestCreator;
            _processPaymentResponseCreator = processPaymentResponseCreator;
            _refundRequestCreator = refundRequestCreator;
            _refundResponseCreator = refundResponseCreator;

            StripeConfiguration.SetApiKey("sk_test_A045JVruzKQRNw2NzAIUnxnU");
        }

        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request)
        {
            var paymentRequest = _processPaymentRequestCreator.CreateRequest(request);
            var service = new StripeChargeService();
            var result = _processPaymentResponseCreator.CreateResponse(service.Create(paymentRequest));

            return result;
        }

        public RefundResponse Refund(RefundRequest request)
        {
            var refundService = new StripeRefundService();
            var result = _refundResponseCreator.CreateResponse(refundService.Create(request.ChargeId, _refundRequestCreator.CreateRequest(request)));

            return result;
        }
    }
}
