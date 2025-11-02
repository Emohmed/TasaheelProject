using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasaheelProject.Models;

namespace TasaheelProject.Data.Viewmodel
{
    public class CitizinViewmodel
    {
       

        [Required, MaxLength(12)]
        public string NationalId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(12)]
        public string Password { get; set; }
       
        
       
    }
}
