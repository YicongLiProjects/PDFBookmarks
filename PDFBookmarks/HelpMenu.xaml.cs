using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenu : Page
    {
        public HelpMenu()
        {
            InitializeComponent();
            HelpItems.SelectionChanged += SelectHelpItem;
            BackButton.Click += GoBack;
        }

        // Select help item and display the corresponding help text
        private void SelectHelpItem(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = (ListBoxItem) HelpItems.SelectedItem;
            string name = selected.Name;
            switch (name)
            {
                case "BookmarksHelp":
                    HelpArea.Text = "";
                    HelpArea.Text = File.ReadAllText("helpMenu/BookmarksHelp.txt");
                    break;
                case "OpenFileHelp":
                    HelpArea.Text = "";
                    HelpArea.Text = File.ReadAllText("helpMenu/OpenFileHelp.txt");
                    break;
                case "SettingsHelp":
                    HelpArea.Text = "";
                    HelpArea.Text = File.ReadAllText("helpMenu/SettingsHelp.txt");
                    break;
                default:
                    HelpArea.Text = "";
                    break;
            }
        }

        // Go back to the main menu or file viewer depending on where the user came from
        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame?.GoBack();
        }
    }
}
