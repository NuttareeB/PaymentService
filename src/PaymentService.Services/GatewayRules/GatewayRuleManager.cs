namespace PaymentService.Services.GatewayRules
{
    using Newtonsoft.Json;
    using PaymentService.Core.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class GatewayRuleManager
    {
        private List<PaymentGatewayRuleModel> GetPaymentGatewayRules()
        {
            return JsonConvert.DeserializeObject<List<PaymentGatewayRuleModel>>(File.ReadAllText("Data/paymentGatewayRules.json"));
        }

        public string GetPaymentGateway(PaymentGatewayRuleModel ruleContext)
        {
            var gatewayRules = GetPaymentGatewayRules();
            var matchedGateway = gatewayRules.AsParallel().Where(
                gatewayRule => gatewayRule.PaymentMethodType == ruleContext.PaymentMethodType
                                && gatewayRule.Country == ruleContext.Country
                                && gatewayRule.Currency == ruleContext.Currency).FirstOrDefault().PaymentGateway;

            if (matchedGateway == null)
            {
                // TODO: matchedGateway = defaultGateway
            }

            return matchedGateway;
        }


    }
}
