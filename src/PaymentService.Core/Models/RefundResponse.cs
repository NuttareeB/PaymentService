namespace PaymentService.Core.Models
{
    public class RefundResponse : ResponseBase
    {
        public string RefundId { get; set; }
        public string ChargeId { get; set; }
        public decimal Amount { get; set; }
    }
}
