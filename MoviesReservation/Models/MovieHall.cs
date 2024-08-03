namespace MoviesReservation.Models
{
    public class MovieHall
    {

        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public DateTime StartDateTime { get; set; }

        public float price { get; set; }
    }
}

