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

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ClassroomPage : Page
    {

        public ClassroomPage(MainWindow mainWindow)
        {
            InitializeComponent();
            main = mainWindow;
        }
        private MainWindow main;
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            if (main != null)
            {
                MainWindow newMain = new MainWindow();
                newMain.Show();
                main.Close();
            }

            //MainWindow main_window = new MainWindow();
            //MainWindow main = Application.Current.MainWindow as MainWindow;
            //NavigationService.Navigate(new MainWindow());
            //this.NavigationService.Navigate(new MainWindow());
            //Content = main_window;

            //main_window.Show();

        }


    }
}
