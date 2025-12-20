namespace GymApp.NutritionService.Data.Entities.JunctionEntities;

public class DietMeal
{
    public int Id { get; set; }

    public int DietId { get; set; }
    public Diet Diet { get; set; } = null!;

    public int MealId { get; set; }
    public Meal Meal { get; set; } = null!;
}