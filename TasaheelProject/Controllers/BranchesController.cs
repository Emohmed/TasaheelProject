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

      

        // 🟢 GET: /Branches
        //public async Task<IActionResult> ManageBranches()
        //{
        //    var branches = await _context.Branches
        //        .Include(b => b.Agency)
        //        .ToListAsync();
        //    return View(branches);
        //}
        //public IActionResult CreateBranch()
        //{
        //    ViewBag.Agencies = _context.Agencies.ToList(); // لعرض الجهات في القائمة
        //    return View();
        //}

        //// 🟢 POST: /Branches/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateBranch(Branch branch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var b=new Branch
        //        {
        //            Name=branch.Name,
        //            City=branch.City,
        //            AgencyId=branch.AgencyId,
        //            Code=branch.Code,
        //            Address=branch.Address,
        //            IsActive=branch.IsActive
        //        };
        //         _context.Branches.Add(b);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(ManageBranches));
        //    }
        //    ViewBag.Agencies = _context.Agencies.ToList();
        //    return View(branch);
        //}

        // GET: /Branches/CreateBranch/{agencyId}
        // ✅ عرض صفحة إضافة فرع ضمن الجهة المختارة
        // ✅ عرض جميع الفروع الخاصة بجهة محددة
        public async Task<IActionResult> ManageBranches(Guid agencyId)
        {
            var agency = await _context.Agencies
                .Include(a => a.Branches)
                .FirstOrDefaultAsync(a => a.AgencyId == agencyId);

            if (agency == null) return NotFound();

            ViewBag.AgencyName = agency.Name;
            ViewBag.AgencyId = agency.AgencyId;

            return View(agency.Branches.ToList());
        }

        public IActionResult CreateBranch(Guid agencyId)
        {
            var agency = _context.Agencies.FirstOrDefault(a => a.AgencyId == agencyId);
            if (agency == null) return NotFound();

            ViewBag.AgencyName = agency.Name;
            ViewBag.AgencyId = agency.AgencyId;

            return View(new Branch { AgencyId = agencyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _context.Branches.Add(branch);
                await _context.SaveChangesAsync();

                // ✅ العودة إلى صفحة إدارة الفروع الخاصة بهذه الجهة
                return RedirectToAction("Manage", new { agencyId = branch.AgencyId });
            }

            ViewBag.AgencyName = _context.Agencies.Find(branch.AgencyId)?.Name;
            ViewBag.AgencyId = branch.AgencyId;
            return View(branch);
        }

        // 🟠 GET: /Branches/Edit/{id}
        public async Task<IActionResult> EditBranch(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            ViewBag.Agencies = _context.Agencies.ToList();
            return View(branch);
        }
        // 🟠 POST: /Branches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBranch(Guid id, Branch branch)
        {
            if (id != branch.BranchId) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Update(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageBranches));
            }

            ViewBag.Agencies = _context.Agencies.ToList();
            return View(branch);
        }

        //delete branch
        [HttpPost]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageBranches));
        }

        // 🔍 جلب فرع محدد
        public async Task<IActionResult> DetailsBranch(Guid id)
        {
            var branch = await _context.Branches
                .Include(b => b.Agency)
                .FirstOrDefaultAsync(b => b.BranchId == id);

            if (branch == null) return NotFound();
            return Json(branch); // أو return View(branch) لو أردت صفحة لاحقاً
        }
    }
}
