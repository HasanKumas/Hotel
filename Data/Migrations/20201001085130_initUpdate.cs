using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Room",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Room",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomNumber",
                table: "Room",
                column: "RoomNumber",
                unique: true,
                filter: "[RoomNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Room_RoomNumber",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Room",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reservation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
