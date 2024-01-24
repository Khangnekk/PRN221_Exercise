using DemoMVVM.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DemoMVVM.ViewModels
{
    class CategoryViewModel : INotifyPropertyChanged
    {
        private RelayCommand submitCommand { get; set; }
        private RelayCommand insertCommand { get; set; }
        private RelayCommand deleteCommand { get; set; }

        private Category editorableCategory;
        public Category EditorbaleCategory
        {
            get => editorableCategory; set
            {
                editorableCategory = value;
                OnPropertyChanged();
            }
        }

        public Category SelectedCategory { get; set; }
        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories; set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SubmitCommand { get => submitCommand; set => submitCommand = value; }
        public RelayCommand InsertCommand { get => insertCommand; set => insertCommand = value; }
        public RelayCommand DeleteCommand { get => deleteCommand; set => deleteCommand = value; }

        private NorthwindContext context;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Mỗi khi property thay đổi giá trị thì get set gọi hàm này => thông báo cho view biết về sự thay đổi của nó thông qua PropertyChanged
        void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public CategoryViewModel()
        {
            context = new NorthwindContext();
            Categories = context.Categories.ToList();

            SubmitCommand = new RelayCommand(OnSubmit);
            InsertCommand = new RelayCommand(Insert);
            DeleteCommand = new RelayCommand(Delete);

            if (EditorbaleCategory == null)
            {
                EditorbaleCategory = new Category();
            }
        }

        private async void OnSubmit()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000); // giả sử đang lưu vào db rất nhiều data và tốn nhiều thời gian
                context.SaveChanges();
            });

            MessageBox.Show("Đã lưu xong");
        }

        private async void Insert()
        {
            await Task.Run(() =>
            {
                Category category = new Category();
                category.CategoryName = EditorbaleCategory.CategoryName;
                category.Description = EditorbaleCategory.Description;
                context.Categories.Add(category);
                context.SaveChanges();
                Categories = context.Categories.ToList();
            });


            MessageBox.Show("Đã thêm 1 category và cập nhật vào DB");
        }

        private async void Delete()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Category category = context.Categories.FirstOrDefault(c => c.CategoryId == SelectedCategory.CategoryId);
                context.Categories.Remove(category);
                context.SaveChanges();
                Categories = context.Categories.ToList();
            });

            MessageBox.Show("Đã xóa");
        }
    }
}
