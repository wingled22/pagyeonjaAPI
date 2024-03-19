using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pagyeonjaAPI.Entities;

public partial class HitchContext : DbContext
{
    public HitchContext()
    {
    }

    public HitchContext(DbContextOptions<HitchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Rider> Riders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Hitch;User ID=SA;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Approval>(entity =>
        {
            entity.ToTable("Approval");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApprovalDate)
                .HasColumnType("date")
                .HasColumnName("approval_date");
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("approval_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Document");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("document_name");
            entity.Property(e => e.DocumentPath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("document_path");
            entity.Property(e => e.DocumentView)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("document_view");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<Rider>(entity =>
        {
            entity.ToTable("Rider");

            entity.Property(e => e.RiderId)
                .ValueGeneratedNever()
                .HasColumnName("rider_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("approval_status");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.DateApplied)
                .HasColumnType("date")
                .HasColumnName("date_applied");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("occupation");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sex");
            entity.Property(e => e.SuspensionStatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("suspension_status");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vehicle_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
