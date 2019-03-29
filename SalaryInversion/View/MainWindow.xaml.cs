using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;

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
        DataAccess db;


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
        /// Selects an Access DB file (TaskBar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Saves the Current Report DataGrid (TaskBar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiSaveAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "report"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Png Image (*.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                SaveToPng(dgReport, dlg.FileName);
            }
        }

        /// <summary>
        /// Exits out of the app (TaskBar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Opens the File (Main Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMMOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Generates the Report (Main Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMMGenerate_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();
        }

        /// <summary>
        /// Selects an Access DB file (WorkSpace)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Generates The reports (WorkSpace)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();
        }

        /// <summary>
        /// Fills the Report Data grid with Report 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport1_Click(object sender, RoutedEventArgs e)
        {
            HighlightSelectedReport(1);
        }

        /// <summary>
        /// Fills the Report Data grid with Report 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport2_Click(object sender, RoutedEventArgs e)
        {
            HighlightSelectedReport(2);
        }

        /// <summary>
        /// Fills the Report Data grid with Report 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport3_Click(object sender, RoutedEventArgs e)
        {
            HighlightSelectedReport(3);
        }

        /// <summary>
        /// Fills the Report Data grid with Report 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport4_Click(object sender, RoutedEventArgs e)
        {
            HighlightSelectedReport(4);
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
                // Open document 
                sFilename = dlg.FileName;
                lFileName.Content = sFilename;

                dgReport.Visibility = Visibility.Hidden;
                rectangle1.Visibility = Visibility.Hidden;
                rectangle2.Visibility = Visibility.Hidden;
                rectangle3.Visibility = Visibility.Hidden;
                rectangle4.Visibility = Visibility.Hidden;
                spFileSelect.Visibility = Visibility.Visible;
                btnWSGenerate.IsEnabled = true;
                btnMMGenerate.IsEnabled = true;
                miSaveAs.IsEnabled = false;
            }
        }
        
        /// <summary>
        /// Generates the report From the Selected File
        /// </summary>
        void GenerateReport()
        {
            //FOR NOW THE DB IS DECLARED RIGHT HERE
            //THE DB SHOULD EXIST IN THE DRIVER CLASS FOR GENERATING THE REPORTS
            db = new DataAccess(sFilename);

            //Hide the SelectFile panel
            spFileSelect.Visibility = Visibility.Hidden;
            //Show the DataGrid
            dgReport.Visibility = Visibility.Visible;


            //I will databind the datagrid to the dataview property
            miSaveAs.IsEnabled = true;
            btnMMGenerate.IsEnabled = false;
            btnWSGenerate.IsEnabled = false;
        }

        /// <summary>
        /// Saves the Report DataGrid to the filepath as a png
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="fileName"></param>
        void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        /// <summary>
        /// Saves a FrameWorkElement to  the filepath using the specified encoder
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="fileName"></param>
        /// <param name="encoder"></param>
        void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        /// <summary>
        /// Highlights the selected Report (SideMenu)
        /// </summary>
        /// <param name="reportNumber"></param>
        void HighlightSelectedReport(int reportNumber)
        {
            if (dgReport.IsVisible)
            {
                rectangle1.Visibility = Visibility.Hidden;
                rectangle2.Visibility = Visibility.Hidden;
                rectangle3.Visibility = Visibility.Hidden;
                rectangle4.Visibility = Visibility.Hidden;
                switch (reportNumber)
                {
                    case 1:
                        rectangle1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        rectangle2.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        rectangle3.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        rectangle4.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        #endregion
    }
}
