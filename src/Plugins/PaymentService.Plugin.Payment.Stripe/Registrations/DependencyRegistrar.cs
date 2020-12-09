namespace PaymentService.Plugin.Payment.Stripe.Registrations
{
    using Common.Registrations;
    using Microsoft.Extensions.DependencyInjection;
    using PaymentService.Plugin.Payment.Stripe.Builder;
    using PaymentService.Plugin.Payment.Stripe.Creator;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            RegisterBuilder(services);
            RegisterCreator(services);
        }

        private void RegisterBuilder(IServiceCollection services)
        {
            services.AddSingleton<IProcessPaymentRequestBuilder, ProcessPaymentRequestBuilder>();
            services.AddSingleton<IProcessPaymentResponseBuilder, ProcessPaymentResponseBuilder>();
            services.AddSingleton<IRefundRequestBuilder, RefundRequestBuilder>();
            services.AddSingleton<IRefundResponseBuilder, RefundResponseBuilder>();
        }

        private void RegisterCreator(IServiceCollection services)
        {
            services.AddSingleton<IProcessPaymentRequestCreator, ProcessPaymentRequestCreator>();
            services.AddSingleton<IProcessPaymentResponseCreator, ProcessPaymentResponseCreator>();
            services.AddSingleton<IRefundRequestCreator, RefundRequestCreator>();
            services.AddSingleton<IRefundResponseCreator, RefundResponseCreator>();
        }
    }
}
