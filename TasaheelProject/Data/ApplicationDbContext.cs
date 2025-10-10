using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasaheelProject.Models;

namespace TasaheelProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
            public DbSet<Branch> Branches { get; set; }
            public DbSet<Service> Services { get; set; }
            public DbSet<Agency> Agencies { get; set; }
            public DbSet<Payment> payments { get; set; }
            public DbSet<Attachment> Attachments { get; set; }
            public DbSet<Notification> Notifications { get; set; }
            public DbSet<Request> Requests { get; set; }
            public DbSet<Citizen> Citizens { get; set; }
    }
 }

