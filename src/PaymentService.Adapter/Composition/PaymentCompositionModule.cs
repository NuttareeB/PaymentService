using Common.ServiceFabric.AspnetCore.Configuration;
using Common.ServiceFabric.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using PaymentService.ClientAdapter.ServiceAdapter.PaymentApiClient;
using PaymentService.ClientAdapter.ServiceProxy;
using System.Fabric;
namespace PaymentService.ClientAdapter.Composition.PaymentService
{
    public class CompositionModule : ICompositionModule
    {
        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPaymentApiOptions, ApiOptions>();
            services.AddSingleton<IApiClientFactory<IPaymentServiceApiClient>, PaymentServiceApiClientFactory>();//<===  Create instance of PaymentServiceApiClient inside here 
            services.AddSingleton<ICommunicationClientFactory<CommunicationClient<IPaymentServiceApiClient>>>(           // ICommunicationClientFactory from microsoft  : CommunicationClientFactoryBase
                                serviceProvider => new PaymentServiceApiCommunicationClientFactory(
                                      new ServicePartitionResolver(() => serviceProvider.GetService<FabricClient>()),               // Param 1 => ServicePartitionResolver(FabricClient)
                                      serviceProvider.GetService<IApiClientFactory<IPaymentServiceApiClient>>())         // Param 2 => PaymentServiceApiClientFactory
                                );
            // Getting retry support with ServicePartitionClient
            services.AddSingleton<IPartitionClientFactory<CommunicationClient<IPaymentServiceApiClient>>, PaymentServiceApiPartitionClientFactory>();
            services.AddScoped<IPaymentServiceProxy, PaymentServiceProxy>();
        }
    }
}
