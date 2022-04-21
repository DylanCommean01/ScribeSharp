using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Login login;
        private SqlConnection con;
        public Registration()
        {
            InitializeComponent();
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
                    Error_Message.Text = "";
                    con = new SqlConnection("Data Source=; Initial Catalog=Data; User ID=; Password=");
                    con.Open();
                    SqlCommand cmd = new("Insert into Registration(FirstName,LastName,Email,Password,Address) values('" + firstname + "','" + lastname + "','" + email + "','" + password + "')", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Error_Message.Text = "You have Registered successfully.";
                    Reset();
                }
            }
        }

        private void Button_NotePad_Click(object sender, RoutedEventArgs e) 
        {
            this.Hide();
        }
    }
}