using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class LastMoneyRegisterForm : Form {
        private string _telephone = null;
        private string _password = null;
        public string Password { get { return _password; } }
        public string Account { get { return _telephone; } }

        public LastMoneyRegisterForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }

        private void btnOK_Click(object sender, EventArgs e) {

            if (!UIValidtion.Create()
               .Null(txtAccount, "手机")
               .Len(11,11)
               .My(p=>{
                   if (Program.LastMoney.user_exists(new { telephone= p.LastValue })) {
                       p.Return(false)
                        .Msg("此手机已注册！")
                        .LastControl.Focus();
                   }
               })
               .Null(txtPassword, "密码")
               .Len(6, 16)
               .Null(txtPassword2, "确认密码")
               .Len(6, 16)
               .Eq(txtPassword.Text,"两次密码输入不一样！")
               .Null(txtName, "姓名")
               .Len(2, 10)
               .Result)
                return;
            bool ok= Program.LastMoney.register(new {
                telephone = txtAccount.Text,
                password = txtPassword.Text,
                nickName = txtName.Text,
                type = "User",
            });
            _telephone = txtAccount.Text;
            _password = txtPassword.Text;
            Symbol.Forms.ProgramHelper.ShowInformation("帐号注册成功！");

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
