using ExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseRazerPage.Pages.Order
{
    public class AddModel : PageModel
    {
        private readonly NorthwindContext _context;

        public AddModel(NorthwindContext context)
        {
            _context = context;
        }
        public List<Models.Employee> employees { get; set; }
        public List<Models.Customer> customers { get; set; }
        #region
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
        #endregion
        public void OnGet()
        {
            customers = _context.Customers.ToList();
            employees = _context.Employees.ToList();
        }

        public void OnPost()
        {
            Models.Order o = new Models.Order();
            o.EmployeeId = EmployeeID;
            o.CustomerId = CustomerID;
            o.OrderDate = OrderDate;
            o.ShipCountry = ShipCountry;
            o.ShipAddress = ShipAddress;
            _context.Orders.Add(o);
            _context.SaveChanges();
            Message = $"Add Order Successfully";
        }
    }
}
