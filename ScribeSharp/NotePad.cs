using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class NotePad //: IImportable, IExportable
    {
        private const string FILE = @"..\Data\Notes.txt";
        private User _user;
        private string _note;
        private int _characterCount;
        public string Note { get; set; }
        public int CharacterCount => Note.Length;

        public NotePad(User user, string note)
        {
            _user = user;
            _note = note;
            _characterCount = note.Length;
        }

        public void Save()
        {
            try
            {
                using StreamWriter stream = new(FILE);
                stream.WriteLine(_note);
                stream.Close();
            }
            catch (InvalidOperationException)
            {
                Trace.WriteLine("WARNING: File exists but is read-only.");
            }
            catch (PathTooLongException)
            {
                Trace.WriteLine("WARNING: The path name is too long.");
            }
            catch (IOException)
            {
                Trace.WriteLine("WARNING: Disk is full.");
            }
            catch (Exception e)
            {
                Trace.WriteLine("WARNING: Something went terribly wrong...");
                Trace.WriteLine(e);
            }
        }

    }
}
