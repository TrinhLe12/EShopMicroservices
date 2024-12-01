using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

//Services are registered in this order, pay attention to order
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


#endregion Add services to the container

var app = builder.Build();

#region Register services into application pipeline

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    //Auto migrate changes and seed data on start up
    await app.InitialiseDatabaseAsync();
}

#endregion Register services into application pipeline

app.Run();
