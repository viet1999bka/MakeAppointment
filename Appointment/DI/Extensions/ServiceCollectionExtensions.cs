using Appointment.API.DI.Options;
using Appointment.API.MessageBus.Consumer.Command;
using Common.Abstractions.IntegrationEvents;
using MassTransit;
using System.Reflection;
using static Common.Abstractions.IntegrationEvents.Command;

namespace Appointment.API.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services) => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        public static IServiceCollection AddConfigureMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration) {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
            services.AddMassTransit(ev =>
            {
                ev.AddConsumer<SendBookApointmentWhenRecieveCommandCosumer>();
                ev.AddConsumers(Assembly.GetExecutingAssembly());
                ev.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });
                    bus.ReceiveEndpoint("send-book-appointment", e =>
                    {
                        e.ConfigureConsumer<SendBookApointmentWhenRecieveCommandCosumer>(context);
                    });
                    bus.ConfigureEndpoints(context);

                });

            });
            return services;
        }
    }
}
