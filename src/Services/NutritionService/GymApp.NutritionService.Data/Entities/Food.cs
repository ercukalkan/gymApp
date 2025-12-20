using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Food
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Carbohydrates { get; set; }
    public int Fats { get; set; }

    public ICollection<MealFood> MealFoods { get; set; } = null!;
}