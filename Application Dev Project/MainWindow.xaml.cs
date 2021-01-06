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

namespace Application_Dev_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void playGameBtn_Click(object sender, RoutedEventArgs e)
        {
            
            //Open new windows

            windowOpener newPlayingWindow = new windowOpener(typeOfWindow.userInput, "","");
            this.Close();

        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void instructionsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            //closes the window when cliked
            this.Close();
        }
    }
}
