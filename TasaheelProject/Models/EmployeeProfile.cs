using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasaheelProject.Models
{
   
   
        public class EmployeeProfile
        {
            [Key]
            public string EmployeeProfileId { get; set; } 

            [ForeignKey("Branch")]

            public Guid? BranchId { get; set; }
            public Branch? Branch { get; set; }

            public DateTime HireDate { get; set; }

            //  العلاقة مع ApplicationUser
            [ForeignKey("ApplicationUser")]                                                                           
            public string EmployeeId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
        }
    }

