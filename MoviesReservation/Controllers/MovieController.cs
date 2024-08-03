using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesReservation.Models;
using MoviesReservation.ViewModel;

namespace MoviesReservation.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _MyDatabase;
        public MovieController(ApplicationDbContext MyDatabase)
        {
            _MyDatabase = MyDatabase;
        }
        public IActionResult Index(int ?id)
        {

            List<Movies> movie = new List<Movies>();
            List<Movie> movies = new List<Movie>();
            if (id == null)
            {
                TempData["Title"] = "All Movie Page";
                movies= _MyDatabase.Movies.ToList();
               


            }
            else if (id== 0)
            {
                TempData["Title"] = "Popular Movie Page";
                var mov= _MyDatabase.Movies.ToList();
                Random random = new Random();

                movies = mov.OrderBy(x => random.Next()).Take(10).ToList();
                
            }
            else if (id == 1)
            {
                ViewData["Title"] = "2D Movie Page";
                movies = _MyDatabase.MovieTypes.Include(m=>m.Movie).Where(m=>m.TypeId==2).Select(m=>m.Movie).ToList();
                
            }
            else if (id == 2)
            {
                ViewData["Title"] = "3D Movie Page";
                movies = _MyDatabase.MovieTypes.Include(m => m.Movie).Where(m => m.TypeId == 3).Select(m => m.Movie).ToList();
             
            }
            else if (id == 3)
            {
                ViewData["Title"] = "Kids Movie Page";
                movies = _MyDatabase.MovieTypes.Include(m => m.Movie).Where(m => m.TypeId == 5).Select(m => m.Movie).ToList();
                
            }
            else if (id == 4)
            {
                movies = _MyDatabase.MovieTypes.Include(m => m.Movie).Where(m => m.TypeId == 4).Select(m => m.Movie).ToList();
               
                ViewData["Title"] = "Comming Soon Movie Page";
            }

            foreach (var mov in movies)
            {
                var genre = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == mov.Id).ToList();
                List<Genre> gen = new List<Genre>();
                foreach (var gen1 in genre)
                {
                    gen.Add(gen1.Genre);
                }
                TimeSpan duration = TimeSpan.FromMinutes(mov.DurationInMinutes);
                Movies m = new Movies()
                {
                    Gener = gen,
                    formattedDuration = $"{duration.Hours}h {duration.Minutes}m",
                    Movie = mov
                };
                movie.Add(m);
            }
            return View(movie);
        }
    
        public IActionResult Detail(int id)
        {
            var movie=_MyDatabase.Movies.SingleOrDefault(m => m.Id == id);
            DetailMovie detailMovie = new DetailMovie();
            if (movie != null)
            {
                var MovieGenres = _MyDatabase.MovieGenres.Include(n=>n.Genre).Where(m=>m.MovieId == id).ToList();
                List<Genre> genres = new List<Genre>();
                foreach(var gen in MovieGenres)
                {
                    genres.Add(gen.Genre);
                }
                var MovieActors = _MyDatabase.MovieActors.Include(n => n.Actor).Where(m => m.MovieId == id).ToList();
                List<Actors> actors = new List<Actors>();
                foreach (var act in MovieActors)
                {
                    actors.Add(act.Actor);
                }
                var MovieTypes = _MyDatabase.MovieTypes.Include(n => n.Type).Where(m => m.MovieId == id).ToList();
                List<Types> types = new List<Types>();
                foreach (var ty in MovieTypes)
                {
                    types.Add(ty.Type);
                }
                TimeSpan duration = TimeSpan.FromMinutes(movie.DurationInMinutes);
                detailMovie.Actors = actors;
                detailMovie.genres = genres;
                detailMovie.Movie = movie;
                detailMovie.types = types;
                detailMovie.formattedDuration = $"{duration.Hours}h {duration.Minutes}m";
            
                List<Movies> AllMoveWithSameGnre = new List<Movies>();
                int counter = 0;
                foreach(var gen in genres)
                {
                    if (counter >= 4)
                    {
                        break;
                    }
                    var allgen = _MyDatabase.MovieGenres.Include(n => n.Movie).Include(n=>n.Genre).Where(n => n.GenreId == gen.Id).ToList();
                    foreach(var MoviesWithGenre in allgen)
                    {
                        Movies movies = new Movies();
                        movies.Movie = MoviesWithGenre.Movie;
                        var RealGenre= _MyDatabase.MovieGenres.Include(n => n.Genre).Where(n => n.MovieId == MoviesWithGenre.Movie.Id).ToList();
                        var ListOfGenre= new List<Genre>();
                        foreach(var i in RealGenre)
                        {
                            ListOfGenre.Add(i.Genre);
                        }
                        movies.Gener = ListOfGenre;
                        TimeSpan duration15 = TimeSpan.FromMinutes(MoviesWithGenre.Movie.DurationInMinutes);
                        movies.formattedDuration = $"{duration15.Hours}h {duration15.Minutes}m";
                        AllMoveWithSameGnre.Add(movies);
                         counter++;
                        if (counter >= 4)
                        {
                            break;
                        }
                        
                    }
                    
                }
                detailMovie.RelatedMovie = AllMoveWithSameGnre;
            }
            return View(detailMovie);
        }
    }
}
