
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Fabric;
using System.Net.Http;
namespace PaymentService.ClientAdapter
{
    public static class PaymentServiceExtentions
    {
        public static void AddPaymentServiceAdapter(this IServiceCollection services, IConfiguration configuration, bool checkPrerequisite = true)
        {
            if (checkPrerequisite) services.PrerequisiteComponents();
            new Composition.PaymentService.CompositionModule().AddServices(services, configuration);
        }

        internal static void PrerequisiteComponents(this IServiceCollection services)
        {
            var needToAddHttpClient = true;
            var needToAddFabricClient = true;
            var components = services.GetEnumerator();
            while (components.MoveNext())
            {
                var component = components.Current;
                if (component.ServiceType == typeof(HttpClient) && component.Lifetime == ServiceLifetime.Singleton)
                {
                    needToAddHttpClient = false;
                }
                if (component.ServiceType == typeof(FabricClient) && component.Lifetime == ServiceLifetime.Singleton)
                {
                    needToAddFabricClient = false;
                }
            }
            if (needToAddHttpClient) services.AddSingleton(new HttpClient());
            if (needToAddFabricClient) services.AddSingleton(new FabricClient());
        }
    }
}
