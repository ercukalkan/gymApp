namespace GymApp.NutritionService.Data.DTOs;

public class MealRequestDTO
{
    public string Name { get; set; } = null!;
    public ICollection<Guid> MealFoodIds { get; set; } = [];
}