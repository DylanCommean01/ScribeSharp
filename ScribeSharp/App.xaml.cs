using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string root = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        void App_Exit(object sender, ExitEventArgs e)
        {
            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @"\resources\currentPowerpoint.pptx"))
            {
                File.Delete(Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @"\resources\currentPowerpoint.pptx");
            }
        }
    }
    
}
