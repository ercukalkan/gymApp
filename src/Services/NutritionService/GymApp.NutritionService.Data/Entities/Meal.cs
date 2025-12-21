using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Meal
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Calories => MealFoods.Sum(mf => mf.Food.Calories);
    public double Protein => MealFoods.Sum(mf => mf.Food.Protein);
    public double Carbohydrates => MealFoods.Sum(mf => mf.Food.Carbohydrates);
    public double Fats => MealFoods.Sum(mf => mf.Food.Fats);

    public ICollection<MealFood> MealFoods { get; set; } = null!;
    public ICollection<DietMeal> DietMeals { get; set; } = null!;
}