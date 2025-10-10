using System.ComponentModel.DataAnnotations;
using TasaheelProject.Models;

namespace TasaheelProject.Models
{
    // المواطن
    public class Citizen
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(13)]
        public string NationalId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        //  ربط بالمستخدم من Identity
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // علاقات
        public List<Request> Requests { get; set; } = new();

        //  الوثائق الرسمية الخاصة بالمواطن
        //public List<OfficialDocument> OfficialDocuments { get; set; } = new();
    }

}
