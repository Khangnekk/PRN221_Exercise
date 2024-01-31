using Lab1.Views;
using System.Windows;

namespace Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        void Init() { cbEmployee.SelectedIndex = 0; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowSummaryWindow showSummaryWindow = new ShowSummaryWindow();
            showSummaryWindow.ShowDialog();
        }
    }
}