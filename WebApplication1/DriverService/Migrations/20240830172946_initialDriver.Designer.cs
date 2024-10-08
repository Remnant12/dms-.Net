﻿// <auto-generated />
using System;
using Driver.dbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Driver.Migrations
{
    [DbContext(typeof(DriverDbContext))]
    [Migration("20240830172946_initialDriver")]
    partial class initialDriver
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Driver.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DriverId"));

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateOfHire")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmergencyContact")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("LicenseExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LicenseNumber")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProfilePhoto")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("ShiftEndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ShiftStartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");
                });
#pragma warning restore 612, 618
        }
    }
}
