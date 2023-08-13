using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class initailcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieHall",
                columns: table => new
                {
                    HallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HallLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSeat = table.Column<int>(type: "int", nullable: false),
                    MovieHallHallId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieHall", x => x.HallId);
                    table.ForeignKey(
                        name: "FK_MovieHall_MovieHall_MovieHallHallId",
                        column: x => x.MovieHallHallId,
                        principalTable: "MovieHall",
                        principalColumn: "HallId");
                });

            migrationBuilder.CreateTable(
                name: "PricingDetails",
                columns: table => new
                {
                    PriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingDetails", x => x.PriceId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    PriceId = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_PricingDetails_PriceId",
                        column: x => x.PriceId,
                        principalTable: "PricingDetails",
                        principalColumn: "PriceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowTiming",
                columns: table => new
                {
                    TimingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    show_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    available_seats = table.Column<int>(type: "int", nullable: false),
                    MoviesMovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowTiming", x => x.TimingId);
                    table.ForeignKey(
                        name: "FK_ShowTiming_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timing_Id = table.Column<int>(type: "int", nullable: false),
                    NoOfBookedSeats = table.Column<int>(type: "int", nullable: false),
                    BookedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowTimingTimingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_Id);
                    table.ForeignKey(
                        name: "FK_Booking_ShowTiming_ShowTimingTimingId",
                        column: x => x.ShowTimingTimingId,
                        principalTable: "ShowTiming",
                        principalColumn: "TimingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ShowTimingTimingId",
                table: "Booking",
                column: "ShowTimingTimingId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieHall_MovieHallHallId",
                table: "MovieHall",
                column: "MovieHallHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PriceId",
                table: "Movies",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTiming_MoviesMovieId",
                table: "ShowTiming",
                column: "MoviesMovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "MovieHall");

            migrationBuilder.DropTable(
                name: "ShowTiming");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "PricingDetails");
        }
    }
}
