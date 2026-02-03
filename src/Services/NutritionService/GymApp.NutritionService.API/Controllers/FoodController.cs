using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
    {
        var foods = await service.GetAllFoodsAsync();

        return Ok(foods);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFoodById(Guid id)
    {
        var food = await service.GetFoodByIdAsync(id);
        if (food == null) return NotFound();

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult<Food>> CreateFood(Food food)
    {
        await service.AddFoodAsync(food);

        return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, food);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFood(Guid id, Food food)
    {
        if (id != food.Id) return BadRequest();

        try
        {
            await service.UpdateFoodAsync(food);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await service.GetFoodByIdAsync(id) == null)
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
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        var food = await service.GetFoodByIdAsync(id);
        if (food == null) return NotFound();

        await service.DeleteFoodAsync(id);

        return NoContent();
    }

    [HttpGet("calories")]
    public async Task<IEnumerable<double>> GetCalories()
    {
        return await service.GetCalories();
    }

    [HttpGet("names")]
    public async Task<IEnumerable<string?>> GetNames()
    {
        return await service.GetNames();
    }
}