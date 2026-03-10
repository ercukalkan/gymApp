using System.ComponentModel.DataAnnotations;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Data.Entities;

public class Meal : BaseEntity
{
    [MaxLength(50)]
    [Required(ErrorMessage = "Name is required for Meal.")]
    public required string Name { get; set; }
    public double Calories { get; private set; }
    public double Protein { get; private set; }
    public double Carbohydrates { get; private set; }
    public double Fats { get; private set; }

    public ICollection<MealFood> MealFoods { get; set; } = [];
    public ICollection<DietMeal> DietMeals { get; set; } = [];

    /// <summary>
    /// Recalculates the total nutritional values for the meal based on its constituent foods and their quantities.
    /// </summary>
    /// <remarks>
    /// This method iterates through all associated meal foods, multiplies each food's nutritional content by its quantity,
    /// and updates the meal's aggregate nutritional properties. Foods with null references are automatically excluded from calculations.
    /// </remarks>
    public void RecalculateNutrients()
    {
        Calories = MealFoods.Where(mf => mf.Food != null).Sum(mf => mf.Food.Calories * mf.Quantity);
        Carbohydrates = MealFoods.Where(mf => mf.Food != null).Sum(mf => mf.Food.Carbohydrates * mf.Quantity);
        Protein = MealFoods.Where(mf => mf.Food != null).Sum(mf => mf.Food.Protein * mf.Quantity);
        Fats = MealFoods.Where(mf => mf.Food != null).Sum(mf => mf.Food.Fats * mf.Quantity);
    }
}