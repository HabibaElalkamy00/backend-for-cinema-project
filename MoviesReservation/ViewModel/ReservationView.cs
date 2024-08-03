using MoviesReservation.Models;

namespace MoviesReservation.ViewModel
{
    public class ReservationView
    {
        public MovieHall PickedMovie { get; set; }
        public MovieHall PickedDate { get; set; }
        public IEnumerable<MovieHall> AllMovieTopick {  get; set; }

        public IEnumerable<DateView> AllAvaliableDay { get; set; }
        public IEnumerable<SeatView> AllSeat { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
