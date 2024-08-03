using MoviesReservation.Models;

namespace MoviesReservation.ViewModel
{
    public class DetailMovie
    {
        public Movie Movie { get; set; }
        public List<Actors> Actors { get; set; }
        public List<Genre> genres { get; set; }

        public List<Types> types { get; set; }
        public string formattedDuration { get; set; }

        public List<Movies> RelatedMovie { get; set; }
    }
}
