using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Specifications;
using GymApp.NutritionService.Core.Specifications.DietSpecifications;
using GymApp.Shared.Pagination;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DietController(IDietService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<Diet>>> GetDiets([FromQuery] DietSpecificationParameters parameters)
    {
        var spec = new DietSortingSpecification(parameters);

        return await GetAllAsync<Diet>(service, spec, parameters.PageNumber, parameters.PageSize);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Diet>> GetDietById(Guid id)
    {
        var diet = await service.GetByIdAsync(id);
        if (diet == null) return NotFound();

        return Ok(diet);
    }

    [HttpPost]
    public async Task<ActionResult<Diet>> CreateDiet(Diet diet)
    {
        await service.CreateAsync(diet);

        return CreatedAtAction(nameof(GetDietById), new { id = diet.Id }, diet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiet(Guid id, Diet diet)
    {
        if (id != diet.Id) return BadRequest();

        try
        {
            await service.UpdateAsync(diet);
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
    public async Task<IActionResult> DeleteDiet(Guid id)
    {
        var diet = await service.GetByIdAsync(id);
        if (diet == null) return NotFound();

        await service.DeleteAsync(id);

        return NoContent();
    }
}