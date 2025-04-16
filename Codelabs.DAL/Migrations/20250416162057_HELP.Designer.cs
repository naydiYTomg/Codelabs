﻿// <auto-generated />
using System;
using Codelabs.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Codelabs.DAL.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250416162057_HELP")]
    partial class HELP
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Codelabs.Core.DTOs.AuthorInfoDTO", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<string>("BIC")
                        .HasColumnType("text");

                    b.Property<string>("BankName")
                        .HasColumnType("text");

                    b.Property<string>("CorrespondentAccount")
                        .HasColumnType("text");

                    b.Property<string>("SettlementAccount")
                        .HasColumnType("text");

                    b.Property<string>("TIN")
                        .HasColumnType("text");

                    b.HasKey("UserID");

                    b.ToTable("AuthorInfos");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.ExerciseDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("DesiredOutput")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LessonID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProgramInput")
                        .HasColumnType("text");

                    b.Property<string>("RequirementsPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("LessonID");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.LanguageDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.LessonDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("AuthorID")
                        .HasColumnType("integer");

                    b.Property<string>("ContentPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("LanguageID");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.PurchaseDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsVisited")
                        .HasColumnType("boolean");

                    b.Property<int>("LessonID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("LessonID");

                    b.HasIndex("UserID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.SolutionDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("Attempts")
                        .HasColumnType("integer");

                    b.Property<string>("CorrectAttemptPath")
                        .HasColumnType("text");

                    b.Property<int>("ExerciseID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSolved")
                        .HasColumnType("boolean");

                    b.Property<int>("PurchaseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ExerciseID");

                    b.HasIndex("PurchaseID");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.UserDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("Password")
                        .HasColumnType("bytea");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.AuthorInfoDTO", b =>
                {
                    b.HasOne("Codelabs.Core.DTOs.UserDTO", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.ExerciseDTO", b =>
                {
                    b.HasOne("Codelabs.Core.DTOs.LessonDTO", "Lesson")
                        .WithMany("Exercises")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.LessonDTO", b =>
                {
                    b.HasOne("Codelabs.Core.DTOs.UserDTO", "Author")
                        .WithMany("Lessons")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codelabs.Core.DTOs.LanguageDTO", "Language")
                        .WithMany("Lessons")
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.PurchaseDTO", b =>
                {
                    b.HasOne("Codelabs.Core.DTOs.LessonDTO", "Lesson")
                        .WithMany("Purchases")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codelabs.Core.DTOs.UserDTO", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.SolutionDTO", b =>
                {
                    b.HasOne("Codelabs.Core.DTOs.ExerciseDTO", "Exercise")
                        .WithMany("Solutions")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codelabs.Core.DTOs.PurchaseDTO", "Purchase")
                        .WithMany("Solutions")
                        .HasForeignKey("PurchaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.ExerciseDTO", b =>
                {
                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.LanguageDTO", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.LessonDTO", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.PurchaseDTO", b =>
                {
                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("Codelabs.Core.DTOs.UserDTO", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
