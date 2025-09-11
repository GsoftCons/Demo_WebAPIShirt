using Microsoft.AspNetCore.Mvc;
using WebAPIShirt.Controllers.Filters;
using WebAPIShirt.Model;
using WebAPIShirt.Model.Repositories;

namespace WebAPIShirt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetAllShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilterAttribute]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            if (shirt == null) return BadRequest();

            var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            if (existingShirt != null) return BadRequest();

            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId },shirt);
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Shirt with ID: {id} updated";
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Shirt with ID: {id} deleted";
        }
    }
}
