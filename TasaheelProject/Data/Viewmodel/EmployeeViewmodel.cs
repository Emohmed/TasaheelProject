using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Data.Viewmodel
{
    public class EmployeeViewmodel
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public DateTime HireDate { get; set; }

        [Required, MaxLength(12)]
        public string NationalId { get; set; }

        [Required, MaxLength(10)]
        public string EmployeeNumber { get; set; } // EmployeeNumber

        [Required, MaxLength(50)]
        public string JobTitle { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(12),DataType(DataType.Password)]
        public string password { get; set; }
    }
}
