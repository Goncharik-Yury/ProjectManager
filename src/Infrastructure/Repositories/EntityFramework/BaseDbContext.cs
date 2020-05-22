using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Infrastructure.Repositories.EntityFramework
{
    public abstract partial class BaseDbContext<T> : DbContext, ITableContext<T> where T : class
    {
        protected abstract string ConnectionString { get; }
        public DbSet<T> Table { get; set; }

        protected readonly ILogger Logger;

        public BaseDbContext(DbContextOptions<BaseDbContext<T>> options)
            : base(options)
        {
        }

        protected BaseDbContext(ILogger logger)
        {
            Logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConstructEmployeeTable(modelBuilder);
            ConstructProjectTable(modelBuilder);
            ConstructProjectTaskTable(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        private void ConstructEmployeeTable(ModelBuilder modelBuilder)
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
        }

        private void ConstructProjectTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        private void ConstructProjectTaskTable(ModelBuilder modelBuilder)
        {
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
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
