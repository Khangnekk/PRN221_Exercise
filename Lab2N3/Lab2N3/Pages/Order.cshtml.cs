using Lab2N3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab2N3.Pages
{
	public class OrderModel : PageModel
	{
		private readonly NorthwindContext context;

		public OrderModel(NorthwindContext context)
		{
			this.context = context;
		}

		public List<Models.Order> Orders { get; set; }
		public List<Models.Employee> Employees { get; set; }
		public List<Models.Customer> Customers { get; set; }
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
		public bool isFilter { get; set; }
		public int? TotalPages { get; set; }
		public int pageSize = 42;
		public int CurrentPageNumber { get; set; }
		public void OnGet(int? pageNumber)
		{
			int pageNum = (pageNumber ?? 1);
			CurrentPageNumber = pageNum;
			isFilter = false;
			Orders = context.Orders
				.Include(c => c.Customer)
				.Include(e => e.Employee)
				.ToList();
			// Calculate the total number of pages
			int totalOrders = Orders.Count();
			TotalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

			var OrdersResult = Orders.Skip((pageNum - 1) * pageSize).Take(pageSize);
			Orders = OrdersResult.ToList();

			Customers = context.Customers.ToList();
			Employees = context.Employees.ToList();
		}

		public void OnGetAdd()
		{
			RedirectToPage("/Add");
		}

		public void OnPostFilter(int? pageNumber)
		{
			isFilter = true;
			int pageNum = (pageNumber ?? 1);
			CurrentPageNumber = pageNum;
			Customers = context.Customers.ToList();
			Employees = context.Employees.ToList();

			Orders = context.Orders
				.Include(c => c.Customer)
				.Include(e => e.Employee)
				.Where(e => EmployeeID == -1 || e.EmployeeId == EmployeeID)
				.Where(e => CustomerID == "-1" || e.CustomerId == CustomerID)
				.Where(e => e.OrderDate >= FromDate && e.OrderDate <= ToDate)
				.ToList();
			// Calculate the total number of pages
			int totalOrders = Orders.Count();
			TotalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

			var OrdersResult = Orders.Skip((pageNum - 1) * pageSize).Take(pageSize);
			Orders = OrdersResult.ToList();
			switch (SortBy)
			{
				case "OrderDate":
					Orders = Orders.OrderByDescending(o => o.OrderDate).ToList();
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
