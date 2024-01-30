using AutoMapper;
using Lab1.DTO;
using Lab1.Models;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lab1.ViewModels
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }

    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly NorthwindContext context = new NorthwindContext();
        private readonly IMapper mapper;

        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<string> Labels { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        List<Employee> employees { get; set; }
        List<EmployeeDTO> employeeDTOs { get; set; }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeeViewModel()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AllowNullCollections = true;
            });
            mapper = config.CreateMapper();

            employees = context.Employees
                .Include(e => e.Orders)
                .ThenInclude(e => e.OrderDetails)
                .ThenInclude(e => e.Product)
                .ToList();

            employeeDTOs = mapper.Map<List<EmployeeDTO>>(employees);

            SeriesCollection = new SeriesCollection();
            Labels = new ObservableCollection<string>();

            UpdateChart(GetAllEmployeeDTOs());
        }

        public void UpdateChart(List<EmployeeDTO> employeeData)
        {
            SeriesCollection.Clear();
            Labels.Clear();

            var columnSeries = new ColumnSeries
            {
                Values = new ChartValues<decimal>(employeeData.Select(e => e.TotalAmount)),
                DataLabels = true
            };

            SeriesCollection.Add(columnSeries);
            Labels.AddRange(employeeData.Select(e => e.EmployeeName));

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
        }

        public List<EmployeeDTO> GetAllEmployeeDTOs()
        {
            foreach (var employeeDTO in employeeDTOs)
            {
                employeeDTO.TotalAmount = CalculateTotalAmount(employeeDTO.Orders);
            }

            return employeeDTOs;
        }

        private decimal CalculateTotalAmount(ICollection<Order> orders)
        {
            decimal totalAmount = 0;

            foreach (var order in orders)
            {
                var orderDetail = order.OrderDetails.FirstOrDefault();

                if (orderDetail != null && orderDetail.Product != null)
                {
                    totalAmount += orderDetail.Quantity * orderDetail.Product.Price ?? 0;
                }
            }

            return totalAmount;
        }
    }
}
