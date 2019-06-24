
namespace Symbol.Forms {
    [System.ComponentModel.DefaultProperty("Value")]
    [System.ComponentModel.DefaultBindingProperty("Value")]
    [System.ComponentModel.DefaultEvent("ValueChanged")]
    [System.ComponentModel.Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.AutoDispatch)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class DateTimePicker : System.Windows.Forms.TextBox {

        #region fields
        private string _format = null;
        private System.DateTime? _value = null;
        private bool _isInited = false;
        #endregion

        #region properties
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.DefaultValue("yyyy-MM-dd HH:mm:ss")]
        public string Format {
            get { return _format; }
            set {
                if (string.IsNullOrEmpty(value))
                    return;
                if (_format != value) {
                    _format = value;
                    OnFormatChanged();
                }
            }
        }
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        [System.ComponentModel.Editor("System.ComponentModel.Design.DateTimeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public System.DateTime? Value {
            get { return _value; }
            set {
                if (_value != value) {
                    _value = value;
                    OnValueChanged();
                }
            }
        }
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        public override string Text {
            get {
                return base.Text;
            }
            set {

                if (DesignMode && !string.IsNullOrEmpty(value) && value.StartsWith("textBox"))
                    return;
                System.DateTime? value2 = TypeExtensions.Convert<System.DateTime?>(value);
                Value = value2;
                return;
            }
        }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        public override bool Multiline {
            get {
                return false;
            }
            set {
                
            }
        }
        public int DayNumber {
            get { return _value == null ? 0 : System.DateTimeExtensions.ToDayNumber(_value.Value); }
        }
        #endregion

        #region ctor
        public DateTimePicker() :base() {
            _format = "yyyy-MM-dd HH:mm:ss";
            Multiline = false;
            HideSelection = false;
            OnValueChanged(true);
            _isInited = true;
        }
        #endregion

        #region methods
        void OnFormatChanged() {
            OnValueChanged();
        }
        public event System.EventHandler ValueChanged;
        void OnValueChanged(bool init=false) {
            base.Text = _value == null ? "" : _value.Value.ToString(_format);
            if (!init) {
                if (ValueChanged != null)
                    ValueChanged(this, System.EventArgs.Empty);
            }
        }

        protected override void OnTextChanged(System.EventArgs e) {
            base.OnTextChanged(e);
            if (!_isInited)
                return;
            System.DateTime? value2 = TypeExtensions.Convert<System.DateTime?>(base.Text);
            if (value2 != _value) {
                _value = value2;
                //if (ValueChanged != null)
                //    ValueChanged(this, System.EventArgs.Empty);
            }
        }
        #endregion
    }
}