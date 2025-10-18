using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace PDFBookmarks
{
    internal class PDFFile
    {
        private string _filePath;
        private string _fileName;

        public PDFFile(string filePath, string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;
        }
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        // Override ToString() to display the file name and page number in the list box
        public override string ToString()
        {
            return _fileName;
        }
    }
}
