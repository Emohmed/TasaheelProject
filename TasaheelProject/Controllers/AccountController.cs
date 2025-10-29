using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class AccountController : Controller
    {
        



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

