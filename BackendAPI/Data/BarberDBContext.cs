using System;
using System.Collections.Generic;
using BackendAPI.Models.Database;
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
                optionsBuilder.UseSqlServer("Server=" + Environment.GetEnvironmentVariable("DATABASECONNECTION") + ";Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;");
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

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.HasOne(d => d.User1Navigation)
                    .WithMany(p => p.WorkerCommunications)
                    .HasForeignKey(d => d.User1)
                    .HasConstraintName("FK_WorkerCommunication_Workers");
            });

            modelBuilder.Entity<WorkerMessage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.HasOne(d => d.Communication)
                    .WithMany()
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
