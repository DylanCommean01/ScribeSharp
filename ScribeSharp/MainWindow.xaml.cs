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

        public class NotePad
        {
            private List<string> notePad = new List<string>();

            public string Notes { get; set; }

            public override string ToString()
            {
                return Notes;
            }

        }

        public abstract class User
        {
            private NoteBook _noteBook;

            public abstract bool IsStudent();
            public abstract bool IsTeacher();

            public abstract void StartConversation();

        }
        public class Teacher : User
        {
            private string _classID;
            private string _lastName;

            public string ClassID { get; set; }

            public string LastName { get; }

            public Teacher(string classID, string lastName)
            {
                _classID = classID;
                _lastName = lastName;
            }

            public override bool IsStudent()
            {
                return false;
            }

            public override bool IsTeacher()
            {
                return true;
            }

            public override void StartConversation()
            {
                throw new NotImplementedException();
            }

            public override string ToString()
            {
                return $"{ClassID} - {LastName}:";
            }
        }

        public class Student : User
        {
            private string _firstName;
            private string _lastName;

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Student(string firstName, string lastName)
            {
                _firstName = firstName;
                _lastName = lastName;
            }

            public override bool IsStudent()
            {
                return true;
            }

            public override bool IsTeacher()
            {
                return false;
            }

            public override void StartConversation()
            {
                throw new NotImplementedException();
            }
            public override string ToString()
            {
                return $"{FirstName} - {LastName}:";
            }
        }


    }
}



