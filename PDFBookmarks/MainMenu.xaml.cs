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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            // Event handlers of UI components
            InitializeComponent();
            HelpButton.Click += OpenHelpMenu;
            SettingsButton.Click += OpenSettingsMenu;
            AllFiles.MouseDoubleClick += OpenFile;
        }

        // Open a PDF file from the "Open File" button in the main menu
        public void OpenFile(object sender, RoutedEventArgs e)
        {

        }

        // Open a PDF file from the list box, overloaded for ListBoxItem
        public void OpenFile(object sender, MouseButtonEventArgs e)
        {
            PDFFile lbi = (PDFFile) AllFiles.SelectedItem;
            if (lbi != null)
            {
                Uri uri = new Uri("PDFViewer.xaml", UriKind.Relative);
                MainWindow.MainFrame?.Navigate(uri);
            }
        }

        public void OpenSettingsMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("SettingsMenu.xaml", UriKind.Relative));
        }

        public void OpenHelpMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.Navigate(new Uri("HelpMenu.xaml", UriKind.Relative));
        }
    }
}
