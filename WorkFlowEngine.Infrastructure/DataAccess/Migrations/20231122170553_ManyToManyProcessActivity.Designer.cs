﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkFlowEngine.Infrastructure.DataAccess;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231122170553_ManyToManyProcessActivity")]
    partial class ManyToManyProcessActivity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ActivityAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivitiesActions");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ActivityDestination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("ActivityDestinationTypes");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.DataField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("ProcessId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.ToTable("DataFields");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProcessId");

                    b.ToTable("ProcessActivities");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentActivityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpened")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OpnenedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("TaskUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentActivityId");

                    b.HasIndex("ProcessId");

                    b.ToTable("ProcessInstances");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstanceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentActivityId")
                        .HasColumnType("int");

                    b.Property<int>("NextActivityId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessInstanceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("CurrentActivityId");

                    b.HasIndex("NextActivityId");

                    b.ToTable("processInstanceHistories");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstanceUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ProcessInstanceID")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserTypeId");

                    b.ToTable("ProcessInstanceUsers");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Transition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentActivityId")
                        .HasColumnType("int");

                    b.Property<int>("NextActivityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("CurrentActivityId");

                    b.HasIndex("NextActivityId");

                    b.ToTable("transitions");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.TransitionConditions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BinaryOperator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TransitionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TransitionId");

                    b.HasIndex("Name", "Value", "Operator")
                        .IsUnique();

                    b.ToTable("transitionConditions");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("DestinationTypes");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ActivityAction", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Action", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "Activity")
                        .WithMany("ActivityActions")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ActivityDestination", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "Activity")
                        .WithMany("ActivityDestinations")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.UserType", "UserType")
                        .WithMany("ActivityDestinations")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.DataField", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Process", null)
                        .WithMany("DataFields")
                        .HasForeignKey("ProcessId");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessActivity", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "Activity")
                        .WithMany("ProcessActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Process", "Process")
                        .WithMany("ProcessActivities")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstance", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "CurrentActivity")
                        .WithMany()
                        .HasForeignKey("CurrentActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentActivity");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstanceHistory", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Action", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "CurrentActivity")
                        .WithMany()
                        .HasForeignKey("CurrentActivityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "NextActivity")
                        .WithMany()
                        .HasForeignKey("NextActivityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("CurrentActivity");

                    b.Navigation("NextActivity");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.ProcessInstanceUser", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Transition", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Action", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "CurrentActivity")
                        .WithMany()
                        .HasForeignKey("CurrentActivityId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("WorkFlowEngine.Domain.Entities.Activity", "NextActivity")
                        .WithMany()
                        .HasForeignKey("NextActivityId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("CurrentActivity");

                    b.Navigation("NextActivity");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.TransitionConditions", b =>
                {
                    b.HasOne("WorkFlowEngine.Domain.Entities.Transition", "Transition")
                        .WithMany()
                        .HasForeignKey("TransitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transition");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Activity", b =>
                {
                    b.Navigation("ActivityActions");

                    b.Navigation("ActivityDestinations");

                    b.Navigation("ProcessActivities");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.Process", b =>
                {
                    b.Navigation("DataFields");

                    b.Navigation("ProcessActivities");
                });

            modelBuilder.Entity("WorkFlowEngine.Domain.Entities.UserType", b =>
                {
                    b.Navigation("ActivityDestinations");
                });
#pragma warning restore 612, 618
        }
    }
}
