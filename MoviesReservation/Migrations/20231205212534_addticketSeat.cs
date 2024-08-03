using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesReservation.Migrations
{
    /// <inheritdoc />
    public partial class addticketSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticketSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketId = table.Column<int>(type: "int", nullable: false),
                    seatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticketSeats_Seats_seatId",
                        column: x => x.seatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticketSeats_Tickets_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ticketSeats_seatId",
                table: "ticketSeats",
                column: "seatId");

            migrationBuilder.CreateIndex(
                name: "IX_ticketSeats_ticketId",
                table: "ticketSeats",
                column: "ticketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticketSeats");
        }
    }
}
