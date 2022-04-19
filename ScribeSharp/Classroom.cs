using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class Classroom
    {
        private List<Student> _students;
        private MessageCenter _messageCenter;

        public Classroom(List<Student> students, MessageCenter messageCenter)
        {
            _students = students;
            _messageCenter = messageCenter;
        }

        public List<Student> Students { get; set; }
        public MessageCenter MessageCenter { get; set; }
    }
}
