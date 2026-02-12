using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.NutritionService.Data.Entities;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}