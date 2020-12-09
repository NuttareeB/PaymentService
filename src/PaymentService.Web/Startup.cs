namespace PaymentService.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using PluginService.Managers;
    using PluginService.Models;
    using ProductReliableCollectionService.Middleware;
    using System;

    public class Startup
    {
        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var hostingEnv = _serviceProvider.GetService<IHostingEnvironment>();
            DirectoryModel.BaseDirectory = hostingEnv.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // initial plugin manager
            var pluginPath = "~/Plugins";
            _serviceProvider.GetRequiredService<IPluginManager>().Initialize(services, pluginPath);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
