using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesReservation.Migrations
{
    /// <inheritdoc />
    public partial class creatColumnQRcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "qrCode",
                table: "ticketSeats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qrCode",
                table: "ticketSeats");
        }
    }
}
