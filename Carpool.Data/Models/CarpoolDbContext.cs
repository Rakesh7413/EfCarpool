using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Carpool.Data.Models
{
    public partial class CarpoolDbContext : DbContext
    {
        public CarpoolDbContext()
        {
        }

        public CarpoolDbContext(DbContextOptions<CarpoolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<PriceLimit> PriceLimit { get; set; }
        public virtual DbSet<Rides> Rides { get; set; }
        public virtual DbSet<RouteInformations> RouteInformations { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Carpool;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK_Booking");

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CostOfBooking).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.CarNo)
                    .HasName("PK_VehicleDetails");

                entity.Property(e => e.CarNo).HasMaxLength(128);
            });

            modelBuilder.Entity<Places>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<PriceLimit>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Rides>(entity =>
            {
                entity.HasKey(e => e.RideId)
                    .HasName("PK_Ris");

                entity.Property(e => e.CarNumber).IsRequired();

                entity.Property(e => e.DateOfRide).HasColumnType("datetime");

                entity.Property(e => e.Destination).IsRequired();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.PricePerKilometer).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Source).IsRequired();

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Password).HasMaxLength(16);

                entity.Property(e => e.PetName).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.UserName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
