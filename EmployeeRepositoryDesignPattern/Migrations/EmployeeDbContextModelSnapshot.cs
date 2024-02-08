﻿// <auto-generated />
using EmployeeRepositoryDesignPattern.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeRepositoryDesignPattern.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    partial class EmployeeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeRepositoryDesignPattern.Models.EmployeeDetails", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("EmployeeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmployeePhone")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId");

                    b.ToTable("EmployeeDetails");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            EmployeeDescription = ".NET Developer",
                            EmployeeName = "Sakshi Modase",
                            EmployeePhone = 8530601006L,
                            IsActive = true
                        },
                        new
                        {
                            EmployeeId = 2,
                            EmployeeDescription = "Business Analyst",
                            EmployeeName = "Saurabh Modase",
                            EmployeePhone = 7387481003L,
                            IsActive = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}