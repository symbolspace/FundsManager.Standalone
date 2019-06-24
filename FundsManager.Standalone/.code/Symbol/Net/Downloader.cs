using System;
using System.Net;
using System.Text;

namespace Symbol.Net {
    /// <summary>
    /// 下载器，基于HttpWebClient
    /// </summary>
    /// <remarks>本类中的所有提到的默认值，将指HttpWebClient的默认设置。注意每次请求，都将创建一个新的HttpWebClient。</remarks>
    public class Downloader : IDisposable {
        /// <summary>
        /// 编码格式，如gzip，如果设置为null将采用默认值。
        /// </summary>
        public DecompressionMethods? AutomaticDecompression { get; set; }
        /// <summary>
        /// 允许自动重定向，有时为了得到重定向地址上面的参数，会不需要自动重定向。如果设置为null将采用默认值。
        /// </summary>
        public bool? AllowAutoRedirect { get; set; }
        /// <summary>
        /// 发送100标头，有的网站限制不能用100标头，所以默认都是关闭的。
        /// </summary>
        public bool? Expect100Continue { get; set; }
        /// <summary>
        /// 自动追加 PostHeader：application/x-www-form-urlencoded。如果设置为null将采用默认值。
        /// </summary>
        public bool? AutoAppendPostHeader { get; set; }

        /// <summary>
        /// 重试次数，有时网络有一些小故障多试一两次会发现可以正常访问。默认为5次。
        /// </summary>
        public int RetryCount { get; set; }
        /// <summary>
        /// 允许抛出异常，默认为false
        /// </summary>
        public bool AllowThrow { get; set; }
        /// <summary>
        /// 超时时间，设置为0时，将采用默认值。
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// 代理服务器
        /// </summary>
        public IWebProxy Proxy { get; set; }
        /// <summary>
        /// 编码，当下载的文本内容无法确定编码时，将采用这个编码。设置为null将采用默认编码。
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 共享Cookies
        /// </summary>
        public CookieContainer Cookies { get; set; }
        /// <summary>
        /// 下次请求时将采用的Headers
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }
        public string BaseAddress { get; set; }
        public WebHeaderCollection ResponseHeaders { get; private set; }
        protected HttpWebClient CreateWebClient(Uri refererUrl) {
            var result = new HttpWebClient();
            result.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; CIBA; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            result.Headers["Accept"] = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            result.Headers["Accept-Language"] = "zh-cn";
            result.BaseAddress = BaseAddress;
            if (Headers != null) {
                foreach (var item in Headers.AllKeys) {
                    result.Headers[item] = Headers[item];
                }
            }
            if (refererUrl != null) {
                result.Headers["Referer"] = refererUrl.AbsoluteUri;
            }
            if (AutomaticDecompression != null)
                result.AutomaticDecompression = AutomaticDecompression.Value;
            if (AllowAutoRedirect != null)
                result.AllowAutoRedirect = AllowAutoRedirect.Value;
            if (Expect100Continue != null)
                result.Expect100Continue = Expect100Continue.Value;
            if (AutoAppendPostHeader != null)
                result.AutoAppendPostHeader = AutoAppendPostHeader.Value;

            if (Timeout > 0)
                result.Timeout = Timeout;
            result.Cookies = Cookies;
            if (Proxy != null)
                result.Proxy = Proxy;
            if (Encoding != null)
                result.Encoding = Encoding;
            return result;
        }
        public Downloader() {
            RetryCount = 5;
            AllowThrow = false;
            Headers = new WebHeaderCollection();
            Cookies = new CookieContainer();
        }

