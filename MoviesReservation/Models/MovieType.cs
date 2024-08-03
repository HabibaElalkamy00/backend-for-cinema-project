namespace MoviesReservation.Models
{
    public class MovieType
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int TypeId { get; set; }
        public Types Type { get; set; }
    }
}
