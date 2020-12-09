namespace PaymentService.Plugin.Payment.Stripe.Builder
{
    using global::Stripe;

    public interface IProcessPaymentRequestBuilder
    {
        ProcessPaymentRequestBuilder Amount(int amount);
        ProcessPaymentRequestBuilder Create();
        ProcessPaymentRequestBuilder Currency(string currency);
        ProcessPaymentRequestBuilder ReceiptEmail(string receiptEmail);
        ProcessPaymentRequestBuilder SourceToken(string sourceToken);
        StripeChargeCreateOptions Value();
    }
}