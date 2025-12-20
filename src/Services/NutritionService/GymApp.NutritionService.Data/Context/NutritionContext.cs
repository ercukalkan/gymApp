using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Data.Entities.JunctionEntities;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Data.Context;

public class NutritionContext(DbContextOptions<NutritionContext> options) : DbContext(options)
{
    public DbSet<Food> Foods { get; set; } = null!;
    public DbSet<Meal> Meals { get; set; } = null!;
    public DbSet<Diet> Diets { get; set; } = null!;

    public DbSet<MealFood> MealFoods { get; set; } = null!;
    public DbSet<DietMeal> DietMeals { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MealFood>()
            .HasOne(mf => mf.Meal)
            .WithMany(m => m.MealFoods)
            .HasForeignKey(mf => mf.MealId);

        modelBuilder.Entity<MealFood>()
            .HasOne(mf => mf.Food)
            .WithMany(f => f.MealFoods)
            .HasForeignKey(mf => mf.FoodId);

        modelBuilder.Entity<DietMeal>()
            .HasOne(dm => dm.Diet)
            .WithMany(d => d.DietMeals)
            .HasForeignKey(dm => dm.DietId);

        modelBuilder.Entity<DietMeal>()
            .HasOne(dm => dm.Meal)
            .WithMany(m => m.DietMeals)
            .HasForeignKey(dm => dm.MealId);
    }
}