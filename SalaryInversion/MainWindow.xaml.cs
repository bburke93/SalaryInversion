using Microsoft.Win32;
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
        #region Member Variables
        /// <summary>
        /// The database file of Professor Salaries
        /// </summary>
        string sFilename;
        private Process dataProcessor;
        private List<Employee> employees = new List<Employee>();


        //THIS DATA ACCESS CLASS SHOULD BELONG TO A DRIVER CLASS FOR CREATING THE REPORTS
        //I have it here for now.
        clsDataAccess db;


        #endregion


        #region UI METHODS
        public MainWindow()
        {
            InitializeComponent();
            dataProcessor = new Process();
            employees = dataProcessor.GetEmployees();
            //DisplayGrid.ItemsSource = employees;
        }

        /// <summary>
        /// Selects an Access DB file (From SideMenu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Selects an Access DB file (From Menubar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Generates The reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerateReports_Click(object sender, RoutedEventArgs e)
        {
            //FOR NOW THE DB IS DECLARED RIGHT HERE
            //THE DB SHOULD EXIST IN THE DRIVER CLASS FOR GENERATING THE REPORTS
            db = new clsDataAccess(sFilename);

            //Hide the SelectFile panel
            spFileSelect.Visibility = Visibility.Hidden;
            //Show the DataGrid
            dgReport.Visibility = Visibility.Visible;


            //I will databind the datagrid to the dataview property
        }

        /// <summary>
        /// Fills the Report Data grid with Report 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport1_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Fills the Report Data grid with Report 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport2_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Fills the Report Data grid with Report 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport3_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Fills the Report Data grid with Report 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport4_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region HELPER FUNCTIONS
        /// <summary>
        /// Creates an Open file Dialogue box to browse your files and select an Access DB file
        /// </summary>
        void OpenFile()
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".accdb";
            dlg.Filter = "Access Database Files (*.accdb)|*.accdb|Access 2000 Database Files (*.mdb)|*.mdb";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                btnGenerateReports.IsEnabled = true;
                // Open document 
                sFilename = dlg.FileName;
                lFileName.Content = sFilename;
            }
        }
        #endregion
    }
}
