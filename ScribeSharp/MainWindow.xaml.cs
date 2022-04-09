using System;
using System.Collections.Generic;
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
        public MainWindow()
        {

            InitializeComponent();
            /*LogIn login;
            NotePad notePad;
            Classroom classroom;

            if (!login.isVerified())
            {
                User user = new User(login);
            }

            switch (user.TypeOf())
            {
                case student:
                    notePad = new NotePad(student);
                    break;
                case teacher:
                    notePad = new NotePad(teacher);
                    classroom = new NotePad(teacher);

                    break;
                default: //Something happens
                    break;
            }*/
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // SaveNotes();
        }

        private void Menu_Save_Click(object sender, EventArgs e)
        {
            // SaveNotes();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            // ScribeSharp.AboutWindow = "Created By Dylan Commean and Rashaad Washington";
        }

        class NotePad
        {
            private List<string> notePad = new List<string>();

            public string Notes { get; set; }

            public override string ToString()
            {
                return Notes;
            }

        }

        class LogIn
        {
            private string _loginEmail;
            private string _loginPassword;
            private bool _signout;

            public LogIn(string loginEmail, string LoginPassword)
            {
                _loginEmail = loginEmail;
                _loginPassword = LoginPassword;
                _signout = false;
            }

            public void Signout()
            {
                _loginEmail = "";
                _loginPassword = "";
                _signout = true;
            }
        }

        class Classroom
        {
            private List<User> students = new List<User>();
        }

        class User
        {
            private enum Users { Student, Teacher, Anonymous }

            public User(LogIn login)
            {
                
            }
        }
    }
}



