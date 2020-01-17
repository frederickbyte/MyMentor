using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Models.Interfaces;

namespace MyMentor.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public string CurrentUserId { get; set; }
        // TODO: For brevity these will be temporily removed. A.S.
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<AcademicInterest> AcademicInterests { get; set; }
        public DbSet<StudentMentor> StudentMentors { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AcademicInterest>().ToTable("AcademicInterest");
            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Teacher>().ToTable("Teacher");
            builder.Entity<StudentMentor>().ToTable("StudentMentor");
            builder.Entity<UserInterest>().ToTable("UserInterest");
            

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims)
                .WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles)
                .WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Claims)
                .WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users)
                .WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>().HasMany(u => u.UserInterests)
                .WithOne().HasForeignKey(ui => ui.UserId);
            builder.Entity<ApplicationUser>().HasOne(u => u.Teacher)
                .WithOne(t => t.User);
            builder.Entity<ApplicationUser>().HasOne(u => u.Student)
                .WithOne(s => s.User);

            builder.Entity<Notification>().HasOne(n => n.Sender);
            builder.Entity<Notification>().HasOne(n => n.Recipient);

        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            if (ChangeTracker != null)
            {
                var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


                foreach (var entry in modifiedEntries)
                {
                    var entity = (IAuditableEntity)entry.Entity;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = now;
                        entity.CreatedBy = CurrentUserId;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedDate = now;
                    entity.UpdatedBy = CurrentUserId;
                }
            }
        }
    }
}
