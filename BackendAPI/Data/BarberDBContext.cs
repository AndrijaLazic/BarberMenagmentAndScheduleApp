using System;
using System.Collections.Generic;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendAPI.Data
{
    public partial class BarberDBContext : DbContext
    {
        public BarberDBContext()
        {
        }

        public BarberDBContext(DbContextOptions<BarberDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;
        public virtual DbSet<WorkerCommunication> WorkerCommunications { get; set; } = null!;
        public virtual DbSet<WorkerMessage> WorkerMessages { get; set; } = null!;
        public virtual DbSet<WorkerType> WorkerTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6F991P0;Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "IX_Users_Email")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "IX_Users_PhoneNumber")
                    .IsUnique();
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasIndex(e => e.WorkerTypeId, "IX_Workers_WorkerTypeId");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.WorkerType)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.WorkerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workers_WorkerTypes");
            });

            modelBuilder.Entity<WorkerCommunication>(entity =>
            {
                entity.ToTable("WorkerCommunication");

                entity.HasIndex(e => new { e.User1, e.User2 }, "IX_WorkerCommunication")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.User1Navigation)
                    .WithMany(p => p.WorkerCommunications)
                    .HasForeignKey(d => d.User1)
                    .HasConstraintName("FK_WorkerCommunication_Workers");
            });

            modelBuilder.Entity<WorkerMessage>(entity =>
            {
                entity.HasIndex(e => e.CommunicationId, "IX_WorkerMessages_CommunicationID");

                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Communication)
                    .WithMany(p => p.WorkerMessages)
                    .HasForeignKey(d => d.CommunicationId)
                    .HasConstraintName("FK_WorkerMessages_WorkerCommunication");
            });

            modelBuilder.Entity<WorkerType>(entity =>
            {
                entity.Property(e => e.WorkerType1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WorkerType");
            });

            WorkerType workertype1 = new WorkerType
            {
                Id = 1,
                WorkerType1 = "Menadzer"
            };
            WorkerType workertype2 = new WorkerType
            {
                Id = 2,
                WorkerType1 = "Frizer"
            };

            Worker worker1 = new Worker
            {
                Id = 1,
                Email = "andrija223@yahoo.com",
                Name = "andrija",
                LastName = "Lazic",
                PasswordHash = Convert.FromHexString("E245A2AE87FFE00286B99B218BB3EC5CD8F034E9203C2848D07D97728FF225EEBE892C0E476627FC74579EA985E27027569C8F6C7A5CDAE93FC5E4FFBFCB2E7B"),
                PasswordSalt = Convert.FromHexString("194BF185B6988BF2932A494AC696749D5642B10C5B7BECAFF752A7EBB66180D8506FDA6EF51E84EEF6E4113EB9A9BD04154C4D18E8777A2AD15C2368EC0AFB934E471418AB0698D7E8827F70B81D6CEC1F6F6B0236FCF7A39A2253AAD7561342B585A33E49A0AACA7F52A08AB0C80D89473CDAEF060C33EAED7998E5049B7791"),
                WorkerTypeId = 1,
                PhoneNumber = "0695561004"
            };

            Worker worker2 = new Worker
            {
                Id = 2,
                Email = "brzi223@yahoo.com",
                Name = "Jovan",
                LastName = "Brzi",
                PasswordHash = Convert.FromHexString("E245A2AE87FFE00286B99B218BB3EC5CD8F034E9203C2848D07D97728FF225EEBE892C0E476627FC74579EA985E27027569C8F6C7A5CDAE93FC5E4FFBFCB2E7B"),
                PasswordSalt = Convert.FromHexString("194BF185B6988BF2932A494AC696749D5642B10C5B7BECAFF752A7EBB66180D8506FDA6EF51E84EEF6E4113EB9A9BD04154C4D18E8777A2AD15C2368EC0AFB934E471418AB0698D7E8827F70B81D6CEC1F6F6B0236FCF7A39A2253AAD7561342B585A33E49A0AACA7F52A08AB0C80D89473CDAEF060C33EAED7998E5049B7791"),
                WorkerTypeId = 2,
                PhoneNumber = "0695561004"
            };

            modelBuilder.Entity<WorkerType>().HasData(workertype1, workertype2);
            modelBuilder.Entity<Worker>().HasData(worker1, worker2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
