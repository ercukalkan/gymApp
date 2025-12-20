using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Meal
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Calories => MealFoods.Sum(mf => mf.Food.Calories);
    public int Protein => MealFoods.Sum(mf => mf.Food.Protein);
    public int Carbohydrates => MealFoods.Sum(mf => mf.Food.Carbohydrates);
    public int Fats => MealFoods.Sum(mf => mf.Food.Fats);

    public ICollection<MealFood> MealFoods { get; set; } = null!;
    public ICollection<DietMeal> DietMeals { get; set; } = null!;
}