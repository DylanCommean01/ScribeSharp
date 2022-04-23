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
        public ClassroomPage()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow main_window = new MainWindow();
            //NavigationService.Navigate(new MainWindow());
            //this.NavigationService.Navigate(new MainWindow());
            //this.Content = new MainWindow();
            
            main_window.Show();

        }
    }
}
