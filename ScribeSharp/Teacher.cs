using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{

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
            // Something I will implement later.
        }

        public override string ToString()
        {
            return $"{ClassID} - {LastName}:";
        }
    }
}
