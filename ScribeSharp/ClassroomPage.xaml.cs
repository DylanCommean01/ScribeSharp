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
using System.Diagnostics;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>


    public partial class ClassroomPage : Page
    {
        private IPresentation pptxDoc;
        private string _fileMessages = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private Teacher teachers;
        private string filepath;
        private int index = 0;
        System.Drawing.Image[] image;
        private string classID;
        string root = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        private sftpConnect upload = new sftpConnect();
        bool open = false;
        private string nondeleted = "";
        private MainWindow main;
        //private chatClient chat_client;
        //private chatServer chat_server;



        public ClassroomPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _fileMessages = Directory.GetParent(Directory.GetParent(Directory.GetParent(_fileMessages).FullName).FullName).FullName + @"\Data\Messages.txt";
            this.DataContext = this;
            main = mainWindow;
            teachers = (Teacher)main.Users;
            client.AutoConnect();
            chatroom.Text = RenderClassIDMessages();
            if (mainWindow.Users == null || mainWindow.Users.IsStudent())
            {
                addPresentation.Visibility = Visibility.Hidden;

                
            } else if (mainWindow.Users.IsTeacher())
            {
                save.Visibility = Visibility.Hidden;
                grab.Visibility = Visibility.Hidden;
            }
            buttonPrevious.Visibility = Visibility.Hidden;
            buttonNext.Visibility = Visibility.Hidden;
        }

        private string RenderClassIDMessages()
        {
            using StreamReader sr = new(_fileMessages);
            string messages = "";
            string line = sr.ReadLine();
            while (line != null) {
                if (line.Equals("DGIMMDA"))
                {
                    string name = sr.ReadLine();
                    string message = sr.ReadLine();
                    messages += $"{name}: {message}\n";
                }
                line = sr.ReadLine();
            }
            return messages;
        }

        public void Button_Submit_Message_Click(object sender, RoutedEventArgs e)
        {
            using StreamWriter sw = new(_fileMessages, true);
            sw.WriteLine(teachers.ClassID);
            sw.WriteLine($"{teachers.FirstName.ElementAt(0)}. {teachers.LastName}");
            sw.WriteLine(chatBox.Text);
            sw.Close();
            chatroom.Text = RenderClassIDMessages();
            chatBox.Text = "";
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            if (main != null)
            {
                MainWindow newMain = new MainWindow();
                newMain.Show();
                main.Close();
                if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @"\resources\currentPowerpoint.pptx"))
                {
                    File.Delete(Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @"\resources\currentPowerpoint.pptx");
                }
            }
            //template on how to return from page to window
        }

        public void storeClassID(string joinCode)
        {
            classID = joinCode;
        }
        private void addPresentation_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {

                filepath = openFileDialog.FileName;
                upload.uploadPPT(filepath, main.classID);
                MessageBox.Show(filepath);
                

                if (open == false) { 
                    
                    open = true;
                } else
                {
                    string applogo = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                    pptxDoc.Close();
                    
                    
                    for (int x =0; x < image.Length; x++)
                    {
                        image[x].Dispose();
                       
                    }
                    Array.Clear(image, 0, image.Length);

                    applogo = Directory.GetParent(Directory.GetParent(Directory.GetParent(applogo).FullName).FullName).FullName + @$"\resources\ScribeSharpLogo.png";
                    img.Source = new BitmapImage(new Uri(applogo));
                }
                
               


                index = 0;
                string test = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                if (Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides")){
                    DeleteDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
                }
                pptxDoc = Presentation.Open(filepath);
                pptxDoc.ChartToImageConverter = new ChartToImageConverter();
                pptxDoc.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;

                Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
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
                /*if (open == true && !nondeleted.Equals(""))
                {
                    //MessageBox.Show(nondeleted);
                    File.Delete(nondeleted);
                }*/
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
                /*if (open == true && !nondeleted.Equals(""))
                {
                    //MessageBox.Show(nondeleted);
                    File.Delete(nondeleted);
                }*/
            }
        }

        public void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            
            
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                try
                {
                    File.Delete(file);
                } catch (Exception ex) { }
              
                
                
            }

            
        }

        private void chat_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            
            string testA = Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @$"\resources\currentPowerpoint.pptx";
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.DefaultExt = "pptx";
            SaveFileDialog1.Filter = "pptx files (*.pptx)|*.pptx|All files (*.*)|*.*";
            SaveFileDialog1.ShowDialog();
            upload.readFile(SaveFileDialog1.FileName, classID,testA);
        }

        private void grabPresentation_Click(object sender, RoutedEventArgs e)
        {
            
            string testA = Directory.GetParent(Directory.GetParent(Directory.GetParent(root).FullName).FullName).FullName + @$"\resources\currentPowerpoint.pptx";
            //upload.uploadPPT(filepath, main.classID);
            upload.readFile(testA, classID);
            if (open == false)
            {
                
                
                open = true;
            }
            else
            {
               
                pptxDoc.Close();


                for (int x = 0; x < image.Length; x++)
                {
                    image[x].Dispose();

                }
                Array.Clear(image, 0, image.Length);
            }




            index = 0;
            string test = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            if (Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides"))
            {
                DeleteDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
            }
            pptxDoc = Presentation.Open(testA);
            pptxDoc.ChartToImageConverter = new ChartToImageConverter();
            pptxDoc.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;

            Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides");
            string newTest = Directory.GetParent(Directory.GetParent(Directory.GetParent(test).FullName).FullName).FullName + @$"\resources\slides\currentSlide{index}.png";
            image = pptxDoc.RenderAsImages(Syncfusion.Drawing.ImageType.Metafile);
            image[index].Save(newTest);
            img.Source = new BitmapImage(new Uri(newTest));

            buttonPrevious.Visibility = Visibility.Visible;
            buttonNext.Visibility = Visibility.Visible;
        }

        private void Menu_Calculator_Click(object sender, RoutedEventArgs e)
        {
            Calculator calc = new();
            calc.Show();
        }

        private void CSharp_Compiler_Click(object sender, RoutedEventArgs e) 
        {
            string url = "https://dotnetfiddle.net/";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = url;
            Process.Start(startInfo);
        }

    }
}

