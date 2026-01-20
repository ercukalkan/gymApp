using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Caching;
using Microsoft.AspNetCore.Authorization;

namespace GymApp.NutritionService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController(NutritionContext context, IRedisService redisService) : ControllerBase
{
    [Authorize(Roles = "manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
    {
        var foods = await context.Foods.AsNoTracking().ToListAsync();

        foods.ForEach(async f => await redisService.SetAsync(f.Id.ToString(), f, TimeSpan.FromSeconds(20)));

        return Ok(await context.Foods.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFoodById(Guid id)
    {
        var food = await context.Foods.FindAsync(id);
        if (food == null) return NotFound();

        await redisService.SetAsync("Test Food", food, TimeSpan.FromSeconds(20));

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult<Food>> CreateFood(Food food)
    {
        context.Foods.Add(food);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, food);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFood(Guid id, Food food)
    {
        if (id != food.Id) return BadRequest();

        context.Entry(food).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Foods.Any(e => e.Id == id))
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
        var food = await context.Foods.FindAsync(id);
        if (food == null) return NotFound();

        context.Foods.Remove(food);
        await context.SaveChangesAsync();

        await redisService.RemoveAsync("Test Food");

        return NoContent();
    }
}