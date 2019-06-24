
namespace FundsManager {
    public class XYChart {

        #region fields
        private System.Collections.Generic.List<LineXLabel> _xlabels = null;
        private System.Collections.Generic.List<Line> _lines = null;
        private System.Drawing.Font _font = null;
        private System.Drawing.Font _numberFont = null;
        #endregion

        #region properties
        public System.Collections.Generic.List<LineXLabel> XLabels { get { return _xlabels; } }
        public System.Collections.Generic.List<Line> Lines { get { return _lines; } }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsSingleLine { get; set; }
        public System.Drawing.Font Font {
            get { return _font; }
        }
        public System.Drawing.Font NumberFont {
            get { return _numberFont; }
            set { _numberFont = value; }
        }
        #endregion

        #region ctor
        public XYChart(int width, int height, System.Drawing.Font font) {
            Width = width;
            Height = height;
            _font = font;
            _numberFont = font;
            _xlabels = new System.Collections.Generic.List<LineXLabel>();
            _lines = new System.Collections.Generic.List<Line>();
        }
        #endregion

        #region methods

        #region AddXLabel
        public void AddXLabel(int x, string name,int lineIndex=-1) {
            _xlabels.Add(new LineXLabel() { X = x, Name = name ,LineIndex= lineIndex});
        }
        #endregion

        #region NewLine
        public Line NewLine(string name, System.Drawing.Color color) {
            Line line = new Line() { Name = name, Color = color };
            Lines.Add(line);
            return line;
        }
        #endregion

        #region DrawBarColumn
        public System.Drawing.Bitmap DrawBarColumn() {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image)) {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(System.Drawing.Color.Transparent);
                int xCount = 0;
                float yCount = 10F;
                decimal yMaxValue = 0M;

                xCount = XLabels.Count;

                Lines.ForEach(p => {
                    if (!IsSingleLine) {
                        int? p10 = LinqHelper.Max(p.Points, p1 => (int?)p1.X);
                        if (p10 !=null && p10 > xCount)
                            xCount = p10.Value;
                    }
                    decimal? p11 = LinqHelper.Max(p.Points, p1 =>(decimal?)p1.Value);
                    if (p11!=null && p11 > yMaxValue) {
                        yMaxValue = p11.Value;
                    }
                });
                System.Drawing.Font font = Font;

                float xStart = 50F;
                float xEnd = Width - 15F;
                float xWidth = xEnd - xStart;
                float xUnit = xWidth / xCount;

                float yStart = 20F;
                float yEnd = Height - font.Height * 2 - 10 - 20F;
                float yHeight = yEnd - yStart;
                float yUnit = yHeight / yCount;
                decimal yMaxValueRange = GetYMax(yMaxValue);
                decimal yValuePercent = ((decimal)(yHeight - 2F) / yMaxValueRange);

                //x line
                System.Drawing.Pen xPen = new System.Drawing.Pen(System.Drawing.Brushes.Black, 1F);
                g.DrawLine(xPen, xStart, yEnd, xEnd, yEnd);
                //x points
                float xLinePointOffset = xStart;
                float XLineUnit = xUnit / 2F;
                for (int i = 0; i <= xCount; i++) {
                    if (i == 0)
                        continue;
                    xLinePointOffset += XLineUnit;
                    g.DrawLine(xPen, xLinePointOffset, yEnd, xLinePointOffset, yEnd + 5);

                    LineXLabel label = XLabels.Find(p => p.X == i);
                    string xLabel = label.Name;
                    System.Drawing.SizeF xLabelSize = g.MeasureString(xLabel, font);
                    float xLabelX = xLinePointOffset - xLabelSize.Width / 2;
                    label.RealX = xLinePointOffset - xLabelSize.Width / 2;
                    label.RealY = yEnd + 8;
                    label.AbsX = xLinePointOffset;
                    System.Drawing.Brush brush = System.Drawing.Brushes.Black;
                    if (label.LineIndex != -1)
                        brush = _lines[label.LineIndex].TextBrush;
                    g.DrawString(xLabel, font, brush, label.RealX, label.RealY);

                    xLinePointOffset += XLineUnit;

                }

                //y line
                g.DrawLine(xPen, xStart, yEnd, xStart, yStart);
                //y points
                float yLinePointOffset = yEnd;
                for (int i = 0; i <= yCount; i++) {
                    g.DrawLine(xPen, xStart - 5, yLinePointOffset, xStart, yLinePointOffset);
                    //g.DrawLine(xPen, xStart - 5, i == 0 ? yLinePointOffset - 2F : yLinePointOffset, xStart, i == 0 ? yLinePointOffset - 2F : yLinePointOffset);

                    string yLabel = NumberToString((yMaxValueRange * i / 10));
                    System.Drawing.SizeF yLabelSize = g.MeasureString(yLabel, NumberFont);
                    float yLabelY = yLinePointOffset - yLabelSize.Height / 2;
                    float yLabelX = xStart - 5 - 3 - yLabelSize.Width;
                    g.DrawString(yLabel, NumberFont, System.Drawing.Brushes.Black, yLabelX, yLabelY);

                    yLinePointOffset -= yUnit;
                }
                //point-lines

                float pointWidth = (xUnit - 6F) / (IsSingleLine ? 1 : Lines.Count);
                float lineNameXOffset = xStart;
                float lineNameY = Height - 10 - Font.Height;
                float pointTextMaxWidth = System.Math.Max(pointWidth / 2F, pointWidth - 10F);
                for (int i = 0; i < Lines.Count; i++) {
                    Line line = Lines[i];
                    //float lastX = xStart;
                    //float lastY = yEnd;
                    float xPointOffset = xStart;

                    //line name
                    g.FillRectangle(line.TextBrush, lineNameXOffset, lineNameY, Font.Height, Font.Height);
                    lineNameXOffset += Font.Height + 3F;
                    System.Drawing.SizeF lineNameSize = g.MeasureString(line.Name, Font);
                    g.DrawString(line.Name, Font, line.TextBrush, lineNameXOffset, lineNameY + (Font.Height - lineNameSize.Height) / 2F);
                    lineNameXOffset += lineNameSize.Width + 10F;
                    if (!string.IsNullOrEmpty(line.Value)) {
                        lineNameXOffset -= 8F;
                        System.Drawing.SizeF lineValueSize = g.MeasureString(line.Value, Font);
                        g.DrawString(line.Value, Font, line.TextBrush, lineNameXOffset, lineNameY + (Font.Height - lineValueSize.Height) / 2F);
                        lineNameXOffset += lineValueSize.Width + 10F;
                    }
                    for (int j = 1; j <= xCount; j++) {
                        LinePoint item = line.Points.Find(p => p.X == j);
                        if (item != null) {
                            LineXLabel label = XLabels.Find(p => p.X == item.X);
                            //y by value percent
                            float pointHeight = (float)(item.Value * yValuePercent);
                            if (pointHeight >= 1F) {
                                float pointY = yEnd - 1F - pointHeight;
                                //float pointY = yEnd - 2F - pointHeight;
                                float pointX = xPointOffset + 3F + (IsSingleLine ? 0F : pointWidth * i);

                                //new System.Drawing.Drawing2D.LinearGradientBrush(
                                g.FillRectangle(new System.Drawing.SolidBrush(line.Color), pointX, pointY, pointWidth, pointHeight);
                                //cires r-3
                                //text value, to y
                                string pointText = NumberToString(item.Value);
                                if (pointText != "0") {
                                    System.Drawing.SizeF pointTextSize = g.MeasureString(pointText, NumberFont);
                                    g.DrawString(pointText, NumberFont, line.TextBrush, pointX + ((pointWidth - pointTextSize.Width) / 2F), pointY - Font.Height - 3F);
                                }
                            }
                        }
                        xPointOffset += xUnit;
                    }
                    //foreach (LinePoint item in line.Points) {
                    //    LineXLabel label = XLabels.Find(p => p.X == item.X);
                    //    //y by value percent
                    //    float pointHeight = (float)(item.Value * yValuePercent);
                    //    if (pointHeight < 1F)
                    //        pointHeight = 1F;
                    //    float pointY = yEnd - 2F - pointHeight;
                    //    float pointX = xPointOffset + 3F + (IsSingleLine ? 0F : pointWidth * i);

                    //    //new System.Drawing.Drawing2D.LinearGradientBrush(
                    //    g.FillRectangle(new System.Drawing.SolidBrush(line.Color), pointX, pointY, pointWidth, pointHeight);
                    //    //cires r-3
                    //    //text value, to y
                    //    string pointText = NumberToString(item.Value);
                    //    if (pointText != "0") {
                    //        System.Drawing.SizeF pointTextSize = g.MeasureString(pointText, NumberFont);
                    //        g.DrawString(pointText, NumberFont, line.TextBrush, pointX + ((pointWidth - pointTextSize.Width) / 2F), pointY - Font.Height - 3F);
                    //    }

                    //    xPointOffset += xUnit;
                    //}
                }

            }

