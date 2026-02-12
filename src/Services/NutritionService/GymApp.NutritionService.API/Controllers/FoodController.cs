using GymApp.NutritionService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FoodController(IFoodService service) : BaseController<Food>(service)
{

}