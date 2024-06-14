using CommonUtils.Helpers;
using CommonUtils.ServiceBus;
using DationBus.Api;
using DationBus.Api.Logics;
using DationBus.Business.Database;
using DationBus.Business.Services;
using DationBus.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var conn = AppConfig.Cloud.DatabaseConnectionString;
    options.UseSqlServer(conn);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//services
builder.Services.AddTransient<IExceptionHelper, ExceptionHelper>();
builder.Services.AddTransient<ITechnicalService, TechnicalService>();

builder.Services.AddTransient<IServiceBus, ServiceBus>(s =>
{
    return new ServiceBus(AppConfig.Cloud.AzureServiceBusConnectionString, builder.Configuration["QueueName"]);
});

var app = builder.Build();

AppConfig.ReadLocalConfig(app.Configuration);
AppConfig.ReadCloudConfig(app.Configuration);

app.UseCors(builder =>
builder
.WithOrigins(AppConfig.Local.AllowedOrigins)
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Equals("Development"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Utils.EnableSwagger = true;
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();