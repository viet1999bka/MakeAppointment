 using Appointment.API.DI.Extensions;
using Appointment.API.Migrations;
using Appointment.API.Services;
using Appointment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<AppointServiceDbContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("OracleDBViet")));

// Configure Masstransit
builder.Services.AddConfigureMasstransitRabbitMq(builder.Configuration);
builder.Services.AddMediatR();
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<GreeterService>().EnableGrpcWeb();
app.MapGrpcService<AppointmentService>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
