using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using System.Linq.Expressions;
using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Core.Specifications.FoodSpecifications;
using GymApp.NutritionService.Core.Specifications;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service, NutritionContext _context) : ControllerBase
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
    public async Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, string? sort, Pagination pagination)
    {
        return await service.GetCalories(minimum, maximum, sort, pagination);
    }

    [HttpGet("names")]
    public async Task<IEnumerable<string?>> GetNames(string? sort, [FromBody] Pagination pagination)
    {
        return await service.GetNames(sort, pagination);
    }

    [HttpGet("namesStartWith/{character}")]
    public async Task<IEnumerable<string?>> GetNames(string character)
    {
        Expression<Func<Food, bool>> expression = f => f.Name!.StartsWith(character);

        return await service.GetNamesStartsWith(expression);
    }

    [HttpGet("dummy")]
    public async Task<IEnumerable<object>> GetDummy(double value)
    {
        var spec = new CalorieGreaterThanSpecification(value);

        return await _context.Foods
            .Where(spec.Criteria!)
            .Select(f => new { f.Name, f.Calories })
            .ToListAsync();
    }

    [HttpGet("dummy2")]
    public async Task<IEnumerable<Food>> GetDummy2(string? sort, int? pageNumber, int? pageSize)
    {
        var spec = new DummyPagingSpecification(sort, pageNumber, pageSize);

        return await SpecificationEvaluator<Food>.GetQuery(_context.Foods, spec).ToListAsync();
    }
}