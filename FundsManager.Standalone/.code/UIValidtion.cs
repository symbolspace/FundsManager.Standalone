
namespace FundsManager {
    public class UIValidtion {
        private bool _result = true;
        private System.Windows.Forms.Control _lastControl = null;
        private string _lastName = null;
        private string _lastValue = null;
        private bool _lastRequired = false;
        public System.Windows.Forms.Control LastControl { get { return _lastControl; } }
        public string LastName { get { return _lastName; } }
        public string LastValue { get { return _lastValue; } }
        public bool LastRequired { get { return _lastRequired; } }

        public delegate string ValueGetter<T>(T control);

        public UIValidtion Next<T>(T control, System.Predicate<T> exp, ValueGetter<T> valueGetter, string name, bool isSelectType = false, bool required = true) where T : System.Windows.Forms.Control {
            if (required && _result && !exp(control)) {
                _result = false;
                Symbol.Forms.ProgramHelper.ShowInformation("请{0}{1}！", isSelectType ? "选择" : "填写", name);
                control.Focus();
            }
            _lastControl = control;
            _lastName = name;
            _lastValue = valueGetter(control);
            _lastRequired = required;
            return this;
        }
        public UIValidtion Null(System.Windows.Forms.TextBox control, string name,bool required=true) {
            return Next(control, p => !string.IsNullOrEmpty(p.Text), p => p.Text, name,false, required);
        }
        public UIValidtion Len(int? minLength, int? maxLength) {
            if (!_result)
                return this;
            if (string.IsNullOrEmpty(_lastValue) && !_lastRequired)
                return this;

            if (minLength != null && minLength > 0 && _lastValue.Length < minLength) {
                Symbol.Forms.ProgramHelper.ShowInformation("{0}不能少于{1}个字符！", _lastName, minLength);
                _result = false;
                _lastControl.Focus();
            }
            if (_result && maxLength != null && maxLength > 0 && _lastValue.Length > maxLength) {
                Symbol.Forms.ProgramHelper.ShowInformation("{0}不能超过{1}个字符！", _lastName, maxLength);
                _result = false;
                _lastControl.Focus();
            }
            return this;
        }
        public UIValidtion Eq(string value, string message) {
            if (!_result)
                return this;
            if (string.IsNullOrEmpty(_lastValue) && !_lastRequired)
                return this;

            if (value != _lastValue) {
                Symbol.Forms.ProgramHelper.ShowInformation(message);
                _result = false;
                _lastControl.Focus();
            }
            return this;
        }
        public UIValidtion NumInt(long? minValue = null, long? maxValue = null) {
            if (!_result)
                return this;
            if (string.IsNullOrEmpty(_lastValue) && !_lastRequired)
                return this;

            long value;
            if (!long.TryParse(_lastValue, out value)) {
                _result = false;
                Symbol.Forms.ProgramHelper.ShowInformation("{0}必须是数字！", _lastName);
                _lastControl.Focus();
            } else {
                if (minValue != null && value < minValue) {
                    Symbol.Forms.ProgramHelper.ShowInformation("{0}不能小于{1}！", _lastName, minValue);
                    _result = false;
                    _lastControl.Focus();
                } else if (maxValue != null && value > maxValue) {
                    Symbol.Forms.ProgramHelper.ShowInformation("{0}不能大于{1}！", _lastName, maxValue);
                    _result = false;
                    _lastControl.Focus();
                }
            }
            return this;
        }
        public UIValidtion NumDec(decimal? minValue = null, decimal? maxValue = null) {
            if (!_result)
                return this;
            if (string.IsNullOrEmpty(_lastValue) && !_lastRequired)
                return this;

            decimal value;
            if (!decimal.TryParse(_lastValue, out value)) {
                _result = false;
                Symbol.Forms.ProgramHelper.ShowInformation("{0}必须是数字！", _lastName);
                _lastControl.Focus();
            } else {
                if (minValue != null && value < minValue) {
                    Symbol.Forms.ProgramHelper.ShowInformation("{0}不能小于{1}！", _lastName, minValue);
                    _result = false;
                    _lastControl.Focus();
                } else if (maxValue != null && value > maxValue) {
                    Symbol.Forms.ProgramHelper.ShowInformation("{0}不能大于{1}！", _lastName, maxValue);
                    _result = false;
                    _lastControl.Focus();
                }
            }
            return this;
        }
        public UIValidtion Return(bool value) {
            _result = value;
            return this;
        }
        public UIValidtion Msg(string message, params object[] args) {
            Symbol.Forms.ProgramHelper.ShowInformation(message, args);
            return this;
        }
        public UIValidtion My(System.Action<UIValidtion> action) {
            if (_result)
                action(this);
            return this;
        }
        public bool Result { get { return _result; } }

        private UIValidtion() {
        }
        public static UIValidtion Create() {
            return new UIValidtion();
        }
    }

}