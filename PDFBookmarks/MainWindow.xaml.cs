using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Make the frame static because it's the only frame and it needs
        // to be accessed from other pages
        public static Frame? MainFrame
        {
            get; set;
        }
        public MainWindow()
        {
            InitializeComponent();
            // Initialize the main frame and navigate to the main menu when the app opens
            MainFrame = AppFrame;
            MainFrame?.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

    }
}