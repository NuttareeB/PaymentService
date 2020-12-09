namespace PaymentService.Core.Registrations
{
    using Common.Registrations;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            RegisterUtils(services);
        }

        private void RegisterUtils(IServiceCollection services)
        {
        }
    }
}
