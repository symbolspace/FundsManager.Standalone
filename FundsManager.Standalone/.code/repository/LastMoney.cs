//#define Local
namespace FundsManager.Standalone {
    class LastMoney : ILastMoney {

        #region fields
        private const string _authCode = "F6F1C479DA7AF9251E537BA110A7B5D5";
#if Local
        private const string _host = "192.168.10.183";
#else
        private const string _host = "lastmoney.api.anycore.cn";
#endif
        private const int _port = 31002;
        private const bool _ssl = false;
        private int _lastCode;
        private string _lastMessage;
        private LastMoneyResult _lastResult;
        private string _token;

        #endregion

        #region properties
        public int lastCode { get { return _lastCode; } }
        public string lastMessage { get { return _lastMessage; } }
        public LastMoneyResult lastResult { get { return _lastResult; } }
        public string token { get { return _token; } }
        #endregion

        #region ctor
        public LastMoney() {
        }
        #endregion

        #region methods

        #region invoke
        public LastMoneyResult invoke(string url, object data = null) {
            if (string.IsNullOrEmpty(url)) {
                return ActionResult(2101, "url missing.");
            }
            if(url[0]!='/')
                url='/'+url;
            try {
                string postUrl = string.Format("{0}://{1}:{2}/lastMoney{3}.api", _ssl ? "https" : "http", _host, _port, url);
                string json = JSON.ToJSON(new { authCode = _authCode, data });
                string code = null;
                using (Symbol.Net.Downloader downloader = new Symbol.Net.Downloader()) {
                    downloader.Headers["Content-Type"] = "appliction/json";
                    downloader.Encoding = System.Text.Encoding.UTF8;
                    code = downloader.UploadString(postUrl, json);
                }
                return ActionResult(JSON.ToObject<LastMoneyResult>(code));
            } catch (System.Exception error) {
                return ActionResult(1904, error.Message, error.StackTrace);
            }
        }
        #endregion
        #region ActionResult
        LastMoneyResult ActionResult(int code, string message, object data = null) {
            _lastCode = code;
            _lastMessage = message;
            LastMoneyResult result = new LastMoneyResult() {
                code = code,
                message = message,
                data = data,
            };
            _lastResult = result;
            return result;
        }
        LastMoneyResult ActionResult(LastMoneyResult result) {
            if (result == null) {
                return ActionResult(2101, "未返回数据");
            }
            _lastCode = result.code;
            _lastMessage = result.message;
            _lastResult = result;
            return result;

        }
        #endregion

        public bool user_exists(object condition) {
            LastMoneyResult result = invoke("/user/exists", new {
                condition,
            });
            return result.success ? TypeExtensions.Convert<bool>(result.data, false) : false;
        }
        public bool register(object info) {
            LastMoneyResult result = invoke("/user/register/base", new {
                info,
            });
            return result.success ? TypeExtensions.Convert<bool>(result.data, false) : false;
        }
        #region login
        public bool login() {
            if (string.IsNullOrEmpty(_token))
                return false;
            LastMoneyResult result = invoke("/user/login/token", new {
                token = _token,
            });
            if (!result.success)
                _token = "";
            return result.success;
        }
        public bool login(string account, string password) {
            LastMoneyResult result=invoke("/user/login/base", new {
                info = new {
                    account,
                    password,
                    userAgent = GetUserAgent()
                }
            });
            _token= result.success? FastObject.Path(result.data,"token") as string: "";
            return result.success;
        }
        string GetUserAgent() {
            return string.Format("FundsManager.Standalone({0};{1})", System.Environment.UserName, Program.CurrentUser.Account);
        }
        #endregion
        public int import(System.Collections.Generic.List<m_lm_ConsumeType_Input> list) {
            LastMoneyResult result = invoke("/consumeType/import", new {
                token = _token,
                list
            });
            return result.success ? TypeExtensions.Convert<int>(result.data, 0) : 0;
        }
        public int import(System.Collections.Generic.List<m_lm_MoneyRecord_Import> list) {
            LastMoneyResult result = invoke("/moneyRecord/import", new {
                token = _token,
                list
            });
            return result.success ? TypeExtensions.Convert<int>(result.data, 0) : 0;
        }

        #endregion

    }
}