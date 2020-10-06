using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fieldsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_Room_RoomId",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Guest_GuestId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservation_Reservation_ReservationId",
                table: "RoomReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservation_Room_RoomId",
                table: "RoomReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomReservation",
                table: "RoomReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "RoomReservation",
                newName: "RoomReservations");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Maintenance",
                newName: "Maintenances");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_RoomReservation_ReservationId",
                table: "RoomReservations",
                newName: "IX_RoomReservations_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_RoomNumber",
                table: "Rooms",
                newName: "IX_Rooms_RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_GuestId",
                table: "Reservations",
                newName: "IX_Reservations_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_RoomId",
                table: "Maintenances",
                newName: "IX_Maintenances_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomReservations",
                table: "RoomReservations",
                columns: new[] { "RoomId", "ReservationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances",
                column: "MaintenanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Rooms_RoomId",
                table: "Maintenances",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_Reservations_ReservationId",
                table: "RoomReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservations_Rooms_RoomId",
                table: "RoomReservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Rooms_RoomId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_Reservations_ReservationId",
                table: "RoomReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomReservations_Rooms_RoomId",
                table: "RoomReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomReservations",
                table: "RoomReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "RoomReservations",
                newName: "RoomReservation");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameTable(
                name: "Maintenances",
                newName: "Maintenance");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomNumber",
                table: "Room",
                newName: "IX_Room_RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_RoomReservations_ReservationId",
                table: "RoomReservation",
                newName: "IX_RoomReservation_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_GuestId",
                table: "Reservation",
                newName: "IX_Reservation_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_RoomId",
                table: "Maintenance",
                newName: "IX_Maintenance_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomReservation",
                table: "RoomReservation",
                columns: new[] { "RoomId", "ReservationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance",
                column: "MaintenanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_Room_RoomId",
                table: "Maintenance",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Guest_GuestId",
                table: "Reservation",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservation_Reservation_ReservationId",
                table: "RoomReservation",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomReservation_Room_RoomId",
                table: "RoomReservation",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
