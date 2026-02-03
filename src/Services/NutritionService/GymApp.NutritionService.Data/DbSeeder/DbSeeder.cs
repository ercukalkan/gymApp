using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Data.DbSeeder;

public class DbSeeder
{
    public static async Task SeedAsync(NutritionContext context)
    {
        await context.Database.MigrateAsync();

        if (!context.Foods.Any())
        {
            var foods = new List<Food>
            {
                new() { Name = "Chicken Breast", Calories = 165, Protein = 31, Carbohydrates = 0, Fats = 3.6 },
                new() { Name = "Brown Rice", Calories = 216, Protein = 5, Carbohydrates = 45, Fats = 1.8 },
                new() { Name = "Broccoli", Calories = 55, Protein = 3.7, Carbohydrates = 11, Fats = 0.6 },
                new() { Name = "Almonds", Calories = 575, Protein = 21, Carbohydrates = 22, Fats = 49 },
                new() { Name = "Salmon", Calories = 208, Protein = 20, Carbohydrates = 0, Fats = 13 },
                new() { Name = "Sweet Potato", Calories = 86, Protein = 1.6, Carbohydrates = 20, Fats = 0.1 },
                new() { Name = "Eggs", Calories = 155, Protein = 13, Carbohydrates = 1.1, Fats = 11 },
                new() { Name = "Greek Yogurt", Calories = 59, Protein = 10, Carbohydrates = 3.6, Fats = 0.4 },
                new() { Name = "Quinoa", Calories = 120, Protein = 4.4, Carbohydrates = 21, Fats = 1.9 },
                new() { Name = "Spinach", Calories = 23, Protein = 2.9, Carbohydrates = 3.6, Fats = 0.4 }
            };

            context.Foods.AddRange(foods);
            await context.SaveChangesAsync();
        }

        if (!context.Meals.Any())
        {
            var meals = new List<Meal>
            {
                new() { Name = "Grilled Chicken with Brown Rice and Broccoli" },
                new() { Name = "Salmon with Sweet Potato and Spinach" },
                new() { Name = "Egg and Greek Yogurt Breakfast Bowl" },
                new() { Name = "Quinoa Salad with Almonds and Vegetables" }
            };

            context.Meals.AddRange(meals);
            await context.SaveChangesAsync();
        }

        if (!context.Diets.Any())
        {
            var diets = new List<Diet>
            {
                new() { Name = "High Protein Diet" },
                new() { Name = "Low Carb Diet" },
                new() { Name = "Balanced Diet" }
            };

            context.Diets.AddRange(diets);
            await context.SaveChangesAsync();
        }
    }
}