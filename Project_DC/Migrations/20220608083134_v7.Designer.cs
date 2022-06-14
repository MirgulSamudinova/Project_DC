﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project_DC.Models;

#nullable disable

namespace Project_DC.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220608083134_v7")]
    partial class v7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Project_DC.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Project_DC.Models.Genders", b =>
                {
                    b.Property<int>("id_gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_gender"));

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_gender");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Project_DC.Models.Patients", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PatientId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("e_mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("inn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PatientId");

                    b.HasIndex("GenderId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Project_DC.Models.Positions", b =>
                {
                    b.Property<int>("Id_position")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_position"));

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id_position");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Project_DC.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Project_DC.Models.Rooms", b =>
                {
                    b.Property<int>("RoomsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoomsId"));

                    b.Property<string>("RoomsName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoomsId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Project_DC.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScheduleId"));

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<int?>("Staffsid_staff")
                        .HasColumnType("integer");

                    b.Property<int>("TemplateID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("withDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ScheduleId");

                    b.HasIndex("Staffsid_staff");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Project_DC.Models.ScheduleTemplate", b =>
                {
                    b.Property<int>("TmeplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TmeplateId"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Staffsid_staff")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("ToTime")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly>("withTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("TmeplateId");

                    b.HasIndex("Staffsid_staff");

                    b.ToTable("ScheduleTemplate");
                });

            modelBuilder.Entity("Project_DC.Models.Services", b =>
                {
                    b.Property<int>("IdService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdService"));

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idGroup")
                        .HasColumnType("integer");

                    b.Property<decimal>("price")
                        .HasColumnType("numeric");

                    b.Property<string>("typeService")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdService");

                    b.HasIndex("idGroup");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Project_DC.Models.ServicesGroup", b =>
                {
                    b.Property<int>("IdGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdGroup"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdGroup");

                    b.ToTable("ServicesGroup");
                });

            modelBuilder.Entity("Project_DC.Models.Staffs", b =>
                {
                    b.Property<int>("id_staff")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_staff"));

                    b.Property<DateTime>("birth_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("e_mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("id_gender")
                        .HasColumnType("integer");

                    b.Property<int>("id_position")
                        .HasColumnType("integer");

                    b.Property<string>("middle_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("mobile_number")
                        .HasColumnType("integer");

                    b.Property<string>("sure_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_staff");

                    b.HasIndex("id_gender");

                    b.HasIndex("id_position");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Project_DC.Models.Patients", b =>
                {
                    b.HasOne("Project_DC.Models.Genders", "Genders")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genders");
                });

            modelBuilder.Entity("Project_DC.Models.Schedule", b =>
                {
                    b.HasOne("Project_DC.Models.Staffs", "Staffs")
                        .WithMany()
                        .HasForeignKey("Staffsid_staff");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("Project_DC.Models.ScheduleTemplate", b =>
                {
                    b.HasOne("Project_DC.Models.Staffs", "Staffs")
                        .WithMany()
                        .HasForeignKey("Staffsid_staff");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("Project_DC.Models.Services", b =>
                {
                    b.HasOne("Project_DC.Models.ServicesGroup", "ServicesGroup")
                        .WithMany()
                        .HasForeignKey("idGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicesGroup");
                });

            modelBuilder.Entity("Project_DC.Models.Staffs", b =>
                {
                    b.HasOne("Project_DC.Models.Genders", "Genders")
                        .WithMany()
                        .HasForeignKey("id_gender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_DC.Models.Positions", "Positions")
                        .WithMany()
                        .HasForeignKey("id_position")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genders");

                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
