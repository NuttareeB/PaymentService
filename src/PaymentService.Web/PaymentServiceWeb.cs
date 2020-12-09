namespace PaymentService.Web
{
    using Common.Registrations;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System.Collections.Generic;
    using System.Fabric;
    using System.IO;
    using System.Threading.Tasks;
    using PaymentCore = global::PaymentService.Core.Registrations;
    using PaymentServices = global::PaymentService.Services.Registrations;
    using PaymentWeb = global::PaymentService.Web.Registrations;
    using Plugin = PluginService.Registrations;

    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class PaymentServiceWeb : StatelessService
    {
        public PaymentServiceWeb(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => {
                                            services.AddSingleton<StatelessServiceContext>(serviceContext);

                                            RegisterDependencies(services);
                                        })
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                                    .UseUrls(url)
                                    .Build();
                    }))
            };
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            // cannot put in Parallel.ForEach because this is used for initialization
            var coreDependencies = new Plugin.DependencyRegistrar();
            coreDependencies.Register(services);

            // register external dependency
            var externalDependencies = new List<IDependencyRegistrar>(){
                new PaymentWeb.DependencyRegistrar(),
                new PaymentServices.DependencyRegistrar(),
                new PaymentCore.DependencyRegistrar()
            };

            Parallel.ForEach(externalDependencies, dependency => dependency.Register(services));
        }
    }
}
