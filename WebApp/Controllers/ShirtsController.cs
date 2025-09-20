using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ShirtsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
        }

        public IActionResult CreateShirt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                var respose = await webApiExecuter.InvokePost("shirts", shirt);
                if (respose != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(shirt);
        }

        public async Task<IActionResult> UpdateShirt(int id)
        {
            var shirt = await webApiExecuter.InvokeGet<Shirt>($"shirts/{id}");
            if (shirt != null)
            {
                return View(shirt);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                await webApiExecuter.InvokePut($"shirts/{shirt.ShirtId}", shirt);
                return RedirectToAction(nameof(Index));
            }

            return View(shirt);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShirt([FromForm] int id)
        {
            await webApiExecuter.InvokeDelete($"shirts/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
