
using Hotel.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext (DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>()
                .Property(c => c.RoomType)
                .HasConversion<string>();
            modelBuilder.Entity<Room>()
                .HasIndex(rn => rn.RoomNumber)
                .IsUnique();
            modelBuilder.Entity<Room>()
                .Property(c => c.RoomSize)
                .HasConversion<string>();
            modelBuilder.Entity<Room>()
                .Property(c => c.Status)
                .HasConversion<string>();
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Status)
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
