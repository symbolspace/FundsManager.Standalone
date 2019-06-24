using System;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace Symbol.Forms
{
    public partial class MessageBoxForm : Form
    {
        protected MessageBoxForm()
        {
            InitializeComponent();
        }

        private void SetButtons(MessageBoxButtons buttons)
        {
            if (buttons == MessageBoxButtons.OK)
            {
                
                panel1.Width= btnOK.Right;
                AcceptButton = btnOK;

                btnOK.Visible = true;
                
            }
            else if (buttons == MessageBoxButtons.OKCancel)
            {
                btnCancel.Left = btnOK.Right + 6;
                panel1.Width = btnCancel.Right;

                AcceptButton = btnOK;

                btnOK.Visible = true;
                btnCancel.Visible = true;
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {
                SystemSounds.Question.Play();
                btnYes.Left = btnOK.Left;
                btnNo.Left = btnYes.Right + 6;
                panel1.Width = btnNo.Right;

                AcceptButton = btnYes;

                btnYes.Visible = true;
                btnNo.Visible = true;
            }
            else if (buttons == MessageBoxButtons.YesNoCancel)
            {
                btnYes.Left = btnOK.Left;
                btnNo.Left = btnYes.Right + 6;
                btnCancel.Left = btnNo.Right + 6;
                panel1.Width = btnCancel.Right;

                AcceptButton = btnYes;

                btnYes.Visible = true;
                btnNo.Visible = true;

            }
            else if (buttons == MessageBoxButtons.RetryCancel)
            {
                throw new NotSupportedException();
            }
            else if (buttons == MessageBoxButtons.AbortRetryIgnore)
            {
                throw new NotSupportedException();
            }
            panel1.Left = (Width - panel1.Width) / 2;
        }

        private void SetTitle(string title)
        {
            Text = title;
        }
        private void SetMessage(string message)
        {
            label1.Text = message;
            panel1.Top = label1.Bottom + label1.Top;

            Width = (Width - ClientRectangle.Width) + (Math.Max(label1.Right + pictureBox1.Left, pictureBox1.Left * 2 + panel1.Width));
            Height = (Height - ClientRectangle.Height) + (panel1.Bottom + label1.Top);
        }
        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Stop://Error,Hand
                    SystemSounds.Hand.Play();
                    pictureBox1.Image = imageList1.Images["error.ico"];
                    break;
                case MessageBoxIcon.Exclamation:
                    SystemSounds.Exclamation.Play();
                    pictureBox1.Image = imageList1.Images["exclamation.ico"];
                    break;
                case MessageBoxIcon.Asterisk://Information
                    SystemSounds.Asterisk.Play();
                    pictureBox1.Image = imageList1.Images["information.ico"];
                    break;
                case MessageBoxIcon.Question:
                    SystemSounds.Question.Play();
                    pictureBox1.Image = imageList1.Images["question.ico"];
                    break;
            }
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Activate();
        }
        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result;
            using (MessageBoxForm form = new MessageBoxForm())
            {
                form.SetTitle(title);
                form.SetMessage(message);
                form.SetIcon(icon);
                form.SetButtons(buttons);
                //icon
                result = form.ShowDialog();
            }
            return result;
        }
    }
}
