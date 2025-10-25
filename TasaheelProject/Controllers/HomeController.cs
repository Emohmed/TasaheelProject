using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
