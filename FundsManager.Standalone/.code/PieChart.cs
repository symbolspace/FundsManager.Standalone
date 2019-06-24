
namespace FundsManager {
    public class ComparisonPieChart {

        #region fields
        private System.Collections.Generic.List<Label> _labels = null;
        private System.Drawing.Font _font = null;
        private System.Drawing.Font _numberFont = null;
        private Pie _leftPie = null;
        private Pie _rightPie = null;
        #endregion

        #region properties
        public System.Collections.Generic.List<Label> Labels { get { return _labels; } }
        public Pie LeftPie { get { return _leftPie; } }
        public Pie RightPie { get { return _rightPie; } }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Drawing.Font Font {
            get { return _font; }
        }
        public System.Drawing.Font NumberFont {
            get { return _numberFont; }
            set { _numberFont = value; }
        }
        #endregion

        #region ctor
        public ComparisonPieChart(int width, int height, System.Drawing.Font font) {
            Width = width;
            Height = height;
            _font = font;
            _numberFont = font;
            _labels = new System.Collections.Generic.List<Label>();
            _leftPie = new Pie();
            _rightPie = new Pie();
        }
        #endregion

        #region methods
        #region AddLabel
        public Label AddLabel(string name, System.Drawing.Color color) {
            Label item = new Label() { Index = _labels.Count + 1, Name = name, Color = color };
            Labels.Add(item);
            return item;
        }
        #endregion

        #region DrawBarColumn
        public System.Drawing.Bitmap DrawPie() {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image)) {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(System.Drawing.Color.Transparent);


                float centerLabelMaxWidth = GetLabelMaxWidth(g)+10F;
                float centerPercentMaxWidth = g.MeasureString("999.999 " + "99.9%", _font).Width + 3F;
                
                float widthSplitOffset = 20F;
                float heightSplitOffset = 10F;
                float bottomHeightOffset = 20F;
                float pieWidth = (Width - (widthSplitOffset * 4F) - centerLabelMaxWidth - centerPercentMaxWidth*2) / 2F - 20F - _font.Height;
                float pieHeight = (Height - (heightSplitOffset * 2F) - bottomHeightOffset);
                pieHeight = pieWidth = System.Math.Min(pieWidth, pieHeight);

                _leftPie.Width = pieWidth; _leftPie.Height = pieHeight;
                _leftPie.X = widthSplitOffset; _leftPie.Y = heightSplitOffset;
                _rightPie.Width = pieWidth; _rightPie.Height = pieHeight;
                _rightPie.X = Width- widthSplitOffset- pieWidth; _rightPie.Y = heightSplitOffset;
                
                
                RenderPie(true,g);
                RenderPie(false, g);
                RenderCenterLabels(g, pieWidth + widthSplitOffset * 2F + centerPercentMaxWidth, centerLabelMaxWidth, centerPercentMaxWidth);
            }

