using Lab1.Models;

namespace Lab1.DTO
{
    public class EmployeeDTO
    {
        public string EmployeeName { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
