namespace FundsManager {
    /// <summary>
    /// 资金消费统计（天）
    /// </summary>
    public class FundsConsumeTypeDay {
        
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
        public long UserId { get; set;}
        #endregion
        #region UserName
        /// <summary>
        /// 获取用户昵称
        /// </summary>
        public string UserName{ get; protected set;}
        #endregion
        #region ConsumeTypeId
        /// <summary>
        /// 获取或设置消费选项Id
        /// </summary>
        public long ConsumeTypeId { get; set;}
        #endregion
        #region ConsumeTypeName
        /// <summary>
        /// 获取消费选项名称
        /// </summary>
        public string ConsumeTypeName{ get; protected set;}
        #endregion
        #region Day
        /// <summary>
        /// 获取或设置日期(20150220)
        /// </summary>
        public int Day{ get; set;}
        #endregion
        #region IsOut
        /// <summary>
        /// 获取或设置是否为支出
        /// </summary>
        public bool IsOut{ get; set;}
        #endregion
        #region Money
        /// <summary>
        /// 获取或设置金额
        /// </summary>
        public decimal Money{ get; set;}
        #endregion
        #endregion
        
    }
}