using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class CitizinController : Controller
    {
        public IActionResult CitizinHome()
        {
            return View();
        }
    }
}
