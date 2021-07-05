using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OpenT2.DataAccess;
using OpenT2.Models;

namespace OpenT2
{
    public partial class postgresContext : DbContext, IDataContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries1 { get; set; }
        public virtual DbSet<Country1> Countries { get; init; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; init; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Server=opentelemetrytest.postgres.database.azure.com;Port=5432;Database=postgres;Username=skiing@opentelemetrytest;Password=Justice4;Ssl Mode=Require;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(2)
                    .HasColumnName("country_id")
                    .IsFixedLength();

                entity.Property(e => e.CountryName)
                    .HasMaxLength(40)
                    .HasColumnName("country_name");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("countries_region_id_fkey");
            });

            modelBuilder.Entity<Country1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Countries");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("department_name");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("departments_location_id_fkey");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.ToTable("dependents");

                entity.Property(e => e.DependentId).HasColumnName("dependent_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("relationship");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("dependents_employee_id_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("hire_date");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("last_name");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Salary)
                    .HasPrecision(8, 2)
                    .HasColumnName("salary");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("employees_department_id_fkey");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("employees_job_id_fkey");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("employees_manager_id_fkey");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("job_title");

                entity.Property(e => e.MaxSalary)
                    .HasPrecision(8, 2)
                    .HasColumnName("max_salary");

                entity.Property(e => e.MinSalary)
                    .HasPrecision(8, 2)
                    .HasColumnName("min_salary");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("city");

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("country_id")
                    .IsFixedLength();

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .HasColumnName("postal_code");

                entity.Property(e => e.StateProvince)
                    .HasMaxLength(25)
                    .HasColumnName("state_province");

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(40)
                    .HasColumnName("street_address");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("locations_country_id_fkey");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("regions");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(25)
                    .HasColumnName("region_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
