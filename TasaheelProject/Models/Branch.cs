using Azure.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasaheelProject.Models
{
    public class Branch
    {
        [Key]
        public Guid BranchId { get; set; }= Guid.NewGuid();

        [Required, StringLength(8)]
        public string Code { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Name { get; set; } = string.Empty; // مثل "فرع طرابلس"

        [Required, StringLength(20)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        [ForeignKey("Agency")]
        public Guid AgencyId { get; set; }
        public Agency Agency { get; set; } = null!;
        public List<EmployeeProfile> EmployeeProfiles { get; set; } = new();
        public List<Request> Requests { get; set; } = new();
    }

}
