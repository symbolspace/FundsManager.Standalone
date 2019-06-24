using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class ConsumeTypeListForm : Form {

        public ConsumeTypeListForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
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
            ConsumeType item = listView1.SelectedItems[0].Tag as ConsumeType;
            if (ShowEdit(this, item)) {
                FillList();
            }
        }
        public static ConsumeType ShowAdd(Form parentForm) {
            using (AddConsumeTypeForm form = new AddConsumeTypeForm()) {
                form.Icon = parentForm.Icon;
                form.Font = parentForm.Font;
                
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return null;
                return form.Model;
            }
        }
        public static bool ShowEdit(Form parentForm, ConsumeType model) {
            if (model == null)
                return false;
            using (EditConsumeTypeForm form = new EditConsumeTypeForm()) {
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
            ConsumeType item = listView1.SelectedItems[0].Tag as ConsumeType;
            if (Program.DataStore.HasRefencenConsumeType(item.Id)) {
                Symbol.Forms.ProgramHelper.ShowInformation("此消费类型正在使用，不能删除！");
                return;
            }
            if (!Symbol.Forms.ProgramHelper.ShowQuestion("确认要删除 {0} 吗？", item.Name))
                return;
            Program.DataStore.Remove(item);
            listView1.SelectedItems[0].Remove();
        }

        private void btnMovePrev_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            ConsumeType item = listView1.SelectedItems[0].Tag as ConsumeType;
        }

        private void btnMoveNext_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            ConsumeType item = listView1.SelectedItems[0].Tag as ConsumeType;
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
                Symbol.Data.IDataQuery<ConsumeType> q = Program.DataStore.FindAllConsumeType();

                foreach (ConsumeType item in q) {
                    AppendItemToList(item);
                }
            } finally {
                listView1.EndUpdate();
            }
        }
        ListViewItem AppendItemToList(ConsumeType item, int index = -1) {
            ListView listView = listView1;
            ListViewItem listViewItem = null;
            if (index == -1) {
                listViewItem = listView.Items.Add("id_" + item.Id, item.Name, -1);
                listViewItem.SubItems.Add(item.Order.ToString());
                listViewItem.ForeColor = item.IsOut ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            } else {
                listViewItem = listView.Items[index];
                listViewItem.Text = item.Name;
                listViewItem.SubItems[1].Text = item.Order.ToString();
            }
            listViewItem.Tag = item;
            return listViewItem;
        }
    }
}
