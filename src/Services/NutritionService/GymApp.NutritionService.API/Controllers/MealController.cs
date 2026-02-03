using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MealController(IMealService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
    {
        var meals = await service.GetAllMealsAsync();

        return Ok(meals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Meal>> GetMealById(Guid id)
    {
        var meal = await service.GetMealByIdAsync(id);
        if (meal == null) return NotFound();

        return Ok(meal);
    }

    [HttpPost]
    public async Task<ActionResult<Meal>> CreateMeal(Meal meal)
    {
        await service.AddMealAsync(meal);

        return CreatedAtAction(nameof(GetMealById), new { id = meal.Id }, meal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeal(Guid id, Meal meal)
    {
        if (id != meal.Id) return BadRequest();

        try
        {
            await service.UpdateMealAsync(meal);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await service.GetMealByIdAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeal(Guid id)
    {
        var meal = await service.GetMealByIdAsync(id);
        if (meal == null) return NotFound();

        await service.DeleteMealAsync(id);

        return NoContent();
    }
}