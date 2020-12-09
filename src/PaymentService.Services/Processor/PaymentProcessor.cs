namespace PaymentService.Services.Processor
{
    using PaymentService.Core.Models;
    using PaymentService.Core.Processor;
    using PluginService.Managers;

    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IPluginManager _pluginManager;

        public PaymentProcessor(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request)
        {
            var plugin = _pluginManager.LoadPlugin<IPaymentMethodProcessor>(PluginManager.PluginDict["Stripe"]);
            return plugin.ProcessPayment(request);
        }

        public RefundResponse Refund(RefundRequest request)
        {
            var plugin = _pluginManager.LoadPlugin<IPaymentMethodProcessor>(PluginManager.PluginDict["Stripe"]);
            return plugin.Refund(request);
        }
    }
}
