using System.ComponentModel.DataAnnotations;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Meal : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }
    public double Calories => MealFoods!.Sum(mf => mf.Food!.Calories);
    public double Protein => MealFoods!.Sum(mf => mf.Food!.Protein);
    public double Carbohydrates => MealFoods!.Sum(mf => mf.Food!.Carbohydrates);
    public double Fats => MealFoods!.Sum(mf => mf.Food!.Fats);

    public ICollection<MealFood> MealFoods { get; set; } = [];
    public ICollection<DietMeal> DietMeals { get; set; } = [];
}