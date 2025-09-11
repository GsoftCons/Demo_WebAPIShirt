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
        public string GetShirts()
        {
            return "List of shirts";
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilterAttribute]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
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
