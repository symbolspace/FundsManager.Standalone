using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class MonthStatForm : Form {

        private XYChart _chart = null;

        public MonthStatForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            txtYear.Text = System.DateTime.Today.Year.ToString();
            new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(50);
                Symbol.Forms.ControlExtensions.ThreadInvoke(this, () => {
                    OnOKClick();
                });
            }) { IsBackground = true }.Start();

        }

        private void btnOK_Click(object sender, EventArgs e) {
            OnOKClick();
        }

        void OnOKClick() {
            if (!UIValidtion.Create()
                           .Null(txtYear, "年份")
                           .NumInt(2010, System.DateTime.Today.Year)
                           .Result)
                return;
            _chart = new XYChart(pictureBox1.Width, pictureBox1.Height, new System.Drawing.Font("宋体", 9F));
            _chart.NumberFont = new System.Drawing.Font("宋体", 9F);
            _chart.NewLine("收入", System.Drawing.Color.ForestGreen);
            _chart.NewLine("支出", System.Drawing.Color.DarkRed);

            //lines[0].Points.Add(new LinePoint(){ X
            string xLabelNames = "零一二三四五六七八九十";
            int year = int.Parse(txtYear.Text);
            decimal totalInMoney = 0M;
            decimal totalOutMoney = 0M;

            for (int i = 1; i < 13; i++) {
                decimal inMoney = Program.DataStore.GetTotalMoneyByMonth(year, i, false);
                decimal outMoney = Program.DataStore.GetTotalMoneyByMonth(year, i, true);
                _chart.Lines[0].AddPoint(i,inMoney);
                _chart.Lines[1].AddPoint(i, outMoney);
                string xLabelName = "";
                if (i > 10) {
                    xLabelName = "十" + xLabelNames[i - 10] + "月";
                } else {
                    xLabelName = xLabelNames[i] + "月";
                }
                _chart.AddXLabel(i, xLabelName);
                totalInMoney += inMoney;
                totalOutMoney += outMoney;
            }
            _chart.Lines[0].Value = totalInMoney.ToString("#,###.##");
            _chart.Lines[1].Value = totalOutMoney.ToString("#,###.##");

            Render();
        }
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            if (WindowState == FormWindowState.Minimized)
                return;
            Render();
        }
        void Render() {
            if (_chart == null) {
                pictureBox1.Image = null;
            } else {
                _chart.Width = pictureBox1.Width;
                _chart.Height = pictureBox1.Height;
                pictureBox1.Image = _chart.DrawBarColumn();
                //pictureBox1.Image = Draw(pictureBox1.Width, pictureBox1.Height, _grid);
            }
        }

    }
}
