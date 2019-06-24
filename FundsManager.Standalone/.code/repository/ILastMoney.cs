namespace FundsManager.Standalone {
    public interface ILastMoney {

        string token { get; }

        int lastCode { get; }
        string lastMessage { get; }
        LastMoneyResult lastResult { get; }


        LastMoneyResult invoke(string url, object data=null);

        bool user_exists(object condition);
        bool register(object info);
        bool login();
        bool login(string account, string password);

        int import(System.Collections.Generic.List<m_lm_ConsumeType_Input> list);
        int import(System.Collections.Generic.List<m_lm_MoneyRecord_Import> list);

    }

    public class LastMoneyResult {
        public bool success { get { return code == 1000; } }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    /// <summary>
    /// 消费选项
    /// </summary>
    public class m_lm_ConsumeType_Input {

        #region properties
        #region order
        /// <summary>
        /// 优先级
        /// </summary>
        public double order { get; set; }
        #endregion
        #region isOut
        /// <summary>
        /// 是否为支出
        /// </summary>
        public bool isOut { get; set; }
        #endregion
        #region name
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        #endregion
        #endregion

    }
    /// <summary>
    /// 金钱记录（导入）
    /// </summary>
    public class m_lm_MoneyRecord_Import {

        #region properties
        #region createDate
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createDate { get; protected set; }
        #endregion
        #region consumeTypeId
        /// <summary>
        /// 消费选项名称
        /// </summary>
        public string consumeTypeName { get; set; }
        #endregion
        #region byDate
        /// <summary>
        /// 消费日期
        /// </summary>
        public System.DateTime byDate { get; set; }
        #endregion
        #region money
        /// <summary>
        /// 金额
        /// </summary>
        public decimal money { get; set; }
        #endregion
        #region isOut
        /// <summary>
        /// 是否为支出
        /// </summary>
        public bool isOut { get; set; }
        #endregion
        #region relatedPerson
        /// <summary>
        /// 相关人
        /// </summary>
        public string relatedPerson { get; set; }
        #endregion
        #region comment
        /// <summary>
        /// 备注
        /// </summary>
        public string comment { get; set; }
        #endregion
        #region sourceId
        /// <summary>
        /// 原始Id
        /// </summary>
        public string sourceId { get; set; }
        #endregion
        #endregion

    }
}