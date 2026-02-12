using System.ComponentModel.DataAnnotations;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Diet : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }
    public double Calories => DietMeals!.Sum(dm => dm.Meal!.Calories);
    public double Protein => DietMeals!.Sum(dm => dm.Meal!.Protein);
    public double Carbohydrates => DietMeals!.Sum(dm => dm.Meal!.Carbohydrates);
    public double Fats => DietMeals!.Sum(dm => dm.Meal!.Fats);

    public ICollection<DietMeal> DietMeals { get; set; } = [];
}