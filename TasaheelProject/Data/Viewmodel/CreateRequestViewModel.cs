using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Data.Viewmodel
{
    public class CreateRequestViewModel
    {

        // 1. المفتاح الخارجي للجهة الحكومية (الفرع)
        [Required(ErrorMessage = "يرجى اختيار الجهة الحكومية.")]
        [Display(Name = "الجهة الحكومية")]
        public Guid BranchId { get; set; }

        // 2. المفتاح الخارجي للخدمة المطلوبة
        [Required(ErrorMessage = "يرجى اختيار الخدمة.")]
        [Display(Name = "الخدمة المطلوبة")]
        public Guid ServiceId { get; set; }

        // 3. ملاحظات إضافية من المستخدم (اختياري)
        [Display(Name = "ملاحظات إضافية")]
        public string? UserNotes { get; set; }

        // 4. قائمة الجهات الحكومية لملء الـ Dropdown List (يتم تعبئتها في GET)
        public List<SelectListItem> BranchesList { get; set; } = new List<SelectListItem>();

        // 5. قائمة الخدمات لملء الـ Dropdown List (يتم تعبئتها في GET)
        public List<SelectListItem> ServicesList { get; set; } = new List<SelectListItem>();

    }
}
