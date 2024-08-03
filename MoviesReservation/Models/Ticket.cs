namespace MoviesReservation.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public int? CardNumber { get; set; }

        public int? CVV { get; set;}

        public DateTime? ExpiryDate { get; set; }


        public int MovieHallId { get; set; }
        public MovieHall MovieHall { get; set; }

    }
}
