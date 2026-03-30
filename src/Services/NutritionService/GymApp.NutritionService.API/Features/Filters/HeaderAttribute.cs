using Microsoft.AspNetCore.Mvc.Filters;

namespace GymApp.NutritionService.API.Features.Filters;

public class HeaderAttribute : ActionFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public HeaderAttribute(string name, string value)
    {
        (_name, _value) = (name, value);
    }

    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Response.Headers.Append(_name, _value);

        await next();
    }
}