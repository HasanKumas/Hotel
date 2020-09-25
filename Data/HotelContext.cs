using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext (DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Guest> Guest { get; set; }
        public DbSet<RoomReservation> RoomReservation { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>()
                .Property(c => c.RoomType)
                .HasConversion<string>();
            modelBuilder.Entity<Room>()
                .Property(c => c.RoomSize)
                .HasConversion<string>();
            modelBuilder.Entity<RoomReservation>()
                .HasKey(bc => new { bc.RoomId, bc.ReservationId });
            modelBuilder.Entity<RoomReservation>()
                .HasOne(bc => bc.Room)
                .WithMany(b => b.RoomReservations)
                .HasForeignKey(bc => bc.RoomId);
            modelBuilder.Entity<RoomReservation>()
                .HasOne(bc => bc.Reservation)
                .WithMany(c => c.RoomReservations)
                .HasForeignKey(bc => bc.ReservationId);
        }
    }
}
