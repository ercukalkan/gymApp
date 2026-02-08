using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit.Initializers;
using GymApp.Shared.Pagination;
using System.Linq.Expressions;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{
    public async Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, string? sort, Pagination pagination)
    {
        var query = Context.Foods.Select(f => f.Calories);

        if (minimum.HasValue)
            query = query.Where(c => c >= minimum);

        if (maximum.HasValue)
            query = query.Where(c => c <= maximum);

        if (sort == "asc")
            query = query.OrderBy(c => c);
        else if (sort == "desc")
            query = query.OrderByDescending(c => c);

        query = query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<string?>> GetNames(string? sort, Pagination pagination)
    {
        var query = Context.Foods.Select(f => f.Name);

        if (sort == "asc")
            query = query.OrderBy(f => f);
        else if (sort == "desc")
            query = query.OrderByDescending(f => f);

        query = query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<string?>> GetNamesStartsWith(Expression<Func<Food, bool>> expression)
    {
        return await Context.Foods.Where(expression).Select(i => i.Name).ToListAsync();
    }
}