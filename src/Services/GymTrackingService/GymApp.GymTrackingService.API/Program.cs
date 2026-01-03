using GymApp.GymTrackingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.GymTrackingService.Data.DbSeeder;
using MassTransit;
using GymApp.GymTrackingService.API.PublisherServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<GymTrackingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GymTrackingDatabase"))
);

builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();
    configurator.UsingRabbitMq((context, configure) =>
    {
        configure.Host("localhost", configure => { });
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.Services.AddScoped<IPublisherService, PublisherService>();

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