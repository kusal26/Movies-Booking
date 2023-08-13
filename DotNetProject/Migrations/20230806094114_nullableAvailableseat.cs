using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class nullableAvailableseat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTiming_ShowTiming_ShowTimingTimingId",
                table: "ShowTiming");

            migrationBuilder.DropIndex(
                name: "IX_ShowTiming_ShowTimingTimingId",
                table: "ShowTiming");

            migrationBuilder.DropColumn(
                name: "ShowTimingTimingId",
                table: "ShowTiming");

            migrationBuilder.AlterColumn<int>(
                name: "available_seats",
                table: "ShowTiming",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "available_seats",
                table: "ShowTiming",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShowTimingTimingId",
                table: "ShowTiming",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTiming_ShowTimingTimingId",
                table: "ShowTiming",
                column: "ShowTimingTimingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTiming_ShowTiming_ShowTimingTimingId",
                table: "ShowTiming",
                column: "ShowTimingTimingId",
                principalTable: "ShowTiming",
                principalColumn: "TimingId");
        }
    }
}
