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
using Path = System.IO.Path;

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
        System.Drawing.Image[] image;
        FtpClient client = new FtpClient("thelastprodigy.site", "ScribeSharp@thelastprodigy.site", "CSCI4600");
        private sftpConnect upload = new sftpConnect();
        bool open = false;
        private string nondeleted = "";
        private MainWindow main;
        //private chatClient chat_client;
        //private chatServer chat_server;



        public ClassroomPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            main = mainWindow;
            client.AutoConnect();
            if (mainWindow.Users == null || mainWindow.Users.IsStudent())
            {
                //addPresentation.Visibility = Visibility.Hidden;

                
            } else if (mainWindow.Users.IsTeacher())
            {

            }
            buttonPrevious.Visibility = Visibility.Hidden;
            buttonNext.Visibility = Visibility.Hidden;
        }
        

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
                //upload.deleteFile(filepath);
                upload.uploadFile(filepath);
                //MessageBox.Show(filepath);
                
                if (open == false) { 
                    pptxDoc = Presentation.Open(filepath);
                    open = true;
                } else
                {
                    string applogo = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                    pptxDoc.Close();
                    
                    pptxDoc = Presentation.Open(filepath);
                    for (int x =0; x < image.Length; x++)
                    {
                        image[x].Dispose();
                    }
                    Array.Clear(image, 0, image.Length);

                    applogo = Directory.GetParent(Directory.GetParent(Directory.GetParent(applogo).FullName).FullName).FullName + @$"\resources\ScribeSharpLogo.png";
                    img.Source = new BitmapImage(new Uri(applogo));
                }
                
                pptxDoc.ChartToImageConverter = new ChartToImageConverter();
                pptxDoc.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;



                index = 0;
                string test = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                //if (Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides")){
                  //  DeleteDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
                //}
                //Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
                string newTest = Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides\currentSlide{index}.png";
                image = pptxDoc.RenderAsImages(Syncfusion.Drawing.ImageType.Metafile);
                image[index].Save(newTest);
                img.Source = new BitmapImage(new Uri(newTest));

                buttonPrevious.Visibility = Visibility.Visible;
                buttonNext.Visibility = Visibility.Visible;
            }
        }

        private void previousSlide_Click(object sender, RoutedEventArgs e)
        {
           

            if (index > 0)
            {

                
                index--;
              
                string test = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                 test = Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides\currentSlide{index}.png";
                
                
                try
                {
                    image[index].Save(test);
                    img.Source = new BitmapImage(new Uri(test));
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    img.Source = new BitmapImage(new Uri(test));
                }
                if (open == true && !nondeleted.Equals(""))
                {
                    //MessageBox.Show(nondeleted);
                    File.Delete(nondeleted);
                }
            }

        }

        private void nextSlide_Click(object sender, RoutedEventArgs e)
        {
            
            if (index < image.Length)
            {
                index++;
                
                string test = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                test = Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides\currentSlide{index}.png";
               
                try
                {
                    image[index].Save(test);
                    img.Source = new BitmapImage(new Uri(test));
                } catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    img.Source = new BitmapImage(new Uri(test));
                }
                if (open == true && !nondeleted.Equals(""))
                {
                    //MessageBox.Show(nondeleted);
                    File.Delete(nondeleted);
                }
            }
        }

        public void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                try
                {
                    File.Delete(file);
                } catch (System.IO.IOException ex){
                    //MessageBox.Show(file);
                    nondeleted = file;
                    
                }
                
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            
        }

        private void chat_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grabPresentation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Calculator_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

