using System;
using System.Drawing;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
            Text += " v" + this.GetType().Assembly.GetName().Version.ToString();
            //dtEndDate.Value = DateTime.Now;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            listView_Resize(listView1, EventArgs.Empty);
            new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(50);
                Symbol.Forms.ControlExtensions.ThreadInvoke(this, () => {
                    Reload();
                });
            }) { IsBackground = true }.Start();
        }
        private void listView_Resize(object sender, EventArgs e) {
            ListView listView = (ListView)sender;
            int maxWidth = listView.Width - 32;
            int count = listView.Columns.Count - 1;
            for (int i = 0; i < count; i++) {
                ColumnHeader column = listView.Columns[i];
                //column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); int width1 = column.Width;
                //column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); int width2 = column.Width;

                //int width = Math.Max(width1, width2);
                //if (width2 != width) {
                //    column.Width = width;
                //}
                int width = column.Width;
                maxWidth -= width; 
                //maxWidth -= listView.Columns[i].Width;

            }
            listView.Columns[listView.Columns.Count - 1].Width = maxWidth;
        }
        private bool _isLoading = false;
        void Reload() {
            _isLoading = true;
            OnQueryClick();
            ShowTotals();

            Symbol.Forms.SelectedItem<ConsumeType> consumeTypeItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.SelectedItem;
            FillConsumeTypeList((consumeTypeItem == null || consumeTypeItem.Value == null) ? null : (int?)consumeTypeItem.Value.Id);
            _isLoading = false;

        }
        void ShowTotals() {
            decimal totalInMoney = Program.DataStore.TotalInMoney;
            decimal totalOutMoney = Program.DataStore.TotalOutMoney;
            decimal totalCurrentMoney = totalInMoney - totalOutMoney;
            txtTotalInMoney.Text = totalInMoney.ToString("#,##0.00");
            txtTotalOutMoney.Text = totalOutMoney.ToString("#,##0.00");
            txtTotalCurrentMoney.Text = totalCurrentMoney.ToString("#,##0.00");
        }
        void FillConsumeTypeList(int? value = null) {
            cbConsumeTypeId.Items.Clear();
            Symbol.Forms.SelectedItem<ConsumeType> listItemFirst = new Symbol.Forms.SelectedItem<ConsumeType>(null, "--全部--");
            cbConsumeTypeId.Items.Add(listItemFirst);
            foreach (ConsumeType item in Program.DataStore.FindAllConsumeType("[Id],[Name],[IsOut]")) {
                Symbol.Forms.SelectedItem<ConsumeType> listItem = new Symbol.Forms.SelectedItem<ConsumeType>(item, item.Name);
                cbConsumeTypeId.Items.Add(listItem);
                if (value == item.Id) {
                    cbConsumeTypeId.SelectedItem = listItem;
                }
            }
            if (cbConsumeTypeId.SelectedIndex == -1)
                cbConsumeTypeId.SelectedIndex = 0;

        }
        private void cbConsumeTypeId_DrawItem(object sender, DrawItemEventArgs e) {
            if (e.Index == -1) {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }

            Symbol.Forms.SelectedItem<ConsumeType> listItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.Items[e.Index];
            e.DrawBackground();
            System.Drawing.SizeF textSize = e.Graphics.MeasureString(listItem.Text, e.Font);
            System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush((listItem.Value != null && listItem.Value.IsOut && (e.State & DrawItemState.Selected) != DrawItemState.Selected) ? System.Drawing.Color.Red : e.ForeColor);
            e.Graphics.DrawString(listItem.Text, e.Font, textBrush, 5F, e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2F);
            //if ((e.State & DrawItemState.NoFocusRect) == DrawItemState.NoFocusRect) {
            //    System.Drawing.Pen pen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.SystemColors.HotTrack));
            //    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 1, e.Bounds.Width, e.Bounds.Top + e.Bounds.Height - 1);
            //}
            e.DrawFocusRectangle();

        }
        private int _page = 0;
        private int _pageMax = -1;
        void FillFundsLog() {
            listView1.Items.Clear();
            listView1.BeginUpdate();
            try {
                Symbol.Forms.SelectedItem<ConsumeType> consumeTypeItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.SelectedItem;
                var paging = Program.DataStore.FindAllFundsLog(_page, dtBeginDate.Value, dtEndDate.Value, (consumeTypeItem == null || consumeTypeItem.Value == null) ? null : (int?)consumeTypeItem.Value.Id, txtKeyword.Text);
                RenderPaging(paging);

                foreach (FundsLog item in paging.Query) {
                    AppendFundsLogToList(item);
                }
            } finally {
                listView1.EndUpdate();
            }
        }
        Paging<FundsLog> Query() {
            Symbol.Forms.SelectedItem<ConsumeType> consumeTypeItem = (Symbol.Forms.SelectedItem<ConsumeType>)cbConsumeTypeId.SelectedItem;
            return Program.DataStore.FindAllFundsLog(-1, dtBeginDate.Value, dtEndDate.Value, (consumeTypeItem == null || consumeTypeItem.Value == null) ? null : (int?)consumeTypeItem.Value.Id, txtKeyword.Text);
        }
        void RenderPaging(Paging<FundsLog> q) {
            _pageMax = q.PageCount;
            _page= q.Page;
            if (q.PageCount <2) {
                lbPagePrev.Enabled = false;
                lbPageNext.Enabled = false;
            }else{
                lbPagePrev.Enabled = _page > 0;
                lbPageNext.Enabled = _page < _pageMax - 1;
            }
            lbPageIndex.Text = string.Format("{0}/{1}页,{2}条",_page+1,_pageMax,q.Count);
            lbPageIndex.Left = lbPageNext.Left - 6 - lbPageIndex.Width;
            lbPagePrev.Left = lbPageIndex.Left - 6 - lbPagePrev.Width;
        }
        ListViewItem AppendFundsLogToList(FundsLog item, int index = -1, bool byAdd = false) {
            ListView listView = listView1;
            ListViewItem listViewItem = null;
            if (index == -1) {
                if (byAdd) {
                    listViewItem = listView.Items.Insert(0, "id_" + item.Id, (listView.Items.Count + 1).ToString(), -1);
                } else {
                    listViewItem = listView.Items.Add("id_" + item.Id, (listView.Items.Count + 1).ToString(), -1);
                }
                listViewItem.SubItems.Add(item.ByDate.ToString("yyyy-MM-dd HH:mm"));
                listViewItem.SubItems.Add(item.ConsumeTypeName);
                listViewItem.SubItems.Add((item.IsOut ? -item.Money : item.Money).ToString("#,##0.00"));
                listViewItem.SubItems.Add(item.RelatedPerson);
                listViewItem.SubItems.Add(item.Comment);
                listViewItem.ForeColor = item.IsOut ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            } else {
                listViewItem = listView.Items[index];
                listViewItem.SubItems[1].Text = item.ByDate.ToString("yyyy-MM-dd HH:mm");
                listViewItem.SubItems[2].Text = item.ConsumeTypeName;
                listViewItem.SubItems[3].Text = (item.IsOut ? -item.Money : item.Money).ToString("#,##0.00");
                listViewItem.SubItems[4].Text = item.RelatedPerson;
                listViewItem.SubItems[5].Text = item.Comment;
            }
            listViewItem.Tag = item;
            return listViewItem;
        }
        void ResetListViewRowIndex(ListView listView) {
            for (int i = 0; i < listView.Items.Count; i++) {
                listView.Items[i].Text = (i + 1).ToString();
            }
        }

        private void 资金库ToolStripMenuItem_Click(object sender, EventArgs e) {
            //using (FundsListForm form = new FundsListForm()) {
            //    form.Icon = Icon;
            //    form.Font = Font;
            //    form.ShowDialog();
            //}

        }

        private void 消费类型ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (ConsumeTypeListForm form = new ConsumeTypeListForm()) {
                form.Icon = Icon;
                form.Font = Font;
                form.ShowDialog();
            }
        }
        private void 界面字体ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (FontDialog dialog = new FontDialog()) {
                dialog.Font = Font;
                dialog.ShowApply = true;
                dialog.ShowEffects = false;
                dialog.FontMustExist = true;
                EventHandler handler = (p1, p2) => {
                    Font = dialog.Font;
                    Program.Config.FontName = dialog.Font.Name;
                    Program.Config.FontSize = dialog.Font.Size;
                    Program.SaveConfig();
                };
                dialog.Apply += handler;
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                handler(dialog, EventArgs.Empty);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            FundsLog item = ShowAdd(this);
            if (item == null)
                return;
            //AppendFundsLogToList(item, -1, true);
            Reload();

        }

        private void btnRemove_Click(object sender, EventArgs e) {
            OnRemoveClick();
        }
        void OnRemoveClick() {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            FundsLog item = listView1.SelectedItems[0].Tag as FundsLog;

            if (!Symbol.Forms.ProgramHelper.ShowQuestion("确认要删除此项吗？\n\n{0} {1} ￥{2}", item.ByDate.ToString("yyyy-MM-dd"), item.ConsumeTypeName, item.Money.ToString("#,##0.##")))
                return;
            Program.DataStore.Remove(item);
            listView1.SelectedItems[0].Remove();
            Reload();
        }
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            //if ((e.Control && e.KeyCode == Keys.W) || e.KeyCode == Keys.Escape) {
            //    Close();
            //} else 
            if (e.KeyCode == Keys.F5) {
                Reload();
            } else if (e.KeyCode == Keys.Enter) {
                if (listView1.Focused) {
                    OnEditClick();
                } else if (txtPageGo.Focused || btnQuery.Focused) {
                    OnGoClick();
                } else {
                    OnQueryClick();
                }
            } else if (e.Control && e.KeyCode == Keys.C) {
                OnCopyClick();
            }
        }
        void OnQueryClick() {
            FillFundsLog();
        }
        void OnEditClick() {
            if (listView1.SelectedItems.Count == 0) {
                Symbol.Forms.ProgramHelper.ShowInformation("请任意选中一行！");
                return;
            }
            FundsLog item = listView1.SelectedItems[0].Tag as FundsLog;
            if (ShowEdit(this, item)) {
                Reload();
            }
        }
        void OnCopyClick() {
            if (listView1.SelectedItems.Count == 0) {
                return;
            }
            FundsLog item = listView1.SelectedItems[0].Tag as FundsLog;
            if (ShowAdd(this, item) != null) {
                Reload();
            }
        }
        public static FundsLog ShowAdd(Form parentForm,FundsLog from=null) {
            using (AddFundsLogForm form = new AddFundsLogForm()) {
                form.Icon = parentForm.Icon;
                form.Font = parentForm.Font;

                if (form.ShowDialog(from) == System.Windows.Forms.DialogResult.Cancel)
                    return null;
                return form.Model;
            }
        }
        public static bool ShowEdit(Form parentForm, FundsLog model) {
            if (model == null)
                return false;
            using (EditFundsLogForm form = new EditFundsLogForm()) {
                form.Icon = parentForm.Icon;
                form.Font = parentForm.Font;
                form.Model = model;
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return false;
                return true;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            OnEditClick();
        }

        private void cbConsumeTypeId_SelectedIndexChanged(object sender, EventArgs e) {
            if (_isLoading)
                return;
            OnQueryClick();
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            OnQueryClick();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                OnEditClick();
            } else if (e.KeyCode == Keys.Delete) {
                OnRemoveClick();
            }
        }

        private void btnGo_Click(object sender, EventArgs e) {
            OnGoClick();
        }
        void OnGoClick() {
            if (!UIValidtion.Create()
                           .Null(txtPageGo, "页码")
                           .NumInt(1, _pageMax)
                           .Result)
                return;
            int value;
            if (!int.TryParse(txtPageGo.Text, out value)) {
                return;
            }
            _page = value - 1;
            OnQueryClick();
        }

        private void lbPageNext_Click(object sender, EventArgs e) {
            if (_page < _pageMax-1) {
                _page++;
                OnQueryClick();
                return;
            }
        }

        private void lbPagePrev_Click(object sender, EventArgs e) {
            if (_page <= 0) {
                _page = 0;
                return;
            }
            _page--;
            OnQueryClick();
        }

        //private void lbWWW_Click(object sender, EventArgs e) {
        //    try { System.Diagnostics.Process.Start("http://www.gouring.com/"); } catch { }
        //}

        private void lbLastVersion_Click(object sender, EventArgs e) {
            try { System.Diagnostics.Process.Start("http://www.afuhao.com/article_articleId-256.shtml"); } catch { }
        }
        private void lbLastMoney_Click(object sender, EventArgs e) {
            try { System.Diagnostics.Process.Start("http://lastmoney.h5.anycore.cn/"); } catch { }
        }


        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (ChangePasswordForm form = new ChangePasswordForm()) {
                form.Icon = this.Icon;
                form.Font = this.Font;
                form.ShowDialog();
            }

        }

        private void 切换帐号ToolStripMenuItem_Click(object sender, EventArgs e) {
            Program.IsSwitchAccount = true;
            Close();
        }

        private void 退出软件ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!OnCloseQuestion())
                return;
            Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing && !OnCloseQuestion())
                e.Cancel = true;
        }
        bool OnCloseQuestion() {
            if (Program.IsSwitchAccount)
                return true;
            return Symbol.Forms.ProgramHelper.ShowQuestion("确认要退出本软件吗？");
        }

        private void 按月统计ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (MonthStatForm form = new MonthStatForm()) {
                form.Icon = this.Icon;
                form.Font = this.Font;
                form.ShowDialog();
            }
        }

        private void 各类消费ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (ConsumeTypeStatForm form = new ConsumeTypeStatForm()) {
                form.Icon = this.Icon;
                form.Font = this.Font;
                form.ShowDialog();
            }
        }

        private void 计算器CToolStripMenuItem_Click(object sender, EventArgs e) {
            try { System.Diagnostics.Process.Start("calc.exe"); } catch { }
        }

        private void 记事本NToolStripMenuItem_Click(object sender, EventArgs e) {
            try { System.Diagnostics.Process.Start("notepad.exe"); } catch { }
        }

        private void 对比统计PToolStripMenuItem_Click(object sender, EventArgs e) {
            using (ComparisonStatForm form = new ComparisonStatForm()) {
                form.Icon = this.Icon;
                form.Font = this.Font;
                form.ShowDialog();
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e) {
            string filename = null;
            using (SaveFileDialog dialog = new SaveFileDialog()) {
                dialog.Filter = "逗号数据库(*.csv)|*.csv";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                filename = dialog.FileName;
            }
            try {
                var q = Query();

                int index = 0;
                int rows = 0;
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filename, false, System.Text.Encoding.UTF8)) {
                    writer.WriteLine("序号,日期,消费方式,金额,相关人,备注");
                    foreach (FundsLog item in q.Query) {
                        index++;
                        writer.WriteLine("{0},{1},{2},{3},{4},{5}",
                            index, item.ByDate.ToString("yyyy-MM-dd HH:mm"), ReplaceCSVChar(item.ConsumeTypeName),
                            ReplaceCSVChar((item.IsOut ? (-item.Money) : item.Money).ToString("#,##0.00")),
                            ReplaceCSVChar(item.RelatedPerson), ReplaceCSVChar(item.Comment));
                        rows++;
                        if (rows == 50) {
                            writer.Flush();
                            rows = 0;
                        }
                    }
                    writer.Flush();
                }
                Symbol.Forms.ProgramHelper.ShowInformation("成功导出 {0} 条记录！", index);
            } catch (System.Exception error) {
                Symbol.Forms.ProgramHelper.ShowWarning("导出失败：{0}！", error.Message);
            }
        }
        static string ReplaceCSVChar(string text) {
            if (string.IsNullOrEmpty(text))
                return "";
            return text.Replace(",", ",,")
                       .Replace("\r\n"," ")
                       .Replace('\r',' ')
                       .Replace('\n',' ');
        }

        private void lastMoneyLToolStripMenuItem_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(Program.LastMoney.token) || !Program.LastMoney.login()) {
                using (LastMoneyLoginForm form = new LastMoneyLoginForm()) {
                    form.Icon = this.Icon;
                    form.Font = this.Font;
                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;
                }
            }
            Program.DataStore.upload_LastMoney_ConsumeType();
            Program.DataStore.upload_LastMoney_FundsLog();
            //Symbol.Forms.ProgramHelper.ShowInformation(Program.LastMoney.token);
        }

    }
}