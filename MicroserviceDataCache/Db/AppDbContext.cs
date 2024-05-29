using MicroserviceDataCache.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicroserviceDataCache.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<UserTaskCache> UserTaskCaches { get; set; }
        public DbSet<AdminTask> AdminTasks { get; set; }
        public DbSet<AdminTaskCache> AdminTaskCaches { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<TaskResponsibility> TaskResponsibilities { get; set; }
        public DbSet<UserTaskResponsibilityCache> UserTaskResponsibilityCaches { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTask>().HasKey(ut => ut.Id);
            modelBuilder.Entity<UserTaskCache>().HasKey(utc => utc.Id);
            modelBuilder.Entity<AdminTask>().HasKey(at => at.Id);
            modelBuilder.Entity<AdminTaskCache>().HasKey(atc => atc.Id);
            modelBuilder.Entity<UserCategory>().HasKey(uc => uc.Id);
            modelBuilder.Entity<TaskResponsibility>().HasKey(tr => tr.Id);
            modelBuilder.Entity<UserTaskResponsibilityCache>().HasKey(utrc => utrc.Id);

            modelBuilder.Entity<UserTask>()
                .HasIndex(ut => new { ut.TaskId, ut.UserId })
                .IsUnique();

            modelBuilder.Entity<UserTaskCache>()
                .HasIndex(utc => utc.UserId)
                .IsUnique();

            modelBuilder.Entity<AdminTask>()
                .HasIndex(at => new { at.TaskId, at.AdminId })
                .IsUnique();

            modelBuilder.Entity<AdminTaskCache>()
                .HasIndex(atc => atc.AdminId)
                .IsUnique();

            modelBuilder.Entity<UserCategory>()
                .HasIndex(uc => new { uc.UserId, uc.CategoryId })
                .IsUnique();

            modelBuilder.Entity<TaskResponsibility>()
                .HasIndex(tr => new { tr.UserId, tr.TaskId })
                .IsUnique();

            modelBuilder.Entity<UserTaskResponsibilityCache>()
                .HasIndex(utrc => new { utrc.UserId, utrc.TaskId })
                .IsUnique();
        }
    }
}