using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class RegisterForm : Form {

        private User _model = null;
        private string _password = null;
        public User Model { get { return _model; } }
        public string Password { get { return _password; } }

        public RegisterForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }

        private void btnOK_Click(object sender, EventArgs e) {

            if (!UIValidtion.Create()
               .Null(txtAccount, "帐号")
               .Len(2, 16)
               .My(p=>{
                   if (Program.DataStore.ExistsUserWithAccount(p.LastValue)) {
                       p.Return(false)
                        .Msg("此帐号已经存在！")
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

            _model = Program.DataStore.Register(txtAccount.Text, txtPassword.Text, txtName.Text);
            _password = txtPassword.Text;
            Symbol.Forms.ProgramHelper.ShowInformation("帐号注册成功！");

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
