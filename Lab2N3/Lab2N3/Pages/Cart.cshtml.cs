using Lab2N3.Logics;
using Lab2N3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Lab2N3.Pages
{
	public class CartModel : PageModel
	{
		private NorthwindContext context;
		private IHttpContextAccessor httpContextAccessor;
		public Dictionary<Product, int> Cart { get; set; } = null!;
		public decimal CartTotal { get; set; }

		public List<Models.Employee> Employees { get; set; }
		public List<Models.Customer> Customers { get; set; }

		[BindProperty]
		public int? EmployeeID { get; set; }
		[BindProperty]
		public string? CustomerID { get; set; }
		[BindProperty]
		public DateTime? OrderDate { get; set; }
		[BindProperty]
		public string? ShipAddress { get; set; }
		[BindProperty]
		public string? ShipCountry { get; set; }
		[BindProperty]
		public string? Message { get; set; }

		private readonly IHubContext<CartHub> _hubContext;

		public CartModel(NorthwindContext context, IHttpContextAccessor httpContextAccessor, IHubContext<CartHub> hubContext)
		{
			this.context = context;
			_hubContext = hubContext;
			this.httpContextAccessor = httpContextAccessor;
			Cart = new Dictionary<Product, int>();
			CartTotal = 0;

			Employees = context.Employees.ToList();
			Customers = context.Customers.ToList();
		}

		public void OnGet()
		{
			ProcessRequest();
		}

		public async Task OnPostAddToCart(int ProductID)
		{
			CartManager cartManager = new CartManager(httpContextAccessor.HttpContext!.Session);
			cartManager.AddToCart(ProductID);
			ProcessRequest();
			await _hubContext.Clients.All.SendAsync("UpdateCart", Cart);
		}
		public async Task OnPostRemoveFromCart(int ProductID)
		{
			CartManager cartManager = new CartManager(httpContextAccessor.HttpContext!.Session);
			cartManager.RemoveFromCart(ProductID);
			ProcessRequest();
			await _hubContext.Clients.All.SendAsync("UpdateCart", Cart);
		}

		public void OnPostOrderNow()
		{
			var newOrder = new Order();
			newOrder.EmployeeId = EmployeeID;
			newOrder.CustomerId = CustomerID;
			newOrder.OrderDate = OrderDate;
			newOrder.ShipCountry = ShipCountry;
			newOrder.ShipAddress = ShipAddress;
			Dictionary<int, int> cart = (new CartManager(httpContextAccessor.HttpContext!.Session)).GetProducts();
			List<Product> products = context.Products.ToList();
			foreach (int productId in cart.Keys)
			{
				Product? p = products.FirstOrDefault(item => item.ProductId == productId);
				if (p != null)
				{
					var orderDetail = new OrderDetail()
					{
						ProductId = productId,
						UnitPrice = (decimal)p.UnitPrice,
						Quantity = (short)cart[productId],
					};
					newOrder.OrderDetails.Add(orderDetail);
				}
			}
			context.Orders.Add(newOrder);
			context.SaveChanges();
			Message = $"Add Order Successfully";
			CartManager cartManager = new CartManager(httpContextAccessor.HttpContext!.Session);
			cartManager.RemoveTemporaryCart();
		}

		void ProcessRequest()
		{
			Dictionary<int, int> cart = (new CartManager(httpContextAccessor.HttpContext!.Session)).GetProducts();
			List<Product> products = context.Products.ToList();
			foreach (int productId in cart.Keys)
			{
				Product? p = products.FirstOrDefault(item => item.ProductId == productId);
				CartTotal += (Convert.ToDecimal(p.UnitPrice) * cart[productId]);
				if (p != null)
					Cart.Add(p, cart[productId]);
			}
		}
	}
}
