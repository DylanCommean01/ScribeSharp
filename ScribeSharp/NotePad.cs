using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class NotePad //: IImportable, IExportable
    {
        private string _note;
        private int _characterCount;
        public string Note { get; set; }
        public int CharacterCount => Note.Length;

        public NotePad(string note)
        {
            _note = note;
            _characterCount = note.Length;
        }

        public void Save()
        {
            // Will implement this later
        }

    }
}
