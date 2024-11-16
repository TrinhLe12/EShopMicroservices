using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

#endregion Add services to the container

var app = builder.Build();

#region Register services into application pipeline



#endregion Register services into application pipeline

app.Run();
