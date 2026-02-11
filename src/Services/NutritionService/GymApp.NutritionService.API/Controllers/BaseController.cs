using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using GymApp.Shared.Specification;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.NutritionService.API.Controllers;

public class BaseController : ControllerBase
{
    protected async Task<ActionResult<Pagination<T>>> GetAllAsync<T>(IService<T> service, ISpecification<T> spec, int PageNumber, int PageSize) where T : class
    {
        var source = await service.GetAllAsync(spec);
        var count = await service.CountAsync(spec);

        return Ok(new Pagination<T>(PageNumber, PageSize, count, source));
    }
}