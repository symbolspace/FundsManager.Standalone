﻿namespace FundsManager.Standalone {
    partial class AddFundsLogForm {
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRelatedPerson = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbConsumeTypeId = new System.Windows.Forms.ComboBox();
            this.btnAddConsumeType = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.dtByDate = new Symbol.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(82, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(213, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(82, 74);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(86, 21);
            this.txtMoney.TabIndex = 1;
            this.txtMoney.Text = "0";
            this.txtMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "金额：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "相关人：";
            // 
            // txtRelatedPerson
            // 
            this.txtRelatedPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRelatedPerson.Location = new System.Drawing.Point(82, 101);
            this.txtRelatedPerson.Name = "txtRelatedPerson";
            this.txtRelatedPerson.Size = new System.Drawing.Size(217, 21);
            this.txtRelatedPerson.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "日期：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "消费方式：";
            // 
            // cbConsumeTypeId
            // 
            this.cbConsumeTypeId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbConsumeTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConsumeTypeId.FormattingEnabled = true;
            this.cbConsumeTypeId.IntegralHeight = false;
            this.cbConsumeTypeId.Location = new System.Drawing.Point(82, 48);
            this.cbConsumeTypeId.Name = "cbConsumeTypeId";
            this.cbConsumeTypeId.Size = new System.Drawing.Size(182, 22);
            this.cbConsumeTypeId.TabIndex = 0;
            this.cbConsumeTypeId.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbConsumeTypeId_DrawItem);
            // 
            // btnAddConsumeType
            // 
            this.btnAddConsumeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddConsumeType.Location = new System.Drawing.Point(270, 48);
            this.btnAddConsumeType.Name = "btnAddConsumeType";
            this.btnAddConsumeType.Size = new System.Drawing.Size(29, 23);
            this.btnAddConsumeType.TabIndex = 7;
            this.btnAddConsumeType.Text = "+";
            this.btnAddConsumeType.UseVisualStyleBackColor = true;
            this.btnAddConsumeType.Click += new System.EventHandler(this.btnAddConsumeType_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "备注：";
            // 
            // txtComment
            // 
            this.txtComment.AcceptsReturn = true;
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(82, 128);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(217, 124);
            this.txtComment.TabIndex = 3;
            // 
            // dtByDate
            // 
            this.dtByDate.Format = "yyyy-MM-dd HH:mm";
            this.dtByDate.HideSelection = false;
            this.dtByDate.Location = new System.Drawing.Point(82, 21);
            this.dtByDate.Name = "dtByDate";
            this.dtByDate.Size = new System.Drawing.Size(182, 21);
            this.dtByDate.TabIndex = 6;
            // 
            // AddFundsLogForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(331, 326);
            this.Controls.Add(this.btnAddConsumeType);
            this.Controls.Add(this.cbConsumeTypeId);
            this.Controls.Add(this.dtByDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtRelatedPerson);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFundsLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRelatedPerson;
        private Symbol.Forms.DateTimePicker dtByDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbConsumeTypeId;
        private System.Windows.Forms.Button btnAddConsumeType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtComment;

    }
}

