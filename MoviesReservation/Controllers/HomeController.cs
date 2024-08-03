using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesReservation.Models;
using MoviesReservation.ViewModel;
using System.Diagnostics;

namespace MoviesReservation.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext _MyDatabase; 
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext MyDatabase )
		{
			_logger = logger;
			_MyDatabase = MyDatabase;
		}

		public IActionResult Index()
		{
            if (TempData.ContainsKey("Validation"))
            {
             
                var validationValue = ViewData["Validation"];
            }
            var AllMovies=_MyDatabase.Movies.ToList();
			var topMovies = AllMovies.FirstOrDefault(m => m.Id == 2);
			var comming = _MyDatabase.MovieTypes.Where(m => m.TypeId == 4).Select(m => m.Movie).ToList();
            HomeView homeView = new HomeView()
            {
                TopMovie = new Movies() { Movie = topMovies, Gener = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == 2).Select(m => m.Genre).ToList(), formattedDuration = $"{TimeSpan.FromMinutes(topMovies.DurationInMinutes).Hours}h {TimeSpan.FromMinutes(topMovies.DurationInMinutes).Minutes}m" },
                CommingSoon = new List<Movies>(),
                BoxOffice = new List<Movies>(),
                Popular = new List<Movies>(),
                Week = new List<Movies>() ,
            };
            foreach (var movie in comming)
			{
				var GenreList = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == movie.Id).Select(m => m.Genre).ToList();
				Movies obj =new Movies() { 
					Movie=movie,
                    formattedDuration = $"{TimeSpan.FromMinutes(movie.DurationInMinutes).Hours}h {TimeSpan.FromMinutes(movie.DurationInMinutes).Minutes}m",
					Gener= GenreList
                };
                homeView.CommingSoon.Add(obj);
            }

            foreach (var movie in AllMovies.GetRange(50, 4))
            {
                var GenreList = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == movie.Id).Select(m => m.Genre).ToList();
                Movies obj = new Movies()
                {
                    Movie = movie,
                    formattedDuration = $"{TimeSpan.FromMinutes(movie.DurationInMinutes).Hours}h {TimeSpan.FromMinutes(movie.DurationInMinutes).Minutes}m",
                    Gener = GenreList
                };
                homeView.BoxOffice.Add(obj);
            }
            foreach (var movie in AllMovies.Take(4).ToList())
            {
                var GenreList = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == movie.Id).Select(m => m.Genre).ToList();
                Movies obj = new Movies()
                {
                    Movie = movie,
                    formattedDuration = $"{TimeSpan.FromMinutes(movie.DurationInMinutes).Hours}h {TimeSpan.FromMinutes(movie.DurationInMinutes).Minutes}m",
                    Gener = GenreList
                };
                homeView.Popular.Add(obj);
            }
            foreach (var movie in AllMovies.Skip(4).Take(4).ToList())
            {
                var GenreList = _MyDatabase.MovieGenres.Include(m => m.Genre).Where(m => m.MovieId == movie.Id).Select(m => m.Genre).ToList();
                Movies obj = new Movies()
                {
                    Movie = movie,
                    formattedDuration = $"{TimeSpan.FromMinutes(movie.DurationInMinutes).Hours}h {TimeSpan.FromMinutes(movie.DurationInMinutes).Minutes}m",
                    Gener = GenreList
                };
                homeView.Week.Add(obj);
            }
            return View(homeView);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
