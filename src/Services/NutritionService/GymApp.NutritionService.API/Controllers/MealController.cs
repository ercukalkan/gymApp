using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using GymApp.NutritionService.Core.Specifications.MealSpecifications;
using GymApp.NutritionService.Core.Specifications;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MealController(IMealService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<Meal>>> GetMeals([FromQuery] MealSpecificationParameters parameters)
    {
        var spec = new MealSortingSpecification(parameters);

        return await GetAllAsync<Meal>(service, spec, parameters.PageNumber, parameters.PageSize);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Meal>> GetMealById(Guid id)
    {
        var meal = await service.GetByIdAsync(id);
        if (meal == null) return NotFound();

        return Ok(meal);
    }

    [HttpPost]
    public async Task<ActionResult<Meal>> CreateMeal(Meal meal)
    {
        await service.CreateAsync(meal);

        return CreatedAtAction(nameof(GetMealById), new { id = meal.Id }, meal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeal(Guid id, Meal meal)
    {
        if (id != meal.Id) return BadRequest();

        try
        {
            await service.UpdateAsync(meal);
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
    public async Task<IActionResult> DeleteMeal(Guid id)
    {
        var meal = await service.GetByIdAsync(id);
        if (meal == null) return NotFound();

        await service.DeleteAsync(id);

        return NoContent();
    }
}