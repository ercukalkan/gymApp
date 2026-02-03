using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit.Initializers;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{

}