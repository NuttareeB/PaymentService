namespace PaymentService.Core.Models
{
    public class ProcessPaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardToken { get; set; }
        public string ReceiptEmail { get; set; }
    }
}
