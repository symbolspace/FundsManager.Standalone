using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class AddConsumeTypeForm : Form {

        private ConsumeType _model = null;
        public ConsumeType Model { get { return _model; } }

        public AddConsumeTypeForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!UIValidtion.Create()
                            .Null(txtName,"名称")
                            .Len(2,10)
                            .My(p => {
                                if (Program.DataStore.ExistsConsumeTypeWithName(p.LastValue,0)) {
                                    p.Return(false)
                                     .Msg("此消费方式已经存在！")
                                     .LastControl.Focus();
                                }
                            })
                            .Null(txtOrder, "顺序")
                            .NumDec()
                            .Result)
                            return;

            _model = new ConsumeType() {
                UserId = Program.CurrentUser.Id,
                Name = txtName.Text,
                Order = TypeExtensions.Convert<double>(txtOrder.Text, 0D),
                IsOut = chkIsOut.Checked,
            };
            Program.DataStore.Add(_model); 
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        
    }

}
