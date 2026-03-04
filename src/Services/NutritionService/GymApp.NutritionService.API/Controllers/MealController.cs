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
public class MealController(IMealService service) : BaseController<Meal>(service)
{
    [HttpGet]
    public async Task<ActionResult<Pagination<MealDTO>>> GetAllAsync([FromQuery] MealSpecificationParameters parameters)
    {
        var spec = new MealSortingSpecification(parameters);

        var pagination = new Pagination<MealDTO>(
            parameters.PageNumber,
            parameters.PageSize,
            await service.CountAsync(spec),
            await service.GetAllAsync(spec)
        );

        return Ok(pagination);
    }
}