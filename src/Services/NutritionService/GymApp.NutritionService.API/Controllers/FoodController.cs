using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service) : BaseController<Food, IFoodService>(service)
{
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, FoodDTO dto)
    {
        if (!await Service.IfExistsAsync(id)) return NotFound();

        await Service.UpdateAsync(id, dto);

        return NoContent();
    }
}