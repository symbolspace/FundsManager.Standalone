using System;
using System.Windows.Forms;

namespace FundsManager.Standalone {
    public partial class ComparisonStatForm : Form {

        //private XYChart _chart = null;
        private ComparisonPieChart _chart = null;

        public ComparisonStatForm() {
            InitializeComponent();
            Icon = Resources.FundsManager_Standalone;
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            DateTime today= System.DateTime.Today;
            dtBeginDate.Value = new DateTime(today.Year, today.Month, 1);
            dtEndDate.Value = today;

            dtBeginDate2.Value = dtBeginDate.Value.Value.AddMonths(-1);
            dtEndDate2.Value = dtBeginDate.Value.Value.AddDays(-1);

            //new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(50);
                Symbol.Forms.ControlExtensions.ThreadInvoke(this, () => {
                    OnOKClick();
                });
            //}) { IsBackground = true }.Start();

        }

        private void btnOK_Click(object sender, EventArgs e) {
            OnOKClick();
        }
        private static System.Drawing.Color[] _outColors = new System.Drawing.Color[]{
            System.Drawing.Color.DarkRed,
            System.Drawing.Color.LightCoral,
            System.Drawing.Color.DarkGoldenrod,

            System.Drawing.Color.MediumPurple,
            System.Drawing.Color.MediumOrchid,
            System.Drawing.Color.Plum,
            System.Drawing.Color.DeepPink,
            System.Drawing.Color.Crimson,
        };
        private static System.Drawing.Color[] _inColors = new System.Drawing.Color[]{
            System.Drawing.Color.Green,
            System.Drawing.Color.DarkSeaGreen,
            System.Drawing.Color.Turquoise,
            System.Drawing.Color.LightSeaGreen,
            System.Drawing.Color.DarkTurquoise,
            System.Drawing.Color.DeepSkyBlue,
            System.Drawing.Color.LawnGreen,
            System.Drawing.Color.YellowGreen,
            System.Drawing.Color.Gold,
            System.Drawing.Color.Goldenrod,
            System.Drawing.Color.LightYellow,
        };
        void OnOKClick() {
            _chart = new ComparisonPieChart(pictureBox1.Width, pictureBox1.Height, new System.Drawing.Font("宋体", 9F));
            _chart.NumberFont = new System.Drawing.Font("宋体", 9F);

            System.Collections.Generic.List<ConsumeType> consumeTypes= Program.DataStore.FindAllConsumeType("[Id],[Name],[IsOut]").ToList();
            int outColorIndex = -1;
            int inColorIndex = -1;
            for (int i = 0; i < consumeTypes.Count; i++) {
                System.Drawing.Color color;
                if (consumeTypes[i].IsOut) {
                    outColorIndex++;
                    if (outColorIndex > _outColors.Length - 1) {
                        color = System.Drawing.Color.FromArgb(255 - i, 0, 0);
                    } else {
                        color = _outColors[outColorIndex];
                    }
                } else {
                    inColorIndex++;
                    if (inColorIndex > _inColors.Length - 1) {
                        color = System.Drawing.Color.FromArgb(0, 255 - i, 0);
                    } else {
                        color = _inColors[inColorIndex];
                    }
                }
                _chart.AddLabel(consumeTypes[i].Name, color);
            }

            DateTime? beginDate = dtBeginDate.Value;
            DateTime? endDate = dtEndDate.Value;
            _chart.LeftPie.Blocks.Clear();
            SetPie(_chart.LeftPie, consumeTypes, beginDate, endDate);

            DateTime? beginDate2 = dtBeginDate2.Value;
            DateTime? endDate2 = dtEndDate2.Value;
            _chart.RightPie.Blocks.Clear();
            SetPie(_chart.RightPie, consumeTypes, beginDate2, endDate2);

            Render();
        }
        void SetPie(ComparisonPieChart.Pie pie,System.Collections.Generic.List<ConsumeType> consumeTypes, DateTime? beginDate, DateTime? endDate) {
            
            pie.AddLabel("收入", System.Drawing.Color.ForestGreen);
            pie.AddLabel("支出", System.Drawing.Color.DarkRed);


            decimal totalInMoney = 0M;
            decimal totalOutMoney = 0M;
            decimal totalMoney = 0M;
            for (int i = 0; i < consumeTypes.Count; i++) {
                ConsumeType consumeType = consumeTypes[i];
                decimal money = Program.DataStore.GetTotalMoneyByConsumeType(consumeType.Id, beginDate, endDate);
                totalMoney += money;
                pie.AddBlock(i + 1, 0F, money);
                //int lineIndex = consumeType.IsOut ? 1 : 0;
                //_chart.Lines[lineIndex].AddPoint(i + 1, money);
                //_chart.AddXLabel(i + 1, consumeType.Name, lineIndex);
                if (consumeType.IsOut)
                    totalOutMoney += money;
                else
                    totalInMoney += money;
            }
            pie.Labels[0].Value = totalInMoney.ToString("#,###.##");
            pie.Labels[1].Value = totalOutMoney.ToString("#,###.##");
            if (totalMoney > 0M) {
                for (int i = 0; i < consumeTypes.Count; i++) {
                    decimal percent = pie.Blocks[i].Value / totalMoney * 360M;
                    pie.Blocks[i].Percent = (float)percent;
                }
            }
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
                pictureBox1.Image = _chart.DrawPie();
                //pictureBox1.Image = Draw(pictureBox1.Width, pictureBox1.Height, _grid);
            }
        }


    }
}
