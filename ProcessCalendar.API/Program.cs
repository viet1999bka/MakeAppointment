using Microsoft.EntityFrameworkCore;
using ProcessCalendar.API.Model;
using ProcessCalendar.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<CalendarDbContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("OracleDBViet")));

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
