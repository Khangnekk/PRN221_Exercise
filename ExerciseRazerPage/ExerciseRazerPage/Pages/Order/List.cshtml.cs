using ExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExerciseRazerPage.Pages.Order
{
	public class ListModel : PageModel
	{
		private readonly NorthwindContext _context;

		public ListModel(NorthwindContext context)
		{
			_context = context;
		}
		public List<Models.Order> Orders { get; set; }
		public void OnGet()
		{
			Orders = _context.Orders
				.Include(c => c.Customer)
				.Include(e => e.Employee)
				.ToList();
		}

		public void OnGetAdd()
		{
			RedirectToPage("/Add");
		}
	}
}
