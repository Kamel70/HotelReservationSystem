using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.DataAccess.Models
{
    public class HotelReservationDBContext:IdentityDbContext<ApplicationUser>
    {
        public HotelReservationDBContext(DbContextOptions<HotelReservationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<RoomImages> RoomImages { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>().Property(r => r.PricePerNight).HasPrecision(18, 4);
            modelBuilder.Entity<Reservation>().Property(r => r.TotalPrice).HasPrecision(18, 4);
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 4);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Hotel>()
                .HasOne<ApplicationUser>()
                .WithOne(u => u.hotel )
                .HasForeignKey<Hotel>(h => h.managerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Rooms)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomImages>()
                .HasOne(ri => ri.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
