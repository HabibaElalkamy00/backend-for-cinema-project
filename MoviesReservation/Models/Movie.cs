namespace MoviesReservation.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public string Country { get; set; }
        public string Language { get; set; }

        public string Director { get; set; }
        public string Trailer { get; set; }

        public string Image { get; set; }

        
    }
}
