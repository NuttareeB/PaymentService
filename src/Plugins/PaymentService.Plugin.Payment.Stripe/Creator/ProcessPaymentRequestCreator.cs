namespace PaymentService.Plugin.Payment.Stripe.Creator
{
    using global::Stripe;
    using PaymentService.Core.Models;
    using PaymentService.Plugin.Payment.Stripe.Builder;
    using System;

    public class ProcessPaymentRequestCreator : IProcessPaymentRequestCreator
    {
        private IProcessPaymentRequestBuilder _processPaymentRequestBuilder;

        public ProcessPaymentRequestCreator(IProcessPaymentRequestBuilder processPaymentRequestBuilder)
        {
            _processPaymentRequestBuilder = processPaymentRequestBuilder;
        }

        public StripeChargeCreateOptions CreateRequest(ProcessPaymentRequest request)
        {
            return _processPaymentRequestBuilder
                .Create()
                .Amount(Decimal.ToInt32(request.Amount))
                .Currency(request.Currency)
                .ReceiptEmail(request.ReceiptEmail)
                .SourceToken(request.CardToken)
                .Value();
        }
    }
}
