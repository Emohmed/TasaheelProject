namespace TasaheelProject.Models
{
    public class Agency
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // مثل "إدارة الجوازات"
        public string Code { get; set; } = string.Empty; // رمز فريد مثل PAS-001

        public List<Branch> Branches { get; set; } = new();
        public List<Service> Services { get; set; } = new();
    }

}
