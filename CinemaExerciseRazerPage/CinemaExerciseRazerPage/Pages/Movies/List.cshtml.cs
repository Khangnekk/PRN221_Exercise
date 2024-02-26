using CinemaExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaExerciseRazerPage.Pages.Movies
{
	public class ListModel : PageModel
	{
		private readonly CinemaContext _context;

		public ListModel(CinemaContext context)
		{
			_context = context;
		}
		public List<Models.Movie> Movies { get; set; }
		public List<Models.Star> Stars { get; set; }
		public void OnGet()
		{
			Movies = _context.Movies.ToList();
			Stars = _context.Stars.ToList();
		}
	}
}
