using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasaheelProject.Models
{
   
   
        public class EmployeeProfile
        {
            [Key]
            public Guid EmployeeProfileId { get; set; } = Guid.NewGuid();

        [ForeignKey("ApplicationUser")]

        public Guid? BranchId { get; set; }
            public Branch? Branch { get; set; }

            public DateTime HireDate { get; set; }

            // 🔹 العلاقة مع ApplicationUser
            [ForeignKey("ApplicationUser")]                                                                           
        public Guid EmployeeId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
        }
    }

