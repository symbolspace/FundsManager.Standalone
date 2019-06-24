using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    static class Program {
        #region fields
        private static readonly byte[] _passwordConfig = new byte[]{
            //000 001 002 003 004 005 006 007 008 009 010 011 012 013 014 015 016 017 018 019 020 021 022 023 024 025 026 027 028 029 030 031
              252,218,063,013,178,214,045,033,024,243,118,088,097,044,194,226,053,195,105,091,252,159,140,118,250,042,211,119,076,001,102,246
        };

        private static Config _config = null;
        private static IDataStore _dataStore = null;
        private static User _currentUser = null;
        private static ILastMoney _lastMoney = null;
        #endregion

        #region properties
        public static Config Config { get { return _config; } }
        public static User CurrentUser {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public static IDataStore DataStore { get { return _dataStore; } }
        public static bool IsSwitchAccount { get; set; }
        public static ILastMoney LastMoney { get { return _lastMoney; } }
        #endregion

        #region methods

        #region Main
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadConfig();

            //_dataStore = new Mssql2005DataStore();
            _dataStore = new SQLite3DataStore();
            _dataStore.CheckSchema();
            _lastMoney = new LastMoney();
        lb_Login:
            System.Drawing.Font font = null;
            System.Drawing.Icon icon = null;
            using (LoginForm loginForm = new LoginForm()) {
                string fontName = Program.Config.FontName;
                if (string.IsNullOrEmpty(fontName))
                    fontName = loginForm.Font.Name;
                loginForm.Font = font= new System.Drawing.Font(fontName, Program.Config.FontSize);
                icon = loginForm.Icon;
                if (loginForm.ShowDialog() != DialogResult.OK) {
                    Application.Exit();
                    return;
                }
            }
            IsSwitchAccount = false;
            MainForm mainForm = new MainForm();
            mainForm.Font = font;
            mainForm.Icon = icon;
            Application.Run(mainForm);
            if (IsSwitchAccount) {
                goto lb_Login;
            }

        }
        #endregion

        #region SaveConfig
        public static void SaveConfig() {
            try {
                Symbol.IO.Packing.TreePackage data = new Symbol.IO.Packing.TreePackage();
                _config.Save(data, "");
                string path = System.IO.Path.Combine(Application.StartupPath, "config.o");
                System.IO.File.WriteAllBytes(path, data.Save(_passwordConfig));
            } catch { }
        }
        #endregion
        #region LoadConfig
        static void LoadConfig() {
            _config = new Config();
            try {
                string path = System.IO.Path.Combine(Application.StartupPath, "config.o");
                if (System.IO.File.Exists(path)) {
                    using (System.IO.FileStream stream = System.IO.File.OpenRead(path)) {
                        Symbol.IO.Packing.TreePackage data = Symbol.IO.Packing.TreePackage.Load(stream, _passwordConfig);
                        _config.Load(data, "");
                    }
                } else {
                    SaveConfig();
                }
            } catch { }
        }
        #endregion

        #endregion

    }
}
