using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasaheelProject.Data;
using TasaheelProject.Models;

namespace TasaheelProject.Controllers
{
    public class BranchesController : Controller
    {


        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ManageBranch()
        {
            return View();
        }


    }
}
