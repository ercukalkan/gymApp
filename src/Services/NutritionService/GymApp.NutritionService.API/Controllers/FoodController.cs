using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using System.Linq.Expressions;
using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Core.Specifications.FoodSpecifications;
using GymApp.Shared.Specification;
using GymApp.NutritionService.Core.Specifications;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<Food>>> GetFoods([FromQuery] FoodSpecificationParameters parameters)
    {
        var spec = new DummyPagingSpecification(parameters);

        return await GetAllAsync<Food>(service, spec, parameters.PageNumber, parameters.PageSize);
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
    public async Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, PaginationParams paginationParams)
    {
        return await service.GetCalories(minimum, maximum, paginationParams);
    }

    [HttpGet("names")]
    public async Task<IEnumerable<string?>> GetNames([FromBody] PaginationParams paginationParams)
    {
        return await service.GetNames(paginationParams);
    }

    [HttpGet("namesStartWith/{character}")]
    public async Task<IEnumerable<string?>> GetNames(string character)
    {
        Expression<Func<Food, bool>> expression = f => f.Name!.StartsWith(character);

        return await service.GetNamesStartsWith(expression);
    }
}