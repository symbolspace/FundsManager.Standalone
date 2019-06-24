namespace FundsManager {
    /// <summary>
    /// 用户
    /// </summary>
    public class User {
        
        #region properties
        #region Id
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public long Id{ get; set;}
        #endregion
        #region CreateDate
        /// <summary>
        /// 获取创建日期
        /// </summary>
        public System.DateTime CreateDate{ get; protected set;}
        #endregion
        #region LastLoginDate
        /// <summary>
        /// 获取最后登录
        /// </summary>
        public System.DateTime LastLoginDate{ get; protected set;}
        #endregion
        #region Account
        /// <summary>
        /// 获取或设置帐号
        /// </summary>
        public string Account{ get; set;}
        #endregion
        #region Password
        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password{ get; set;}
        #endregion
        #region Name
        /// <summary>
        /// 获取或设置昵称
        /// </summary>
        public string Name{ get; set;}
        #endregion
        #endregion
        
    }
}