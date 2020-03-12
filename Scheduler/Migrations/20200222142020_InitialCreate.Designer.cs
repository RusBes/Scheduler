﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduler;

namespace Scheduler.Migrations
{
    [DbContext(typeof(GoalContext))]
    [Migration("20200222142020_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Scheduler.Notification", b =>
                {
                    b.Property<long>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<long?>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("NotificationId");

                    b.HasIndex("TaskId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Scheduler.Task", b =>
                {
                    b.Property<long>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long?>("TaskParentId")
                        .HasColumnType("bigint");

                    b.HasKey("TaskId");

                    b.HasIndex("TaskParentId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Scheduler.Notification", b =>
                {
                    b.HasOne("Scheduler.Task", "Task")
                        .WithMany("Notifications")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("Scheduler.Task", b =>
                {
                    b.HasOne("Scheduler.Task", "TaskParent")
                        .WithMany("Goals")
                        .HasForeignKey("TaskParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
