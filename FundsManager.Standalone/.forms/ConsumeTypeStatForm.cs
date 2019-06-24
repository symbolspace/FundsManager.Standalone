using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class ConsumeTypeStatForm : Form {

        private XYChart _chart = null;

        public ConsumeTypeStatForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            DateTime today= System.DateTime.Today;
            dtBeginDate.Value = new DateTime(today.Year, today.Month, 1);
            dtEndDate.Value = today;
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
            _chart = new XYChart(pictureBox1.Width, pictureBox1.Height, new System.Drawing.Font("宋体", 9F));
            _chart.IsSingleLine = true;
            _chart.NumberFont = new System.Drawing.Font("宋体", 9F);
            _chart.NewLine("收入", System.Drawing.Color.ForestGreen);
            _chart.NewLine("支出", System.Drawing.Color.DarkRed);

            System.Collections.Generic.List<ConsumeType> consumeTypes= Program.DataStore.FindAllConsumeType("[Id],[Name],[IsOut]").ToList();
            DateTime? beginDate = dtBeginDate.Value;
            DateTime? endDate = dtEndDate.Value;
            decimal totalInMoney = 0M;
            decimal totalOutMoney = 0M;
            for (int i = 0; i < consumeTypes.Count; i++) {
                ConsumeType consumeType= consumeTypes[i];
                decimal money = Program.DataStore.GetTotalMoneyByConsumeType(consumeType.Id, beginDate, endDate);

                int lineIndex= consumeType.IsOut ? 1:0;
                _chart.Lines[lineIndex].AddPoint(i + 1, money);
                _chart.AddXLabel(i + 1, consumeType.Name, lineIndex);
                if (consumeType.IsOut)
                    totalOutMoney += money;
                else
                    totalInMoney += money;
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
