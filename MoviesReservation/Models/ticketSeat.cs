using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesReservation.Models
{
    public class ticketSeat
    {
        public int Id { get; set; }
        [ForeignKey("ticketId")]
        public int ticketId { get; set; }
        public Ticket ticket { get; set; }
        [ForeignKey("seatId")]
        public int seatId { get; set; }
        public Seat seat { get; set; }

        public string qrCode { get; set; }


    }
}
