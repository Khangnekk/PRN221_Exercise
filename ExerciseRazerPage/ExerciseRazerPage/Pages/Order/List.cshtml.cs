using ExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc;
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
		public List<Models.Employee> employees { get; set; }
		public List<Models.Customer> customers { get; set; }
		#region
		[BindProperty]
		public int? EmployeeID { get; set; }
		[BindProperty]
		public string? CustomerID { get; set; }
		[BindProperty]
		public DateTime? FromDate { get; set; }
		[BindProperty]
		public DateTime? ToDate { get; set; }
		[BindProperty]
		public string SortBy { get; set; }
		[BindProperty]
		public string? Message { get; set; }
		#endregion
		public void OnGet()
		{
			Orders = _context.Orders
				.Include(c => c.Customer)
				.Include(e => e.Employee)
				.ToList();
			customers = _context.Customers.ToList();
			employees = _context.Employees.ToList();
		}

		public void OnGetAdd()
		{
			RedirectToPage("/Add");
		}

		public void OnPost()
		{
			customers = _context.Customers.ToList();
			employees = _context.Employees.ToList();

			Orders = _context.Orders
				.Include(c => c.Customer)
				.Include(e => e.Employee)
				.Where(e => EmployeeID == -1 || e.EmployeeId == EmployeeID)
				.Where(e => CustomerID == "-1" || e.CustomerId == CustomerID)
				.Where(e => e.OrderDate >= FromDate && e.OrderDate <= ToDate)
				.ToList();
			switch (SortBy)
			{
				case "OrderDate":
					Orders = Orders.OrderBy(o => o.OrderDate).ToList();
					break;
				case "Freight":
					Orders = Orders.OrderBy(o => o.Freight).ToList();
					break;
				default:
					break;
			}
		}
	}
}