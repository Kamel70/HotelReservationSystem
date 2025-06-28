using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class makerelationbetweenroomanduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Room",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Room_UserId",
                table: "Room",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_AspNetUsers_UserId",
                table: "Room",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_AspNetUsers_UserId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_UserId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Room");
        }
    }
}
