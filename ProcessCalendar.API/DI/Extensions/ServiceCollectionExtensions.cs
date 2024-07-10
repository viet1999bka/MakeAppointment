using MassTransit;
using ProcessCalendar.API.DI.Options;
using ProcessCalendar.API.MessageBus.Consumer.Event;

//using ProcessCalendar.API.MessageBus.Consumer.Event;
using System.Reflection;

namespace ProcessCalendar.API.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services) => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        public static IServiceCollection AddConfigureMasstransitRabbitMq(this IServiceCollection services, IConfiguration configuration) {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
            services.AddMassTransit(ev =>
            {
                //ev.AddConsumer<SendAppointmentWhenRecievedEventConsumer>();
                ev.AddConsumers(Assembly.GetExecutingAssembly());
                ev.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
