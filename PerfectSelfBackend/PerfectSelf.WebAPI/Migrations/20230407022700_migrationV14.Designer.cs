﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PerfectSelf.WebAPI.Context;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    [DbContext(typeof(PerfectSelfContext))]
    [Migration("20230407022700_migrationV14")]
    partial class migrationV14
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.ActorProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ActorUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AgeRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AgencyCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ReviewCount")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("VaccinationStatus")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ActorUid")
                        .IsUnique();

                    b.ToTable("ActorProfile");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FromTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStandBy")
                        .HasColumnType("bit");

                    b.Property<Guid>("ReaderUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Repeat")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Availability");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ActorUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BookStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccept")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ReaderReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ReaderScore")
                        .HasColumnType("real");

                    b.Property<Guid>("ReaderUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ScriptFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.BookList", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ActorUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BookStartTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("HourlyPrice")
                        .HasColumnType("float");

                    b.Property<int>("Others")
                        .HasColumnType("int");

                    b.Property<bool>("ReaderIsLogin")
                        .HasColumnType("bit");

                    b.Property<string>("ReaderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReaderUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ScriptFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VoiceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToView("booklist");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.PerfectSelfVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuildNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MajorVersion")
                        .HasColumnType("int");

                    b.Property<int>("MinorVersion")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PerfectSelf");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.ReaderList", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<double?>("HourlyPrice")
                        .HasColumnType("float");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReviewCount")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.ToView("readerlist");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.ReaderProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("HourlyPrice")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Others")
                        .HasColumnType("int");

                    b.Property<Guid>("ReaderUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReviewCount")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("VoiceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReaderUid")
                        .IsUnique();

                    b.ToTable("ReaderProfile");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.Tape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BucketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("ReaderUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TapeKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TapeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tape");
                });

            modelBuilder.Entity("PerfectSelf.WebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermanentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
