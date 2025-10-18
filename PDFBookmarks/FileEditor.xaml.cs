using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
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

namespace PDFBookmarks
{
    /// <summary>
    /// Interaction logic for FileEditor.xaml
    /// </summary>
    public partial class FileEditor : Page
    {

        private readonly string _filePath;
        public FileEditor(string filePath)
        {
            InitializeComponent();
            HelpButton.Click += OpenHelpMenu;
            BackButton.Click += GoBackToMainMenu;
            this.Loaded += pageLoaded;
            _filePath = filePath;
        }

        // Initialize WebView2 to display the PDF file when the page is loaded
        private async void pageLoaded(object sender, RoutedEventArgs e)
        {
            Viewer.CoreWebView2InitializationCompleted += CoreWebView2InitializationComplete;
            await Viewer.EnsureCoreWebView2Async();
        }

        // Initialize the PDF viewer
        private void CoreWebView2InitializationComplete(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            string fileName = System.IO.Path.GetFileName(_filePath);
            string folderName = System.IO.Path.GetDirectoryName(_filePath)!;
            Viewer.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "localfiles", folderName, CoreWebView2HostResourceAccessKind.Allow
            );
            Viewer.Source = new Uri($"https://localfiles/{fileName}");
        }

        // Event handlers for all buttons
        private void OpenHelpMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("HelpMenu.xaml", UriKind.Relative));
        }

        private void GoBackToMainMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }
    }
}
