using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class EditFundsForm : Form {

        private Funds _model = null;
        public Funds Model {
            get { return _model; }
            set { _model = value; }
        }

        public EditFundsForm() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            txtName.Text = _model.Name;
            txtMoney.Text = _model.Money.ToString();
            txtOrder.Text = _model.Order.ToString();
            chkIsBank.Checked = _model.IsBank;
            if (_model.IsBank) {
                txtBankName.Text = _model.BankName;
                txtCardNumber.Text = _model.CardNumber;
                txtBankName.ReadOnly = txtCardNumber.ReadOnly = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!UIValidtion.Create()
                            .Null(txtName, "名称")
                            .Len(2, 16)
                            .My(p => {
                                if (Symbol.TypeExtensions.Convert<int>(Program.DataContext.ExecuteScalar("select top 1 [Id] from [Funds] where [UserId]=@p1 and [Name]=@p2 and [Id]<>@p3", Program.CurrentUser.Id, p.LastValue,_model.Id), 0) > 0) {
                                    p.Return(false)
                                     .Msg("此资金库已经存在！")
                                     .LastControl.Focus();
                                }
                            })
                            .Null(txtMoney, "余额")
                            .NumDec()
                            .Null(txtOrder, "顺序")
                            .NumDec()
                            .Null(txtBankName, "银行", chkIsBank.Checked)
                            .Len(2, 16)
                            .Null(txtCardNumber, "卡号", chkIsBank.Checked)
                            .Len(2, 32)
                            .Result)
                return;


            _model.Name = txtName.Text;
            _model.Money = Symbol.TypeExtensions.Convert<decimal>(txtMoney.Text, 0M);
            _model.Order = Symbol.TypeExtensions.Convert<double>(txtOrder.Text, 0D);
            _model.BankName = txtBankName.Text;
            _model.CardNumber = txtCardNumber.Text;

            using (UpdateCommandBuilder builder = new UpdateCommandBuilder("Funds")) {
                builder.Fields.SetValues(_model);
                builder.Fields.Remove("Id");
                builder.Fields.Remove("IsBank");
                builder.Fields.Remove("UserId");
                Program.DataContext.ExecuteNonQuery(builder.CommandText + " where [Id]=" + _model.Id, builder.Values);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        
    }
}
