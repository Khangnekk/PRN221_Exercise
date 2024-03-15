using Lab2N3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab2N3.Pages
{
	public class OrderDetailModel : PageModel
	{
		private readonly NorthwindContext context;

		public OrderDetailModel(NorthwindContext context)
		{
			this.context = context;
		}

		public List<Models.OrderDetail> OrderDetails { get; set; }

		public void OnGet(int OrderID)
		{
			OrderDetails = context.OrderDetails
				.Include(od => od.Product)
				.Where(od => od.OrderId == OrderID)
				.ToList();
		}
	}
}
