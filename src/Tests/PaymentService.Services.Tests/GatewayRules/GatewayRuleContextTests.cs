namespace PaymentService.Services.Tests.GatewayRules
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PaymentService.Core.Models;
    using PaymentService.Services.GatewayRules;

    [TestClass]
    public class GatewayRuleContextTests
    {
        [TestMethod]
        public void GetPaymentGatewayTest()
        {
            var gatewayRuleContext = new GatewayRuleManager();

            var request = new PaymentGatewayRuleModel()
            {
                Country = "TH",
                Currency = "THB",
                PaymentMethodType = "Direct",
                PaymentMethod = "CreditCard"
            };
            var gateway = gatewayRuleContext.GetPaymentGateway(request);
            Assert.AreEqual("Stripe", gateway);
        }
    }
}
