using DiscountGRPC.Protos;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

#region Application Services
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddMediatR(config =>
{
    //MediatR installed in Building Block --> need specify which assembly using service
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
#endregion

#region Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //Use usernam as id for shopping cart
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    if (isDevelopment)
    {
        opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
    }
    else
    {
        opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.None;
    }
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});
#endregion

#region Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();

    if (isDevelopment)
    {
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    }
    
    return handler;
});
#endregion

#region Message Broker service
//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);
#endregion

#region Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
#endregion

#endregion Add services to the container

var app = builder.Build();

//Config pipeline
app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
