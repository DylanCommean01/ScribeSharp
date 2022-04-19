using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public abstract class User
    {
        private NoteBook _noteBook;

        public abstract bool IsStudent();
        public abstract bool IsTeacher();

        public abstract void StartConversation();

    }
}
