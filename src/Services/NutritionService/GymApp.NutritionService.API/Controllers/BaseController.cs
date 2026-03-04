using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Specifications;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.API.Controllers;

public abstract class BaseController<T>(IService<T> service) : ControllerBase where T : BaseEntity
{
    protected readonly IService<T> Service = service;

    [HttpGet("generic")]
    public async Task<ActionResult<Pagination<T>>> GetAllAsyncGeneric([FromQuery] PaginationParams parameters)
    {
        var spec = new PagingSpecification<T>(parameters);

        var source = await Service.GetAllAsyncGeneric(spec);
        var count = await Service.CountAsync(spec);

        return Ok(new Pagination<T>(parameters.PageNumber, parameters.PageSize, count, source));
    }

    [HttpGet("generic/{id}")]
    [ActionName(nameof(GetByIdAsyncGeneric))]
    public async Task<ActionResult<T>> GetByIdAsyncGeneric(Guid id)
    {
        var entity = await Service.GetByIdAsyncGeneric(id);
        if (entity == null) return NotFound();

        return Ok(entity);
    }

    [HttpPost("generic")]
    public async Task<ActionResult<T>> CreateAsyncGeneric(T entity)
    {
        await Service.CreateAsyncGeneric(entity);

        return CreatedAtAction(nameof(GetByIdAsyncGeneric), new { id = entity.Id }, entity);
    }

    [HttpPut("generic/{id}")]
    public async Task<IActionResult> UpdateAsyncGeneric(Guid id, T entity)
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