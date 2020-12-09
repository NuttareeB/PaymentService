namespace PaymentService.Core.Models
{
    public class PaymentGatewayRuleModel
    {
        public string PaymentMethodType { get; set; }
        public string PaymentMethod { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string PaymentGateway { get; set; }
    }
}
