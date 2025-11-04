namespace TasaheelProject.Data.Viewmodel
{
    internal class ServiceDetailsViewModel
    {
        public Guid ServiceId { get; set; }
        public Guid AgencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string AgencyName { get; set; }
    }
}