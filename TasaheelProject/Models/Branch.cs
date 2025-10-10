using Azure.Core;

namespace TasaheelProject.Models
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // مثل "فرع طرابلس"
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public Guid AgencyId { get; set; }
        public Agency Agency { get; set; } = null!;

        public List<Request> Requests { get; set; } = new();
    }

}
