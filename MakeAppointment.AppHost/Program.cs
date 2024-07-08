var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebClient>("webclient");

builder.AddProject<Projects.ProcessCalendar_API>("processcalendar-api");

builder.Build().Run();