            return image;
        }
        #region GetYMax
        decimal GetYMax(decimal value) {
            string text = value.ToString("0");
            decimal result = decimal.Parse("1".PadRight(text.Length - 1, '0'));
            while (result < value)
                result *= 5M;

            if (result < 500M)
                result = 500M;
            return result;
        }
        #endregion
        #region NumberToString
        string NumberToString(decimal value) {
            string text = System.Math.Round(value, 0).ToString();
            string text2 = null;
            if (text.Length < 4) {
            } else if (text.Length == 4) {//1 000
                text2 = "千";
                value /= 1000;
            } else if (text.Length >= 5 && text.Length <= 7) {//1 0000,   100 0000
                text2 = "万";
                value /= 10000;
            } else if (text.Length == 8) {//1000 0000
                text2 = "千万";
                value /= 10000000;
            } else if (text.Length == 9) {//1 0000 0000
                text2 = "亿";
                value /= 100000000;
            } else {
                text2 = "X";
            }
            text = value.ToString("0.00");
            if (text.Length > 3)
                text = text.Substring(0, 3);
            if (text == "0.0")
                text = "0";
            return text + text2;
        }
        #endregion

        #endregion

        #endregion

        #region types
        public class Line {
            private System.Collections.Generic.List<LinePoint> _points = null;
            private System.Drawing.Color _color;
            public System.Drawing.Color Color {
                get { return _color; }
                set {
                    _color = value;
                    TextBrush = new System.Drawing.SolidBrush(value);
                }
            }
            public System.Drawing.Brush TextBrush { get; set; }
            public System.Collections.Generic.List<LinePoint> Points { get { return _points; } }
            public string Name { get; set; }
            public string Value { get; set; }

            public Line() {
                _points = new System.Collections.Generic.List<LinePoint>();
            }

            public LinePoint AddPoint(int x, decimal value) {
                LinePoint point = new LinePoint() { X = x, Value = value };
                _points.Add(point);
                return point;
            }
        }
        public class LineXLabel {
            public int X { get; set; }
            public string Name { get; set; }
            public float RealX { get; set; }
            public float RealY { get; set; }
            public float AbsX { get; set; }
            public int LineIndex { get; set; }

        }
        public class LinePoint {
            public int X { get; set; }
            public decimal Value { get; set; }
        }
        #endregion
    }
}