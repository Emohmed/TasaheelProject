using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Data.Viewmodel
{
    public class AdminViewmodel
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(12),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
