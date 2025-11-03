using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasaheelProject.Data;
using TasaheelProject.Data.Viewmodel;
using TasaheelProject.Models;

namespace TasaheelProject.Controllers
{
    public class AdminController : Controller
    {

     
            private readonly ApplicationDbContext _context;

            public AdminController(ApplicationDbContext context)
            {
                _context = context;
            }

        // GET: /Agencies

        public IActionResult AdminHome() 
        {
            return View();
        
        }
            public async Task<IActionResult> AgencyManagment()
            {
                var agencies = await _context.Agencies
                    .Select(a => new AgencyViewModel
                    {
                        AgencyId = a.AgencyId,
                        Name = a.Name,
                        Code = a.Code
                    }).ToListAsync();

                return View(agencies);
            }

        // GET: /Agencies/Create
        public IActionResult CreateAgencies()
        {
            return View();
        }

        // POST: /Agencies/Create
        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateAgencies(AgencyViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var agency = new Agency
                    {
                        Name = model.Name,
                        Code = model.Code
                    };

                    _context.Add(agency);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(AdminHome));
                }
                return View(model);
            }

            // GET: /Agencies/Edit/{id}
            //public async Task<IActionResult> EditAgency(Guid id)
            //{
            //    var agency = await _context.Agencies.FindAsync(id);
            //    if (agency == null) return NotFound();

            //    var viewModel = new AgencyViewModel
            //    {
            //        AgencyId = agency.AgencyId,
            //        Name = agency.Name,
            //        Code = agency.Code
            //    };

            //    return View(viewModel);
            //}

            // POST: /Agencies/Edit
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditAgency(AgencyViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var agency = await _context.Agencies.FindAsync(model.AgencyId);
                    if (agency == null) return NotFound();

                    agency.Name = model.Name;
                    agency.Code = model.Code;

                    _context.Update(agency);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(AdminHome));
                }
                return View(model);
            }

            // GET: /Agencies/Delete/{id}
            public async Task<IActionResult> DeleteAgencies(Guid id)
            {
                var agency = await _context.Agencies.FindAsync(id);
                if (agency == null) return NotFound();

                return View(new AgencyViewModel
                {
                    AgencyId = agency.AgencyId,
                    Name = agency.Name,
                    Code = agency.Code
                });
            }

            // POST: /Agencies/DeleteConfirmed/{id}
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                var agency = await _context.Agencies.FindAsync(id);
                if (agency == null) return NotFound();

                _context.Agencies.Remove(agency);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(AdminHome));
            }
        }
    }

  
