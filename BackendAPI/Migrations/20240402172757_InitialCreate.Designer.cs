﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendAPI.Migrations
{
    [DbContext(typeof(BarberDBContext))]
    [Migration("20240402172757_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BackendAPI.Models.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Users_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "PhoneNumber" }, "IX_Users_PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackendAPI.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("WorkerTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "WorkerTypeId" }, "IX_Workers_WorkerTypeId");

                    b.ToTable("Workers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "andrija223@yahoo.com",
                            LastName = "Lazic",
                            Name = "andrija",
                            PasswordHash = new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 },
                            PasswordSalt = new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 },
                            PhoneNumber = "0695561004",
                            WorkerTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "brzi223@yahoo.com",
                            LastName = "Brzi",
                            Name = "Jovan",
                            PasswordHash = new byte[] { 226, 69, 162, 174, 135, 255, 224, 2, 134, 185, 155, 33, 139, 179, 236, 92, 216, 240, 52, 233, 32, 60, 40, 72, 208, 125, 151, 114, 143, 242, 37, 238, 190, 137, 44, 14, 71, 102, 39, 252, 116, 87, 158, 169, 133, 226, 112, 39, 86, 156, 143, 108, 122, 92, 218, 233, 63, 197, 228, 255, 191, 203, 46, 123 },
                            PasswordSalt = new byte[] { 25, 75, 241, 133, 182, 152, 139, 242, 147, 42, 73, 74, 198, 150, 116, 157, 86, 66, 177, 12, 91, 123, 236, 175, 247, 82, 167, 235, 182, 97, 128, 216, 80, 111, 218, 110, 245, 30, 132, 238, 246, 228, 17, 62, 185, 169, 189, 4, 21, 76, 77, 24, 232, 119, 122, 42, 209, 92, 35, 104, 236, 10, 251, 147, 78, 71, 20, 24, 171, 6, 152, 215, 232, 130, 127, 112, 184, 29, 108, 236, 31, 111, 107, 2, 54, 252, 247, 163, 154, 34, 83, 170, 215, 86, 19, 66, 181, 133, 163, 62, 73, 160, 170, 202, 127, 82, 160, 138, 176, 200, 13, 137, 71, 60, 218, 239, 6, 12, 51, 234, 237, 121, 152, 229, 4, 155, 119, 145 },
                            PhoneNumber = "0695561004",
                            WorkerTypeId = 2
                        });
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerCommunication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UnreadMessages")
                        .HasColumnType("int");

                    b.Property<int>("User1")
                        .HasColumnType("int");

                    b.Property<int>("User2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "User1", "User2" }, "IX_WorkerCommunication")
                        .IsUnique();

                    b.ToTable("WorkerCommunication", (string)null);
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommunicationId")
                        .HasColumnType("int")
                        .HasColumnName("CommunicationID");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("SenderID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CommunicationId" }, "IX_WorkerMessages_CommunicationID");

                    b.ToTable("WorkerMessages");
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("WorkerType1")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("WorkerType");

                    b.HasKey("Id");

                    b.ToTable("WorkerTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            WorkerType1 = "Menadzer"
                        },
                        new
                        {
                            Id = 2,
                            WorkerType1 = "Frizer"
                        });
                });

            modelBuilder.Entity("BackendAPI.Models.Worker", b =>
                {
                    b.HasOne("BackendAPI.Models.WorkerType", "WorkerType")
                        .WithMany("Workers")
                        .HasForeignKey("WorkerTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Workers_WorkerTypes");

                    b.Navigation("WorkerType");
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerCommunication", b =>
                {
                    b.HasOne("BackendAPI.Models.Worker", "User1Navigation")
                        .WithMany("WorkerCommunications")
                        .HasForeignKey("User1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_WorkerCommunication_Workers");

                    b.Navigation("User1Navigation");
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerMessage", b =>
                {
                    b.HasOne("BackendAPI.Models.WorkerCommunication", "Communication")
                        .WithMany("WorkerMessages")
                        .HasForeignKey("CommunicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_WorkerMessages_WorkerCommunication");

                    b.Navigation("Communication");
                });

            modelBuilder.Entity("BackendAPI.Models.Worker", b =>
                {
                    b.Navigation("WorkerCommunications");
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerCommunication", b =>
                {
                    b.Navigation("WorkerMessages");
                });

            modelBuilder.Entity("BackendAPI.Models.WorkerType", b =>
                {
                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
