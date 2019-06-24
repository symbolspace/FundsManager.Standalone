namespace FundsManager {
    /// <summary>
    /// 资金
    /// </summary>
    public class Funds {
        
        #region properties
        #region Id
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public int Id{ get; set;}
        #endregion
        #region UserId
        /// <summary>
        /// 获取或设置用户Id
        /// </summary>
        public int UserId{ get; set;}
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
        #region IsBank
        /// <summary>
        /// 获取或设置是否为银行卡
        /// </summary>
        public bool IsBank{ get; set;}
        #endregion
        #region BankName
        /// <summary>
        /// 获取或设置银行名称
        /// </summary>
        public string BankName{ get; set;}
        #endregion
        #region CardNumber
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardNumber{ get; set;}
        #endregion
        #region Name
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name{ get; set;}
        #endregion
        #region Money
        /// <summary>
        /// 获取或设置当前余额
        /// </summary>
        public decimal Money{ get; set;}
        #endregion
        #region CreateDate
        /// <summary>
        /// 获取创建日期
        /// </summary>
        public System.DateTime CreateDate{ get; protected set;}
        #endregion
        #endregion
        
    }
}