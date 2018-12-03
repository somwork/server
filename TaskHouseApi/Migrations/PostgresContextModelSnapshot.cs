﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskHouseApi.Persistence.DatabaseContext;

namespace TaskHouseApi.Migrations
{
    [DbContext(typeof(PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TaskHouseApi.Model.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.Property<decimal>("From");

                    b.Property<decimal>("To");

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaskHouseApi.Model.CategorySkill", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("SkillId");

                    b.HasKey("CategoryId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("CategorySkill");
                });

            modelBuilder.Entity("TaskHouseApi.Model.CategoryTask", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("TaskId");

                    b.HasKey("CategoryId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("CategoryTask");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Base");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("RatesId");

                    b.Property<bool>("Success");

                    b.Property<long>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("RatesId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<int>("Complexity");

                    b.Property<decimal>("HourlyWage");

                    b.Property<int>("TaskId");

                    b.Property<int>("TotalHours");

                    b.Property<decimal>("Urgency");

                    b.Property<int>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("PrimaryLine")
                        .IsRequired();

                    b.Property<string>("SecondaryLine")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("SendAt");

                    b.Property<int>("TaskId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Rates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Dkk");

                    b.Property<double>("Usd");

                    b.HasKey("Id");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Rating");

                    b.Property<string>("Statement")
                        .IsRequired();

                    b.Property<int>("TaskId");

                    b.Property<int>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.HasIndex("WorkerId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("TaskHouseApi.Model.ServiceModel.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AverageEstimate");

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("EmployerId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<decimal>("Urgency");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskHouseApi.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Salt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Employer", b =>
                {
                    b.HasBaseType("TaskHouseApi.Model.User");


                    b.ToTable("Employer");

                    b.HasDiscriminator().HasValue("Employer");
                });

            modelBuilder.Entity("TaskHouseApi.Model.QualityAssurance", b =>
                {
                    b.HasBaseType("TaskHouseApi.Model.User");


                    b.ToTable("QualityAssurance");

                    b.HasDiscriminator().HasValue("QualityAssurance");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Worker", b =>
                {
                    b.HasBaseType("TaskHouseApi.Model.User");


                    b.ToTable("Worker");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("TaskHouseApi.Model.CategorySkill", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Category", "Category")
                        .WithMany("CategorySkill")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskHouseApi.Model.Skill", "Skill")
                        .WithMany("CategorySkill")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.CategoryTask", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Category", "Category")
                        .WithMany("CategoryTask")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskHouseApi.Model.Task", "Task")
                        .WithMany("CategoryTask")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Currency", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Rates", "Rates")
                        .WithMany()
                        .HasForeignKey("RatesId");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Education", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Worker")
                        .WithMany("Educations")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Estimate", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Task")
                        .WithMany("Estimates")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskHouseApi.Model.Worker")
                        .WithMany("Estimates")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Location", b =>
                {
                    b.HasOne("TaskHouseApi.Model.User")
                        .WithOne("Location")
                        .HasForeignKey("TaskHouseApi.Model.Location", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Message", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Task", "Task")
                        .WithMany("Messages")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskHouseApi.Model.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Reference", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Task")
                        .WithOne("Reference")
                        .HasForeignKey("TaskHouseApi.Model.Reference", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskHouseApi.Model.Worker")
                        .WithMany("References")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.ServiceModel.RefreshToken", b =>
                {
                    b.HasOne("TaskHouseApi.Model.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Skill", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Worker")
                        .WithMany("Skills")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskHouseApi.Model.Task", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Employer", "Employer")
                        .WithMany("Tasks")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
