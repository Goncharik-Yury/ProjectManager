using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectManager.Infrastructure.Models;

namespace ProjectManager.Infrastructure.EntityFramework
{
    public partial class ProjectManagerDbContext : DbContext
    {
        public ProjectManagerDbContext()
        {
        }

        public ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectTask> ProjectTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\seles\\ProjectManagerDB.mdf;Integrated Security=True;Connect Timeout=10");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProjectTask)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_Employee");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTask)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTask_Project");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
