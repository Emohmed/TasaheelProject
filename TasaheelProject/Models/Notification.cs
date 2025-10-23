using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasaheelProject.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }= Guid.NewGuid();

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(300)]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }= false;

        [ForeignKey("Request")]
        public Guid RequestId { get; set; }
        public Request Request { get; set; }
    }
}
