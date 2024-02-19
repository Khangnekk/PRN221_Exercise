using AutoMapper;
using GalaSoft.MvvmLight.Command;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lab1.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly IMapper _mapper;
        private readonly NorthwindContext context = new NorthwindContext();


        public List<Order> orders { get; set; }
        public List<OrderDTO> orderDTOs { get; set; }
        public List<Employee> employees { get; set; }


        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                FilterOrdersByEmployee();
            }
        }

        private ObservableCollection<OrderDTO> filteredOrders;
        public ObservableCollection<OrderDTO> FilteredOrders
        {
            get { return filteredOrders; }
            set
            {
                filteredOrders = value;
                OnPropertyChanged(nameof(FilteredOrders));
            }
        }
        public OrderDTO SelectedOrderDTO { get; set; }


        private RelayCommand editCommand { get; set; }
        private RelayCommand deleteCommand { get; set; }
        private RelayCommand saveChanges { get; set; }

        public RelayCommand EditCommand { get => editCommand; set => editCommand = value; }
        public RelayCommand DeleteCommand { get => deleteCommand; set => deleteCommand = value; }
        public RelayCommand SaveChangesCommand { get => saveChanges; set => saveChanges = value; }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public OrderViewModel()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AllowNullCollections = true;
            });
            _mapper = config.CreateMapper();

            employees = context.Employees.ToList();
            orders = context.Orders
                        .Include(o => o.Employee)
                        .Include(o => o.OrderDetails)
                        .ToList();
            orderDTOs = _mapper.Map<List<OrderDTO>>(orders);


            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private async void SaveChanges()
        {
            await Task.Run(() =>
            {
                OrderDTO orderDTO = SelectedOrderDTO;
                try
                {
                    Order orderToEdit = context.Orders.Find(orderDTO.ID);

                    if (orderToEdit != null)
                    {
                        orderToEdit.OrderDate = orderDTO.OrderDate;
                        context.SaveChanges();
                        FilterOrdersByEmployee();
                    }

                    MessageBox.Show("Edit successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing it: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                context.SaveChanges();
                FilterOrdersByEmployee();
            });
        }

        private async void Delete()
        {
            await Task.Run(() =>
            {
                OrderDTO orderDTO = SelectedOrderDTO;
                OnPropertyChanged();

                MessageBoxResult result = MessageBox.Show("Do you really want to delete it?\n" +
                    $"----------------------------------\n" +
                    $"Order ID = {orderDTO.ID}\n" +
                    $"Employee Name = {orderDTO.EmployeeName}\n" +
                    $"Order Date = {orderDTO.OrderDate}\n" +
                    $"Total Amount = {orderDTO.TotalAmount}\n"
                    , "Lab1 Warning!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Order orderToDelete = context.Orders.Find(orderDTO.ID);

                        if (orderToDelete != null)
                        {
                            context.Orders.Remove(orderToDelete);
                            context.SaveChanges();
                            FilterOrdersByEmployee();
                        }

                        MessageBox.Show("Order deleted successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Your data is not changed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }


        private async void Edit()
        {
            await Task.Run(() =>
            {
                OrderDTO orderDTO = SelectedOrderDTO;
                OnPropertyChanged(nameof(SelectedOrderDTO));
            });
        }

        private void FilterOrdersByEmployee()
        {
            if (SelectedEmployee != null)
            {
                FilteredOrders = new ObservableCollection<OrderDTO>(
                    //orderDTOs.Where(o => o.Employees.FirstOrDefault().EmployeeId == SelectedEmployee.EmployeeId).ToList();
                    context.Orders
                        .Include(o => o.Employee)
                        .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .Where(order => order.EmployeeId == SelectedEmployee.EmployeeId)
                        .Select(order => new OrderDTO
                        {
                            ID = order.OrderId,
                            EmployeeName = $"{order.Employee.FirstName} {order.Employee.LastName}",
                            OrderDate = order.OrderDate ?? DateTime.MinValue,
                            TotalAmount = order.OrderDetails.Sum(od => (od.Quantity * od.Product.Price)) ?? 0
                        })
                        .ToList()
                ); ;
            }
            else
            {
                FilteredOrders = new ObservableCollection<OrderDTO>(orderDTOs);
            }
        }

    }
}
