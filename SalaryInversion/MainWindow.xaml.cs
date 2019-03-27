using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalaryInversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process dataProcessor;
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            dataProcessor = new Process();
            employees = dataProcessor.GetEmployees();
            DisplayGrid.ItemsSource = employees;
        }
    }
}
