using GymApp.NutritionService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.DbSeeder;
using GymApp.Shared.MessageQueues.Configuration;
using GymApp.NutritionService.API.Features.EventConsumers;
using GymApp.Shared.RedisCache.Configuration;
using GymApp.NutritionService.Core.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymApp.NutritionService.Core.Services;
using GymApp.NutritionService.Core.Repositories;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddDbContext<NutritionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NutritionDatabase")));

builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"]
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddMassTransitConfiguration(
    builder.Configuration["RabbitMQ:Host"] ?? "localhost",
    builder.Configuration["RabbitMQ:Username"] ?? "guest",
    builder.Configuration["RabbitMQ:Password"] ?? "guest",
    typeof(WorkoutCompletedEventConsumer)
);

builder.Services.AddScoped<WorkoutCompletedEventConsumer>();

builder.Services.AddRedisConfiguration(
    builder.Configuration.GetValue<string>("Redis:RedisCacheDb") ?? "localhost:6379"
);
builder.Services.AddSingleton<IRedisService, RedisService>();

builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IDietRepository, DietRepository>();
builder.Services.AddScoped<IDietService, DietService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NutritionContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.Map("/", () => "Nutrition Service is running...");

app.MapDelete("/foodsBulkDelete", async (NutritionContext context) =>
{
    var foodsList = context.Foods.ToList();

    if (foodsList.Count == 0) return Results.Empty;

    context.Foods.RemoveRange(foodsList);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();