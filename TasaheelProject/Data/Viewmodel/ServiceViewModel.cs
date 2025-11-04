using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Data.Viewmodel
{
    public class ServiceViewModel
    {

        // حقل مخفي لتخزين AgencyId الذي يأتي من رابط الجهة
        public Guid AgencyId { get; set; }
        public Guid serviceid { get; set; }= Guid.Empty;

        [Required(ErrorMessage = "الرجاء إدخال اسم الخدمة.")]
        [StringLength(50, ErrorMessage = "يجب ألا يتجاوز طول اسم الخدمة 50 حرفًا.")]
        [Display(Name = "اسم الخدمة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال وصف الخدمة.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز طول وصف الخدمة 100 حرفًا.")]
        [Display(Name = "وصف الخدمة")]
        public string Description { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال رسوم الخدمة.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب أن تكون الرسوم قيمة موجبة.")]
        [Display(Name = "الرسوم")]
        public decimal Fee { get; set; }
    }
    
}
