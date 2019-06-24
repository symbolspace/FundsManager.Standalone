namespace FundsManager {
    /// <summary>
    /// 资金记录
    /// </summary>
    public class FundsLog {
        
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
        #region ConsumeTypeId
        /// <summary>
        /// 获取或设置消费选项Id
        /// </summary>
        public long ConsumeTypeId{ get; set;}
        #endregion
        #region ConsumeTypeName
        /// <summary>
        /// 获取消费选项名称
        /// </summary>
        public string ConsumeTypeName{ get; protected set;}
        #endregion
        #region ByDateDay
        /// <summary>
        /// 获取或设置消费日期(20150220)
        /// </summary>
        public int ByDateDay{ get; set;}
        #endregion
        #region CreateDateDay
        /// <summary>
        /// 获取创建日期(20150220)
        /// </summary>
        public int CreateDateDay{ get; protected set;}
        #endregion
        #region ByDate
        /// <summary>
        /// 获取或设置消费日期
        /// </summary>
        public System.DateTime ByDate{ get; set;}
        #endregion
        #region CreateDate
        /// <summary>
        /// 获取创建日期
        /// </summary>
        public System.DateTime CreateDate{ get; protected set;}
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
        #region LastMoney
        /// <summary>
        /// 获取或设置最后余额（操作前）
        /// </summary>
        public decimal LastMoney{ get; set;}
        #endregion
        #region RelatedPerson
        /// <summary>
        /// 获取或设置相关人
        /// </summary>
        public string RelatedPerson{ get; set;}
        #endregion
        #region Comment
        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Comment{ get; set;}
        #endregion
        #region SourceId
        /// <summary>
        /// 源Id
        /// </summary>
        public string SourceId { get; set; }
        #endregion
        #endregion

    }
}