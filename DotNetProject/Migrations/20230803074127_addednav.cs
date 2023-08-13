using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class addednav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTiming_Movies_MoviesMovieId",
                table: "ShowTiming");

            migrationBuilder.DropIndex(
                name: "IX_ShowTiming_MoviesMovieId",
                table: "ShowTiming");

            migrationBuilder.DropColumn(
                name: "MoviesMovieId",
                table: "ShowTiming");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTiming_HallId",
                table: "ShowTiming",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTiming_MovieId",
                table: "ShowTiming",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTiming_MovieHall_HallId",
                table: "ShowTiming",
                column: "HallId",
                principalTable: "MovieHall",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTiming_Movies_MovieId",
                table: "ShowTiming",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTiming_MovieHall_HallId",
                table: "ShowTiming");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTiming_Movies_MovieId",
                table: "ShowTiming");

            migrationBuilder.DropIndex(
                name: "IX_ShowTiming_HallId",
                table: "ShowTiming");

            migrationBuilder.DropIndex(
                name: "IX_ShowTiming_MovieId",
                table: "ShowTiming");

            migrationBuilder.AddColumn<int>(
                name: "MoviesMovieId",
                table: "ShowTiming",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTiming_MoviesMovieId",
                table: "ShowTiming",
                column: "MoviesMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTiming_Movies_MoviesMovieId",
                table: "ShowTiming",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");
        }
    }
}
