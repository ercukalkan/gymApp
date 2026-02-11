using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DietController(IDietService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Diet>> GetDietById(Guid id)
    {
        var diet = await service.GetDietByIdAsync(id);
        if (diet == null) return NotFound();

        return Ok(diet);
    }

    [HttpPost]
    public async Task<ActionResult<Diet>> CreateDiet(Diet diet)
    {
        await service.AddDietAsync(diet);

        return CreatedAtAction(nameof(GetDietById), new { id = diet.Id }, diet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiet(Guid id, Diet diet)
    {
        if (id != diet.Id) return BadRequest();

        try
        {
            await service.UpdateDietAsync(diet);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await service.GetDietByIdAsync(id) == null)
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
        var diet = await service.GetDietByIdAsync(id);
        if (diet == null) return NotFound();

        await service.DeleteDietAsync(id);

        return NoContent();
    }
}