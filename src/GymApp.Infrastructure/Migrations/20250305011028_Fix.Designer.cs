﻿// <auto-generated />
using System;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymApp.Infrastructure.Migrations
{
    [DbContext(typeof(GymManagementDbContext))]
    [Migration("20250305011028_Fix")]
    partial class Fix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("GymApp.Domain.AdminAggregate.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("GymApp.Domain.GymAggregate.Gym", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("_maxRooms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxRooms");

                    b.Property<string>("_roomIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("RoomIds");

                    b.Property<string>("_trainerIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("TrainerIds");

                    b.HasKey("Id");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("GymApp.Domain.RoomAggregate.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GymId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("_maxDailySessions")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxDailySessions");

                    b.Property<string>("_sessionsIdsByDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("SessionIdsByDate");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("GymApp.Domain.SubscriptionAggregate.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("_gymIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("GymIds");

                    b.Property<int>("_maxGyms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxGyms");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("GymApp.Domain.TrainerAggregate.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("_sessionIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("SessionIds");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("GymApp.Domain.TrainerAggregate.Trainer", b =>
                {
                    b.OwnsOne("GymApp.Domain.Common.Entities.Schedule", "_schedule", b1 =>
                        {
                            b1.Property<Guid>("TrainerId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT")
                                .HasColumnName("ScheduleId");

                            b1.Property<string>("_calendar")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("ScheduleCalendar");

                            b1.HasKey("TrainerId");

                            b1.ToTable("Trainers");

                            b1.WithOwner()
                                .HasForeignKey("TrainerId");
                        });

                    b.Navigation("_schedule");
                });
#pragma warning restore 612, 618
        }
    }
}
