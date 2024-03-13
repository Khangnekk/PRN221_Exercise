using Lab2N3.Logics;
using Lab2N3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab2N3.Pages
{
	public class CartModel : PageModel
	{
		private NorthwindContext context;
		private IHttpContextAccessor httpContextAccessor;
		public Dictionary<Product, int> Cart { get; set; } = null!;
		public decimal CartTotal { get; set; }

		public CartModel(NorthwindContext context, IHttpContextAccessor httpContextAccessor)
		{
			this.context = context;
			this.httpContextAccessor = httpContextAccessor;
			Cart = new Dictionary<Product, int>();
			CartTotal = 0;
		}

		public void OnGet()
		{
			ProcessRequest();
		}

		public void OnPostAddToCart(int ProductID)
		{
			CartManager cartManager = new CartManager(httpContextAccessor.HttpContext!.Session);
			cartManager.AddToCart(ProductID);
			ProcessRequest();
		}
		public void OnPostRemoveFromCart(int ProductID)
		{
			CartManager cartManager = new CartManager(httpContextAccessor.HttpContext!.Session);
			cartManager.RemoveFromCart(ProductID);
			ProcessRequest();
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
