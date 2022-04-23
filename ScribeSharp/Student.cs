using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class Student : User
    {
        private string _firstName;
        private string _lastName;
        private NoteBook _noteBook;

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
            return $"{FirstName} {LastName}";
        }
    }
}
