using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Diet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Calories => DietMeals.Sum(dm => dm.Meal.Calories);
    public int Protein => DietMeals.Sum(dm => dm.Meal.Protein);
    public int Carbohydrates => DietMeals.Sum(dm => dm.Meal.Carbohydrates);
    public int Fats => DietMeals.Sum(dm => dm.Meal.Fats);

    public ICollection<DietMeal> DietMeals { get; set; } = null!;
}