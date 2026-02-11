using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using GymApp.Shared.Specification;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.NutritionService.API.Controllers;

public class BaseController<T> : ControllerBase where T : class
{
    public async Task<Pagination<T>> GetAllAsync(IService<T> service, ISpecification<T> spec, int PageNumber, int PageSize)
    {
        var source = await service.GetAllAsync(spec);
        var count = await service.CountAsync(spec);

        return new Pagination<T>(PageNumber, PageSize, count, source);
    }
}