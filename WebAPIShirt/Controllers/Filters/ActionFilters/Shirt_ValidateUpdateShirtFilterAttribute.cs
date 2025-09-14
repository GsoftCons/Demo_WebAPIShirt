using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIShirt.Model;

namespace WebAPIShirt.Controllers.Filters.ActionFilters
{
    public class Shirt_ValidateUpdateShirtFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var Id = context.ActionArguments["id"] as int?;
            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (Id.HasValue && shirt != null && Id != shirt.ShirtId)
            {
                context.ModelState.AddModelError("ShirtId", "ShirtId is not the same as id");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }


        }
    }
}
