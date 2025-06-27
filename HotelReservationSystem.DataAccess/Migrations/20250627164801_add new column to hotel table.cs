using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumntohoteltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "managerId",
                table: "Hotel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_managerId",
                table: "Hotel",
                column: "managerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_AspNetUsers_managerId",
                table: "Hotel",
                column: "managerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_AspNetUsers_managerId",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_managerId",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "managerId",
                table: "Hotel");
        }
    }
}
