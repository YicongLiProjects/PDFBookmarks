using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace PDFBookmarks
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        // ObservableCollection allows for real time modification tailored for the UI
        private ObservableCollection<PDFFile> filesList = new ObservableCollection<PDFFile>();

        // Keep a reference to the Json file that stores the list of files
        private string jsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDFFileListBoxData.json");
        public MainMenu()
        {
            InitializeComponent();
            // Load the list of files from the Json file into the ObservableCollection
            // storing all recent files
            if (File.Exists(jsonFilePath) && new FileInfo(jsonFilePath).Length > 0)
            {
                filesList = JsonSerializer.Deserialize<ObservableCollection<PDFFile>>(File.ReadAllText(jsonFilePath));
            }

            // Button event handlers
            OpenFileButton.Click += OpenFile;
            HelpButton.Click += OpenHelpMenu;

            AllFiles.MouseDoubleClick += OpenFile;
            AllFiles.AllowDrop = true;
            AllFiles.Drop += DropFileIntoListBox;
            AllFiles.ItemsSource = filesList;

            SortButton.Click += SortFiles;
            ClearButton.Click += ClearListBox;
        }

        // Open a PDF file from the "Open File" button in the main menu
        // Navigate to file viewer page
        // Open the file where the user left off from the config file
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "PDF files (.pdf) | *.pdf";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filePath = dialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);
                string folderName = System.IO.Path.GetDirectoryName(filePath)!;
                if (System.IO.Path.GetExtension(filePath).ToLower() != ".pdf")
                {
                    MessageBox.Show("Please select a PDF file");
                    return;
                }
                PDFFile file = new PDFFile(filePath, fileName);

                // Add file to the main menu's list box
                // so the user can access it in the future directly
                filesList.Add(file);

                // Update Json file
                string newJsonString = JsonSerializer.Serialize(filesList);
                File.WriteAllText("PDFFileListBoxData.json", newJsonString);

                MainWindow.MainFrame?.Navigate(new FileEditor(filePath));
            }
        }

        // Open a PDF file from the list box, overloaded for ListBoxItem
        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            PDFFile lbi = (PDFFile) AllFiles.SelectedItem;
            if (lbi != null)
            {
                string filePath = lbi.FilePath;
                MainWindow.MainFrame?.Navigate(new FileEditor(filePath));
            }
        }

        // Method for dragging and dropping files into the list box
        // Add the files to the list box
        private void DropFileIntoListBox(object sender, DragEventArgs e) {
            // Functional programming to clean up the code and transform all the files dropped
            string[] addedFilesFullPath = (string[]) (e.Data.GetData(DataFormats.FileDrop));
            string[] addedFilesNames = addedFilesFullPath.Select(f => System.IO.Path.GetFileName(f)).ToArray<string>();
            PDFFile[] addedPDFFiles = addedFilesFullPath.Zip(addedFilesNames, (fullPath, fileName) => new PDFFile(fullPath, fileName)).ToArray();

            // The Array library is used here to add all files to the list box
            Array.ForEach(addedPDFFiles, file => filesList.Add(file));

            // Update the Json file
            string updatedFileList = JsonSerializer.Serialize<ObservableCollection<PDFFile>>(filesList);
            File.WriteAllText("PDFFileListBoxData.json", updatedFileList);
        }

        private void OpenSettingsMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("SettingsMenu.xaml", UriKind.Relative));
        }

        private void OpenHelpMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("HelpMenu.xaml", UriKind.Relative));
        }

        private void ClearListBox(object sender, RoutedEventArgs e)
        {
            filesList.Clear();
            File.WriteAllText("PDFFileListBoxData.json", "");
        }

        // Event handler for the sorting button
        // Calls the sorting algorithm
        private void SortFiles(object sender, RoutedEventArgs e)
        {
            SortFiles(filesList, 0, filesList.Count - 1);
        }


        // Quick sort algorithm to sort files in the list box
        // Sort according to the file name in alphabetical order
        private ObservableCollection<PDFFile> SortFiles(ObservableCollection<PDFFile> oc, int left, int right)
        {
            if (left >= right)
            {
                return oc;
            }
            else
            {
                int i = PlaceWall(oc, left, right);
                SortFiles(oc, left, i - 1);
                SortFiles(oc, i + 1, right);
            }
            return oc;
        }

        // Helper for the sorting method
        private int PlaceWall(ObservableCollection<PDFFile> oc, int left, int right)
        {
            string pivot = oc[oc.Count - 1].FileName;
            int wall = left - 1;
            for (int i = left; i < right; i++)
            {
                if (oc[i].FileName.CompareTo(pivot) < 0)
                {
                    wall++;
                    (oc[i], oc[wall]) = (oc[wall], oc[i]);
                }
            }
            (oc[right], oc[wall + 1]) = (oc[wall + 1], oc[right]);
            return wall + 1;
        }
    }
}
