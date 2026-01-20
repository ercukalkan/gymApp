using GymApp.GymTrackingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.GymTrackingService.Data.DbSeeder;
using GymApp.Shared.MessageQueues.Configuration;
using GymApp.GymTrackingService.API.Features.EventPublishers;
using GymApp.GymTrackingService.API.Features.EventConsumers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<GymTrackingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GymTrackingDatabase"))
);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors();

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

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GymTrackingContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.MapControllers();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.Run();