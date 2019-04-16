using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;

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
        #endregion
        
        #region UI METHODS
        public MainWindow()
        {
            InitializeComponent();
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
        /// Exits out of the app (TaskBar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiHowToUse_Click(object sender, RoutedEventArgs e)
        {
            showContainer("HelpMenu");
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
        /// Fills the Report Data grid with the Inversion Count by Department report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport1_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.CountInversionTypeByDepartment();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(1);
            lblReportName.Content = "Count Report by Department";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Inversion Count by College report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport2_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.CountInversionTypeByCollege();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(2);
            lblReportName.Content = "Count Report by College";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Inversion Cost by Department report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport3_Click(object sender, RoutedEventArgs e)
        {
            if(spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.CostInversionTypeByDepartment();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            HighlightSelectedReport(3);
            lblReportName.Content = "Cost Report by Department";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Inversion Cost by College report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport4_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.CostInversionTypeByCollege();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(4);
            lblReportName.Content = "Cost Report by College";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Inverted Employees report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport5_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.InvertedEmployees();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(5);
            lblReportName.Content = "Inverted Employees";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Summary by Dept report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport6_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.SummaryReport();
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(6);
            lblReportName.Content = "Summary";
            lblReportName.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fills the Report Data grid with the Summary by College report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReport7_Click(object sender, RoutedEventArgs e)
        {
            if (spHelpMenu.IsVisible)
            {
                showContainer("Report");
            }
            DataSet reportData = dataProcessor.SummaryReportTotals();
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            HighlightSelectedReport(7);
            lblReportName.Content = "Summary Totals";
            lblReportName.Visibility = Visibility.Visible;
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
            dlg.DefaultExt = ".mdb";
            dlg.Filter = "Access 2000 Database Files (*.mdb)|*.mdb";

            // Switch back to following line if functionality is implemented to allow .accdb files
            //dlg.Filter = "Access Database Files (*.accdb)|*.accdb|Access 2000 Database Files (*.mdb)|*.mdb";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                // Open document 
                sFilename = dlg.FileName;
                lFileName.Content = sFilename;

            
                
                btnWSGenerate.IsEnabled = true;
                btnMMGenerate.IsEnabled = true;
                miSaveAs.IsEnabled = false;
                showContainer("SelectFile");
            }
        }
        
        /// <summary>
        /// Generates the report From the Selected File
        /// </summary>
        void GenerateReport()
        {
            dataProcessor = new Process(sFilename);

            //I will databind the datagrid to the dataview property
            miSaveAs.IsEnabled = true;
            btnMMGenerate.IsEnabled = false;
            btnWSGenerate.IsEnabled = false;

            showContainer("Report");
            defaultReport();
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
        /// Saves a FrameWorkElement to the filepath using the specified encoder
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
            if (bDGReport.IsVisible)
            {
                rectangle1.Visibility = Visibility.Hidden;
                rectangle2.Visibility = Visibility.Hidden;
                rectangle3.Visibility = Visibility.Hidden;
                rectangle4.Visibility = Visibility.Hidden;
                rectangle5.Visibility = Visibility.Hidden;
                rectangle6.Visibility = Visibility.Hidden;
                rectangle7.Visibility = Visibility.Hidden;
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
                    case 5:
                        rectangle5.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        rectangle6.Visibility = Visibility.Visible;
                        break;
                    case 7:
                        rectangle7.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        void showContainer(string sContainer)
        {
            rectangle1.Visibility = Visibility.Hidden;
            rectangle2.Visibility = Visibility.Hidden;
            rectangle3.Visibility = Visibility.Hidden;
            rectangle4.Visibility = Visibility.Hidden;
            rectangle5.Visibility = Visibility.Hidden;
            rectangle6.Visibility = Visibility.Hidden;
            rectangle7.Visibility = Visibility.Hidden;
            spFileSelect.Visibility = Visibility.Hidden;
            spHelpMenu.Visibility = Visibility.Hidden;
            report.Visibility = Visibility.Hidden;
            switch (sContainer)
            {
                case "HelpMenu":
                    spHelpMenu.Visibility = Visibility.Visible;
                    break;
                case "Report":
                    report.Visibility = Visibility.Visible;
                    spSideMenu.IsEnabled = true;
                    break;
                case "SelectFile":
                    spFileSelect.Visibility = Visibility.Visible;
                    spSideMenu.IsEnabled = false;
                    break;
            }
        }

        void defaultReport()
        {
            DataSet reportData = dataProcessor.SummaryReport();
            dgReport.ItemsSource = null;
            dgReport.ItemsSource = reportData.Tables[0].AsDataView();
            bDGReport.Visibility = Visibility.Visible;
            HighlightSelectedReport(6);
            lblReportName.Content = "Summary";
            lblReportName.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
