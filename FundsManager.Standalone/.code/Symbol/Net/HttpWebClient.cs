using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Symbol.Net {
    public class HttpWebClient : WebClient {

        private class DownloadStringUserToken {
            public object UserToken {
                get;
                private set;
            }

            public DownloadStringUserToken(object userToken) {
                UserToken = userToken;
            }
        }

        #region property

        private List<string> _listCacheFile;
        private List<string> CacheFiles {
            get {
                if (_listCacheFile == null) {
                    _listCacheFile = new List<string>();
                }
                return _listCacheFile;
            }
            set {
                _listCacheFile = value;
            }
        }

        public bool AllowAutoRedirect {
            get;
            set;
        }

        public bool Expect100Continue {
            get;
            set;
        }

        public string StringAccept {
            get;
            set;
        }

        public string DataAccept {
            get;
            set;
        }

        public DecompressionMethods AutomaticDecompression {
            get;
            set;
        }

        public CookieContainer Cookies {
            get;
            set;
        }

        public int Timeout {
            get;
            set;
        }
        /// <summary>
        /// 自动追加 PostHeader：application/x-www-form-urlencoded
        /// </summary>
        public bool AutoAppendPostHeader { get; set; }
        #endregion

        #region method

        public HttpWebClient() {
            StringAccept = "*/*";
            DataAccept = "*/*";
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            Expect100Continue = false;
            AllowAutoRedirect = true;
            Cookies = new CookieContainer();
            Timeout = 100;
            Proxy = null;// GlobalProxySelection.GetEmptyWebProxy();
            Headers = new WebHeaderCollection();
            Headers["Accept"] = "*/*";
            Headers["Accept-Language"] = "zh-cn";
            AutoAppendPostHeader = true;
            //Headers.Add( HttpRequestHeader.KeepAlive,"TRUE");
        }
        #endregion

        #region SetUseUnsafeHeaderParsing
        static HttpWebClient() {
            SetUseUnsafeHeaderParsing();
        }
        private static void SetUseUnsafeHeaderParsing() {
            Type type = FastWrapper.GetWarpperType("System.Net.Configuration.SettingsSectionInternal,System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            if (type == null)
                return;
            object instance = FastWrapper.Get(type, "Section");
            if (instance == null)
                return;
            FastWrapper.Set(instance, "useUnsafeHeaderParsing", true);
        }
        #endregion

        #region GetWebRequest
        protected override WebRequest GetWebRequest(System.Uri address) {
            string ifModifiedSince = null;
            try {
                ifModifiedSince = base.Headers[System.Net.HttpRequestHeader.IfModifiedSince];
                base.Headers.Remove(System.Net.HttpRequestHeader.IfModifiedSince);
            } catch {
            }
            WebRequest request = base.GetWebRequest(address);
            request.Timeout = this.Timeout * 1000;
            if (request is HttpWebRequest) {
                HttpWebRequest request2 = (HttpWebRequest)request;
                if (!string.IsNullOrEmpty(ifModifiedSince)) {
                    request2.IfModifiedSince = DateTime.ParseExact(
                                                                    ifModifiedSince,
                                                                    @"ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                                                    CultureInfo.GetCultureInfo("en-US")
                                                                   );
                }
                request2.AutomaticDecompression = this.AutomaticDecompression;
                request2.ServicePoint.Expect100Continue = this.Expect100Continue;
                request2.AllowAutoRedirect = this.AllowAutoRedirect;
                request2.CookieContainer = this.Cookies;
            }
            if (AutoAppendPostHeader) {
                if (string.Equals(request.Method, "POST", StringComparison.CurrentCultureIgnoreCase) && string.IsNullOrEmpty(request.ContentType))
                    request.ContentType = "application/x-www-form-urlencoded";
            }

            return request;
        }
        #endregion

        #region DownloadString
        public new string DownloadString(string address) {
            Headers["Accept"] = StringAccept;
            return BufferToString(base.DownloadData(address));
        }
        public new string DownloadString(Uri address) {
            Headers["Accept"] = StringAccept;
            return BufferToString(base.DownloadData(address));
        }
        #endregion

        #region DownloadStringAsync
        public void DownloadStringAsync(string address) {
            this.DownloadStringAsync(new Uri(address));
        }
        public new void DownloadStringAsync(Uri address) {
            DownloadStringAsync(address, null);
        }
        public void DownloadStringAsync(string address, object userToken) {
            this.DownloadStringAsync(new Uri(address), userToken);
        }
        public new void DownloadStringAsync(Uri address, object userToken) {
            Headers["Accept"] = StringAccept;
            base.DownloadDataAsync(address, new DownloadStringUserToken(userToken));
        }
        #endregion

        #region DownloadDataAsync
        public void DownloadDataAsync(string address) {
            Headers["Accept"] = DataAccept;
            base.DownloadDataAsync(new Uri(address));
        }
        public void DownloadDataAsync(string address, object userToken) {
            Headers["Accept"] = DataAccept;
            base.DownloadDataAsync(new Uri(address), userToken);

        }

        #endregion

        #region OnDownloadDataCompleted
        protected override void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e) {
            if (!(e.UserState is DownloadStringUserToken)) {
                base.OnDownloadDataCompleted(e);
                return;
            }
            string result = null;
            if (!e.Cancelled && e.Error == null && e.Result != null) {
                try {
                    result = BufferToString(e.Result);
                } catch {
                }
            }
            DownloadStringCompletedEventArgs e2 =
                new DownloadStringCompletedEventArgs(
                                                        result,
                                                        e.Error,
                                                        e.Cancelled,
                                                        ((DownloadStringUserToken)e.UserState).UserToken
                                                     );
            OnDownloadStringCompleted(e2);
        }
        #endregion

        #region DownloadFile
        public new void DownloadFile(string address, string fileName) {
            DownloadFile(new Uri(address), fileName);
        }
        public new void DownloadFile(Uri address, string fileName) {
            Headers["Accept"] = DataAccept;
            //if (this.CacheFiles.Contains(address.AbsoluteUri))
            //    return;
            //try
            //{
            base.DownloadFile(address, fileName);

            //if (string.IsNullOrEmpty(address.Query))
            //    this.CacheFiles.Add(address.AbsoluteUri);
            //}
            //catch
            //{
            //}
        }
        #endregion

        public void ClearCacheFile() {
            this.CacheFiles.Clear();
        }

        private Encoding GetEncoding(byte[] buffer) {
            string encoding = null;
            Encoding result = GetBufferEncoding(buffer);
            if (result == null) {
                string contentType = this.ResponseHeaders == null ? null : this.ResponseHeaders["Content-Type"];
                if (contentType != null) {
                    int charsetIndex = contentType.IndexOf("charset=", StringComparison.CurrentCultureIgnoreCase);
                    if (charsetIndex != -1)
                        encoding = contentType.Substring(charsetIndex + 8);
                }
                if (!string.IsNullOrEmpty(encoding))
                    return Encoding.GetEncoding(encoding);
            }

            if (result == null)
                return this.Encoding;
            else
                return result;
        }

        public static Encoding GetBufferEncoding(byte[] buffer) {
            string encoding = null;
            string pageCode = null;
            MemoryStream memoryStream = null;
            StreamReader streamReader = null;
            try {
                memoryStream = new MemoryStream(buffer);
                streamReader = new StreamReader(memoryStream, Encoding.ASCII);
                pageCode = streamReader.ReadToEnd();
                Regex regexCharset = new Regex("\\s*;\\s*charset=(?<charset>[\\w-]*)", RegexOptions.IgnoreCase);
                if (regexCharset.IsMatch(pageCode))
                    encoding = regexCharset.Match(pageCode).Groups["charset"].Value;
            } finally {
                if (memoryStream != null)
                    memoryStream.Close();
                if (streamReader != null)
                    streamReader.Close();
                memoryStream = null;
                streamReader = null;
            }
            return string.IsNullOrEmpty(encoding) ? null : Encoding.GetEncoding(encoding);
        }

        public string BufferToString(byte[] buffer) {
            Encoding encoding = GetEncoding(buffer);
            return BufferToString(buffer, encoding);
        }
        public static string BufferToString(byte[] buffer, Encoding encoding) {
            if (encoding == null)
                encoding = GetBufferEncoding(buffer);

            if (encoding == null)
                encoding = Encoding.Default;

            MemoryStream memoryStream = null;
            StreamReader streamReader = null;
            string result = null;
            try {
                memoryStream = new MemoryStream(buffer);
                streamReader = new StreamReader(memoryStream, encoding);
                result = streamReader.ReadToEnd();
            } finally {
                if (memoryStream != null)
                    memoryStream.Close();
                if (streamReader != null)
                    streamReader.Close();
                memoryStream = null;
                streamReader = null;
            }
            return result;
        }

        public new event EventHandler<DownloadStringCompletedEventArgs> DownloadStringCompleted;
        protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e) {
            if (this.DownloadStringCompleted != null) {
                this.DownloadStringCompleted(this, e);
            }
        }

        #region IWebClient 成员


        //NameValueCollection IWebClient.Headers
        //{
        //    get
        //    {
        //        return base.Headers;
        //    }
        //    set
        //    {
        //        if (value is WebHeaderCollection)
        //            Headers = (WebHeaderCollection)value;
        //        if (value == null)
        //        {
        //            Headers = null;
        //            return;
        //        }

        //        if (Headers == null)
        //            Headers = new WebHeaderCollection();

        //        if (value.Count == 0)
        //        {
        //            Headers.Clear();
        //            return;
        //        }

        //        foreach (var key in value.AllKeys)
        //        {
        //            Headers[key] = value[key];
        //        }
        //    }
        //}

        //WebProxy IWebClient.Proxy
        //{
        //    get
        //    {
        //        return Proxy as WebProxy;
        //    }
        //    set
        //    {
        //        Proxy= value;
        //    }
        //}

        #endregion
    }


    public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs {
        private string m_Result;
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return this.m_Result;
            }
        }

        internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
            : base(exception, cancelled, userToken) {
            this.m_Result = result;
        }
    }
}