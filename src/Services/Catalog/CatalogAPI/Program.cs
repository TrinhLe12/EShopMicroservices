var builder = WebApplication.CreateBuilder(args);

//Add services
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

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    if (isDevelopment)
    {
        opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
    }
    else
    {
        opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.None;
    }
}).UseLightweightSessions();

if (isDevelopment)
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

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
