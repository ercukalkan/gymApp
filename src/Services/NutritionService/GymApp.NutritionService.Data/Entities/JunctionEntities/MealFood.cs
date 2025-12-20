namespace GymApp.NutritionService.Data.Entities.JunctionEntities;

public class MealFood
{
    public int Id { get; set; }

    public int MealId { get; set; }
    public Meal Meal { get; set; } = null!;

    public int FoodId { get; set; }
    public Food Food { get; set; } = null!;
}