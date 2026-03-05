using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Data.DTOs;

public class MealResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Calories { get; set; }
    public double Carbohydrates { get; set; }
    public double Protein { get; set; }
    public double Fats { get; set; }
    public IEnumerable<NameDTO>? MealFoods { get; set; }

    public static MealResponseDTO FromEntity(Meal entity)
    {
        return new MealResponseDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Calories = entity.Calories,
            Carbohydrates = entity.Carbohydrates,
            Protein = entity.Protein,
            Fats = entity.Fats,
            MealFoods = entity.MealFoods.Select(mf => new NameDTO { Name = mf.Food.Name })
        };
    }
}