using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Parsec.Configuration
{
    class FolderTrace
    {
        private delegate void ActionDelegate(FileInfo finfo, string folder);

        private void saveDel(FileInfo finfo, string folder)
        {
            Gatherer.Compresser.AddFile(finfo, folder);
        }

        private void saveOnlyWithExtension(FileInfo finfo, string folder)
        {
            if (finfo.Extension == ".xml" || finfo.Extension == ".ini" || finfo.Extension == ".config")
                Gatherer.Compresser.AddFile(finfo, folder);
        }

        public List<string> CompressFolder(string path, string folder = "", string offset = "")
        {
            Gatherer.Compresser.RootFolder = path;
            return Tracer(saveDel, path, folder, offset);
        }

        public List<string> TraceFolder(string path, string folder = "", string offset = "")
        {
            Gatherer.Compresser.RootFolder = path;
            return Tracer(saveOnlyWithExtension, path, folder, offset);
        }

        private List<string> Tracer(ActionDelegate del, string path, string folder = "", string offset = "")
        {
            List<string> output = new List<string>();

            FileInfo[] finfos;
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] subDirs = info.GetDirectories();
            FileInfo[] infos = info.GetFiles();

            foreach (FileInfo finfo in infos)
            {
                output.Add(offset + "+ " + finfo.Name + " | " + FileVersionInfo.GetVersionInfo(finfo.FullName).FileVersion);
                del(finfo, folder);
            }

            foreach (DirectoryInfo dir in subDirs)
            {
                output.Add(offset + "+ " + dir.Name);
                output.AddRange(Tracer(del, path + @"\" + dir.Name, folder + @"\" + dir.Name, offset + "|"));

                finfos = dir.GetFiles();
                foreach (FileInfo finfo in finfos)
                {
                    output.Add(offset + "    " + finfo.Name + " | " + FileVersionInfo.GetVersionInfo(finfo.FullName).FileVersion);
                    del(finfo, folder);                    
                }
            }

            return output;
        }
    }
}
