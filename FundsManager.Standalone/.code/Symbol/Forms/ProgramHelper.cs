using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
//using Symbol.Serialization;

namespace Symbol.Forms {

    public class ProgramHelper {
        public static string MapPath(string path) {
            if (string.IsNullOrEmpty(path))
                return AppPath;
            if (path.StartsWith("/") || path.StartsWith("\\")) {
                //ShowCustomInformation("MapPath\r\npath:{0}\r\nnew:{1}\r\nappPath:{2}", path, Path.Combine(AppPath, path), AppPath);
                path = path.Substring(1);
                //ShowCustomInformation("MapPath\r\npath:{0}\r\nnew:{1}\r\nappPath:{2}", path, Path.Combine(AppPath, path), AppPath);
            }
            return Path.Combine(AppPath, path);
        }
        private static string _appPath;
        public static string AppPath {
            get {
                if (string.IsNullOrEmpty(_appPath)) {
                    _appPath = Path.GetDirectoryName(AppFile);
                    if (!_appPath.EndsWith("\\"))
                        _appPath += "\\";
                }
                return _appPath;
            }

        }

        private static string _appFile;
        public static string AppFile {
            get {
                if (string.IsNullOrEmpty(_appFile)) {
                    Assembly assembly = Assembly;
                    if (!string.IsNullOrEmpty(assembly.CodeBase)) {
                        _appFile = assembly.CodeBase.Replace("file:///", "").Replace("/", "\\");
                    } else {
                        _appFile = assembly.Location;
                    }
                }
                return _appFile;
            }
        }

        private static Assembly _assembly;
        public static Assembly Assembly {
            get {
                if (_assembly == null)
                    _assembly = Assembly.GetEntryAssembly();
                if (_assembly == null)
                    _assembly = Assembly.GetCallingAssembly();
                return _assembly;
            }
        }
        //public static TConfig LoadXmlConfig<TConfig>() where TConfig : class {
        //    string name = typeof(TConfig).FullName2().Replace('<', '(').Replace('>', ')');
        //    string path = MapPath(string.Format("{0}.xml", name));
        //    TConfig result = default(TConfig);
        //    if (File.Exists(path)) {
        //        result = result.FromXmlFile(path);
        //    }
        //    return result;
        //}
        //public static void SaveXmlConfig<TConfig>(TConfig config) where TConfig : class {
        //    string name = typeof(TConfig).FullName2().Replace('<', '(').Replace('>', ')');
        //    string path = MapPath(string.Format("{0}.xml", name));
        //    string directory = Path.GetDirectoryName(path);
        //    if (!Directory.Exists(directory))
        //        Directory.CreateDirectory(directory);
        //    config.ToXmlFile(path);
        //}

        public static string LoadTextFile(string path) {
            return LoadTextFile(path, null);
        }
        public static string LoadTextFile(string path, Encoding encoding) {
            string file = MapPath(path);
            if (!File.Exists(file))
                return string.Empty;
            if (encoding == null) {
                return File.ReadAllText(path);
            } else {
                return File.ReadAllText(path, encoding);
            }
        }

        public static void SaveTextFile(string path, string contents) {
            SaveTextFile(path, contents, false, null);
        }
        public static void SaveTextFile(string path, string contents, Encoding encoding) {
            SaveTextFile(path, contents, false, encoding);
        }
        public static void SaveTextFile(string path, string contents, bool isAppend) {
            SaveTextFile(path, contents, isAppend, null);
        }
        public static void SaveTextFile(string path, string contents, bool isAppend, Encoding encoding) {
            string file = MapPath(path);
            string directory = Path.GetDirectoryName(file);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            if (isAppend) {
                if (encoding == null)
                    File.AppendAllText(file, contents);
                else
                    File.AppendAllText(file, contents, encoding);
            } else {
                if (encoding == null)
                    File.WriteAllText(file, contents);
                else
                    File.WriteAllText(file, contents, encoding);
            }
        }

