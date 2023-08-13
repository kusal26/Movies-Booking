using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieHall_MovieHall_MovieHallHallId",
                table: "MovieHall");

            migrationBuilder.DropIndex(
                name: "IX_MovieHall_MovieHallHallId",
                table: "MovieHall");

            migrationBuilder.DropColumn(
                name: "MovieHallHallId",
                table: "MovieHall");

            migrationBuilder.AddColumn<int>(
                name: "PricePerEach",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Totalprice",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerEach",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Totalprice",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "MovieHallHallId",
                table: "MovieHall",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieHall_MovieHallHallId",
                table: "MovieHall",
                column: "MovieHallHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieHall_MovieHall_MovieHallHallId",
                table: "MovieHall",
                column: "MovieHallHallId",
                principalTable: "MovieHall",
                principalColumn: "HallId");
        }
    }
}
