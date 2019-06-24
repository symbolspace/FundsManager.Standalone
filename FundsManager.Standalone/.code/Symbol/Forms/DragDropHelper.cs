using System.IO;
using System.Windows.Forms;
using System;

namespace Symbol.Forms {
    public class DragDropHelper {
        private static readonly string[] _emptyFiles = new string[0];
        public static string[] GetDragFiles(DragEventArgs e) {
            return GetDragFiles(e, true);
        }
        public static string[] GetDragFiles(DragEventArgs e, bool onlyFile) {
            if (e == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
                return _emptyFiles;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0)
                return _emptyFiles;
            if (onlyFile)
                return Array.FindAll(files,p => File.Exists(p));
            else
                return Array.FindAll(files,p => File.Exists(p) || Directory.Exists(p));
        }

        public static string GetDragFolder(DragEventArgs e) {
            if (e == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
                return null;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0)
                return null;
            string file = files[0];
            if (File.Exists(file))
                return new FileInfo(file).DirectoryName;
            else
                return file;
        }
    }
}