        public static void DeleteFile(string path) {
            path = MapPath(path);
            if (!File.Exists(path))
                return;
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
        }
        public static void DeleteDirectory(string path) {
            DeleteDirectory(path, true);
        }
        public static void DeleteDirectory(string path, bool deleteSelf) {
            path = MapPath(path);
            if (!Directory.Exists(path))
                return;
            foreach (string item in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly)) {
                DeleteDirectory(item);
            }
            foreach (string item in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly)) {
                System.IO.File.SetAttributes(item, FileAttributes.Normal);
                System.IO.File.Delete(item);
            }
            if (deleteSelf)
                Directory.Delete(path);
        }
        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs) {
            sourceDirName = MapPath(sourceDirName);
            destDirName = MapPath(destDirName);

            if (!Directory.Exists(sourceDirName))
                throw new DirectoryNotFoundException(sourceDirName);
            if (!Directory.Exists(destDirName))
                CreateDirectory(destDirName);

            FileInfo[] files = new DirectoryInfo(sourceDirName).GetFiles();
            foreach (FileInfo file in files) {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs) {

                DirectoryInfo[] dirs = new DirectoryInfo(sourceDirName).GetDirectories();
                foreach (DirectoryInfo subdir in dirs) {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public static void CreateDirectory(string path) {
            CreateDirectory(path, true);
        }
        public static void CreateDirectory(string path, bool isMapPath) {
            if (isMapPath)
                path = MapPath(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #region show message
        public static void ShowWarning(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowCustomWarning(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            MessageBoxForm.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool ShowQuestion(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            return MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }
        public static DialogResult ShowQuestion(MessageBoxButtons buttons, string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            return MessageBox.Show(message, "提示", buttons, MessageBoxIcon.Question);
        }
        public static bool ShowCustomQuestion(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            return MessageBoxForm.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }
        public static DialogResult ShowCustomQuestion(MessageBoxButtons buttons, string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            return MessageBoxForm.Show(message, "提示", buttons, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 弹出信息窗口
        /// </summary>
        /// <param name="format">信息格式</param>
        /// <param name="args">需要的参数</param>
        /// <remarks>由于这是调用系统的MessageBox，在多线程时会被其它程序窗口挡住。</remarks>
        public static void ShowInformation(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            MessageBox.Show(message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 弹出信息窗口（置顶）
        /// </summary>
        /// <param name="format">信息格式</param>
        /// <param name="args">需要的参数</param>
        /// <remarks>由于这是一个置顶的窗口，在多线程时不会被其它程序窗口挡住。</remarks>
        public static void ShowCustomInformation(string format, params object[] args) {
            string message = args == null ? format : string.Format(format, args);
            MessageBoxForm.Show(message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion


        /// <summary>
        /// 从当前屏幕上消失
        /// </summary>
        /// <param name="form">需要调整的窗体</param>
        /// <remarks>屏幕左上角之外的位置，为了防止多屏被另一个屏幕看见，是当前屏幕大小的双倍。</remarks>
        public static void HideLoadForm(Form form) {
            ControlExtensions.ThreadInvoke(form,() => {
                Screen screen = Screen.FromControl(form);
                form.Left = -screen.Bounds.Width * 2;
                form.Top = -screen.Bounds.Height * 2;
            });
        }

        /// <summary>
        /// 显示到当前窗体所在屏幕的中心
        /// </summary>
        /// <param name="form">需要调整的窗体</param>
        public static void ShowLoadForm(Form form) {
            ControlExtensions.ThreadInvoke(form,() => {
                Screen screen = Screen.FromControl(form);
                form.Left = (screen.Bounds.Width - form.Width) / 2;
                form.Top = (screen.Bounds.Height - form.Height) / 2;
            });
        }

        private static System.Threading.Mutex _instance;
        public static bool IsPrevInstance(string instanceName) {
            bool createdNew;
            _instance = new System.Threading.Mutex(true, instanceName, out createdNew); //同步基元变量
            if (createdNew)
                return false;
            return true;
        }
        public static void Exit() {
            if (_instance != null) {
                _instance.Close();
                //_instance.Dispose();
                _instance = null;
            }
        }
    }

}