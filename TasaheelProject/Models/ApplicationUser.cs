namespace TasaheelProject.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

   
        // المستخدم داخل النظام (مواطن / موظف / مشرف)
        public class ApplicationUser : IdentityUser
        {
            [Required, MaxLength(100)]
            public string FullName { get; set; }

            
            [Required,MaxLength(13)]
            public string NationalId { get; set; } 

            [Required,MaxLength(20)]
            public string RoleType { get; set; } = "Citizen"; // Admin / Employee / Citizen

            public bool IsActive { get; set; } = true;

            //  إذا كان المواطن مسجلاً في جدول Citizen
            public Guid? CitizenId { get; set; }
            public Citizen? Citizen { get; set; }

            

           
        }

     
    }


