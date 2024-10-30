var builder = WebApplication.CreateBuilder(args);

//Add services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //MediatR installed in Building Block --> need specify which assembly using service
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Config pipeline
app.MapCarter();

app.Run();
