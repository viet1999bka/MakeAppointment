
using Appointment.API.Migrations;
using Appointment.API.Services;
using Appointment.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<AppointServiceDbContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("OracleDBViet")));

// Configure Masstransit
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddMassTransit(busConfig =>
{
    //busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri("rabbitmq://localhost/rabbit"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        configurator.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<GreeterService>().EnableGrpcWeb();
app.MapGrpcService<AppointmentService>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
