using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedObject = MyListbox.SelectedItem as Student;

            if (selectedObject != null)
            {
                //    Binding bindingId = new Binding();
                //    bindingId.Source = selectedObject;
                //    bindingId.Path = new PropertyPath("Id");
                //    txtID.SetBinding(TextBox.TextProperty, bindingId);

                //    Binding bindingFullName = new Binding();
                //    bindingFullName.Source = selectedObject;
                //    bindingFullName.Path = new PropertyPath("FullName");
                //    txtFullName.SetBinding(TextBox.TextProperty, bindingFullName);

                //    Binding bindingMajor = new Binding();
                //    bindingMajor.Source = selectedObject;
                //    bindingMajor.Path = new PropertyPath("Major");
                //    txtMajor.SetBinding(TextBox.TextProperty, bindingMajor);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student
            {
                Id = Convert.ToInt32(txtID.Text),
                FullName = txtFullName.Text,
                Major = txtMajor.Text,
            };
            StudentVM studentVM = (StudentVM)this.Resources["StudentVM"];
            if (studentVM.Students.Any(s => student.Id == s.Id))
            {
                MessageBox.Show(" Id is unique! Please try again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                studentVM.Students.Add(student);
                CollectionViewSource.GetDefaultView(MyListbox.ItemsSource).Refresh();
                MessageBox.Show("Successfully added new student", "Notfication", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student
            {
                Id = Convert.ToInt32(txtID.Text),
                FullName = txtFullName.Text,
                Major = txtMajor.Text,
            };
            StudentVM studentVM = (StudentVM)this.Resources["StudentVM"];
            Student? currentStudent = studentVM.Students.FirstOrDefault(s => s.Id == student.Id);
            if (currentStudent != null)
            {
                currentStudent.FullName = student.FullName;
                currentStudent.Major = student.Major;
                CollectionViewSource.GetDefaultView(MyListbox.ItemsSource).Refresh();
                MessageBox.Show($"Successfully edited a student with Id{student.Id}", "Notfication", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(" Maybe something wrong! Please try again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}