﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user;
        private NotePad notePad;
        private Registration registration;
        private Login login;
        private string note;
        public MainWindow()
        {
            InitializeComponent();
            registration = new();
            login = new();
            notePad = new(user, note);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notePad.Save();
        }


        private void Menu_Save_Click(object sender, EventArgs e)
        {
            notePad.Save();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            // ScribeSharp.AboutWindow = "Created By Dylan Commean and Rashaad Washington";
        }

        private void Menu_Calculator_Click(object sender, RoutedEventArgs e)
        {
            Calculator calc = new Calculator();
            calc.Show();
        }

        private void Menu_Start_Class(object sender, RoutedEventArgs e)
        {
            ClassroomPage classroom_page = new ClassroomPage(this);
            this.Content = classroom_page;
            //template on how to open page

        }

        private void Button_Sign_In_Click(object sender, RoutedEventArgs e) 
        {
            Login login = new();
            login.Show();
            this.Close();
        }

        private void User_Profile_Click(object sender, RoutedEventArgs e)
        {
            // Implement sometime.
        }

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            note = Notes.Text;
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}