        #region DownloadString
        public string DownloadString(string address) {
            return DownloadString(address, (Uri)null);
        }
        public string DownloadString(string address, string refererUrl) {
            return DownloadString(new Uri(address, UriKind.RelativeOrAbsolute), string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public string DownloadString(string address, Uri refererUrl) {
            return DownloadString(new Uri(address, UriKind.RelativeOrAbsolute), refererUrl);
        }
        public string DownloadString(Uri address) {
            return DownloadString(address, (Uri)null);
        }
        public string DownloadString(Uri address, string refererUrl) {
            return DownloadString(address, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public string DownloadString(Uri address, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    var result = DownloadStringInternal(address, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                    return result;
                }
            }
        }
        private string DownloadStringInternal(Uri address, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return string.Empty;
            }

            try {
                return webclient.DownloadString(address);
            } catch (WebException ex) {
                return DownloadStringInternal(address, webclient, count + 1, ex);
            }
        }
        #endregion

        #region DownloadData
        public byte[] DownloadData(string address) {
            return DownloadData(address, (Uri)null);
        }
        public byte[] DownloadData(string address, string refererUrl) {
            return DownloadData(new Uri(address, UriKind.RelativeOrAbsolute), string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public byte[] DownloadData(string address, Uri refererUrl) {
            return DownloadData(new Uri(address, UriKind.RelativeOrAbsolute), refererUrl);
        }
        public byte[] DownloadData(Uri address) {
            return DownloadData(address, (Uri)null);
        }
        public byte[] DownloadData(Uri address, string refererUrl) {
            return DownloadData(address, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public byte[] DownloadData(Uri address, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    var result = DownloadDataInternal(address, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                    return result;
                }
            }
        }
        private byte[] DownloadDataInternal(Uri address, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return new byte[0];
            }

            try {
                return webclient.DownloadData(address);
            } catch (WebException ex) {
                return DownloadDataInternal(address, webclient, count + 1, ex);
            }
        }
        #endregion

        #region DownloadFile
        public void DownloadFile(string address, string fileName) {
            DownloadFile(address, fileName, (Uri)null);
        }
        public void DownloadFile(string address, string fileName, string refererUrl) {
            DownloadFile(new Uri(address), fileName, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public void DownloadFile(string address, string fileName, Uri refererUrl) {
            DownloadFile(new Uri(address), fileName, refererUrl);
        }
        public void DownloadFile(Uri address, string fileName) {
            DownloadFile(address, fileName, (Uri)null);
        }
        public void DownloadFile(Uri address, string fileName, string refererUrl) {
            DownloadFile(address, fileName, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public void DownloadFile(Uri address, string fileName, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    DownloadFileInternal(address, fileName, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                }
            }
        }
        private void DownloadFileInternal(Uri address, string fileName, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return;
            }

            try {
                webclient.DownloadFile(address, fileName);
            } catch (WebException ex) {
                DownloadFileInternal(address, fileName, webclient, count + 1, ex);
            }
        }
        #endregion

        #region UploadString
        public string UploadString(string address, string data) {
            return UploadString(new Uri(address, UriKind.RelativeOrAbsolute), data);
        }
        public string UploadString(string address, string data, string refererUrl) {
            return UploadString(new Uri(address, UriKind.RelativeOrAbsolute), data, refererUrl);
        }
        public string UploadString(string address, string data, Uri refererUrl) {
            return UploadString(new Uri(address, UriKind.RelativeOrAbsolute), data, refererUrl);
        }
        public string UploadString(Uri address, string data) {
            return UploadString(address, data, (Uri)null);
        }
        public string UploadString(Uri address, string data, string refererUrl) {
            return UploadString(address, data, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public string UploadString(Uri address, string data, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    var result = UploadStringInternal(address, data, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                    return result;
                }
            }
        }
        private string UploadStringInternal(Uri address, string data, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return string.Empty;
            }

            try {
                return webclient.UploadString(address, data);
            } catch (WebException ex) {
                return UploadStringInternal(address, data, webclient, count + 1, ex);
            }
        }
        #endregion
        #region UploadFile
        public byte[] UploadFile(string address, string fileName) {
            return UploadFile(address, fileName, (Uri)null);
        }
        public byte[] UploadFile(Uri address, string fileName) {
            return UploadFile(address, fileName, (Uri)null);
        }
        public byte[] UploadFile(string address, string fileName, string refererUrl) {
            return UploadFile(new Uri(address, UriKind.RelativeOrAbsolute), fileName, refererUrl);
        }
        public byte[] UploadFile(string address, string fileName, Uri refererUrl) {
            return UploadFile(new Uri(address, UriKind.RelativeOrAbsolute), fileName, refererUrl);
        }
        public byte[] UploadFile(Uri address, string fileName, string refererUrl) {
            return UploadFile(address, fileName, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public byte[] UploadFile(Uri address, string fileName, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    var result = UploadFileInternal(address, fileName, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                    return result;
                }
            }
        }
        private byte[] UploadFileInternal(Uri address, string fileName, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return new byte[0];
            }

            try {
                return webclient.UploadFile(address, fileName);
            } catch (WebException ex) {
                return UploadFileInternal(address, fileName, webclient, count + 1, ex);
            }
        }

        #endregion
        #region UploadData
        public byte[] UploadData(string address, byte[] data) {
            return UploadData(address, data, (Uri)null);
        }
        public byte[] UploadData(Uri address, byte[] data) {
            return UploadData(address, data, (Uri)null);
        }
        public byte[] UploadData(string address, byte[] data, string refererUrl) {
            return UploadData(new Uri(address, UriKind.RelativeOrAbsolute), data, refererUrl);
        }
        public byte[] UploadData(string address, byte[] data, Uri refererUrl) {
            return UploadData(new Uri(address, UriKind.RelativeOrAbsolute), data, refererUrl);
        }
        public byte[] UploadData(Uri address, byte[] data, string refererUrl) {
            return UploadData(address, data, string.IsNullOrEmpty(refererUrl) ? null : new Uri(refererUrl));
        }
        public byte[] UploadData(Uri address, byte[] data, Uri refererUrl) {
            lock (this) {
                using (var webclient = CreateWebClient(refererUrl)) {
                    var result = UploadDataInternal(address, data, webclient, 0, null);
                    ResponseHeaders = webclient.ResponseHeaders;
                    return result;
                }
            }
        }
        private byte[] UploadDataInternal(Uri address, byte[] data, HttpWebClient webclient, int count, Exception e) {
            if (count > RetryCount) {
                if (e != null)
                    throw e;
                return new byte[0];
            }

            try {
                return webclient.UploadData(address, data);
            } catch (WebException ex) {
                return UploadDataInternal(address, data, webclient, count + 1, ex);
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose() {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                Proxy = null;
                Headers = null;
                Cookies = null;
                Encoding = null;
            }
        }

        #endregion
    }
}