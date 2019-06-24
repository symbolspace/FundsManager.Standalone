using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class LastMoneyLoginForm : Form {

        public LastMoneyLoginForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            OnOKClick();
        }

        void OnOKClick() {

            if (!UIValidtion.Create()
                           .Null(txtAccount, "帐号")
                           .Len(2, 16)
                           .Null(txtPassword, "密码")
                           .Len(2, 16)
                           .Result)
                return;

            //User model = Program.DataStore.FindUserByAccount(txtAccount.Text);
            //if (model == null) {
            //    Symbol.Forms.ProgramHelper.ShowInformation("帐号不存在！");
            //    txtAccount.Focus();
            //    return;
            //}

            //string md5 = Symbol.Encryption.MD5EncryptionHelper.Encrypt(txtPassword.Text);
            //if (!string.Equals(md5, model.Password, StringComparison.OrdinalIgnoreCase)) {
            //    Symbol.Forms.ProgramHelper.ShowInformation("密码不正确！");
            //    txtAccount.Focus();
            //    return;
            //}
            if (!Program.LastMoney.login(txtAccount.Text, txtPassword.Text)) {
                Symbol.Forms.ProgramHelper.ShowInformation(Program.LastMoney.lastMessage);
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void lbRegister_Click(object sender, EventArgs e) {
            using (LastMoneyRegisterForm form = new LastMoneyRegisterForm()) {
                form.Icon = Icon;
                form.Font = Font;

                if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                txtAccount.Text = form.Account;
                txtPassword.Text = form.Password;
            }
        }

    }
}
