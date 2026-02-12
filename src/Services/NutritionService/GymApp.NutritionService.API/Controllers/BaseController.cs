using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Specifications;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.API.Controllers;

public abstract class BaseController<T>(IService<T> service) : ControllerBase where T : BaseEntity
{
    [HttpGet]
    public virtual async Task<ActionResult<Pagination<T>>> GetAllAsync([FromQuery] PaginationParams parameters)
    {
        var spec = new PagingSpecification<T>(parameters);

        var source = await service.GetAllAsync(spec);
        var count = await service.CountAsync(spec);

        return Ok(new Pagination<T>(parameters.PageNumber, parameters.PageSize, count, source));
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetByIdAsync))]
    public virtual async Task<ActionResult<T>> GetByIdAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<T>> CreateAsync(T entity)
    {
        await service.CreateAsync(entity);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(Guid id, T entity)
    {
        if (id != entity.Id) return BadRequest();

        try
        {
            await service.UpdateAsync(entity);
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
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        await service.DeleteAsync(id);

        return NoContent();
    }
}