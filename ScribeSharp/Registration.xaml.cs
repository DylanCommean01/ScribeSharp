using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private Login login;
        private bool userExists = false;
        private MainWindow mainWindow;

        public Registration()
        {
            InitializeComponent();
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\Users.txt";
        }
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            login = new Login();
            login.Show();
            Close();
        }
        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            Text_Box_First_Name.Text = "";
            Text_Box_Last_Name.Text = "";
            Text_Box_Email.Text = "";
            Password_Box.Password = "";
            Password_Box_Confirm.Password = "";
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            userExists = false;
            if (Text_Box_Email.Text.Length == 0)
            {
                Error_Message.Text = "Enter an appropriate email.";
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
                string firstname = Text_Box_First_Name.Text;
                string lastname = Text_Box_Last_Name.Text;
                string email = Text_Box_Email.Text;
                string profession = "Student";
                string classID = "";
                if (email.Contains("@my.apsu.edu")) {
                    profession = "Student";
                } else if (email.Contains("@apsu.edu")) {
                    profession = "Teacher";
                    classID = RandomString();
                }
                string password = Password_Box.Password;
                if (Password_Box.Password.Length == 0)
                {
                    Error_Message.Text = "Enter an appropriate password.";
                    Password_Box.Focus();
                }
                else if (Password_Box_Confirm.Password.Length == 0)
                {
                    Error_Message.Text = "Enter Confirm password.";
                    Password_Box_Confirm.Focus();
                }
                else if (Password_Box.Password != Password_Box_Confirm.Password)
                {
                    Error_Message.Text = "Confirm password must be same as password.";
                    Password_Box_Confirm.Focus();
                }
                else
                {
                    using StreamReader sr = new(_filePath);
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] lines = line.Split(' ');
                        if (lines[3] == email)
                        {
                            userExists = true;
                            Error_Message.Text = "A user already exists with this email.";
                            break;
                        }
                        else
                        {
                            line = sr.ReadLine();
                        }
                    }
                    sr.Close();
                    if (!userExists)
                    {
                        try
                        {
                            using StreamWriter stream = new(_filePath, true);
                            stream.Write($"{firstname} {lastname} {profession} {email} {password} {classID}\n");
                            stream.Close();
                            Error_Message.Text = "You have registered successfully.";
                            this.Close();
                            mainWindow = new();
                            mainWindow.user = new Student(firstname, lastname);
                            mainWindow.Show();
                        }
                        catch (InvalidOperationException)
                        {
                            Trace.WriteLine("WARNING: File exists but is read-only.");
                        }
                        catch (PathTooLongException)
                        {
                            Trace.WriteLine("WARNING: The path name is too long.");
                        }
                        catch (IOException)
                        {
                            Trace.WriteLine("WARNING: Disk is full.");
                        }
                        catch (Exception exc)
                        {
                            Trace.WriteLine("WARNING: Something went terribly wrong...");
                            Trace.WriteLine(exc);
                        }
                    }
                }
            }
        }
        private static string RandomString() {
            Random rand = new Random();

            // Choosing the size of string
            // Using Next() string
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number.
                randValue = rand.Next(0, 26);

                // Generating random character by converting
                // the random number into character.
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string.
                str = str + letter;
            }
            return str;
        }
        private void Button_Sign_In_Click(object sender, RoutedEventArgs e) {
            login = new();
            this.Close();
            login.Show();
        }
    }
}