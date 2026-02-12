using System.ComponentModel.DataAnnotations;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Food : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbohydrates { get; set; }
    public double Fats { get; set; }

    public ICollection<MealFood> MealFoods { get; set; } = [];
}