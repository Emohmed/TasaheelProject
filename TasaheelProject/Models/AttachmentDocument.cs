using System.ComponentModel.DataAnnotations;
using TasaheelProject.Models;

namespace TasaheelProject.Models {

//  الملفات التي يرفعها المواطن

    public class AttachmentDocument
    {
        [Key]
        public Guid AttachmentDocumentId { get; set; } = Guid.NewGuid();
  
        
        [Required, MaxLength(50)]
        //  اسم الملف مع الامتداد
        public string FileName { get; set; }

       
        //  نوع الملف (امتداد)
        [Required,MaxLength(50)]
        public string FileType { get; set; }

        //  حجم الملف بالبايت


        public long Size { get; set; }

        [Required]
        //  مسار الملف على الخادم
        public string FilePath { get; set; }

      //  تاريخ رفع الملف
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    //  الطلب المرتبط به الملف
        public Guid RequestId { get; set; }
        public Request Request { get; set; }

    //  المواطن الذي رفع الملف
        public Guid CitizenId { get; set; }
        public Citizen Citizen { get; set; }
      
    }
}