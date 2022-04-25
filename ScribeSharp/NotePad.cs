using System;
using System.Diagnostics;
using System.IO;

namespace ScribeSharp
{
    public class NotePad //: IImportable, IExportable
    {
        private string filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        private User user;
        private string fileName;
        public string Note { get; set; }
        public int CharacterCount => Note.Length;

        public NotePad(User user, string note, string fileName = "untitled")
        {
            this.user = user;
            Note = note;
            this.fileName = fileName;
            filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filePath).FullName).FullName).FullName + @"\Data\NoteBooks.txt";
        }

        public void Save()
        {
            try
            {
                using StreamWriter stream = new(filePath, true);
                stream.WriteLine(user.ToString());
                stream.WriteLine(fileName);
                stream.WriteLine(Note.Replace("\n", "˥"));
                stream.WriteLine(CharacterCount);
                

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
