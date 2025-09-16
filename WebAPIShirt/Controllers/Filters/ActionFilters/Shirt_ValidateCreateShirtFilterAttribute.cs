using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using System.Reflection;
using WebAPIShirt.Data;
using WebAPIShirt.Model;
using WebAPIShirt.Model.Repositories;

namespace WebAPIShirt.Controllers.Filters.ActionFilters
{
    public class Shirt_ValidateCreateShirtFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDBContext _db;
        public Shirt_ValidateCreateShirtFilterAttribute(ApplicationDBContext db)
        {
            this._db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null)
            {
                context.ModelState.AddModelError("shirt", "Shirt object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingShirt =  _db.Shirts.FirstOrDefault(x => 
                                            !string.IsNullOrWhiteSpace(shirt.Brand) && 
                                            !string.IsNullOrWhiteSpace(x.Brand) && 
                                            x.Brand.ToLower() == shirt.Brand.ToLower() &&
                                            !string.IsNullOrWhiteSpace(shirt.Gender) && 
                                            !string.IsNullOrWhiteSpace(x.Gender) && 
                                            x.Gender.ToLower() == shirt.Gender.ToLower() &&
                                            !string.IsNullOrWhiteSpace(shirt.Color) && 
                                            !string.IsNullOrWhiteSpace(x.Color) && 
                                            x.Color.ToLower() == shirt.Color.ToLower() &&
                                            shirt.Size.HasValue && 
                                            x.Size.HasValue && 
                                            shirt.Size.Value == x.Size.Value);

                
                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("shirt", "Shirt with the same properties already exists.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}
