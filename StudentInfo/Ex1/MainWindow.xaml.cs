using System.Windows;
using System.Windows.Controls;

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

    }
}