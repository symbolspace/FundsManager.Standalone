using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class AddFundsForm : Form {

        private Funds _model = null;
        public Funds Model { get { return _model; } }

        public AddFundsForm() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!UIValidtion.Create()
                            .Null(txtName, "名称")
                            .Len(2, 16)
                            .My(p => {
                                if (Symbol.TypeExtensions.Convert<int>(Program.DataContext.ExecuteScalar("select top 1 [Id] from [Funds] where [UserId]=@p1 and [Name]=@p2", Program.CurrentUser.Id, p.LastValue), 0) > 0) {
                                    p.Return(false)
                                     .Msg("此资金库已经存在！")
                                     .LastControl.Focus();
                                }
                            })
                            .Null(txtMoney, "余额")
                            .NumDec()
                            .Null(txtOrder, "顺序")
                            .NumDec()
                            .Null(txtBankName,"银行",chkIsBank.Checked)
                            .Len(2,16)
                            .Null(txtCardNumber, "卡号", chkIsBank.Checked)
                            .Len(2, 32)
                            .Result)
                return;


            _model = new Funds() {
                UserId = Program.CurrentUser.Id,
                Name = txtName.Text,
                Money= Symbol.TypeExtensions.Convert<decimal>(txtMoney.Text,0M),
                Order = Symbol.TypeExtensions.Convert<double>(txtOrder.Text, 0D),
                IsBank = chkIsBank.Checked,
                BankName= txtBankName.Text,
                CardNumber= txtCardNumber.Text,
            };
            using (InsertCommandBuilder builder = new InsertCommandBuilder("Funds")) {
                builder.Fields.SetValues(_model);
                builder.Fields.Remove("Id");
                _model.Id = Symbol.TypeExtensions.Convert<int>(Program.DataContext.ExecuteScalar(builder.CommandText, builder.Values), 0);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void chkIsBank_CheckedChanged(object sender, EventArgs e) {
            txtBankName.ReadOnly = txtCardNumber.ReadOnly = !chkIsBank.Checked;
        }

        
    }
}
