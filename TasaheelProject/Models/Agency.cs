using System.ComponentModel.DataAnnotations;

namespace TasaheelProject.Models
{
    public class Agency
    {
        [Key]
        public Guid AgencyId { get; set; }= Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // مثل "إدارة الجوازات"
        public string Code { get; set; } = string.Empty; 

        public List<Branch> Branches { get; set; } 
        public List<Service> Services { get; set; } 
    }

}
