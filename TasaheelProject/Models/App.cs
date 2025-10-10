namespace TasaheelProject.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

   
        // 🔹 المستخدم داخل النظام (مواطن / موظف / مشرف)
        public class ApplicationUser : IdentityUser
        {
            [Required, MaxLength(100)]
            public string FullName { get; set; }

            [Required]
            [MaxLength(13)]
            public string NationalId { get; set; } // للمواطنين فقط

            [Required]
            [MaxLength(10)]
            public string RoleType { get; set; } = "Citizen"; // Admin / Employee / Citizen

            public bool IsActive { get; set; } = true;

            // 🔸 إذا كان المواطن مسجلاً في جدول Citizen
            public Guid? CitizenId { get; set; }
            public Citizen? Citizen { get; set; }

            // 🔸 إذا كان موظفًا
            public Guid? BranchId { get; set; }
            public Branch? Branch { get; set; }

            // 🔸 تواريخ إنشاء وتحديث الحساب
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? LastLogin { get; set; }
        }

     
    }