            return image;
        }
        void RenderCenterLabels(System.Drawing.Graphics g,float x,float labelWidth,float percentWidth) {
            if (_labels.Count == 0)
                return;
            
            float rowHeight = _font.Height + 10F;
            float rowOffset = rowHeight / 2F;
            float y = (Height - _labels.Count * (rowHeight + rowOffset) + rowOffset) / 2F;
            System.Drawing.Brush textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0xDA, 0xDA, 0xDA));
            for (int i = 0; i < _labels.Count; i++) {
                Label label = _labels[i];
                g.FillRectangle(label.Brush, x, y, labelWidth, rowHeight);

                string text = label.Name;
                System.Drawing.SizeF textSize = g.MeasureString(text, _font);
                g.DrawString(text, _font, textBrush, x + (labelWidth - textSize.Width) / 2F, y + (rowHeight - textSize.Height) / 2F);
                
                //label left percent
                PieBlock blockLeft= _leftPie.Blocks.Find(p => p.Index == label.Index);
                if (blockLeft != null) {
                    text = NumberToString(blockLeft.Value) + " " + (blockLeft.Percent / 360F * 100F).ToString("0.#'%'").PadLeft( 5, ' ');
                    textSize = g.MeasureString(text, _font);
                    g.DrawString(text, _font, label.Brush,x-3F- textSize.Width, y + (rowHeight - textSize.Height) / 2F);
                }
                //label right percent
                PieBlock blockRight = _rightPie.Blocks.Find(p => p.Index == label.Index);
                if (blockRight != null) {
                    text = (blockRight.Percent / 360F * 100F).ToString("0.#'%'");
                    if (text.Length != 5) {
                        text += "".PadRight(5 - text.Length, ' ');
                    }
                    text += " " + NumberToString(blockRight.Value);
                    textSize = g.MeasureString(text, _font);
                    g.DrawString(text, _font, label.Brush, x + labelWidth+ 3F , y + (rowHeight - textSize.Height) / 2F);
                }

                y += rowHeight + rowOffset;


            }
        }
        void RenderPie(bool left, System.Drawing.Graphics g) {
            Pie pie = left ? _leftPie : _rightPie;
            Label firstLabel = null;
            Label lastLabel = null;
            float startAngle = 0F;
            for (int i = 0; i < pie.Blocks.Count; i++) {
                PieBlock block = pie.Blocks[i];
                Label label = _labels.Find(p => p.Index == block.Index);
                if (label == null)
                    continue;
                if (i == 0) {
                    firstLabel = label;
                }
                lastLabel = label;
                g.FillPie(label.Brush, pie.X, pie.Y, pie.Width, pie.Height, startAngle, block.Percent);
                startAngle += block.Percent;
            }

            if (firstLabel == null) {
                firstLabel = new Label() { Index = 0, Color = System.Drawing.Color.Gray, Name = "" };
                g.FillPie(firstLabel.Brush, pie.X, pie.Y, pie.Width, pie.Height, 0F, 1F);

            }
            g.DrawPie(new System.Drawing.Pen(firstLabel.Brush), pie.X, pie.Y, pie.Width, pie.Height, 0F, 360F);

            //bottom labels
            float bottomY = pie.Y + pie.Height + 15F;
            if (left) {
                float bottomX = pie.X + 4F;
                for (int i = 0; i < pie.Labels.Count; i++) {
                    Label label = pie.Labels[i];
                    g.FillRectangle(label.Brush, bottomX, bottomY, Font.Height, Font.Height);
                    bottomX += Font.Height + 3F;

                    string text = label.Name;
                    System.Drawing.SizeF textSize = g.MeasureString(text, _font);
                    g.DrawString(text, _font, label.Brush, bottomX, bottomY + (_font.Height - textSize.Height) / 2F);
                    bottomX += textSize.Width + 10F;

                    if (!string.IsNullOrEmpty(label.Value)) {
                        text = label.Value;
                        bottomX -= 8F;
                        textSize = g.MeasureString(text, _font);
                        g.DrawString(text, _font, label.Brush, bottomX, bottomY + (_font.Height - textSize.Height) / 2F);
                        bottomX += textSize.Width + 10F;
                    }
                }
            } else {
                float bottomX = pie.X + pie.Width - 4F;
                for (int i = pie.Labels.Count-1; i >=0; i--) {
                    Label label = pie.Labels[i];
                    string text = null;
                    System.Drawing.SizeF textSize;

                    if (!string.IsNullOrEmpty(label.Value)) {
                        text = label.Value;
                        textSize = g.MeasureString(text, _font);
                        bottomX -= textSize.Width;
                        g.DrawString(text, _font, label.Brush, bottomX, bottomY + (_font.Height - textSize.Height) / 2F);
                        bottomX -= 3F;
                    }

                    text = label.Name;
                    textSize = g.MeasureString(text, _font);
                    bottomX -= textSize.Width;
                    g.DrawString(text, _font, label.Brush, bottomX, bottomY + (_font.Height - textSize.Height) / 2F);
                    bottomX -= 3F;
                    bottomX -= Font.Height;
                    g.FillRectangle(label.Brush, bottomX, bottomY, Font.Height, Font.Height);
                    bottomX -= 10F;
                }
            }
        }

        float GetLabelMaxWidth(System.Drawing.Graphics g) {
            return LinqHelper.Max(_labels, p => g.MeasureString(p.Name, _font).Width);
        }
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

        public class Label{
            private System.Drawing.Color _color;
           // private System.Drawing.Brush _brush;
            public System.Drawing.Color Color {
                get { return _color; }
                set {
                    _color = value;
                    Brush = new System.Drawing.SolidBrush(value);
                }
            }
            public System.Drawing.Brush Brush {get;set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int Index{get;set;}
        }
        public class Pie {
            private System.Collections.Generic.List<PieBlock> _blocks = null;
            private System.Collections.Generic.List<Label> _labels = null;

            public float X { get; set; }
            public float Y { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public System.Collections.Generic.List<PieBlock> Blocks { get { return _blocks; } }
            public System.Collections.Generic.List<Label> Labels { get { return _labels; } }

            public Pie() {
                _blocks = new System.Collections.Generic.List<PieBlock>();
                _labels = new System.Collections.Generic.List<Label>();
            }
            public PieBlock AddBlock(int index, float percent, decimal value) {
                PieBlock item = new PieBlock() { Index = index, Percent = percent, Value = value };
                _blocks.Add(item);
                return item;
            }
            #region AddLabel
            public Label AddLabel(string name, System.Drawing.Color color) {
                Label item = new Label() { Index = _labels.Count + 1, Name = name, Color = color };
                Labels.Add(item);
                return item;
            }
            #endregion
        }
        public class PieBlock{
            public int Index{get;set;}
            public float Percent{get;set;}
            public decimal Value{get;set;}
        }
        #endregion
    }
}