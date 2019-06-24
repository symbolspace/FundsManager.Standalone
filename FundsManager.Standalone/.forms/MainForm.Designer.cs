namespace FundsManager.Standalone {
    partial class MainForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.帐号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换帐号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按月统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.各类消费ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.对比统计PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.功能设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资金库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消费类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.界面字体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算器CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.记事本NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastMoneyLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTotalInMoney = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalOutMoney = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalCurrentMoney = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbConsumeTypeId = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPageIndex = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.lbPagePrev = new System.Windows.Forms.Label();
            this.lbPageNext = new System.Windows.Forms.Label();
            this.txtPageGo = new System.Windows.Forms.TextBox();
            this.lbLastVersion = new System.Windows.Forms.Label();
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.dtEndDate = new Symbol.Forms.DateTimePicker();
            this.dtBeginDate = new Symbol.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 67);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(886, 390);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日期";
            this.columnHeader2.Width = 113;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "消费方式";
            this.columnHeader3.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "金额";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "相关人";
            this.columnHeader5.Width = 67;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "备注";
            this.columnHeader6.Width = 380;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帐号ToolStripMenuItem,
            this.统计ToolStripMenuItem,
            this.功能设置ToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.同步UToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 帐号ToolStripMenuItem
            // 
            this.帐号ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem,
            this.切换帐号ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.退出软件ToolStripMenuItem});
            this.帐号ToolStripMenuItem.Name = "帐号ToolStripMenuItem";
            this.帐号ToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.帐号ToolStripMenuItem.Text = "帐号(&A)";
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.修改密码ToolStripMenuItem.Text = "修改密码(&P)";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 切换帐号ToolStripMenuItem
            // 
            this.切换帐号ToolStripMenuItem.Name = "切换帐号ToolStripMenuItem";
            this.切换帐号ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.切换帐号ToolStripMenuItem.Text = "切换帐号(&L)";
            this.切换帐号ToolStripMenuItem.Click += new System.EventHandler(this.切换帐号ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // 退出软件ToolStripMenuItem
            // 
            this.退出软件ToolStripMenuItem.Name = "退出软件ToolStripMenuItem";
            this.退出软件ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.退出软件ToolStripMenuItem.Text = "退出软件(&X)";
            this.退出软件ToolStripMenuItem.Click += new System.EventHandler(this.退出软件ToolStripMenuItem_Click);
            // 
            // 统计ToolStripMenuItem
            // 
            this.统计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按月统计ToolStripMenuItem,
            this.各类消费ToolStripMenuItem,
            this.对比统计PToolStripMenuItem});
            this.统计ToolStripMenuItem.Name = "统计ToolStripMenuItem";
            this.统计ToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.统计ToolStripMenuItem.Text = "统计(&W)";
            // 
            // 按月统计ToolStripMenuItem
            // 
            this.按月统计ToolStripMenuItem.Name = "按月统计ToolStripMenuItem";
            this.按月统计ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.按月统计ToolStripMenuItem.Text = "按月统计(&M)";
            this.按月统计ToolStripMenuItem.Click += new System.EventHandler(this.按月统计ToolStripMenuItem_Click);
            // 
            // 各类消费ToolStripMenuItem
            // 
            this.各类消费ToolStripMenuItem.Name = "各类消费ToolStripMenuItem";
            this.各类消费ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.各类消费ToolStripMenuItem.Text = "各类消费(&C)";
            this.各类消费ToolStripMenuItem.Click += new System.EventHandler(this.各类消费ToolStripMenuItem_Click);
            // 
            // 对比统计PToolStripMenuItem
            // 
            this.对比统计PToolStripMenuItem.Name = "对比统计PToolStripMenuItem";
            this.对比统计PToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.对比统计PToolStripMenuItem.Text = "对比统计(&P)";
            this.对比统计PToolStripMenuItem.Click += new System.EventHandler(this.对比统计PToolStripMenuItem_Click);
            // 
            // 功能设置ToolStripMenuItem
            // 
            this.功能设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.资金库ToolStripMenuItem,
            this.消费类型ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.界面字体ToolStripMenuItem});
            this.功能设置ToolStripMenuItem.Name = "功能设置ToolStripMenuItem";
            this.功能设置ToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.功能设置ToolStripMenuItem.Text = "设置(&S)";
            // 
            // 资金库ToolStripMenuItem
            // 
            this.资金库ToolStripMenuItem.Name = "资金库ToolStripMenuItem";
            this.资金库ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.资金库ToolStripMenuItem.Text = "资金库";
            this.资金库ToolStripMenuItem.Visible = false;
            this.资金库ToolStripMenuItem.Click += new System.EventHandler(this.资金库ToolStripMenuItem_Click);
            // 
            // 消费类型ToolStripMenuItem
            // 
            this.消费类型ToolStripMenuItem.Name = "消费类型ToolStripMenuItem";
            this.消费类型ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.消费类型ToolStripMenuItem.Text = "消费方式(&C)";
            this.消费类型ToolStripMenuItem.Click += new System.EventHandler(this.消费类型ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // 界面字体ToolStripMenuItem
            // 
            this.界面字体ToolStripMenuItem.Name = "界面字体ToolStripMenuItem";
            this.界面字体ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.界面字体ToolStripMenuItem.Text = "界面字体(&F)";
            this.界面字体ToolStripMenuItem.Click += new System.EventHandler(this.界面字体ToolStripMenuItem_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.计算器CToolStripMenuItem,
            this.记事本NToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.工具TToolStripMenuItem.Text = "工具(T)";
            // 
            // 计算器CToolStripMenuItem
            // 
            this.计算器CToolStripMenuItem.Name = "计算器CToolStripMenuItem";
            this.计算器CToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.计算器CToolStripMenuItem.Text = "计算器(&C)";
            this.计算器CToolStripMenuItem.Click += new System.EventHandler(this.计算器CToolStripMenuItem_Click);
            // 
            // 记事本NToolStripMenuItem
            // 
            this.记事本NToolStripMenuItem.Name = "记事本NToolStripMenuItem";
            this.记事本NToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.记事本NToolStripMenuItem.Text = "记事本(&N)";
            this.记事本NToolStripMenuItem.Click += new System.EventHandler(this.记事本NToolStripMenuItem_Click);
            // 
            // 同步UToolStripMenuItem
            // 
            this.同步UToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastMoneyLToolStripMenuItem});
            this.同步UToolStripMenuItem.Name = "同步UToolStripMenuItem";
            this.同步UToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.同步UToolStripMenuItem.Text = "同步(&U)";
            // 
            // lastMoneyLToolStripMenuItem
            // 
            this.lastMoneyLToolStripMenuItem.Name = "lastMoneyLToolStripMenuItem";
            this.lastMoneyLToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.lastMoneyLToolStripMenuItem.Text = "LastMoney(&L)";
            this.lastMoneyLToolStripMenuItem.Click += new System.EventHandler(this.lastMoneyLToolStripMenuItem_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(53, 34);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(35, 23);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTotalInMoney
            // 
            this.txtTotalInMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalInMoney.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotalInMoney.ForeColor = System.Drawing.Color.Green;
            this.txtTotalInMoney.Location = new System.Drawing.Point(68, 470);
            this.txtTotalInMoney.Name = "txtTotalInMoney";
            this.txtTotalInMoney.ReadOnly = true;
            this.txtTotalInMoney.Size = new System.Drawing.Size(86, 21);
            this.txtTotalInMoney.TabIndex = 12;
            this.txtTotalInMoney.Text = "0";
            this.txtTotalInMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "总收入：";
            // 
            // txtTotalOutMoney
            // 
            this.txtTotalOutMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalOutMoney.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotalOutMoney.ForeColor = System.Drawing.Color.Red;
            this.txtTotalOutMoney.Location = new System.Drawing.Point(216, 470);
            this.txtTotalOutMoney.Name = "txtTotalOutMoney";
            this.txtTotalOutMoney.ReadOnly = true;
            this.txtTotalOutMoney.Size = new System.Drawing.Size(86, 21);
            this.txtTotalOutMoney.TabIndex = 13;
            this.txtTotalOutMoney.Text = "0";
            this.txtTotalOutMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 473);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "总支出：";
            // 
            // txtTotalCurrentMoney
            // 
            this.txtTotalCurrentMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalCurrentMoney.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotalCurrentMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalCurrentMoney.Location = new System.Drawing.Point(362, 470);
            this.txtTotalCurrentMoney.Name = "txtTotalCurrentMoney";
            this.txtTotalCurrentMoney.ReadOnly = true;
            this.txtTotalCurrentMoney.Size = new System.Drawing.Size(86, 21);
            this.txtTotalCurrentMoney.TabIndex = 14;
            this.txtTotalCurrentMoney.Text = "0";
            this.txtTotalCurrentMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 473);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "总余额：";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "日期：";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "-";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.Location = new System.Drawing.Point(666, 36);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(100, 21);
            this.txtKeyword.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(614, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "关键词：";
            // 
            // cbConsumeTypeId
            // 
            this.cbConsumeTypeId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbConsumeTypeId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbConsumeTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConsumeTypeId.FormattingEnabled = true;
            this.cbConsumeTypeId.IntegralHeight = false;
            this.cbConsumeTypeId.Location = new System.Drawing.Point(487, 35);
            this.cbConsumeTypeId.Name = "cbConsumeTypeId";
            this.cbConsumeTypeId.Size = new System.Drawing.Size(116, 22);
            this.cbConsumeTypeId.TabIndex = 2;
            this.cbConsumeTypeId.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbConsumeTypeId_DrawItem);
            this.cbConsumeTypeId.SelectedIndexChanged += new System.EventHandler(this.cbConsumeTypeId_SelectedIndexChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(772, 34);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(60, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(421, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "消费方式：";
            // 
            // lbPageIndex
            // 
            this.lbPageIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPageIndex.AutoSize = true;
            this.lbPageIndex.Location = new System.Drawing.Point(755, 473);
            this.lbPageIndex.Name = "lbPageIndex";
            this.lbPageIndex.Size = new System.Drawing.Size(23, 12);
            this.lbPageIndex.TabIndex = 15;
            this.lbPageIndex.Text = "0/0";
            this.lbPageIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(867, 469);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(31, 23);
            this.btnGo.TabIndex = 9;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lbPagePrev
            // 
            this.lbPagePrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPagePrev.AutoSize = true;
            this.lbPagePrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPagePrev.Enabled = false;
            this.lbPagePrev.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbPagePrev.Location = new System.Drawing.Point(720, 473);
            this.lbPagePrev.Name = "lbPagePrev";
            this.lbPagePrev.Size = new System.Drawing.Size(29, 12);
            this.lbPagePrev.TabIndex = 6;
            this.lbPagePrev.Text = "上页";
            this.lbPagePrev.Click += new System.EventHandler(this.lbPagePrev_Click);
            // 
            // lbPageNext
            // 
            this.lbPageNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPageNext.AutoSize = true;
            this.lbPageNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPageNext.Enabled = false;
            this.lbPageNext.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbPageNext.Location = new System.Drawing.Point(784, 473);
            this.lbPageNext.Name = "lbPageNext";
            this.lbPageNext.Size = new System.Drawing.Size(29, 12);
            this.lbPageNext.TabIndex = 7;
            this.lbPageNext.Text = "下页";
            this.lbPageNext.Click += new System.EventHandler(this.lbPageNext_Click);
            // 
            // txtPageGo
            // 
            this.txtPageGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPageGo.Location = new System.Drawing.Point(823, 470);
            this.txtPageGo.Name = "txtPageGo";
            this.txtPageGo.Size = new System.Drawing.Size(38, 21);
            this.txtPageGo.TabIndex = 8;
            // 
            // lbLastVersion
            // 
            this.lbLastVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLastVersion.AutoSize = true;
            this.lbLastVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbLastVersion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbLastVersion.Location = new System.Drawing.Point(543, 474);
            this.lbLastVersion.Name = "lbLastVersion";
            this.lbLastVersion.Size = new System.Drawing.Size(53, 12);
            this.lbLastVersion.TabIndex = 8;
            this.lbLastVersion.Text = "最新版本";
            this.lbLastVersion.Click += new System.EventHandler(this.lbLastVersion_Click);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCSV.Location = new System.Drawing.Point(838, 34);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(60, 23);
            this.btnExportCSV.TabIndex = 4;
            this.btnExportCSV.Text = "导出csv";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // dtEndDate
            // 
            this.dtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtEndDate.Format = "yyyy-MM-dd HH:mm";
            this.dtEndDate.HideSelection = false;
            this.dtEndDate.Location = new System.Drawing.Point(336, 36);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(76, 21);
            this.dtEndDate.TabIndex = 1;
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtBeginDate.Format = "yyyy-MM-dd HH:mm";
            this.dtBeginDate.HideSelection = false;
            this.dtBeginDate.Location = new System.Drawing.Point(245, 36);
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Size = new System.Drawing.Size(76, 21);
            this.dtBeginDate.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(607, 474);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "云版";
            this.label8.Click += new System.EventHandler(this.lbLastMoney_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 508);
            this.Controls.Add(this.lbPageNext);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbLastVersion);
            this.Controls.Add(this.lbPagePrev);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lbPageIndex);
            this.Controls.Add(this.txtPageGo);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.cbConsumeTypeId);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtBeginDate);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalCurrentMoney);
            this.Controls.Add(this.txtTotalOutMoney);
            this.Controls.Add(this.txtTotalInMoney);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小型财务管理(单机版)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 功能设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 资金库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消费类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 界面字体ToolStripMenuItem;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtTotalInMoney;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalOutMoney;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalCurrentMoney;
        private System.Windows.Forms.Label label3;
        private Symbol.Forms.DateTimePicker dtBeginDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Symbol.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbConsumeTypeId;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPageIndex;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lbPagePrev;
        private System.Windows.Forms.Label lbPageNext;
        private System.Windows.Forms.TextBox txtPageGo;
        private System.Windows.Forms.ToolStripMenuItem 帐号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换帐号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 退出软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按月统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 各类消费ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算器CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记事本NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 对比统计PToolStripMenuItem;
        private System.Windows.Forms.Label lbLastVersion;
        private System.Windows.Forms.Button btnExportCSV;
        private System.Windows.Forms.ToolStripMenuItem 同步UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastMoneyLToolStripMenuItem;
        private System.Windows.Forms.Label label8;
    }
}

