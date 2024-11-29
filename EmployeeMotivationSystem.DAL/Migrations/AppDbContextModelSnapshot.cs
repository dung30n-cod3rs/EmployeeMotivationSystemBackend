﻿// <auto-generated />
using System;
using EmployeeMotivationSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeMotivationSystem.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("Salary")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PositionId");

                    b.HasIndex("UserId");

                    b.ToTable("CompaniesUsers");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUsersFilials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FilialId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyUserId");

                    b.HasIndex("FilialId");

                    b.ToTable("CompaniesUsersFilials");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUsersMetric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<int>("MetricId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("MetricId");

                    b.ToTable("CompaniesUsersMetrics");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Filial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Filials");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<double>("TargetValue")
                        .HasColumnType("double precision");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ParentId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUser", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMotivationSystem.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUsersFilials", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.CompaniesUser", "CompanyUser")
                        .WithMany()
                        .HasForeignKey("CompanyUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Filial", "Filial")
                        .WithMany()
                        .HasForeignKey("FilialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyUser");

                    b.Navigation("Filial");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.CompaniesUsersMetric", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.CompaniesUser", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Metric", "Metric")
                        .WithMany()
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Metric");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Company", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.Navigation("CreatorUser");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Filial", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Metric", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Position", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMotivationSystem.DAL.Models.Position", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Company");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.RefreshToken", b =>
                {
                    b.HasOne("EmployeeMotivationSystem.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeMotivationSystem.DAL.Models.Position", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
