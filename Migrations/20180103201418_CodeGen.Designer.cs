﻿// <auto-generated />
using Fyo.Enums;
using Fyo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Fyo.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180103201418_CodeGen")]
    partial class CodeGen
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fyo.Models.Code", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("DeviceId");

                    b.Property<string>("Digits");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("Player");

                    b.HasKey("ID");

                    b.HasIndex("DeviceId");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("Fyo.Models.Device", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IPAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<Guid>("UniqueIdentifier");

                    b.Property<string>("WirelessAccessPoint");

                    b.Property<string>("WirelessAccessPointIP");

                    b.HasKey("ID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Fyo.Models.DeviceSoftwareVersion", b =>
                {
                    b.Property<long>("DeviceId");

                    b.Property<long>("SoftwareVersionId");

                    b.HasKey("DeviceId", "SoftwareVersionId");

                    b.HasIndex("SoftwareVersionId");

                    b.ToTable("DeviceSoftwareVersions");
                });

            modelBuilder.Entity("Fyo.Models.Software", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Software");
                });

            modelBuilder.Entity("Fyo.Models.SoftwareVersion", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apk");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("SoftwareId");

                    b.Property<string>("Url");

                    b.Property<string>("Version");

                    b.HasKey("ID");

                    b.HasIndex("SoftwareId");

                    b.ToTable("SoftwareVersions");
                });

            modelBuilder.Entity("Fyo.Models.User", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("ThirdPartyUserId");

                    b.Property<int>("UserRole");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fyo.Models.Code", b =>
                {
                    b.HasOne("Fyo.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fyo.Models.DeviceSoftwareVersion", b =>
                {
                    b.HasOne("Fyo.Models.Device", "Device")
                        .WithMany("DeviceSoftwareVersions")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fyo.Models.SoftwareVersion", "SoftwareVersion")
                        .WithMany()
                        .HasForeignKey("SoftwareVersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fyo.Models.SoftwareVersion", b =>
                {
                    b.HasOne("Fyo.Models.Software", "Software")
                        .WithMany()
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
