using GymApp.NutritionService.Data.Context;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.DbSeeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NutritionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NutritionDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NutritionContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.MapGet("/", (NutritionContext context) => context.Foods.ToList());

app.Run();