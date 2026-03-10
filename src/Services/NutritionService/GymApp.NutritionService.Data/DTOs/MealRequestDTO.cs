namespace GymApp.NutritionService.Data.DTOs;

public class MealRequestDTO
{
    public string Name { get; set; } = null!;
    public ICollection<MealFoodRequestDTO> MealFoodDTOs { get; set; } = [];
}