namespace FundsManager {
    /// <summary>
    /// 消费选项
    /// </summary>
    public class ConsumeType {
        
        #region properties
        #region Id
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public long Id{ get; set;}
        #endregion
        #region UserId
        /// <summary>
        /// 获取或设置用户Id
        /// </summary>
        public long UserId{ get; set;}
        #endregion
        #region UserName
        /// <summary>
        /// 获取用户昵称
        /// </summary>
        public string UserName{ get; protected set;}
        #endregion
        #region Order
        /// <summary>
        /// 获取或设置顺序
        /// </summary>
        public double Order{ get; set;}
        #endregion
        #region IsOut
        /// <summary>
        /// 获取或设置是否为支出
        /// </summary>
        public bool IsOut{ get; set;}
        #endregion
        #region Name
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name{ get; set;}
        #endregion
        #endregion
        
    }
}