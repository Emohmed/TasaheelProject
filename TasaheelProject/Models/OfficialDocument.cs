using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Models
{
    // المستندات الرسمية المخزنة في النظام (شهادة ميلاد، جواز، ... إلخ)
    public class OfficialDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; } // مثل: شهادة ميلاد

        [Required]
        [MaxLength(20)]
        public string DocumentNumber { get; set; } // رقم الوثيقة الرسمي

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }

        // المواطن المالك
        public Guid CitizenId { get; set; }
        public Citizen Owner { get; set; }

        // الجهة التي أصدرت الوثيقة
        public Guid AgencyId { get; set; }
        public Agency IssuedBy { get; set; }

        // مسار الملف المخزن
        public string FilePath { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

