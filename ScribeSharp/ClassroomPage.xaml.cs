using Syncfusion.Presentation;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.OfficeChartToImageConverter;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Syncfusion.Drawing;
using AspDotNet.FTPHelper;
using FluentFTP;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>


    public partial class ClassroomPage : Page
    {
        private IPresentation pptxDoc;
        private string filepath;
        private int index = 0;
        System.Drawing.Image image;
        FtpClient client = new FtpClient("thelastprodigy.site", "ScribeSharp@thelastprodigy.site", "CSCI4600");
        private sftpConnect upload;
        //FTPHelper fTPHelper = new FTPHelper("ftp.thelastprodigy.site", "ScribeSharp", "CSCI4600");


        public ClassroomPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            main = mainWindow;
            client.AutoConnect();
            if (mainWindow.Users == null || mainWindow.Users.IsStudent())
            {
                //addPresentation.Visibility = Visibility.Hidden;
                //buttonPrevious.Visibility = Visibility.Hidden;
                //buttonNext.Visibility = Visibility.Hidden;
            } else if (mainWindow.Users.IsTeacher())
            {

            }
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
            //template on how to return from page to window
        }

        private void addPresentation_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                filepath = openFileDialog.FileName;
                //MessageBox.Show(filepath);
                pptxDoc = Presentation.Open(filepath);
                pptxDoc.ChartToImageConverter = new ChartToImageConverter();
                pptxDoc.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;
                image = pptxDoc.Slides[index].ConvertToImage(Syncfusion.Drawing.ImageType.Metafile);

                image.Save(@$"../currentSlide{index}.png");
                upload = new(image);
                img.Source = new BitmapImage(new Uri(@$"\\Mac\Home\Desktop\Spring2022\Hasan4600\ScribeSharp2\ScribeSharp\bin\Debug\currentSlide{index}.png"));
                addPresentation.Visibility = Visibility.Hidden;
            }
            //\\Mac\Home\Desktop\Spring\2022
        }

        private void previousSlide_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("test Console");

            if (index > 0)
            {
                index--;
                image = pptxDoc.Slides[index].ConvertToImage(Syncfusion.Drawing.ImageType.Metafile);
                try
                {
                    image.Save(@$"../currentSlide{index}.png");
                    img.Source = new BitmapImage(new Uri(@$"\\Mac\Home\Desktop\Spring2022\Hasan4600\ScribeSharp2\ScribeSharp\bin\Debug\currentSlide{index}.png"));
                }
                catch (Exception ex) { }

            }
        }

        private void nextSlide_Click(object sender, RoutedEventArgs e)
        {
            index++;
            image.Dispose();
            try
            {
                image = pptxDoc.Slides[index++].ConvertToImage(Syncfusion.Drawing.ImageType.Metafile);
                image.Save(@$"../currentSlide{index}.png");
                img.Source = new BitmapImage(new Uri(@$"\\Mac\Home\Desktop\Spring2022\Hasan4600\ScribeSharp2\ScribeSharp\bin\Debug\currentSlide{index}.png"));
            }
            catch (Exception ex)
            {
                index--;
            }



        }
    }
}

