using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var eventsService = builder.AddProject<Events>("events");
var ticketsService = builder.AddProject<Tickets>("Tickets");

builder.Build().Run();
