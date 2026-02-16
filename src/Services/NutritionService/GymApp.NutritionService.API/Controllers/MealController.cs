using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MealController(IMealService service) : BaseController<Meal>(service)
{
    [HttpPost]
    public override async Task<ActionResult<Meal>> CreateAsync(Meal entity)
    {
        if (entity == null)
            return BadRequest();

        var newMeal = await Service.CreateAsync(entity);
        var createdMeal = await Service.GetByIdAsync(newMeal.Id);

        if (createdMeal != null)
        {
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdMeal.Id },
                new
                {
                    createdMeal.Id,
                    createdMeal.Name,
                    createdMeal.Calories,
                    createdMeal.Carbohydrates,
                    createdMeal.Protein,
                    createdMeal.Fats
                });
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public override async Task<IActionResult> UpdateAsync(Guid id, Meal entity)
    {
        if (!await Service.IfExistsAsync(id)) return NotFound();

        await Service.UpdateAsync(entity);

        return NoContent();
    }
}