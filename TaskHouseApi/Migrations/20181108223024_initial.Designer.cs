﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskHouseApi.DatabaseContext;

namespace TaskHouseApi.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20181108223024_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Currency");

                    b.Property<decimal>("From");

                    b.Property<decimal>("To");

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("PrimaryLine");

                    b.Property<string>("SecondaryLine");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("SendAt");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("Description");

                    b.Property<int?>("EmployerId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Urgency");

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

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<string>("Username");

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

            modelBuilder.Entity("TaskHouseApi.Model.Worker", b =>
                {
                    b.HasBaseType("TaskHouseApi.Model.User");


                    b.ToTable("Worker");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("TaskHouseApi.Model.Task", b =>
                {
                    b.HasOne("TaskHouseApi.Model.Employer")
                        .WithMany("Tasks")
                        .HasForeignKey("EmployerId");
                });
#pragma warning restore 612, 618
        }
    }
}