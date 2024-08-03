using MoviesReservation.Models;

namespace MoviesReservation.ViewModel
{
    public class HomeView
    {
        public Movies TopMovie { get; set; }
        public List<Movies> Popular{ get; set; }
        public List<Movies> CommingSoon { get; set; }
        public List<Movies> BoxOffice { get; set; }

        public List<Movies> Week {  get; set; }
    }
}
