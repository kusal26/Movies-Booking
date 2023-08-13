using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class error1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ShowTiming_ShowTimingTimingId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ShowTimingTimingId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ShowTimingTimingId",
                table: "Booking");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Timing_Id",
                table: "Booking",
                column: "Timing_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ShowTiming_Timing_Id",
                table: "Booking",
                column: "Timing_Id",
                principalTable: "ShowTiming",
                principalColumn: "TimingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ShowTiming_Timing_Id",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_Timing_Id",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "ShowTimingTimingId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ShowTimingTimingId",
                table: "Booking",
                column: "ShowTimingTimingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ShowTiming_ShowTimingTimingId",
                table: "Booking",
                column: "ShowTimingTimingId",
                principalTable: "ShowTiming",
                principalColumn: "TimingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
