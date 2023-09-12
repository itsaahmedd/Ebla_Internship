using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using employee_system.Models;

namespace employee_system.Data
{


    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Employee_1)
                .WithMany()
                .HasForeignKey(t => t.Assigned_By);


            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Employee_2)
                .WithMany()
                .HasForeignKey(t => t.Assigned_To);

            modelBuilder.Entity<Models.Request>()
                .HasOne(t => t.Employee_1)
                .WithMany()
                .HasForeignKey(t => t.Requested_By);

            modelBuilder.Entity<Models.Announcement>()
                .HasOne(t => t.Employee_1)
                .WithMany()
                .HasForeignKey(t => t.Published_By);


        }


        public DbSet<Employee> Employee_table { get; set; }

        public DbSet<Request> Request_table { get; set; }


        public DbSet<Announcement> Announcement_table { get; set; }

        public DbSet<Models.Task> Task_table { get; set; }


    }

}

