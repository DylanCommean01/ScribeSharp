using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class NoteBook
    {
        private SortedDictionary<string, NotePad> _notes;
        public NoteBook(SortedDictionary<string, NotePad> notes)
        {
            _notes = notes;
        }

        public void AddNotes(string name, NotePad note)
        {
            _notes.Add(name, note);
        }

        public bool DeleteNotes(string name)
        {
            return _notes.Remove(name);
        }

        public void EditNotes(string name, NotePad note)
        {
            // Will implement this later
        }
    }
}
