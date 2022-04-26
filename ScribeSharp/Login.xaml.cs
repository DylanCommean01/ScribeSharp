using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace ScribeSharp
{
    public partial class Login : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private string _fileRoot = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private Registration registration;
        private MainWindow mainWindow;
        private sftpConnect connect;
        
        public Login()
        {
            InitializeComponent();
           
        }
        
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            connect = new();
            connect.readFile(Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\Users.txt");
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\Users.txt";
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
                string password = Password_Box.Password;
                StreamReader sr = new(_filePath);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lines = line.Split(' ');
                    if (lines[3] == email && lines[4] == password && lines[2] == "Student")
                    {
                        string firstName = lines[0];
                        string lastName = lines[1];
                        User user = new Student(firstName, lastName);
                        mainWindow = new();
                        mainWindow.Users = user; //Sending value from one form to another form.
                        mainWindow.menuItemUserProfile.Header = user.ToString();
                        
                        mainWindow.Show();
                        sr.Close();
                        Close();
                        if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetParent(_fileRoot).FullName).FullName).FullName + @"\Data\Users.txt"))
                        {
                            File.Delete(Directory.GetParent(Directory.GetParent(Directory.GetParent(_fileRoot).FullName).FullName).FullName + @"\Data\Users.txt");
                        }
                        break;
                    }
                    else if (lines[3] == email && lines[4] == password && lines[2] == "Teacher")
                    {
                        string classID = lines[5];
                        string lastName = lines[1];
                        string firstName = lines[0];
                        User user = new Teacher(firstName, lastName, classID);
                        mainWindow = new(classID);
                        mainWindow.Users = user; //Sending value from one form to another form.
                        mainWindow.menuItemUserProfile.Header = user.ToString();
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
            mainWindow = new();
            mainWindow.Show();
        }
    }
}
