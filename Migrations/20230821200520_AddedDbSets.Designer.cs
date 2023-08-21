﻿// <auto-generated />
using System;
using FlashLeit_API.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlashLeit_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230821200520_AddedDbSets")]
    partial class AddedDbSets
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CollectionModelUserModel", b =>
                {
                    b.Property<int>("CollectionsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("CollectionsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("CollectionModelUserModel");
                });

            modelBuilder.Entity("FlashLeit_API.TestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TestTable");
                });

            modelBuilder.Entity("flashleit_class_library.Models.AchievementModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAchieved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("UserStatsModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserStatsModelId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CardModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<int?>("CollectionModelId")
                        .HasColumnType("int");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("IncorrectAnswerOne")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("IncorrectAnswerThree")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("IncorrectAnswerTwo")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CollectionModelId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CollectionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CounterModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountOfCardsAnswered")
                        .HasColumnType("int");

                    b.Property<int>("AmountOfCorrectAnswers")
                        .HasColumnType("int");

                    b.Property<int>("AmountOfIncorrectAnswers")
                        .HasColumnType("int");

                    b.Property<int>("AmountOfPerfectRuns")
                        .HasColumnType("int");

                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TimesFinished")
                        .HasColumnType("int");

                    b.Property<int>("TimesStarted")
                        .HasColumnType("int");

                    b.Property<int?>("UserStatsModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserStatsModelId");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("flashleit_class_library.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("flashleit_class_library.Models.UserStatsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserStats");
                });

            modelBuilder.Entity("CollectionModelUserModel", b =>
                {
                    b.HasOne("flashleit_class_library.Models.CollectionModel", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("flashleit_class_library.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("flashleit_class_library.Models.AchievementModel", b =>
                {
                    b.HasOne("flashleit_class_library.Models.UserStatsModel", null)
                        .WithMany("Achievements")
                        .HasForeignKey("UserStatsModelId");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CardModel", b =>
                {
                    b.HasOne("flashleit_class_library.Models.CollectionModel", null)
                        .WithMany("FlashCards")
                        .HasForeignKey("CollectionModelId");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CounterModel", b =>
                {
                    b.HasOne("flashleit_class_library.Models.UserStatsModel", null)
                        .WithMany("Counters")
                        .HasForeignKey("UserStatsModelId");
                });

            modelBuilder.Entity("flashleit_class_library.Models.UserStatsModel", b =>
                {
                    b.HasOne("flashleit_class_library.Models.UserModel", "User")
                        .WithMany("UserStats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("flashleit_class_library.Models.CollectionModel", b =>
                {
                    b.Navigation("FlashCards");
                });

            modelBuilder.Entity("flashleit_class_library.Models.UserModel", b =>
                {
                    b.Navigation("UserStats");
                });

            modelBuilder.Entity("flashleit_class_library.Models.UserStatsModel", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Counters");
                });
#pragma warning restore 612, 618
        }
    }
}
