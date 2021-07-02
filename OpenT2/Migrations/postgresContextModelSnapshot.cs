﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenT2;

namespace OpenT2.Migrations
{
    [DbContext(typeof(postgresContext))]
    partial class postgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements")
                .HasAnnotation("Relational:Collation", "English_United States.1252")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.5.21301.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("OpenT2.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasMaxLength(2)
                        .HasColumnType("character(2)")
                        .HasColumnName("country_id")
                        .IsFixedLength();

                    b.Property<string>("CountryName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("country_name");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer")
                        .HasColumnName("region_id");

                    b.HasKey("CountryId");

                    b.HasIndex("RegionId");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("OpenT2.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("department_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("department_name");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.HasKey("DepartmentId");

                    b.HasIndex("LocationId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("OpenT2.Dependent", b =>
                {
                    b.Property<int>("DependentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("dependent_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("relationship");

                    b.HasKey("DependentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("dependents");
                });

            modelBuilder.Entity("OpenT2.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("employee_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("department_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("first_name");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("date")
                        .HasColumnName("hire_date");

                    b.Property<int>("JobId")
                        .HasColumnType("integer")
                        .HasColumnName("job_id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("last_name");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("integer")
                        .HasColumnName("manager_id");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<decimal>("Salary")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)")
                        .HasColumnName("salary");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("JobId");

                    b.HasIndex("ManagerId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("OpenT2.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("job_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)")
                        .HasColumnName("job_title");

                    b.Property<decimal?>("MaxSalary")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)")
                        .HasColumnName("max_salary");

                    b.Property<decimal?>("MinSalary")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)")
                        .HasColumnName("min_salary");

                    b.HasKey("JobId");

                    b.ToTable("jobs");
                });

            modelBuilder.Entity("OpenT2.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("location_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("city");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character(2)")
                        .HasColumnName("country_id")
                        .IsFixedLength();

                    b.Property<string>("PostalCode")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("postal_code");

                    b.Property<string>("StateProvince")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("state_province");

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("street_address");

                    b.HasKey("LocationId");

                    b.HasIndex("CountryId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("OpenT2.Models.Country1", b =>
                {
                    b.Property<string>("CountryId")
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .HasColumnType("text");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("OpenT2.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("region_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RegionName")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("region_name");

                    b.HasKey("RegionId");

                    b.ToTable("regions");
                });

            modelBuilder.Entity("OpenT2.Country", b =>
                {
                    b.HasOne("OpenT2.Region", "Region")
                        .WithMany("Countries")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("countries_region_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("OpenT2.Department", b =>
                {
                    b.HasOne("OpenT2.Location", "Location")
                        .WithMany("Departments")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("departments_location_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Location");
                });

            modelBuilder.Entity("OpenT2.Dependent", b =>
                {
                    b.HasOne("OpenT2.Employee", "Employee")
                        .WithMany("Dependents")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("dependents_employee_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OpenT2.Employee", b =>
                {
                    b.HasOne("OpenT2.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("employees_department_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenT2.Job", "Job")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .HasConstraintName("employees_job_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenT2.Employee", "Manager")
                        .WithMany("InverseManager")
                        .HasForeignKey("ManagerId")
                        .HasConstraintName("employees_manager_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Department");

                    b.Navigation("Job");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("OpenT2.Location", b =>
                {
                    b.HasOne("OpenT2.Country", "Country")
                        .WithMany("Locations")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("locations_country_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("OpenT2.Country", b =>
                {
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("OpenT2.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("OpenT2.Employee", b =>
                {
                    b.Navigation("Dependents");

                    b.Navigation("InverseManager");
                });

            modelBuilder.Entity("OpenT2.Job", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("OpenT2.Location", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("OpenT2.Region", b =>
                {
                    b.Navigation("Countries");
                });
#pragma warning restore 612, 618
        }
    }
}
