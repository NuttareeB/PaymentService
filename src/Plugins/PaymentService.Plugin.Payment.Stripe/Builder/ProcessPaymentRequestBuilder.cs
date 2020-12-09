namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    using global::Stripe;

    public class ProcessPaymentRequestBuilder : IProcessPaymentRequestBuilder
    {
        private StripeChargeCreateOptions _request;

        public ProcessPaymentRequestBuilder Create()
        {
            _request = new StripeChargeCreateOptions();
            return this;
        }

        public ProcessPaymentRequestBuilder Amount(int amount)
        {
            _request.Amount = amount;
            return this;
        }

        public ProcessPaymentRequestBuilder Currency(string currency)
        {
            _request.Currency = currency;
            return this;
        }

        public ProcessPaymentRequestBuilder SourceToken(string sourceToken)
        {
            _request.SourceTokenOrExistingSourceId = sourceToken;
            return this;
        }

        public ProcessPaymentRequestBuilder ReceiptEmail(string receiptEmail)
        {
            _request.ReceiptEmail = receiptEmail;
            return this;
        }

        public StripeChargeCreateOptions Value()
        {
            return _request;
        }
    }
}
