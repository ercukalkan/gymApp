using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using GymApp.NutritionService.Core.Specifications.FoodSpecifications;
using GymApp.NutritionService.Core.Specifications;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<Food>>> GetFoods([FromQuery] FoodSpecificationParameters parameters)
    {
        var spec = new FoodSortingSpecification(parameters);

        return await GetAllAsync<Food>(service, spec, parameters.PageNumber, parameters.PageSize);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFoodById(Guid id)
    {
        var food = await service.GetByIdAsync(id);
        if (food == null) return NotFound();

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult<Food>> CreateFood(Food food)
    {
        await service.CreateAsync(food);

        return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, food);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFood(Guid id, Food food)
    {
        if (id != food.Id) return BadRequest();

        try
        {
            await service.UpdateAsync(food);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await service.GetByIdAsync(id) == null)
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
        var food = await service.GetByIdAsync(id);
        if (food == null) return NotFound();

        await service.DeleteAsync(id);

        return NoContent();
    }
}