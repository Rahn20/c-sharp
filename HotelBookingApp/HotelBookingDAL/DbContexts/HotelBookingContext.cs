using System;
using System.Linq;
using System.Text;
using HotelBookingDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;


namespace HotelBookingDAL.DbContexts
{
    /// <summary>
    ///   Represents the database context for the hotel booking system, managing access to the database tables 
    ///   for guests, rooms, payments, and bookings.
    /// </summary>
    public class HotelBookingContext : DbContext
    {
        /// <summary>
        ///   Represents the Guests table in the database.
        /// </summary>
        public DbSet<Guest> Guests { get; set; }

        /// <summary>
        ///   Represents the Rooms table in the database.
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        ///  Represents the Payments table in the database.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        ///  Represents the Bookings table in the database.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // for testing purpose
            string? dbProvider = Environment.GetEnvironmentVariable("HOTELBOOKINGAPP_DB_PROVIDER");

            if (dbProvider != null && dbProvider == "InMemory")
            {
                // Unit Testing with In-Memory Database
                optionsBuilder.UseInMemoryDatabase("HotelBookingTestDB");
                optionsBuilder.EnableSensitiveDataLogging();
            }
            else
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=HotelBookingApp;Trusted_Connection=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF (cascading delete by default)
            // Configure Guest relationships
            modelBuilder.Entity<Guest>()
                .HasMany(g => g.Payments)
                .WithOne(p => p.Guest)
                .HasForeignKey(p => p.GuestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Guest>()
               .HasMany(g => g.Bookings)
               .WithOne(b => b.Guest)
               .HasForeignKey(b => b.GuestId)
               .OnDelete(DeleteBehavior.Cascade);   // Deleting a Guest deletes associated Bookings.

            // Configure Booking relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a Booking deletes its Payment.

            modelBuilder.Entity<Booking>()
               .HasOne(b => b.Guest)
               .WithMany(g => g.Bookings)
               .HasForeignKey(b => b.GuestId)
               .OnDelete(DeleteBehavior.Restrict); // Deleting a Booking does not delete its Guest.

            // KOntrollera sen
            modelBuilder.Entity<Booking>()
              .HasOne(b => b.Room)
              .WithMany(r => r.Bookings)
              .HasForeignKey(b => b.RoomId)
              .OnDelete(DeleteBehavior.Restrict);  // Deleting a Booking does not delete its Room.

            // Configure Room relationships
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a Room deletes associated Bookings.


            // Default values and precision
            modelBuilder.Entity<Booking>()
                .Property(b => b.IsPaid)
                .HasDefaultValue(false);

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentUpdatedDate)
                .IsRequired(false);

            // Payments and Bookings relations
            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Booking)
            //    .WithOne(b => b.Payment)
            //    .HasForeignKey<Payment>(p => p.BookingId);
            //.OnDelete(DeleteBehavior.Restrict); // When a Payment is deleted, its Booking record is not deleted.

            // Precision for monetary fields, Configure decimal precision
            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
