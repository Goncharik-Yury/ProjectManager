using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Repositories;
using ProjectManager.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Infrastructure.EntityFramework
{
    public class EmployeeRepositoryEf : BaseDbContextEf<Employee>, IRepository<Employee>
    {
        protected override string ConnectionString { get; }
        private DbSet<Employee> entityContext { get; set; }
        public EmployeeRepositoryEf(string connectionString, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
        }

        public void Create(Employee item)
        {
            Logger.LogDebug(GetType() + ".Create is called");
            entityContext.Add(item);
            SaveChanges();
        }

        public void Delete(int id)
        {
            Logger.LogDebug(GetType() + ".Delete is called");
            Employee employeeToDelete = new Employee() { Id = id };
            Entity.Remove(employeeToDelete);
            SaveChanges();
        }

        public Employee GetSingle(int id)
        {
            Logger.LogDebug(GetType() + ".Get is called");
            Employee employee = Entity.FirstOrDefault(item => item.Id == id);
            return employee;
        }

        public IList<Employee> GetAll()
        {
            Logger.LogDebug(GetType() + ".GetAll is called");
            List<Employee> qemployeesList = Entity.ToList();
            return qemployeesList;
        }

        public void Update(Employee item)
        {
            Logger.LogDebug(GetType() + ".Update is called");
            Entry(Entity.FirstOrDefault(itemq => itemq.Id == item.Id)).CurrentValues.SetValues(item);
            SaveChanges();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    ConstructEmployeeTable(modelBuilder);
        //    ConstructProjectTable(modelBuilder);
        //    ConstructProjectTaskTable(modelBuilder);

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //private void ConstructEmployeeTable(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>(entity =>
        //    {
        //        entity.Property(e => e.FirstName)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.LastName)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Patronymic).HasMaxLength(50);

        //        entity.Property(e => e.Position)
        //            .IsRequired()
        //            .HasMaxLength(50);
        //    });
        //}

        //private void ConstructProjectTable(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Project>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.ShortName)
        //            .IsRequired()
        //            .HasMaxLength(50);
        //    });
        //}

        //private void ConstructProjectTaskTable(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProjectTask>(entity =>
        //    {
        //        entity.Property(e => e.BeginDate).HasColumnType("date");

        //        entity.Property(e => e.EndDate).HasColumnType("date");

        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Status)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.HasOne(d => d.Employee)
        //            .WithMany(p => p.ProjectTask)
        //            .HasForeignKey(d => d.EmployeeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_ProjectTask_Employee");

        //        entity.HasOne(d => d.Project)
        //            .WithMany(p => p.ProjectTask)
        //            .HasForeignKey(d => d.ProjectId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_ProjectTask_Project");
        //    });
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
