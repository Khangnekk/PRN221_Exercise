using ExerciseRazerPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseRazerPage.Pages.Employee
{
    public class DetailModel : PageModel
    {
        private readonly NorthwindContext _context;

        public DetailModel(NorthwindContext context)
        {
            _context = context;
        }
        public Models.Employee employee { get; set; }
        public void OnGet(int eid)
        {
            employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == eid);
        }
    }
}
