using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TasaheelProject.Data;
using TasaheelProject.Data.Viewmodel;
using TasaheelProject.Models;


namespace TasaheelProject.Controllers
{
    public class RequestsController : Controller
    {




        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // دالة لعرض تفاصيل طلب محدد بناءً على معرّفه (id)
        public async Task<IActionResult> RequestDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            // جلب الطلب المحدد مع تضمين (Eager Loading) لكافة العلاقات الضرورية
            var request = await _context.Requests
                .Include(r => r.CitizenUser)     // تضمين بيانات المواطن
                .Include(r => r.Service)         // تضمين بيانات الخدمة
                .Include(r => r.Branch)          // تضمين بيانات الفرع/الجهة الحكومية
                .Include(r => r.Attachments)     // تضمين المستندات المرفقة
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null)
            {
                return NotFound();
            }

            // يمكنك هنا إجراء تحقق إضافي للتأكد من أن المستخدم المسجل دخوله هو صاحب الطلب
            
            // مثال للتحقق من هوية صاحب الطلب:
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (request.CitizenUser.ApplicationUserId != currentUserId)
            {
                return Forbid(); // منع الوصول إذا لم يكن صاحب الطلب
            }
            

            // تمرير كائن الطلب إلى الواجهة الرسومية
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Create(Guid? serviceId, Guid? branchId)
        {
            // تهيئة نموذج العرض
            var model = new CreateRequestViewModel();

            // جلب الخدمات المتاحة
            model.ServicesList = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.ServiceId.ToString(),
                    Text = s.Name,
                    Selected = serviceId.HasValue && s.ServiceId == serviceId.Value
                })
                .ToListAsync();

            // جلب الفروع المتاحة (Agencies/Branches)
            model.BranchesList = await _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.BranchId.ToString(),
                    Text = b.Name,
                    Selected = branchId.HasValue && b.BranchId == branchId.Value
                })
                .ToListAsync();

            // إذا تم تمرير معرف الخدمة أو الفرع من صفحات سابقة، قم بتحديدها في النموذج
            if (serviceId.HasValue && serviceId.Value != Guid.Empty) model.ServiceId = serviceId.Value;
            if (branchId.HasValue && branchId.Value != Guid.Empty) model.BranchId = branchId.Value;

            return View(model);
        }

        // ===============================================
        // دالة إنشاء طلب جديد (POST) - لمعالجة النموذج وحفظ البيانات
        // ===============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRequestViewModel model)
        {
            // 1. التحقق من صحة النموذج
            if (!ModelState.IsValid)
            {
                // إعادة تعبئة قوائم الاختيار قبل إرجاع النموذج للـ View
                model.ServicesList = await _context.Services.Select(s => new SelectListItem { Value = s.ServiceId.ToString(), Text = s.Name }).ToListAsync();
                model.BranchesList = await _context.Branches.Select(b => new SelectListItem { Value = b.BranchId.ToString(), Text = b.Name }).ToListAsync();
                return View(model);
            }

            // 2. الحصول على CitizenId للمستخدم الحالي
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var citizenProfile = await _context.Citizens
                                           .FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);

            if (citizenProfile == null)
            {
                ModelState.AddModelError("", "يجب أن يكون لديك حساب مواطن لإرسال طلب. يرجى إكمال ملفك الشخصي.");
                // إعادة تعبئة قوائم الاختيار قبل إرجاع النموذج للـ View
                model.ServicesList = await _context.Services.Select(s => new SelectListItem { Value = s.ServiceId.ToString(), Text = s.Name }).ToListAsync();
                model.BranchesList = await _context.Branches.Select(b => new SelectListItem { Value = b.BranchId.ToString(), Text = b.Name }).ToListAsync();
                return View(model);
            }

            // 3. إنشاء كائن الطلب (Request)
            var newRequest = new Request
            {
                ServiceId = model.ServiceId,
                BranchId = model.BranchId,
                CitizenId = citizenProfile.CitizenId, // استخدام CitizenId الذي تم جلبه
                Status = RequestStatus.Pending, // تعيين الحالة المبدئية
                                                // يتم تعيين ReferenceNumber و CreatedAt تلقائياً في نموذج Request
            };

            // 4. حفظ الطلب في قاعدة البيانات
            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();

            // 5. إعادة التوجيه إلى صفحة تفاصيل الطلب الجديد
            return RedirectToAction("RequestDetails", new { id = newRequest.RequestId });
        }
    }

}
    

