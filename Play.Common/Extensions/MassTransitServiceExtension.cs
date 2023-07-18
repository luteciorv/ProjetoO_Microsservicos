using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Commom.Settings;
using System.Reflection;

namespace Play.Common.Extensions
{
    public static class MassTransitServiceExtension
    {
        public static void ConfigureMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(bus =>
            {
                bus.AddConsumers(Assembly.GetEntryAssembly());

                bus.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration?.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

                    var rabbitMQSettings = configuration?.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings?.Host);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings?.Name, false));
                    configurator.UseMessageRetry(retryConigurator =>
                    {
                        retryConigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });
        }
    }
}
