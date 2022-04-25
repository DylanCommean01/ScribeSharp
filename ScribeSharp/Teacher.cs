using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScribeSharp
{

    public class Teacher : User
    {
        private string _firstName;
        private string _lastName;
        private string _classID;
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public string FirstName { get { return _firstName; } }
        public string ClassID { get { return _classID; } }

        public string LastName { get { return _lastName; } }

        public Teacher(string firstName, string lastName, string classID)
        {
            _lastName = lastName;
            _classID = classID;
            _firstName = firstName;
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
            return false;
        }

        public override bool IsTeacher()
        {
            return true;
        }

        public override void StartConversation()
        {
            // Something I will implement later.
        }

        public override string ToString()
        {
            return $"{LastName} {ClassID}";
        }
    }
}
