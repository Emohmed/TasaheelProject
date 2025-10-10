using System;
using System.ComponentModel.DataAnnotations;
namespace TasaheelProject.Models
{
    public class Request
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ReferenceNumber { get; set; } = $"REQ-{DateTime.UtcNow.Ticks}";

        public Guid CitizenId { get; set; }
        public Citizen Applicant { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Guid BranchId { get; set; }      //  هنا نربطه بالفرع
        public Branch Branch { get; set; } = null!;

        // المستندات التي رفعها المواطن مع الطلب
        public List<AttachmentDocument> Attachments { get; set; } = new();

        
        }

       



        //  حالة الطلب
        public enum RequestStatus
        {
            Pending,
            Completed,
            Rejected
        }



       



    }


