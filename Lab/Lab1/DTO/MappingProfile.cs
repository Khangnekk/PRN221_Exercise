using AutoMapper;
using Lab1;
using Lab1.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => GetEmployeeName(src.Employee != null ? src.Employee.EmployeeId : (int?)null)))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderDetails != null && src.OrderDetails.Any() ? src.OrderDetails.FirstOrDefault().Product.ProductName : null))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderDetails != null ? src.OrderDetails.Sum(od => od.Quantity * od.Product.Price) : 0));
    }

    private string GetEmployeeName(int? employeeID)
    {
        if (employeeID == null)
            return string.Empty;

        using (var context = new NorthwindContext())
        {
            Employee em = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeID);

            return em != null ? $"{em.FirstName} {em.LastName}" : string.Empty;
        }
    }
}

