using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.NutritionService.Data.Entities.JunctionEntities;

public class MealFood
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid MealId { get; set; }
    public Meal Meal { get; set; } = null!;

    public Guid FoodId { get; set; }
    public Food Food { get; set; } = null!;
}