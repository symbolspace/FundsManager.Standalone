using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class EditFundsLogForm : Form {

        private FundsLog _model = null;
        public FundsLog Model { 
            get { return _model; }
            set { _model = value; }
        }

        public EditFundsLogForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            dtByDate.Value = _model.ByDate;
            txtConsumeTypeName.Text = _model.ConsumeTypeName;
            txtMoney.Text = _model.Money.ToString("#,##0.##");
            txtRelatedPerson.Text = _model.RelatedPerson;
            txtComment.Text = _model.Comment;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!UIValidtion.Create()
                            .Next(dtByDate,p=>p.Value.HasValue,p=>p.Value==null?"":p.Value.Value.ToString(p.Format),"日期")
                            .Null(txtMoney, "金额")
                            .NumDec(0)
                            .Null(txtRelatedPerson, "相关人")
                            .Len(1, 10)
                            .Result)
                return;
            _model.RelatedPerson = txtRelatedPerson.Text;
            _model.ByDate = dtByDate.Value.Value;
            _model.ByDateDay=dtByDate.DayNumber;
            _model.Money = Math.Abs(TypeExtensions.Convert<decimal>(txtMoney.Text, 0M));
            _model.Comment = txtComment.Text;
            Program.DataStore.Edit(_model); 

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
