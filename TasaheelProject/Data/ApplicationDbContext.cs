using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasaheelProject.Models;

namespace TasaheelProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
            public DbSet<Branch> Branches { get; set; }
            public DbSet<Service> Services { get; set; }
            public DbSet<Agency> Agencies { get; set; }
            public DbSet<Payment> payments { get; set; }
            public DbSet<AttachmentDocument> Attachments { get; set; }
            public DbSet<Notification> Notifications { get; set; }
            public DbSet<Request> Requests { get; set; }
            public DbSet<CitizenProfile> Citizens { get; set; }
            public DbSet<EmployeeProfile> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // علاقة ApplicationUser مع CitizenProfile (واحد إلى واحد)
            
            builder.Entity<ApplicationUser>()
             .HasOne(u => u.Citizen)
             .WithOne(c => c.ApplicationUser)
             .HasForeignKey<CitizenProfile>(c => c.CitizenId);

            // علاقة ApplicationUser مع EmployeeProfile (واحد إلى واحد)
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<EmployeeProfile>(e => e.EmployeeId);

            // علاقة Branch مع EmployeeProfile (واحد إلى كثير)
            builder.Entity<Branch>()
                .HasMany(b => b.EmployeeProfiles)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId);

            // علاقة Branch مع Request (واحد إلى كثير)
            builder.Entity<Branch>()
                .HasMany(b => b.Requests)
                .WithOne(r => r.Branch)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.NoAction);
            // علاقة Agency مع Branch (واحد إلى كثير)
            builder.Entity<Agency>()
                .HasMany(a => a.Branches)
                .WithOne(b => b.Agency)
                .HasForeignKey(b => b.AgencyId);
            // علاقة Agency مع Service (واحد إلى كثير)
            builder.Entity<Agency>()
                .HasMany(a => a.Services)
                .WithOne(s => s.Agency)
                .HasForeignKey(s => s.AgencyId);
            // علاقة Service مع Request (واحد إلى كثير)
            builder.Entity<Service>()
                .HasMany(s => s.Requests)
                .WithOne(r => r.Service)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
            // علاقة CitizenProfile مع Request (واحد إلى كثير)
            builder.Entity<CitizenProfile>()
                .HasMany(c => c.Requests)
                .WithOne(r => r.CitizenUser)
                .HasForeignKey(r => r.CitizenId)
                .OnDelete(DeleteBehavior.Restrict);
            // علاقة Request مع Payment (واحد إلى واحد)
            builder.Entity<Request>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Request)
                .HasForeignKey<Payment>(p => p.RequestId);
            
            // علاقة Request مع AttachmentDocument (واحد إلى كثير)
            builder.Entity<Request>()
                .HasMany(r => r.Attachments)
                .WithOne(a => a.Request)
                .HasForeignKey(a => a.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
            // علاقة Request مع Notification (واحد إلى كثير)
            builder.Entity<Request>()
                .HasMany(r => r.Notifications)
                .WithOne(n => n.Request)
                .HasForeignKey(n => n.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
           
            // Unique constraints
           
            builder.Entity<CitizenProfile>()
                .HasIndex(c => c.NationalId)
                .IsUnique();

            builder.Entity<EmployeeProfile>()
                .HasIndex(e=>e.EmployeeNumber)
                .IsUnique();









        }
    }
 }

