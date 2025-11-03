using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Data.Viewmodel
{
    public class LoginViewmodel
    {
        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(6)]
        public string Password { get; set; }

    }
}
