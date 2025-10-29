using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class AgenciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
