using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.NutritionService.Data.Entities.JunctionEntities;

public class DietMeal
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid DietId { get; set; }
    public Diet? Diet { get; set; } = null!;

    public Guid MealId { get; set; }
    public Meal? Meal { get; set; } = null!;
}