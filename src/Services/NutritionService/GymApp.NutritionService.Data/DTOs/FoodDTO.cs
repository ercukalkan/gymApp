namespace GymApp.NutritionService.Data.DTOs;

public class FoodDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Calories { get; set; }
    public double Carbohydrates { get; set; }
    public double Protein { get; set; }
    public double Fats { get; set; }
}