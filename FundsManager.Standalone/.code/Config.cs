namespace FundsManager {
    public class Config {
        
        #region properties
        //public string DatabaseServer { get; set; }
        //public string DatabaseAccount { get; set; }
        //public string DatabasePassword { get; set; }
        public string FontName { get; set; }
        public float FontSize { get; set; }
        public int ItemPerPage { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
        public bool IsAutoLogin { get; set; }
        #endregion

        public Config() {
            FontName = "宋体";
            FontSize = 9F;
            ItemPerPage = 25;
        }

        public void Save(Symbol.IO.Packing.TreePackage data, string keyBefore = null) {
            //data[keyBefore + "DatabaseServer"] = DatabaseServer;
            //data[keyBefore + "DatabaseAccount"] = DatabaseAccount;
            //data[keyBefore + "DatabasePassword"] = DatabasePassword;

            data[keyBefore + "FontName"] = FontName;
            data[keyBefore + "FontSize"] = FontSize;

            data[keyBefore + "ItemPerPage"] = ItemPerPage;

            data[keyBefore + "Account"] = Account;
            data[keyBefore + "Password"] = Password;
            data[keyBefore + "IsRemember"] = IsRemember;
            data[keyBefore + "IsAutoLogin"] = IsAutoLogin;
        }
        public void Load(Symbol.IO.Packing.TreePackage data, string keyBefore = null) {
            //DatabaseServer = data[keyBefore + "DatabaseServer"] as string;
            //DatabaseAccount = data[keyBefore + "DatabaseAccount"] as string;
            //DatabasePassword = data[keyBefore + "DatabasePassword"] as string;

            FontName = data[keyBefore + "FontName"] as string;
            if (string.IsNullOrEmpty(FontName))
                FontName = "宋体";
            FontSize = TypeExtensions.Convert<float>(data[keyBefore + "FontSize"], 9F);

            ItemPerPage = TypeExtensions.Convert<int>(data[keyBefore + "ItemPerPage"], 25);

            Account = data[keyBefore + "Account"] as string;
            Password = data[keyBefore + "Password"] as string;
            IsRemember = TypeExtensions.Convert<bool>(data[keyBefore + "IsRemember"], false);
            IsAutoLogin = TypeExtensions.Convert<bool>(data[keyBefore + "IsAutoLogin"], false);

        }

    }
}