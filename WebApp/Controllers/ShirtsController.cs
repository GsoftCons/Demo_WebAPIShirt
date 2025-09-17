using Microsoft.AspNetCore.Mvc;
using WebApp.Model.Repositories;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        public IActionResult Index()
        {
            return View(ShirtRepository.GetAllShirts());
        }
    }
}
