using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pagyeonja.Entities.Entities;

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

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<BookingRequest> BookingRequests { get; set; }

    public virtual DbSet<Commuter> Commuters { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<RideHistory> RideHistories { get; set; }

    public virtual DbSet<Rider> Riders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<Suspension> Suspensions { get; set; }

    public virtual DbSet<TopupHistory> TopupHistories { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

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
            entity.Property(e => e.ApprovalStatus).HasColumnName("approval_status");
            entity.Property(e => e.RejectionMessage)
                .IsUnicode(false)
                .HasColumnName("rejection_message");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<BookingRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BookingRequest");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingStatus).HasColumnName("booking_status");
            entity.Property(e => e.CommuterId).HasColumnName("commuter_id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.DropoffLat).HasColumnName("dropoff_lat");
            entity.Property(e => e.DropoffLng).HasColumnName("dropoff_lng");
            entity.Property(e => e.DropoffLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dropoff_location");
            entity.Property(e => e.NumberOfPassengers)
                .HasDefaultValueSql("((1))")
                .HasColumnName("number_of_passengers");
            entity.Property(e => e.PickupLat).HasColumnName("pickup_lat");
            entity.Property(e => e.PickupLng).HasColumnName("pickup_lng");
            entity.Property(e => e.PickupLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pickup_location");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
        });

        modelBuilder.Entity<Commuter>(entity =>
        {
            entity.ToTable("Commuter");

            entity.Property(e => e.CommuterId)
                .ValueGeneratedNever()
                .HasColumnName("commuter_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ApprovalStatus).HasColumnName("approval_status");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("civil_status");
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
                .HasColumnName("middle_name");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("occupation");
            entity.Property(e => e.ProfilePath)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("profile_path");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sex");
            entity.Property(e => e.SuspensionStatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("suspension_status");
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

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CommuterId).HasColumnName("commuter_id");
            entity.Property(e => e.Rate)
                .HasDefaultValueSql("((0))")
                .HasColumnName("rate");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
        });

        modelBuilder.Entity<RideHistory>(entity =>
        {
            entity.ToTable("RideHistory");

            entity.Property(e => e.RideHistoryId)
                .ValueGeneratedNever()
                .HasColumnName("ride_history_id");
            entity.Property(e => e.CommuterId).HasColumnName("commuter_id");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
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
            entity.Property(e => e.ApprovalStatus).HasColumnName("approval_status");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("((0))")
                .HasColumnName("balance");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("civil_status");
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
            entity.Property(e => e.ProfilePath)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("profile_path");
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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Suspension>(entity =>
        {
            entity.ToTable("Suspension");

            entity.Property(e => e.SuspensionId)
                .ValueGeneratedNever()
                .HasColumnName("suspension_id");
            entity.Property(e => e.InvokedSuspensionDate)
                .HasColumnType("date")
                .HasColumnName("invoked_suspension_date");
            entity.Property(e => e.Reason)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.SuspensionDate)
                .HasColumnType("date")
                .HasColumnName("suspension_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<TopupHistory>(entity =>
        {
            entity.HasKey(e => e.TopupId);

            entity.ToTable("TopupHistory");

            entity.Property(e => e.TopupId)
                .ValueGeneratedNever()
                .HasColumnName("topup_id");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("status");
            entity.Property(e => e.TopupAfter)
                .HasDefaultValueSql("((0))")
                .HasColumnName("topup_after");
            entity.Property(e => e.TopupAmount)
                .HasDefaultValueSql("((0))")
                .HasColumnName("topup_amount");
            entity.Property(e => e.TopupBefore)
                .HasDefaultValueSql("((0))")
                .HasColumnName("topup_before");
            entity.Property(e => e.TopupDate)
                .HasColumnType("datetime")
                .HasColumnName("topup_date");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("transaction_id");
            entity.Property(e => e.CommuterId).HasColumnName("commuter_id");
            entity.Property(e => e.EndDestination)
                .IsUnicode(false)
                .HasColumnName("end_destination");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.Fare)
                .HasDefaultValueSql("((0))")
                .HasColumnName("fare");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
            entity.Property(e => e.StartingPoint)
                .IsUnicode(false)
                .HasColumnName("starting_point");
            entity.Property(e => e.StartingTime)
                .HasColumnType("datetime")
                .HasColumnName("starting_time");
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
