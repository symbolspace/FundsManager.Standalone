using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class ChangePasswordForm : Form {

        private string _password = null;
        public string Password { get { return _password; } }

        public ChangePasswordForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            txtAccount.Text = Program.CurrentUser.Account;
        }

        private void btnOK_Click(object sender, EventArgs e) {

            if (!UIValidtion.Create()
               .Null(txtOldPassword, "旧密码")
               .Len(6, 16)
               .My(p=>{
                   string md5 = Symbol.Encryption.MD5EncryptionHelper.Encrypt(p.LastValue);
                   if (!string.Equals(md5, Program.CurrentUser.Password)) {
                       p.Return(false).Msg("旧密码不正确！").LastControl.Focus();
                   }
               })
               .Null(txtPassword, "密码")
               .Len(6, 16)
               .Null(txtPassword2, "确认密码")
               .Len(6, 16)
               .Eq(txtPassword.Text,"两次密码输入不一样！")
               .Result)
                return;

            string newPassword = txtPassword.Text;
            string newPasswordMd5 = Symbol.Encryption.MD5EncryptionHelper.Encrypt(newPassword);
            _password = newPassword;

            Program.DataStore.ChangePassword(Program.CurrentUser.Account, newPassword);
            Program.CurrentUser.Password = newPasswordMd5;

            if (Program.Config.IsRemember && !string.IsNullOrEmpty(Program.Config.Password)) {
                Program.Config.Password = newPassword;
                Program.SaveConfig();
            }
            Symbol.Forms.ProgramHelper.ShowInformation("密码修改成功！");

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
