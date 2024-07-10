var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebClient>("webclient");

builder.AddProject<Projects.ProcessCalendar_API>("processcalendar-api");
builder.AddProject<Projects.Appointment_API>("appointmen-api");

builder.AddProject<Projects.ReverseProxy>("reverseproxy");

builder.Build().Run();
