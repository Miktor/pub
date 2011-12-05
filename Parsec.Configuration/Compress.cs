using System.IO;
using System;
using System.IO.Packaging;

namespace Parsec.Configuration
{
    class Compress
    {       
        public static string PathToFile;
        Package package;

        public void CreateFile(string path)
        {
            PathToFile = path;
            package = Package.Open(PathToFile, FileMode.Create);
        }

        public void CloseFile()
        {
            package.Close();
        }

        public string RootFolder { get; set; }

        private Uri getFreeName(string destFilename)
        {
            int i = 0;
            int index = 0;
            string testName = string.Empty;

            Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
            if (Properties.Settings.Default.CreatingNameMode == 0)
                while (package.PartExists(uri))
                {
                    i++;
                    if ((index = destFilename.LastIndexOf('.')) > 0)
                        testName = destFilename.Insert(index, ("(" + i.ToString() + ")"));
                    uri = PackUriHelper.CreatePartUri(new Uri(testName, UriKind.Relative));
                }
            if (Properties.Settings.Default.CreatingNameMode == 1)
                while (package.PartExists(uri))
                {
                    i++;
                    if ((index = destFilename.LastIndexOf('.')) > 0)
                        testName = destFilename.Insert(index, ("(" + i.ToString() + ")"));
                    uri = PackUriHelper.CreatePartUri(new Uri(testName, UriKind.Relative));
                }

            return uri;
        }

        /// <summary>
        /// Adds the file to specific folder in package, defaut to root
        /// </summary>
        public void AddFile(string file, string folder = "")
        {
            string destFilename = folder + @"\" + Path.GetFileName(file);          
            
            PackagePart part = package.CreatePart(getFreeName(destFilename), "", CompressionOption.Normal);            
            
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (Stream dest = part.GetStream())
                {
                    CopyStream(fileStream, dest);
                }
            }    

        }

        /// <summary>
        /// Adds the file to specific folder in package, defaut to root
        /// </summary>
        public void AddFile(FileInfo fi, string folder = "")
        {
            AddFile(fi.FullName, folder);
        }

        /// <summary>
        /// Adds the file to package root
        /// </summary>
        public void AddFile(FileInfo fi)
        {
            AddFile(fi.FullName);
        }


        private void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }
    }
}
