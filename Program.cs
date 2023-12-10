using DAL8.Models.DB;
using Net8Features;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false, true)
           .Build();

builder.Services.AddTransient<NorthwindContext>();
builder.Services.AddKeyedScoped<NorthwindContext>("scoped");

builder.Services.Configure<WeatherServiceOptions>(configuration.GetSection("WeatherServiceOptions"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddKeyedTransient<IWeatherService, WeatherService>("weather");
builder.Services.AddKeyedTransient<IWeatherService, WeatherService2>("weather2");

builder.Services.AddTransient<IWeatherService>(serviceProvider =>
{    
    var key = configuration.GetSection("WeatherServiceOptions")["ServiceName"];
    return serviceProvider.GetRequiredKeyedService<IWeatherService>(key);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
