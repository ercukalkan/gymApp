using System.ComponentModel.DataAnnotations;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Meal : BaseEntity
{
    [MaxLength(50)]
    [Required(ErrorMessage = "Name is required for Meal.")]
    public required string Name { get; set; }
    public double Calories => MealFoods?.Where(mf => mf.Food != null).Sum(mf => mf.Food!.Calories) ?? 0;
    public double Protein => MealFoods?.Where(mf => mf.Food != null).Sum(mf => mf.Food!.Protein) ?? 0;
    public double Carbohydrates => MealFoods?.Where(mf => mf.Food != null).Sum(mf => mf.Food!.Carbohydrates) ?? 0;
    public double Fats => MealFoods?.Where(mf => mf.Food != null).Sum(mf => mf.Food!.Fats) ?? 0;

    public ICollection<MealFood> MealFoods { get; set; } = [];
    public ICollection<DietMeal> DietMeals { get; set; } = [];
}