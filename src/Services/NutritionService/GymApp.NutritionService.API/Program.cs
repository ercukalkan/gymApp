using GymApp.NutritionService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.DbSeeder;
using GymApp.Shared.MessageQueues.Configuration;
using GymApp.Shared.MessageQueues.Consumers;
using GymApp.Shared.RedisCache.Configuration;
using GymApp.NutritionService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddDbContext<NutritionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NutritionDatabase")));

builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddRateLimiting();

builder.Services.AddAuth(builder.Configuration.GetSection("jwtSettings"));

builder.Services.AddMassTransitConfiguration(
    builder.Configuration["RabbitMQ:Host"] ?? "localhost",
    builder.Configuration["RabbitMQ:Username"] ?? "guest",
    builder.Configuration["RabbitMQ:Password"] ?? "guest",
    typeof(WorkoutCompletedEventConsumer)
);

builder.Services.AddRedisConfiguration(
    builder.Configuration.GetValue<string>("Redis:RedisCacheDb") ?? "localhost:6379"
);

builder.Services.AddNutritionServices();
builder.Services.AddNutritionRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(policy =>
    policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200")
);

app.UseHttpsRedirection();

app.UseAuth();

app.UseRateLimiting();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NutritionContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.Map("/", () => "Nutrition Service is running...");

app.Run();