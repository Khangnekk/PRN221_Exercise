using Lab2N3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab2N3.Pages
{
	public class IndexModel : PageModel
	{
		private NorthwindContext context;
		private IHttpContextAccessor httpContextAccessor;

		//cort: Constructor
		public IndexModel(NorthwindContext context, IHttpContextAccessor httpContextAccessor)
		{
			this.context = context;
			this.httpContextAccessor = httpContextAccessor;
		}
		public List<Models.Product> Products { get; set; }
		public List<Models.Category> Categories { get; set; }
		public List<Models.Supplier> Suppliers { get; set; }

		[BindProperty]
		public int? CategoryID { get; set; }
		[BindProperty]
		public int? SupplierID { get; set; }
		[BindProperty]
		public string SortBy { get; set; }
		[BindProperty(SupportsGet = true)]
		public string key { get; set; }
		public int pageSize = 12;
		public bool isFilter { get; set; }
		public int? TotalPages { get; set; }
		public int CurrentPageNumber { get; set; }
		public int? PreviousPage { get; set; }
		public int? NextPage { get; set; }

		public void OnGet(int? pageNumber)
		{
			isFilter = false;
			int pageNum = (pageNumber ?? 1);
			CurrentPageNumber = pageNum;

			Products = context.Products
				.Include(p => p.Category)
				.Include(p => p.Supplier)
				.ToList();


			int totalProducts = Products.Count();
			TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			var ProductResult = Products.Skip((pageNum - 1) * pageSize).Take(pageSize);
			Products = ProductResult.ToList();

			Categories = context.Categories.ToList();
			Suppliers = context.Suppliers.ToList();

			SortBy = "ProductNameAZ";
			ProcessPage();
		}

		public void OnPostFilter(int? pageNumber)
		{
			isFilter = true;
			int pageNum = (pageNumber ?? 1);
			CurrentPageNumber = pageNum;
			Categories = context.Categories.ToList();
			Suppliers = context.Suppliers.ToList();

			Products = context.Products
					.Include(p => p.Category)
					.Include(p => p.Supplier)
					.Where(p => CategoryID == -1 || p.CategoryId == CategoryID || CategoryID == null)
					.Where(p => SupplierID == -1 || p.SupplierId == SupplierID || SupplierID == null)
					.ToList();

			List<Product> SearchResult = new List<Product>();
			key = httpContextAccessor.HttpContext.Session.GetString("Key");
			if (key != null)
			{
				foreach (var p in Products)
				{
					if (p.ProductName.ToLower().Contains(key.ToLower()))
					{
						SearchResult.Add(p);
					}
				}
				if (SearchResult != null)
					Products = SearchResult;
			}
			int totalProducts = Products.Count();
			TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			var ProductsResult = Products.Skip((pageNum - 1) * pageSize).Take(pageSize);
			Products = ProductsResult.ToList();

			switch (SortBy)
			{
				case "ProductNameAZ":
					Products = Products.OrderBy(o => o.ProductName).ToList();
					break;
				case "ProductNameZA":
					Products = Products.OrderByDescending(o => o.ProductName).ToList();
					break;
				case "UnitPriceAZ":
					Products = Products.OrderBy(o => o.UnitPrice).ToList();
					break;
				case "UnitPriceZA":
					Products = Products.OrderByDescending(o => o.UnitPrice).ToList();
					break;
				case "UnitsInStockAZ":
					Products = Products.OrderBy(o => o.UnitsInStock).ToList();
					break;
				case "UnitsInStockZA":
					Products = Products.OrderByDescending(o => o.UnitsInStock).ToList();
					break;
				default:
					break;
			}
			ProcessPage();
		}


		public void OnPostSearch(int? pageNumber)
		{
			httpContextAccessor.HttpContext.Session.SetString("Key", "");
			isFilter = true;
			int pageNum = (pageNumber ?? 1);
			CurrentPageNumber = pageNum;
			Categories = context.Categories.ToList();
			Suppliers = context.Suppliers.ToList();
			Products = context.Products.ToList();

			List<Product> SearchResult = new List<Product>();
			if (key != null)
			{
				httpContextAccessor.HttpContext.Session.SetString("Key", key);
				foreach (var p in Products)
				{
					if (p.ProductName.ToLower().Contains(key.ToLower()))
					{
						SearchResult.Add(p);
					}
				}
				if (SearchResult != null)
					Products = SearchResult;
			}
			int totalProducts = Products.Count();
			TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			var ProductsResult = Products.Skip((pageNum - 1) * pageSize).Take(pageSize);
			Products = ProductsResult.ToList();

			if (SortBy == null)
			{
				SortBy = "ProductNameAZ";
			}
		}

		void ProcessPage()
		{
			PreviousPage = CurrentPageNumber - 1;
			NextPage = CurrentPageNumber + 1;
			if (PreviousPage <= 1)
				PreviousPage = 1;
			if (NextPage >= TotalPages)
				NextPage = TotalPages;
		}

	}
}
