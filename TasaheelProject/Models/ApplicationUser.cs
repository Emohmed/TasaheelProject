namespace TasaheelProject.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    // المستخدم داخل النظام (مواطن / موظف / مشرف)
    public class ApplicationUser : IdentityUser
        {
            [Required, MaxLength(100)]
            public string FullName { get; set; }

            
           

            [Required,MaxLength(20)]
            public string RoleType { get; set; } = "Citizen"; // Admin / Employee / Citizen

            public bool IsActive { get; set; } = true;

        //  إذا كان المواطن مسجلاً في جدول Citizen
          
            public CitizenProfile? Citizen { get; set; }

        //  إذا كان الموظف مسجلاً في جدول Employee
           
            public EmployeeProfile? Employee { get; set; }




    }


}


