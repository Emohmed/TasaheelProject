using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasaheelProject.Models
{
   
   
        public class EmployeeProfile
        {
            [Key]
            public string EmployeeProfileId { get; set; }
            public DateTime HireDate { get; set; }

        [Required, MaxLength(12)]
            public string NationalId { get; set; }

        [Required, MaxLength(10)]
           public string EmployeeNumber { get; set; } // EmployeeNumber

        [Required, MaxLength(50)]
            public string JobTitle { get; set; }


         [ForeignKey("Branch")]

            public Guid? BranchId { get; set; }
            public Branch? Branch { get; set; }


            //  العلاقة مع ApplicationUser
         [ForeignKey("ApplicationUser")]                                                                           
            public string EmployeeId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
        }
    }

