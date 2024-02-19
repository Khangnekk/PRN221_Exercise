using ExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseRazerPage.Pages.Employee
{
	public class ListModel : PageModel
	{
		private readonly NorthwindContext _context;
		public ListModel(NorthwindContext context)
		{
			_context = context;
		}
		public List<Models.Employee> employees { get; set; }
		public void OnGet()
		{
			employees = _context.Employees.ToList();
		}

		public void OnGetDetail()
		{
			RedirectToPage("/Employee/Detail");
		}
	}
}
