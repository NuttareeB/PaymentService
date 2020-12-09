namespace PaymentService.Web.Registrations
{
    using Common.Registrations;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            RegisterControllers(services);
        }

        private void RegisterControllers(IServiceCollection services)
        {
        }
    }
}
