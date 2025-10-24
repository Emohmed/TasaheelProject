using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
