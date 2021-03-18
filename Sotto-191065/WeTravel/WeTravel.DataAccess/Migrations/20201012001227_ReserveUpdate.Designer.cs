﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeTravel.DataAccess;

namespace WeTravel.DataAccess.Migrations
{
    [DbContext(typeof(WeTravelDbContext))]
    [Migration("20201012001227_ReserveUpdate")]
    partial class ReserveUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WeTravel.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WeTravel.Domain.Lodging", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerNight")
                        .HasColumnType("float");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<Guid?>("TouristLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TouristLocationId");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("WeTravel.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("WeTravel.Domain.Reserve", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Adults")
                        .HasColumnType("int");

                    b.Property<int>("Babies")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("Children")
                        .HasColumnType("int");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformationText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LodgingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Telephone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LodgingId");

                    b.ToTable("Reserves");
                });

            modelBuilder.Entity("WeTravel.Domain.ReserveDescription", b =>
                {
                    b.Property<Guid>("ReserveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("ReserveId");

                    b.ToTable("ReserveDescriptions");
                });

            modelBuilder.Entity("WeTravel.Domain.TouristLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("TouristLocations");
                });

            modelBuilder.Entity("WeTravel.Domain.TouristLocationCategory", b =>
                {
                    b.Property<Guid>("TouristLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TouristLocationId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("TouristLocationCategories");
                });

            modelBuilder.Entity("WeTravel.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeTravel.Domain.Lodging", b =>
                {
                    b.HasOne("WeTravel.Domain.TouristLocation", "TouristLocation")
                        .WithMany("Lodgings")
                        .HasForeignKey("TouristLocationId");
                });

            modelBuilder.Entity("WeTravel.Domain.Reserve", b =>
                {
                    b.HasOne("WeTravel.Domain.Lodging", "Lodging")
                        .WithMany("Reserves")
                        .HasForeignKey("LodgingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeTravel.Domain.ReserveDescription", b =>
                {
                    b.HasOne("WeTravel.Domain.Reserve", null)
                        .WithOne("ReserveDescription")
                        .HasForeignKey("WeTravel.Domain.ReserveDescription", "ReserveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeTravel.Domain.TouristLocation", b =>
                {
                    b.HasOne("WeTravel.Domain.Region", "Region")
                        .WithMany("TouristLocations")
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("WeTravel.Domain.TouristLocationCategory", b =>
                {
                    b.HasOne("WeTravel.Domain.Category", "Category")
                        .WithMany("TouristLocationCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeTravel.Domain.TouristLocation", "TouristLocation")
                        .WithMany("TouristLocationCategories")
                        .HasForeignKey("TouristLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}