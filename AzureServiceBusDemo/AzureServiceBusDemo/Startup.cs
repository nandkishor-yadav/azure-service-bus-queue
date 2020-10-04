using AzureServiceBusDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureServiceBusDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
            services.AddTransient<IProcessData, ProcessData>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bus = app.ApplicationServices.GetService<IServiceBusConsumer>();
            bus.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
