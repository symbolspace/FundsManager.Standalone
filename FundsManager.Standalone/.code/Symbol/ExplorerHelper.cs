using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Symbol {
    [Flags]
    public enum ExplorerShowOptions {
        None = 0,
        NewWindow = 1,
        Select = 2,
        DefaultStyle = 4,
    }
    public class ExplorerHelper {
        public static Process Show(string path) {
            return Show(path, ExplorerShowOptions.NewWindow | ExplorerShowOptions.Select);
        }
        public static Process Show(string path, ExplorerShowOptions options) {
            List<string> list = new List<string>();
            if (options != ExplorerShowOptions.None) {
                if ((options & ExplorerShowOptions.NewWindow) == ExplorerShowOptions.NewWindow)
                    list.Add("/n");
                if ((options & ExplorerShowOptions.DefaultStyle) == ExplorerShowOptions.DefaultStyle)
                    list.Add("/e");
                if ((options & ExplorerShowOptions.Select) == ExplorerShowOptions.Select)
                    list.Add("/select");
            }
            list.Add(string.Format("\"{0}\"", path));

            return Process.Start("explorer", string.Join(",", list.ToArray()));
        }
    }
}