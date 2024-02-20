using ExerciseRazerPage.Models;
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
        public void OnGet()
        {
            customers = _context.Customers.ToList();
            employees = _context.Employees.ToList();
        }

        public void OnPost()
        {

        }
    }
}
