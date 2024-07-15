using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.Model;
using ProcessCalendar.API.Repositories;
using ProcessCalendar.API.Services;
using ProcessCalendar.API.UseCases.Event;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<CalendarDbContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("OracleDBViet")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

// Configure Masstransit
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddMassTransit(busConfig =>
{
    //busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.AddConsumer<ProcessAppointConsumer>();
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
app.UseGrpcWeb();


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DoctorService>().EnableGrpcWeb();
app.MapGrpcService<AppointRegistedService>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
