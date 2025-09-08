using Microsoft.AspNetCore.Mvc;
using WebAPIShirt.Model;

namespace WebAPIShirt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt {ShirtId = 1, Brand = "MyBrand", Color = "Blue", Gender="Men", Price = 30, Size = 10},
            new Shirt {ShirtId = 2, Brand = "MyBrand", Color = "Black", Gender="Men", Price = 35, Size = 12},
            new Shirt {ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender="Women", Price = 28, Size = 8},
            new Shirt {ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender="Women", Price = 30, Size = 9}
        };

        [HttpGet]
        public string GetShirts()
        {
            return "List of shirts";
        }

        [HttpGet("{id}")]
        public IActionResult GetShirtById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var shirt = shirts.FirstOrDefault(x => x.ShirtId == id);
            if (shirt == null)
                return NotFound();

            return Ok(shirt);
        }

        [HttpPost]
        public string CreateShirt([FromBody] Shirt shirt)
        {
            return "Shirt created";
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
