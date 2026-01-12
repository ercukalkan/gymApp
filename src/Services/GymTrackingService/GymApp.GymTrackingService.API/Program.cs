using GymApp.GymTrackingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.GymTrackingService.Data.DbSeeder;
using GymApp.Shared.MessageQueues.Configuration;
using GymApp.GymTrackingService.API.Features.EventPublishers;
using GymApp.GymTrackingService.API.Features.EventConsumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<GymTrackingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GymTrackingDatabase"))
);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors();

var rabbitmqHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
var rabbitmqUsername = builder.Configuration["RabbitMQ:Username"] ?? "guest";
var rabbitmqPassword = builder.Configuration["RabbitMQ:Password"] ?? "guest";

builder.Services.AddMassTransitConfiguration(rabbitmqHost, rabbitmqUsername, rabbitmqPassword, typeof(NewUserCreatedEventConsumer));
builder.Services.AddScoped<WorkoutCompletedEventPublisher>();
builder.Services.AddScoped<NewUserCreatedEventConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GymTrackingContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.MapControllers();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.Run();