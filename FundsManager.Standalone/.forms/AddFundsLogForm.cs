using System;
using System.Windows.Forms;
using Symbol.Data;

namespace FundsManager.Standalone {
    public partial class AddFundsLogForm : Form {

        private FundsLog _model = null;
        public FundsLog Model { get { return _model; } }

        public AddFundsLogForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            //FillFundsList();
            //FillConsumeTypeList();
            dtByDate.Value = DateTime.Now;
        }
        public DialogResult ShowDialog(FundsLog from) {
            if (from != null) {
                dtByDate.Value = from.ByDate;
                FillConsumeTypeList(from.ConsumeTypeId);
                txtMoney.Text = from.Money.ToString("#,##0.##");
                txtRelatedPerson.Text = from.RelatedPerson;
                txtComment.Text = from.Comment;
            } else {
                FillConsumeTypeList();
            }
            return base.ShowDialog();
        }
        private void btnOK_Click(object sender, EventArgs e) {
            //Symbol.Forms.SelectedItem<Funds> fundsItem = (Symbol.Forms.SelectedItem<Funds>)cbFundsId.SelectedItem;
            Symbol.Forms.SelectedItem<ConsumeType> consumeTypeItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.SelectedItem;
            if (!UIValidtion.Create()
                            .Next(cbConsumeTypeId, p => p.SelectedIndex != -1, p => consumeTypeItem == null ? "" : consumeTypeItem.Text, "消费方式", true)
                            .Next(dtByDate, p => p.Value.HasValue, p => p.Value == null ? "" : p.Value.Value.ToString(p.Format), "日期")
                            .Null(txtMoney, "金额")
                            .NumDec(0)
                            .Null(txtRelatedPerson, "相关人")
                            .Len(1, 10)
                            .Result)
                return;

            _model = new FundsLog() {
                UserId = Program.CurrentUser.Id,
                ByDate = dtByDate.Value.Value,
                ByDateDay = dtByDate.DayNumber,
                //FundsId = fundsItem.Value.Id,
                ConsumeTypeId = consumeTypeItem.Value.Id,
                Money =Math.Abs( TypeExtensions.Convert<decimal>(txtMoney.Text, 0M)),
                //LastMoney= fundsItem.Value.Money,
                RelatedPerson = txtRelatedPerson.Text,
                Comment = txtComment.Text,
                IsOut = consumeTypeItem.Value.IsOut,
            };
            Program.DataStore.Add(_model);
            //Symbol.FastWrapper.Set(_model, "FundsName", fundsItem.Value.Name);
            FastWrapper.Set(_model, "ConsumeTypeName", consumeTypeItem.Value.Name);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void FillConsumeTypeList(long? value=null) {
            cbConsumeTypeId.Items.Clear();
            foreach (ConsumeType item in Program.DataStore.FindAllConsumeType("[Id],[Name],[IsOut]")) {
                Symbol.Forms.SelectedItem<ConsumeType> listItem = new Symbol.Forms.SelectedItem<ConsumeType>(item, item.Name);
                cbConsumeTypeId.Items.Add(listItem);
                if (value == item.Id) {
                    cbConsumeTypeId.SelectedItem = listItem;
                }
            }
        }
        //void FillFundsList(int? value=null) {
        //    cbFundsId.Items.Clear();
        //    foreach (Funds item in Program.DataContext.CreateQuery<Funds>("select [Id],[Name],[IsBank],[BankName],[CardNumber],[Money] from [Funds] where [UserId]=@p1 order by [Order],[Id]", Program.CurrentUser.Id)) {
        //        string name = item.Name+",￥"+item.Money;
        //        Symbol.Forms.SelectedItem<Funds> listItem = new Symbol.Forms.SelectedItem<Funds>(item, name);
        //        cbFundsId.Items.Add(listItem);
        //        if (value == item.Id) {
        //            cbFundsId.SelectedItem = listItem;
        //        }
        //    }
        //}

        //private void cbFundsId_DrawItem(object sender, DrawItemEventArgs e) {
        //    if (e.Index == -1) {
        //        e.DrawBackground();
        //        e.DrawFocusRectangle();
        //        return;
        //    }
        //    Symbol.Forms.SelectedItem<Funds> listItem = (Symbol.Forms.SelectedItem<Funds>)cbFundsId.Items[e.Index];
        //    e.DrawBackground();
        //    System.Drawing.SizeF textSize = e.Graphics.MeasureString(listItem.Text, e.Font);
        //    System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush((listItem.Value.IsBank || (e.State & DrawItemState.Selected) == DrawItemState.Selected) ? e.ForeColor : System.Drawing.Color.Blue);
        //    e.Graphics.DrawString(listItem.Text, e.Font, textBrush, 5F, e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2F);
        //    if ((e.State & DrawItemState.NoFocusRect) == DrawItemState.NoFocusRect) {
        //        System.Drawing.Pen pen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.SystemColors.HotTrack));
        //        e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 1, e.Bounds.Width, e.Bounds.Top + e.Bounds.Height - 1);
        //    }
        //    e.DrawFocusRectangle();
        //}

        private void cbConsumeTypeId_DrawItem(object sender, DrawItemEventArgs e) {
            if (e.Index == -1) {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }

            Symbol.Forms.SelectedItem<ConsumeType> listItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.Items[e.Index];
            e.DrawBackground();
            System.Drawing.SizeF textSize = e.Graphics.MeasureString(listItem.Text, e.Font);
            System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush((listItem.Value.IsOut && (e.State & DrawItemState.Selected) != DrawItemState.Selected) ? System.Drawing.Color.Red : e.ForeColor);
            e.Graphics.DrawString(listItem.Text, e.Font, textBrush, 5F, e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2F);
            //if ((e.State & DrawItemState.NoFocusRect) == DrawItemState.NoFocusRect) {
            //    System.Drawing.Pen pen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.SystemColors.HotTrack));
            //    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 1, e.Bounds.Width, e.Bounds.Top + e.Bounds.Height - 1);
            //}
            e.DrawFocusRectangle();

        }

        //private void btnAddFunds_Click(object sender, EventArgs e) {
        //    Funds item = FundsListForm.ShowAdd(this);
        //    if (item != null)
        //        FillFundsList(item.Id);
        //}

        private void btnAddConsumeType_Click(object sender, EventArgs e) {
            ConsumeType item = ConsumeTypeListForm.ShowAdd(this);
            if (item != null)
                FillConsumeTypeList(item.Id);
        }
    }
}
