using AutoMapper;
using Lab1;
using Lab1.DTO;
using Lab1.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : string.Empty))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderDetails != null ? src.OrderDetails.Sum(od => od.Quantity * od.Product.Price) : 0));

        CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => CalculateTotalAmount(src.Orders)))
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders.ToList()));
    }

    private decimal? CalculateTotalAmount(ICollection<Order> orders)
    {
        decimal? totalAmount = 0;

        foreach (var order in orders)
        {
            var orderDetail = order.OrderDetails.FirstOrDefault();

            if (orderDetail != null && orderDetail.Product != null)
            {
                totalAmount += orderDetail.Quantity * orderDetail.Product.Price;
            }
        }

        return totalAmount;
    }
}


