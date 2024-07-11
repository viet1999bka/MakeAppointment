using Appointment.API.Protos;
using Blazored.Toast;
using Grpc.Net.Client.Web;
using ProcessCalendar.API;
using WebClient.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddGrpcClient<doctor.doctorClient>(option => option.Address = new Uri("https://localhost:7265/calendarAPI")).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));
builder.Services.AddGrpcClient<AppointRegisted.AppointRegistedClient>(option => option.Address = new Uri("https://localhost:7180")).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));
builder.Services.AddGrpcClient<AppointmentBookingApi.AppointmentBookingApiClient>(option => option.Address = new Uri("https://localhost:7238")).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
