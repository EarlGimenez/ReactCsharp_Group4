using ASI.Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ASI.Basecode.Data
{
    public partial class AsiBasecodeDBContext : DbContext
    {
        public AsiBasecodeDBContext()
        {
        }

        public AsiBasecodeDBContext(DbContextOptions<AsiBasecodeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);
                
                entity.HasIndex(e => e.Username)
                    .IsUnique()
                    .HasDatabaseName("UC_Users_Username");
                    
                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("UC_Users_Email");

                entity.Property(e => e.UserID)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Company)
                    .HasMaxLength(150);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .ValueGeneratedNever();  // Tell EF to always include this in INSERT

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                // Ignore old properties that don't exist in BookItDB
                entity.Ignore(e => e.Id);
                entity.Ignore(e => e.UserId);
                entity.Ignore(e => e.Name);
                entity.Ignore(e => e.Password);
                entity.Ignore(e => e.CreatedTime);
                entity.Ignore(e => e.UpdatedTime);
                entity.Ignore(e => e.UpdatedBy);
            });

            // Room entity configuration
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomID);

                entity.Property(e => e.RoomID)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TimeStart)
                    .IsRequired();

                entity.Property(e => e.TimeEnd)
                    .IsRequired();

                entity.Property(e => e.Purpose)
                    .HasMaxLength(100);

                entity.Property(e => e.ImageURL)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Booking entity configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingID);

                entity.Property(e => e.BookingID)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.RoomID)
                    .IsRequired();

                entity.Property(e => e.UserID)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BookingDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.StartTime)
                    .IsRequired();

                entity.Property(e => e.EndTime)
                    .IsRequired();

                entity.Property(e => e.Description);

                entity.Property(e => e.RecurrenceRule);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedAt)
                    .IsRequired()
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                // Foreign key relationships
                entity.HasOne(e => e.Room)
                    .WithMany()
                    .HasForeignKey(e => e.RoomID)
                    .HasConstraintName("FK_Bookings_Rooms")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserID)
                    .HasConstraintName("FK_Bookings_Users")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
