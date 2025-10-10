using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Models
{
    // الخدمة المقدمة (مثل:إصدار جواز سفر)
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public decimal Fee { get; set; }

        // العلاقة مع الجهة
        public Guid AgencyId { get; set; }
        public Agency Agency { get; set; }

        // العلاقة مع الطلبات
        public List<Request> Requests { get; set; } = new();
    }
}