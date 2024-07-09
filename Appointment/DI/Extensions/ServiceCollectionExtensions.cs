using Appointment.API.DI.Options;
using MassTransit;

namespace Appointment.API.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration) {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
            services.AddMassTransit(ev =>
            {
                ev.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });
                });
            });
            return services;
        }
    }
}
