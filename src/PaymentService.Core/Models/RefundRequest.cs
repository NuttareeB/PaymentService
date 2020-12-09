namespace PaymentService.Core.Models
{
    public class RefundRequest
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string ChargeId { get; set; }
    }
}
