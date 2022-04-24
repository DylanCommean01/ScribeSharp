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
using System.Windows.Shapes;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PowerPointViewer : Window
    {
        Process proc = new Process();
        Window main;
        public PowerPointViewer(Window main)
        {
            InitializeComponent();
            this.main = main;
        }

        public void open(string source)
        {
            proc.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft Office\Office14\POWERPNT.EXE";
            proc.StartInfo.Arguments = " /s " + source;
            proc.Start();
            Show();
        }

        private void bt_close_Click(object sender, RoutedEventArgs e)
        {
            if (!proc.HasExited)
                proc.Kill();
            Close();
            main.Focus();
        }
    }
}
