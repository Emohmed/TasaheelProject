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
                    return RedirectToAction(nameof(AgencyManagment));
                }
                return View(model);
            }

        [HttpGet]
        public IActionResult EditAgency()
        {
            return View();
        }

            [HttpPost]
             [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditAgency(Guid id)
            {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency == null) return NotFound();

            var viewModel = new AgencyViewModel
            {
                AgencyId = agency.AgencyId,
                Name = agency.Name,
                Code = agency.Code
            };
            _context.Update(viewModel);
            await _context.SaveChangesAsync();

            return View(viewModel);
            }

        //// POST: /Agencies/Edit
        //[HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> EditAgency(AgencyViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var agency = await _context.Agencies.FindAsync(model.AgencyId);
        //            if (agency == null) return NotFound();

        //            agency.Name = model.Name;
        //            agency.Code = model.Code;

        //            _context.Update(agency);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction(nameof(AdminHome));
        //        }
        //        return View(model);
        //    }

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

             [HttpGet]
            public IActionResult AddService(Guid agencyId) 
           {

            // تهيئة نموذج العرض وتعيين AgencyId الذي تم تمريره
            var model = new ServiceViewModel
            {
                AgencyId = agencyId
            };
            // يمكنك هنا جلب اسم الجهة وعرضه في الصفحة لزيادة الوضوح
            // var agencyName = _context.Agencies.Find(agencyId)?.Name;
            // ViewBag.AgencyName = agencyName;
            var agencyName = _context.Agencies.Find(agencyId)?.Name;
            ViewBag.AgencyName = agencyName;
            

            return View(model);
           }

        // 2. دالة POST لمعالجة إرسال النموذج وحفظ الخدمة
        [HttpPost]
        [ValidateAntiForgeryToken] // لحماية النموذج
        public async Task<IActionResult> AddService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // تحويل نموذج العرض (ViewModel) إلى نموذج البيانات (Entity Model)
                var service = new Service
                {
                    ServiceId = Guid.NewGuid(), // يتم إنشاؤه في فئة Service، لكن من الجيد التأكيد
                    Name = model.Name,
                    Description = model.Description,
                    Fee = model.Fee,
                    AgencyId = model.AgencyId // تخزين الـ AgencyId الذي تم تمريره بشكل مخفي
                };

                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                // إعادة التوجيه إلى قائمة الجهات أو صفحة خدمات الجهة
                // يمكنك تغيير هذه القيمة حسب المتحكم والدالة المناسبين
                return RedirectToAction("AgencyManagment");
            }

            // إذا كان هناك خطأ في التحقق، عد إلى نفس الصفحة مع البيانات المُدخلة ورسائل الأخطاء
            return View(model);
        }

        // داخل AdminController

// تأكد من استخدام Include لربط البيانات


             [HttpGet]
            public async Task<IActionResult> ListServices()
            {
              // جلب جميع الخدمات مع بيانات الجهة المرتبطة بها (Agency)
             var services = await _context.Services
            .Include(s => s.Agency) // لتضمين بيانات الجهة
            .Select(s => new ServiceDetailsViewModel
            {
                ServiceId = s.ServiceId,
                AgencyId = s.AgencyId,
                Name = s.Name,
                Description = s.Description,
                Fee = s.Fee,
                AgencyName = s.Agency.Name // جلب اسم الجهة
            })
            .ToListAsync();

             return View(services);
            }
        // داخل AdminController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(Guid serviceid) // المتغير id هو ServiceId
        {
            var service =_context.Services.Find(serviceid);

            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            

            // العودة إلى قائمة الخدمات
            return RedirectToAction(nameof(ListServices));
        }
        // داخل AdminController

        [HttpGet]
        public async Task<IActionResult> EditService(Guid serviceId) // ⬅️ يطابق asp-route-serviceid
        {
            var service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            // يمكنك جلب اسم الجهة وعرضه في الصفحة لزيادة الوضوح
            var agencyName = _context.Agencies.Find(service.AgencyId)?.Name;
            ViewBag.AgencyName = agencyName;
            ViewBag.ServiceId = service.ServiceId; // إضافي إذا لزم الأمر

            // تحويل نموذج البيانات (Entity) إلى نموذج العرض (ViewModel)
            var model = new ServiceViewModel
            {
                serviceid=serviceId,
                AgencyId = service.AgencyId,
                Name = service.Name,
                Description = service.Description,
                Fee = service.Fee
            };

            return View(model);
        }

        // داخل AdminController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. جلب الخدمة الحالية من قاعدة البيانات
                var serviceToUpdate = await _context.Services.FindAsync(model.serviceid);

                if (serviceToUpdate == null)
                {
                    return NotFound();
                }

                // 2. تحديث الخصائص بناءً على النموذج المُرسل
                serviceToUpdate.Name = model.Name;
                serviceToUpdate.Description = model.Description;
                serviceToUpdate.Fee = model.Fee;
                // لا نحتاج لتحديث AgencyId عادةً إلا إذا كنت تسمح بنقل الخدمات بين الجهات

                try
                {
                    _context.Update(serviceToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // التعامل مع أخطاء التزامن إذا كانت الخدمة قد حُذفت أو عُدلت من مكان آخر
                    if (!ServiceExists(model.serviceid))
                    {
                        return NotFound();
                    }
                    throw;
                }

                // 3. إعادة التوجيه بعد النجاح
                // يمكنك توجيهه إلى قائمة الخدمات الخاصة بهذه الجهة أو القائمة العامة
                return RedirectToAction(nameof(ListServices));
            }

            // إذا فشل التحقق، أعد النموذج إلى صفحة التعديل
            // يمكنك إعادة تعيين ViewBag.AgencyName هنا أيضًا إذا كنت تستخدمه في الـ View
            return View(model);
        }

        // دالة مساعدة للتحقق من وجود الخدمة
        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
    

  
