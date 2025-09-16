using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIShirt.Data;

namespace WebAPIShirt.Controllers.Filters.ExeptionFilters
{
    public class Shirt_HandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ApplicationDBContext db;

        public Shirt_HandleUpdateExceptionFilterAttribute(ApplicationDBContext db)
        {
            this.db = db;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out int shirtId))
            {
                if (db.Shirts.FirstOrDefault(x => x.ShirtId == shirtId) == null)
                {
                    context.ModelState.AddModelError("shirtId", "Shirt doesn't exist anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }

                context.ExceptionHandled = true;
                context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult($"An error occurred while updating the shirt with ID {shirtId}. Please try again later.")
                {
                    StatusCode = 500
                };
            }
        }

    }
}
