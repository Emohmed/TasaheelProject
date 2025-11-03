using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TasaheelProject.Data;
using TasaheelProject.Data.Viewmodel;
using TasaheelProject.Models;

namespace TasaheelProject.Controllers
{

    public class CitizinController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context; 

        public CitizinController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult CitizenHome()
        {
            return View();
        }
        //public IActionResult NewTransaction()
        //{
        //    return View();
        //}

        //public IActionResult MyTransaction()
        //{
        //    return View();
     
        //}

        public async Task<IActionResult> MyTransaction()
        {
            // 1. الحصول على هوية المستخدم المسجل دخوله (ApplicationUserId)
            // User.FindFirstValue(ClaimTypes.NameIdentifier) هي الطريقة القياسية للحصول على الـ UserId
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // في حال لم يكن مسجلاً
            }

            // 2. استخدام EF Core لتحميل (Include) جميع العلاقات الضرورية
            // نبدأ من جدول CitizenProfile ثم نحمّل الطلبات (Requests)

            var citizenProfile = await _context.Citizens
                .Include(c => c.Requests) // تحميل قائمة الطلبات المرتبطة بالمواطن
                                          // .ThenInclude(r => r.Service) // يمكنك إضافة هذا لتحميل بيانات الخدمة لكل طلب
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId); // البحث باستخدام Identity UserId

            if (citizenProfile == null)
            {
                // التعامل مع حالة تسجيل المستخدم في Identity ولكن لا يوجد CitizenProfile
                return RedirectToAction("RegisterCitizen","Account");
            }

            // 3. استخراج قائمة الطلبات
            var requestsList = citizenProfile.Requests;

            // 4. تمرير البيانات إلى الواجهة الرسومية (View)
            return View(requestsList);
        }




        [HttpGet]
        public async Task<IActionResult> NewTransaction()
        {
            // جلب جميع كائنات الوكالات (Agencies) بالكامل لتتوافق مع ما تتوقعه الواجهة الرسومية (@model List<Agency>)
            var Agencies = await _context.Agencies
                .ToListAsync();

            return View(Agencies);
        }

        [HttpGet]
        // تستقبل branchId الذي هو AgencyId المرسل من الواجهة
        public async Task<IActionResult> ServicesByAgency(Guid agencyid)
        {
            // 1. جلب الخدمات المرتبطة بالـ branchId (AgencyId) المحدد
            // سنستخدم هنا نموذج Service مباشرة لعدم تعقيد الأمور بنموذج عرض مؤقت
            var services = await _context.Services
                
                .Where(s => s.AgencyId == agencyid)
                .Include(s => s.Agency) // جلب بيانات الجهة (Agency) لعرض اسمها
                .ToListAsync();

            // 2. جلب اسم الجهة لعرضه في العنوان (اختياري)
            // إذا لم نقم بـ Include أعلاه، كنا سنستخدم هذا
            var agencyName = await _context.Agencies
                .Where(a => a.AgencyId == agencyid)
                .Select(a => a.Name)
                .FirstOrDefaultAsync() ?? "خدمات غير محددة";

            ViewBag.AgencyName = agencyName;
            ViewBag.AgencyId = agencyid;

            // 3. تمرير قائمة الخدمات إلى الواجهة
            return View(services);
        }




    }
}
