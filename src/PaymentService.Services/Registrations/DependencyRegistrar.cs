namespace PaymentService.Services.Registrations
{
    using Common.Registrations;
    using Microsoft.Extensions.DependencyInjection;
    using PaymentService.Services.Processor;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            RegisterProcessor(services);
        }

        private void RegisterProcessor(IServiceCollection services)
        {
            services.AddSingleton<IPaymentProcessor, PaymentProcessor>();
        }
    }
}
