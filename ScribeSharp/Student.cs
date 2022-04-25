using System;
using System.Collections.Generic;
using System.IO;

namespace ScribeSharp
{
    public class Student : User
    {
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\NoteBooks.txt";
            NBook = InitializeNoteBook();
        }

        public NoteBook NBook { get; set; }
        private NoteBook InitializeNoteBook()
        {
            StreamReader sr = new(_filePath);
            SortedDictionary<string, string> sd = new();
            NoteBook noteBook = new(sd);
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Contains($"{FirstName} {LastName}"))
                {
                    string title = sr.ReadLine();
                    string notes = sr.ReadLine().Replace("˥", "\n");
                    noteBook.AddNotes(title, notes);
                    sr.ReadLine();
                }
                line = sr.ReadLine();
            }
            return noteBook;
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
            return $"{FirstName} {LastName}";
        }
    }
}
