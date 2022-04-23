using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private Registration registration;
        private MainWindow mainWindow;

        public Login()
        {
            InitializeComponent();
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\Users.txt";
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (Text_Box_Email.Text.Length == 0)
            {
                Error_Message.Text = "Enter an email.";
                Text_Box_Email.Focus();
            }
            else if (!Regex.IsMatch(Text_Box_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Error_Message.Text = "Enter a valid email.";
                Text_Box_Email.Select(0, Text_Box_Email.Text.Length);
                Text_Box_Email.Focus();
            }
            else
            {
                string email = Text_Box_Email.Text;
                string password = Password_Box.Text;
                StreamReader sr = new(_filePath);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lines = line.Split(' ');
                    if (lines[3] == email && lines[4] == password && lines[2] == "Student")
                    {
                        string firstName = lines[1];
                        string lastName = lines[2];
                        mainWindow = new();
                        mainWindow.user = new Student(firstName, lastName); //Sending value from one form to another form.  
                        mainWindow.Show();
                        sr.Close();
                        Close();
                        break;
                    }
                    else if (lines[3] == email && lines[4] == password && lines[2] == "Teacher")
                    {
                        string classID = lines[6];
                        string lastName = lines[2];
                        mainWindow = new();
                        mainWindow.user = new Teacher(lastName, classID); //Sending value from one form to another form.
                        sr.Close();
                        mainWindow.Show();
                        this.Close();
                        break;
                    }
                    else
                    {
                        line = sr.ReadLine();
                    }
                }
                sr.Close();
                Error_Message.Text = "Please enter an existing email or password.";
            }
        }
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            registration = new Registration();
            registration.Show();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
