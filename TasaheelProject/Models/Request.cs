using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TasaheelProject.Models
{
    public class Request
    {
        [Key]
        public Guid RequestId { get; set; } = Guid.NewGuid();

        public string ReferenceNumber { get; set; } = $"REQ-{DateTime.UtcNow.Ticks}";

        [ForeignKey("CitizenUser")]
        public string CitizenId { get; set; }
        public CitizenProfile CitizenUser  { get; set; }

        [ForeignKey("Service")]
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("Branch")]
        public Guid BranchId { get; set; }      //  هنا نربطه بالفرع
        public Branch Branch { get; set; } = null!;
        public Payment? Payment { get; set; }

        // المستندات التي رفعها المواطن مع الطلب
        public List<AttachmentDocument> Attachments { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();  //  الاشعارات>



    }





    //  حالة الطلب
    public enum RequestStatus
        {
            Pending,
            Completed,
            Rejected
        }



       



    }


