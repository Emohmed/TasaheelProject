using Microsoft.AspNetCore.Mvc;

namespace TasaheelProject.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult EmployeeHome()
        {
            return View();
        }
    }
}
