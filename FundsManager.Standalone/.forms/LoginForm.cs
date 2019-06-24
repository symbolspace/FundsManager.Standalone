using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class LoginForm : Form {

        public LoginForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            txtAccount.Text = Program.Config.Account;
            txtPassword.Text = Program.Config.Password;
            chkIsRemember.Checked = Program.Config.IsRemember;
            chkIsAutoLogin.Checked = Program.Config.IsAutoLogin;

            if (string.IsNullOrEmpty(txtAccount.Text) && string.IsNullOrEmpty(txtPassword.Text) && !chkIsRemember.Checked && !chkIsAutoLogin.Checked) {
                //chkIsAutoLogin.Checked = true;
            } else if (chkIsAutoLogin.Checked && !Program.IsSwitchAccount) {
                new System.Threading.Thread(() => {
                    System.Threading.Thread.Sleep(50);
                    Symbol.Forms.ControlExtensions.ThreadInvoke(this, () => {
                        OnOKClick();
                    });
                }) { IsBackground=true }.Start();
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            OnOKClick();
        }

        void OnOKClick() {

            if (!UIValidtion.Create()
                           .Null(txtAccount, "帐号")
                           .Len(2, 16)
                           .Null(txtPassword, "密码")
                           .Len(6, 16)
                           .Result)
                return;

            User model = Program.DataStore.FindUserByAccount(txtAccount.Text);
            if (model == null) {
                Symbol.Forms.ProgramHelper.ShowInformation("帐号不存在！");
                txtAccount.Focus();
                return;
            }

            string md5 = Symbol.Encryption.MD5EncryptionHelper.Encrypt(txtPassword.Text);
            if (!string.Equals(md5, model.Password, StringComparison.OrdinalIgnoreCase)) {
                Symbol.Forms.ProgramHelper.ShowInformation("密码不正确！");
                txtAccount.Focus();
                return;
            }

            Program.CurrentUser = model;

            if (chkIsRemember.Checked) {
                Program.Config.Account = txtAccount.Text;
                Program.Config.Password = txtPassword.Text;
                Program.Config.IsRemember = true;
                Program.Config.IsAutoLogin = chkIsAutoLogin.Checked;
            } else {
                Program.Config.Account = "";
                Program.Config.Password = "";
                Program.Config.IsRemember = false;
                Program.Config.IsAutoLogin = false;
            }
            Program.SaveConfig();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void chkIsAutoLogin_CheckedChanged(object sender, EventArgs e) {
            if (chkIsAutoLogin.Checked)
                chkIsRemember.Checked = true;
        }

        private void lbRegister_Click(object sender, EventArgs e) {
            using (RegisterForm form = new RegisterForm()) {
                form.Icon = Icon;
                form.Font = Font;

                if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                txtAccount.Text = form.Model.Account;
                txtPassword.Text = form.Password;
            }
        }

        private void chkIsRemember_CheckedChanged(object sender, EventArgs e) {
            if (!chkIsRemember.Checked)
                chkIsAutoLogin.Checked = false;
        }
    }
}
