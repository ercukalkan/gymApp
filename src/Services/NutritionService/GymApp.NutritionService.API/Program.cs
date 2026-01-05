using GymApp.NutritionService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.DbSeeder;
using GymApp.Shared.MessageQueues.Configuration;
using GymApp.NutritionService.API.Features.EventConsumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddDbContext<NutritionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NutritionDatabase")));

builder.Services.AddOpenApi();
builder.Services.AddLogging();

var rabbitmqHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
var rabbitmqUsername = builder.Configuration["RabbitMQ:Username"] ?? "guest";
var rabbitmqPassword = builder.Configuration["RabbitMQ:Password"] ?? "guest";

builder.Services.AddMassTransitConfiguration(rabbitmqHost, rabbitmqUsername, rabbitmqPassword, typeof(WorkoutCompletedEventConsumer));
builder.Services.AddScoped<WorkoutCompletedEventConsumer>();

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