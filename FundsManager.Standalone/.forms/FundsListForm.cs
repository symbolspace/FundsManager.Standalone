using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class FundsListForm : Form {

        public FundsListForm() {
            InitializeComponent();
        }
        

        private void btnAdd_Click(object sender, EventArgs e) {
            if (ShowAdd(this) != null) {
                FillList();
            }
        }
        void OnEditClick() {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            Funds item = listView1.SelectedItems[0].Tag as Funds;
            if (ShowEdit(this, item)) {
                FillList();
            }
        }
        public static Funds ShowAdd(Form parentForm) {
            using (AddFundsForm form = new AddFundsForm()) {
                form.Icon = parentForm.Icon;
                form.Font = parentForm.Font;
                
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return null;
                return form.Model;
            }
        }
        public static bool ShowEdit(Form parentForm, Funds model) {
            if (model == null)
                return false;
            using (EditFundsForm form = new EditFundsForm()) {
                form.Icon = parentForm.Icon;
                form.Font = parentForm.Font;
                form.Model = model;
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return false;
                return true;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e) {
            OnRemoveClick();
        }
        void OnRemoveClick(){
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            Funds item = listView1.SelectedItems[0].Tag as Funds;
            if (Symbol.TypeExtensions.Convert<int>(Program.DataContext.ExecuteScalar("select top 1 [Id] from [FundsLog] where [UserId]=@p1 and [FundsId]=@p2", Program.CurrentUser.Id, item.Id), 0) > 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("此资金库正在使用，不能删除！");
                return;
            }
            if (!Symbol.Forms.ProgramHelper.ShowQuestion("确认要删除 {0} 吗？", item.Name))
                return;
            Program.DataContext.ExecuteNonQuery("delete from [Funds] where [Id]=@p1",item.Id);
            listView1.SelectedItems[0].Remove();
        }

        private void btnMovePrev_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            Funds item = listView1.SelectedItems[0].Tag as Funds;
        }

        private void btnMoveNext_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            Funds item = listView1.SelectedItems[0].Tag as Funds;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            FillList();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        void listView1_DoubleClick(object sender, EventArgs e) {
            OnEditClick();   
        }
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if ((e.Control && e.KeyCode == Keys.W) || e.KeyCode == Keys.Escape) {
                Close();
            } else if (e.KeyCode == Keys.F5) {
                FillList();
            } else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Enter) {
                OnEditClick();
            } else if (e.KeyCode == Keys.Delete) {
                OnRemoveClick();
            }
        }

        void FillList() {
            listView1.Items.Clear();
            listView1.BeginUpdate();
            try {
                Symbol.Data.IDataQuery<Funds> q = Program.DataContext.CreateQuery<Funds>("select * from [Funds] where [UserId]=" + Program.CurrentUser.Id + " order by [Order],[Id]");

                foreach (Funds item in q) {
                    AppendItemToList(item);
                }
            } finally {
                listView1.EndUpdate();
            }
        }
        ListViewItem AppendItemToList(Funds item, int index = -1) {
            ListView listView = listView1;
            ListViewItem listViewItem = null;
            if (index == -1) {
                listViewItem = listView.Items.Add("id_" + item.Id, item.Name, -1);
                listViewItem.SubItems.Add(item.Money.ToString());
                listViewItem.SubItems.Add(item.Order.ToString());
                listViewItem.SubItems.Add(item.IsBank ? (item.BankName+","+ item.CardNumber):"");

                if (!item.IsBank)
                    listViewItem.ForeColor = System.Drawing.Color.Blue;
            } else {
                listViewItem = listView.Items[index];
                listViewItem.Text = item.Name;
                listViewItem.SubItems[1].Text = item.Money.ToString();
                listViewItem.SubItems[2].Text = item.Order.ToString();
                listViewItem.SubItems[3].Text = item.IsBank ? (item.BankName + "," + item.CardNumber) : "";
            }
            listViewItem.Tag = item;
            return listViewItem;
        }
    }
}
