using Microsoft.AspNetCore.Mvc;
using WebAPIShirt.Controllers.Filters.ActionFilters;
using WebAPIShirt.Controllers.Filters.ExeptionFilters;
using WebAPIShirt.Data;
using WebAPIShirt.Model;
using WebAPIShirt.Model.Repositories;

namespace WebAPIShirt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private readonly ApplicationDBContext db;

        public ShirtsController(ApplicationDBContext db)
        {
            this.db = db;
            
        }

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirts.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            this.db.Shirts.Add(shirt);
            this.db.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            if (id != shirt.ShirtId)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }
    }
}
