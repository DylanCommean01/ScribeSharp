using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScribeSharp
{
    public partial class MainWindow : Window
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private NotePad notePad;
        private Registration registration;
        private NoteBook noteBook;
        private SortedDictionary<string, string> sd;
        private Login login;
        private User users;
        private string note;
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\NoteBooks.txt";
            fileName = "untitled";
            Note = Notes.Text;
            registration = new();
            login = new();
            sd = new();
        }
        public User Users
        {
            get => users;
            set
            {
                users = value;
                if (Users != null)
                {
                    // User is signed in and functionality is allowed.
                    if (Users.IsStudent())
                    {
                        ShowFunctionalityForStudent();
                    }
                    else if (Users.IsTeacher())
                    {
                        ShowFunctionalityForTeacher();
                    }
                    //Initialize current notePad and Notebook
                    sd = GetDataForNoteBook();
                    notePad = new(Users, Note, fileName);
                    noteBook = new(sd);
                }
                else
                {
                    // User has signed out functionality is removed.
                    HideFunctionalityFromNonUser();
                }
            }
        }

        public string Note
        {
            get { return note; }
            set
            {
                Notes.Text = value;
                note = value;
                Users = users;
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                noteNameBlock.Text = value;
                noteNameBox.Text = value;
                fileName = value;
                Users = users;
            }
        }

        private void Window_Closed(object sender, RoutedEventArgs e)
        {
            //   if (Users != null && ItemIsNotDuplicateName())
            //   {
            //       notePad.Save();
            //   }
        }

        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Users != null && ItemIsNotDuplicateName())
            {
                notePad.Save();
                ReOpenOnNewWindow();
            }
            else
            {
                MessageBox.Show("The file you tried to add already exists, please rename your note.");
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
            else if (saveFileDialog.Filter == "PDF Document|.*pdf")
            {
                //Implement later
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
            //if (Users != null && Users.IsTeacher())
            //{
                ClassroomPage classroom_page = new ClassroomPage(this);
                this.Content = classroom_page;
                //template on how to open page
            //}

        }

        private void Button_Sign_In_Click(object sender, RoutedEventArgs e)
        {
            if (Users == null)
            {
                login = new();
                login.Show();
                Close();
            }
        }

        private void User_Profile_Click(object sender, RoutedEventArgs e)
        {
            // Implement maybe later.
        }

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            Note = Notes.Text;
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            if (Users == null)
            {
                registration = new Registration();
                registration.Show();
                Close();
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

        private void HideFunctionalityFromNonUser()
        {
            menuItemSave.Visibility = Visibility.Collapsed;
            menuItemUserProfile.Header = "User Profile";
            menuTools.Visibility = Visibility.Collapsed;
            noteBookHeader.Visibility = Visibility.Collapsed;
            menuItemClassroom.Visibility = Visibility.Collapsed;
            menuItemLogin.Visibility = Visibility.Visible;
            menuItemRegister.Visibility = Visibility.Visible;
            menuItemLogout.Visibility = Visibility.Collapsed;
            note = "";
            fileName = "untitled";
        }

        private void ShowFunctionalityForStudent()
        {
            menuItemSave.Visibility = Visibility.Visible;
            menuTools.Visibility = Visibility.Visible;
            noteBookHeader.Visibility = Visibility.Visible;
            menuItemClassroom.Visibility = Visibility.Visible;
            menuItemLogin.Visibility = Visibility.Collapsed;
            menuItemRegister.Visibility = Visibility.Collapsed;
            menuItemLogout.Visibility = Visibility.Visible;
            MenuItemJoinClass.Visibility = Visibility.Visible;
            MenuItemLeaveClass.Visibility = Visibility.Visible;
        }
        private void ShowFunctionalityForTeacher()
        {
            menuItemSave.Visibility = Visibility.Visible;
            menuTools.Visibility = Visibility.Visible;
            noteBookHeader.Visibility = Visibility.Visible;
            menuItemClassroom.Visibility = Visibility.Visible;
            MenuItemEndClass.Visibility = Visibility.Visible;
            MenuItemStartClass.Visibility = Visibility.Visible;
            menuItemLogin.Visibility = Visibility.Collapsed;
            menuItemRegister.Visibility = Visibility.Collapsed;
            menuItemLogout.Visibility = Visibility.Visible;
        }

        private SortedDictionary<string, string> GetDataForNoteBook()
        {
            SortedDictionary<string, string> sd = new();
            StreamReader sr = new(_filePath);
            string line = sr.ReadLine();
            int count = 0;
            while (line != null)
            {
                if (line.Equals(Users.ToString()))
                {
                    string noteName = sr.ReadLine();
                    string noteBody = sr.ReadLine();
                    sd.Add(noteName, noteBody);
                    count++;
                    FillMenuItemsWithNoteBooks(noteName, count);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return sd;
        }

        private void FillMenuItemsWithNoteBooks(string noteName, int count)
        {
            switch (count)
            {
                case 1:
                    noteBooksOne.Header = noteName;
                    noteBooksOne.Visibility = Visibility.Visible;
                    break;
                case 2:
                    noteBooksTwo.Header = noteName;
                    noteBooksTwo.Visibility = Visibility.Visible;
                    break;
                case 3:
                    noteBooksThree.Header = noteName;
                    noteBooksThree.Visibility = Visibility.Visible;
                    break;
                case 4:
                    noteBooksFour.Header = noteName;
                    noteBooksFour.Visibility = Visibility.Visible;
                    break;
                case 5:
                    noteBooksFive.Header = noteName;
                    noteBooksFive.Visibility = Visibility.Visible;
                    break;
                case 6:
                    noteBooksSix.Header = noteName;
                    noteBooksSix.Visibility = Visibility.Visible;
                    break;
                case 7:
                    noteBooksSeven.Header = noteName;
                    noteBooksSeven.Visibility = Visibility.Visible;
                    break;
                case 8:
                    noteBooksEight.Header = noteName;
                    noteBooksEight.Visibility = Visibility.Visible;
                    break;
                case 9:
                    noteBooksNine.Header = noteName;
                    noteBooksNine.Visibility = Visibility.Visible;
                    break;
                case 10:
                    noteBooksTen.Header = noteName;
                    noteBooksTen.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void Button_Open_NoteBook_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)e.OriginalSource;
            MenuItem parent = (MenuItem)mi.Parent;
            string currentNoteBookName = parent.Header.ToString();
            notePad = new NotePad(Users, sd.GetValueOrDefault(currentNoteBookName), currentNoteBookName);
            Note = sd.GetValueOrDefault(currentNoteBookName).Replace("˥", "\n");
            FileName = currentNoteBookName;
            noteNameBlock.Text = currentNoteBookName;
            noteNameBox.Text = currentNoteBookName;
            ReOpenOnNewWindow();
        }

        private void Button_Delete_NoteBook_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)e.OriginalSource;
            MenuItem parent = (MenuItem)mi.Parent;
            string currentNoteBookName = parent.Header.ToString();
            noteBook.DeleteNotes(currentNoteBookName);
            RemoveFromNoteBookData(currentNoteBookName);
            ReOpenOnNewWindow();

        }

        private void ReOpenOnNewWindow()
        {
            MainWindow newWindow = new();
            Application.Current.MainWindow = newWindow;
            newWindow.Users = users;
            newWindow.Note = note;
            newWindow.FileName = fileName;
            newWindow.Show();
            Close();
        }

        private bool ItemIsNotDuplicateName()
        {
            StreamReader sr = new(_filePath);
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Equals(Users.ToString()))
                {
                    line = sr.ReadLine();
                    if (line.Equals(fileName))
                    {
                        return false;
                    }
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return true;
        }

        private void RemoveFromNoteBookData(string currentNoteBookName)
        {
            string tempFile = Path.GetTempFileName();
            using StreamReader sr = new(_filePath);
            using StreamWriter sw = new(tempFile);
            string userName = sr.ReadLine();
            string title;
            string noteBody;
            string charCount;

            while (userName != null)
            {
                title = sr.ReadLine();
                noteBody = sr.ReadLine();
                charCount = sr.ReadLine();
                if (!currentNoteBookName.Equals(title))
                {
                    sw.WriteLine(userName);
                    sw.WriteLine(title);
                    sw.WriteLine(noteBody);
                    sw.WriteLine(charCount);
                }
                if (!userName.Equals(""))
                {
                    userName = sr.ReadLine();
                }
            }
            sr.Close();
            sw.Close();
            File.Delete(_filePath);
            File.Move(tempFile, _filePath);
        }

        private void Button_New_Note_Click(object sender, RoutedEventArgs e)
        {
            notePad = new(Users, "");
            Note = "";
            fileName = "untitled";
            ReOpenOnNewWindow();
        }

        private void Button_Compiler_Click(object sender, RoutedEventArgs e) 
        {
            string url = "https://dotnetfiddle.net/";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = url;
            Process.Start(startInfo);
        }
    }
}