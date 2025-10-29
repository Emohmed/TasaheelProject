using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
