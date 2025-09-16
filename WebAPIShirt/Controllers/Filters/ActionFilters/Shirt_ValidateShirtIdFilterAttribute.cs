using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIShirt.Data;
using WebAPIShirt.Model.Repositories;

namespace WebAPIShirt.Controllers.Filters.ActionFilters
{
    public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDBContext db;

        public Shirt_ValidateShirtIdFilterAttribute(ApplicationDBContext db)
        {
            this.db = db;
        }

        //Impedisce al dato di arrivare al controller se non è valido
        override public void OnActionExecuting(ActionExecutingContext context)
        { 

            if (context.ActionArguments.ContainsKey("id"))
            {
                var shirtId = context.ActionArguments["id"] as int?;
                if (shirtId.HasValue)
                {
                    if (shirtId <= 0)
                    {
                        context.ModelState.AddModelError("shirtId", "Shirt ID must be greater than zero");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);

                    }
                    else
                    {
                        var shirt = db.Shirts.Find(shirtId.Value);

                        if (shirt == null)
                        {
                            context.ModelState.AddModelError("ShirtId", "Shirt does not exist.");
                            var problemDetails = new ValidationProblemDetails(context.ModelState)
                            {
                                Status = StatusCodes.Status404NotFound
                            };
                            context.Result = new NotFoundObjectResult(problemDetails);
                        }
                        else
                        {
                            context.HttpContext.Items["shirt"] = shirt;
                        }
                    }

                }

            }
        }
    }
}
