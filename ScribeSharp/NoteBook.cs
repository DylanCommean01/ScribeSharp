using System.Collections.Generic;

namespace ScribeSharp
{
    public class NoteBook
    {
        private SortedDictionary<string, string> _notes;
        public NoteBook(SortedDictionary<string, string> notes)
        {
            _notes = notes;
        }

        public void AddNotes(string name, string note)
        {
            _notes.Add(name, note);
        }

        public bool DeleteNotes(string name)
        {
            return _notes.Remove(name);
        }

        public void EditNotes(string name, string note)
        {
            // Will implement this later
        }
    }
}
