using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Specifications;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.API.Controllers;

public abstract class BaseController<TEntity, TService>(TService service) : ControllerBase
where TEntity : BaseEntity
where TService : IService<TEntity>
{
    protected readonly TService Service = service;

    [HttpGet("generic")]
    public async Task<ActionResult<Pagination<TEntity>>> GetAllAsyncGeneric([FromQuery] PaginationParams parameters)
    {
        var spec = new PagingSpecification<TEntity>(parameters);

        var source = await Service.GetAllAsyncGeneric(spec);
        var count = await Service.CountAsync(spec);

        return Ok(new Pagination<TEntity>(parameters.PageNumber, parameters.PageSize, count, source));
    }

    [HttpGet("generic/{id}")]
    [ActionName(nameof(GetByIdAsyncGeneric))]
    public async Task<ActionResult<TEntity>> GetByIdAsyncGeneric(Guid id)
    {
        var entity = await Service.GetByIdAsyncGeneric(id);
        if (entity == null) return NotFound();

        return Ok(entity);
    }

    [HttpPost("generic")]
    public async Task<ActionResult<TEntity>> CreateAsyncGeneric(TEntity entity)
    {
        await Service.CreateAsyncGeneric(entity);

        return CreatedAtAction(nameof(GetByIdAsyncGeneric), new { id = entity.Id }, entity);
    }

    [HttpPut("generic/{id}")]
    public async Task<IActionResult> UpdateAsyncGeneric(Guid id, TEntity entity)
    {
        if (id != entity.Id) return BadRequest();

        try
        {
            await Service.UpdateAsyncGeneric(entity);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await Service.GetByIdAsyncGeneric(id) == null)
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

    [HttpDelete("generic/{id}")]
    public async Task<IActionResult> DeleteAsyncGeneric(Guid id)
    {
        var entity = await Service.GetByIdAsyncGeneric(id);
        if (entity == null) return NotFound();

        await Service.DeleteAsyncGeneric(id);

        return NoContent();
    }
}