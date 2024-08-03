using Microsoft.EntityFrameworkCore;

namespace MoviesReservation.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MoviesActors> MovieActors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieType> MovieTypes { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<MovieHall> MovieHalls { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ticketSeat> ticketSeats { get; set; }


    }
}
