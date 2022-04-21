﻿using System;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Registration registration;
        private MainWindow mainWindow;
        private SqlConnection con;

        public Login()
        {
            InitializeComponent();
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
                string password = Password_Box.Password;
                con = new SqlConnection("Data Source=; Initial Catalog=Data; User ID=; Password=");
                con.Open();
                SqlCommand cmd = new("Select * from Registration where Email='" + email + "'  and password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new();
                //adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                    mainWindow = new MainWindow();
                    mainWindow.menuItemUserProfile.Header = username; //Sending value from one form to another form.  
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    Error_Message.Text = "Please enter an existing email or password.";
                }
                // con.Close();
            }
        }
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            registration = new Registration();
            registration.Show();
        }

        private void Button_MainWindow_Click(object sender, RoutedEventArgs e) 
        {
            this.Hide();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            //return to Home screen
        }
    }
} 
