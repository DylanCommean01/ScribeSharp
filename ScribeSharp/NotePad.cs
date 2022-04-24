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
        private string _filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private User _user;
        private string _note;
        private string _fileName;
        private int _characterCount;
        public string Note { get; set; }
        public int CharacterCount => Note.Length;

        public NotePad(User user, string note)
        {
            _user = user;
            _note = note;
            _fileName = "untitled.txt";
            _characterCount = note.Length;
            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName + @"\Data\NoteBooks.txt";
        }

        public void Save()
        {
            try
            {
                using StreamWriter stream = new(_filePath, true);
                stream.WriteLine(_user.ToString());
                stream.WriteLine(_fileName);
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
