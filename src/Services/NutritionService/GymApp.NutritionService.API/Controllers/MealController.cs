using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using GymApp.NutritionService.Core.Specifications.MealSpecifications;
using GymApp.NutritionService.Core.Specifications;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MealController(IMealService service) : BaseController<Meal, IMealService>(service)
{
    [HttpGet]
    public async Task<ActionResult<Pagination<MealResponseDTO>>> GetAllAsync([FromQuery] MealSpecificationParameters parameters)
    {
        var spec = new MealSortingSpecification(parameters);

        var pagination = new Pagination<MealResponseDTO>(
            parameters.PageNumber,
            parameters.PageSize,
            await Service.CountAsync(spec),
            await Service.GetAllAsync(spec)
        );

        return Ok(pagination);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<ActionResult<MealResponseDTO>> GetByIdAsync(Guid id)
    {
        var entity = await Service.GetByIdAsync(id);

        if (entity == null)
            return NotFound("Entity not found.");

        return entity;
    }

    [HttpPost]
    public async Task<ActionResult<MealResponseDTO>> CreateAsync(MealRequestDTO dto)
    {
        if (dto == null)
            return NotFound("Object is null.");

        var createdMeal = await Service.CreateAsync(dto);

        if (createdMeal != null)
        {
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdMeal.Id },
                createdMeal
            );
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, MealRequestDTO dto)
    {
        if (!await Service.IfExistsAsync(id)) return NotFound();

        await Service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!await Service.IfExistsAsync(id)) return NotFound();

        await Service.DeleteAsync(id);

        return NoContent();
    }
}