using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotePad notePad;
        private Registration registration;
        private Login login;
        private User users;
        private string note;

        public MainWindow()
        {
            InitializeComponent();
            note = Notes.Text;
            registration = new();
            login = new();
        }

        public User Users
        {
            get => users;
            set
            {
                users = value;
                notePad = new(Users, note);
            }
        }

        public string FileName { get; set; }

        private void Window_Closed(object sender, RoutedEventArgs e)
        {
            if (Users != null)
            {
                notePad.Save();
            }
        }

        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Users != null)
            {
                notePad.Save();
            }
        }

        private void Menu_Save_As_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Text file|*.txt|Word Document|*.docx|PDF Document|*.pdf";
            saveFileDialog.Title = "Save a text File";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, Notes.Text);
            }
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
            if (Users != null)
            {
                Calculator calc = new();
                calc.Show();
            }
        }

        private void Menu_Start_Class(object sender, RoutedEventArgs e)
        {
            if (Users != null && Users.IsTeacher())
            {
                ClassroomPage classroom_page = new ClassroomPage(this);
                this.Content = classroom_page;
                //template on how to open page
            }

        }

        private void Button_Sign_In_Click(object sender, RoutedEventArgs e)
        {
            if (Users == null)
            {
                login = new();
                login.Show();
                this.Close();
            }
        }

        private void User_Profile_Click(object sender, RoutedEventArgs e)
        {
            // Implement maybe later.
        }

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            note = Notes.Text;
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            if (Users == null)
            {
                registration = new Registration();
                registration.Show();
                this.Close();
            }
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            if (Users != null)
            {
                Users = null;
            }
        }

        private void Menu_Rename_Click(object sender, RoutedEventArgs e)
        {
            noteNameBlock.Visibility = Visibility.Hidden;
            noteNameBox.Visibility = Visibility.Visible;
        }

        private void On_Enter_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                noteNameBlock.Text = noteNameBox.Text;
                FileName = noteNameBlock.Text;
                noteNameBlock.Visibility = Visibility.Visible;
                noteNameBox.Visibility = Visibility.Hidden;
            }
        }

    }
}



