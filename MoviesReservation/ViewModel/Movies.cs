using MoviesReservation.Models;

namespace MoviesReservation.ViewModel
{
    public class Movies
    {
        public Movie Movie { get; set; }
        public string formattedDuration { get; set; }
        public IEnumerable<Genre>? Gener{ get; set; }
    }
}
