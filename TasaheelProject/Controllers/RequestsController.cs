using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class RequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
