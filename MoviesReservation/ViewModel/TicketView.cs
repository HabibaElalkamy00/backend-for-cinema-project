using MoviesReservation.Models;

namespace MoviesReservation.ViewModel
{
    public class TicketView
    {
        public Ticket ticket { get; set; }
        public List<ticketSeat> ticketSeats { get; set; }

    }
}
