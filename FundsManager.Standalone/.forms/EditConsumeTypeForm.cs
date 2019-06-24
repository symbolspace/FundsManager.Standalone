using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class EditConsumeTypeForm : Form {

        private ConsumeType _model = null;
        public ConsumeType Model {
            get { return _model; }
            set { _model = value; }
        }

        public EditConsumeTypeForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            txtName.Text = _model.Name;
            txtOrder.Text = _model.Order.ToString();
            chkIsOut.Checked = _model.IsOut;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!UIValidtion.Create()
                            .Null(txtName, "名称")
                            .Len(2, 10)
                            .My(p => {
                                if (Program.DataStore.ExistsConsumeTypeWithName(p.LastValue,_model.Id)) {
                                    p.Return(false)
                                     .Msg("此消费方式已经存在！")
                                     .LastControl.Focus();
                                }
                            })
                            .Null(txtOrder, "顺序")
                            .NumDec()
                            .Result)
                return;


            _model.Name = txtName.Text;
            _model.Order = TypeExtensions.Convert<double>(txtOrder.Text, 0D);

            Program.DataStore.Edit(_model); 
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        
    }
}
