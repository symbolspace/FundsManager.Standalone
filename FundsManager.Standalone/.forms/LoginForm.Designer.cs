namespace FundsManager.Standalone {
    partial class LoginForm {
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkIsRemember = new System.Windows.Forms.CheckBox();
            this.chkIsAutoLogin = new System.Windows.Forms.CheckBox();
            this.lbRegister = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(76, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(76, 48);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(192, 21);
            this.txtPassword.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "帐号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "密码：";
            // 
            // chkIsRemember
            // 
            this.chkIsRemember.AutoSize = true;
            this.chkIsRemember.Location = new System.Drawing.Point(76, 81);
            this.chkIsRemember.Name = "chkIsRemember";
            this.chkIsRemember.Size = new System.Drawing.Size(72, 16);
            this.chkIsRemember.TabIndex = 2;
            this.chkIsRemember.Text = "记住密码";
            this.chkIsRemember.UseVisualStyleBackColor = true;
            this.chkIsRemember.CheckedChanged += new System.EventHandler(this.chkIsRemember_CheckedChanged);
            // 
            // chkIsAutoLogin
            // 
            this.chkIsAutoLogin.AutoSize = true;
            this.chkIsAutoLogin.Location = new System.Drawing.Point(176, 81);
            this.chkIsAutoLogin.Name = "chkIsAutoLogin";
            this.chkIsAutoLogin.Size = new System.Drawing.Size(72, 16);
            this.chkIsAutoLogin.TabIndex = 3;
            this.chkIsAutoLogin.Text = "自动登录";
            this.chkIsAutoLogin.UseVisualStyleBackColor = true;
            this.chkIsAutoLogin.CheckedChanged += new System.EventHandler(this.chkIsAutoLogin_CheckedChanged);
            // 
            // lbRegister
            // 
            this.lbRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbRegister.AutoSize = true;
            this.lbRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbRegister.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbRegister.Location = new System.Drawing.Point(174, 119);
            this.lbRegister.Name = "lbRegister";
            this.lbRegister.Size = new System.Drawing.Size(41, 12);
            this.lbRegister.TabIndex = 5;
            this.lbRegister.Text = "新用户";
            this.lbRegister.Click += new System.EventHandler(this.lbRegister_Click);
            // 
            // txtAccount
            // 
            this.txtAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccount.Location = new System.Drawing.Point(76, 21);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(192, 21);
            this.txtAccount.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 155);
            this.Controls.Add(this.lbRegister);
            this.Controls.Add(this.chkIsAutoLogin);
            this.Controls.Add(this.chkIsRemember);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帐号登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkIsRemember;
        private System.Windows.Forms.CheckBox chkIsAutoLogin;
        private System.Windows.Forms.Label lbRegister;
        private System.Windows.Forms.TextBox txtAccount;

    }
}

