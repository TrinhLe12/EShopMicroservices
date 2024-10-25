var builder = WebApplication.CreateBuilder(args);

//Add services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//Config pipeline
app.MapCarter();

app.Run();
