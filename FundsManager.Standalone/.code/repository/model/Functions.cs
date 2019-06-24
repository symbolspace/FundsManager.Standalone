namespace FundsManager {
    public static class Functions {
        #region GetName
        private static string GetName(System.Reflection.MethodBase methodInfo) {
            return string.Format("[dbo].[{0}]", methodInfo.Name);
        }
        #endregion
        
        #region functions
        #region randX
        /// <summary>
        /// 包装系统的rand函数
        /// </summary>
        public static double randX(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod())));
        }
        #endregion
        #region randomNext
        /// <summary>
        /// 求随机值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值,输出的值，始终小于此值</param>
        /// <param name="asInteger">为1表示输出为整数</param>
        public static double randomNext(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,double min,double max,bool asInteger){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                min,max,asInteger));
        }
        #endregion
        #region valuePaddingLeft
        /// <summary>
        /// 将数字左边追加一些0
        /// </summary>
        /// <param name="value">需要处理的数据</param>
        /// <param name="length">期望长度</param>
        /// <param name="char">填充字符</param>
        public static string valuePaddingLeft(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,object @value,int length,string @char){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                @value,length,@char));
        }
        #endregion
        #region getDayNumber
        /// <summary>
        /// 获取日期的数字格式：20120720
        /// </summary>
        /// <param name="date">日期</param>
        public static int getDayNumber(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime date){
            return Symbol.TypeExtensions.Convert<int>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                date));
        }
        #endregion
        #region getLength
        /// <summary>
        /// 求字符串长度
        /// </summary>
        /// <param name="value">值</param>
        public static int getLength(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string @value){
            return Symbol.TypeExtensions.Convert<int>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                @value));
        }
        #endregion
        #region guidToString
        /// <summary>
        /// guid 转为字符串，32位char
        /// </summary>
        /// <param name="guid">GUID</param>
        public static string guidToString(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.Guid guid){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                guid));
        }
        #endregion
        #region anyValue_4
        /// <summary>
        /// 判断第一个长度大于0，返回第一个，反之第二个 3 4 
        /// </summary>
        /// <param name="value0">待检查的参数1</param>
        /// <param name="value1">待检查的参数2</param>
        /// <param name="value2">待检查的参数3</param>
        /// <param name="value3">默认的参数</param>
        public static string anyValue_4(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string value0,string value1,string value2,string value3){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                value0,value1,value2,value3));
        }
        #endregion
        #region anyValue_2
        /// <summary>
        /// 判断第一个长度大于0，返回第一个，反之第二个
        /// </summary>
        /// <param name="value0">待检查的参数1</param>
        /// <param name="value1">默认的参数</param>
        public static string anyValue_2(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string value0,string value1){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                value0,value1));
        }
        #endregion
        #region all_bool_3
        /// <summary>
        /// 表求值专用,前3个参数和后3个参数完全相等返回1，反之为0。
        /// </summary>
        /// <param name="value0">value0</param>
        /// <param name="value1">value1</param>
        /// <param name="value2">value2</param>
        /// <param name="value0True">value0True</param>
        /// <param name="value1True">value1True</param>
        /// <param name="value2True">value2True</param>
        public static bool all_bool_3(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,bool value0,bool value1,bool value2,bool value0True,bool value1True,bool value2True){
            return Symbol.TypeExtensions.Convert<bool>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                value0,value1,value2,value0True,value1True,value2True));
        }
        #endregion
        #region getDateOffset
        /// <summary>
        /// 求时间差，以小时为单，小数，求第二个与第一个之间的相差。
        /// </summary>
        /// <param name="dateX">第一个日期</param>
        /// <param name="dateY">第二个日期</param>
        public static double getDateOffset(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime dateX,System.DateTime dateY){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                dateX,dateY));
        }
        #endregion
        #region getDateOffset_days
        /// <summary>
        /// 求时间差，以天为单位
        /// </summary>
        /// <param name="dateX">第一个日期</param>
        /// <param name="dateY">第二个日期</param>
        public static int getDateOffset_days(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime dateX,System.DateTime dateY){
            return Symbol.TypeExtensions.Convert<int>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                dateX,dateY));
        }
        #endregion
        #region getDateOffset_minutes
        /// <summary>
        /// （分钟）求时间差，以小时为单，小数
        /// </summary>
        /// <param name="dateX">第一个日期</param>
        /// <param name="dateY">第二个日期</param>
        public static double getDateOffset_minutes(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime dateX,System.DateTime dateY){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                dateX,dateY));
        }
        #endregion
        #region getDateOffset_seconds
        /// <summary>
        /// （秒数）求时间差，以小时为单，小数
        /// </summary>
        /// <param name="dateX">第一个日期</param>
        /// <param name="dateY">第二个日期</param>
        public static double getDateOffset_seconds(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime dateX,System.DateTime dateY){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                dateX,dateY));
        }
        #endregion
        #region checkTime_minutes
        /// <summary>
        /// （按分钟）判断指定的时间与当前时间相差值是否合理
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="min">允许范围。</param>
        public static bool checkTime_minutes(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime date,int min){
            return Symbol.TypeExtensions.Convert<bool>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                date,min));
        }
        #endregion
        #region checkTime_days
        /// <summary>
        /// （按天数）判断指定的时间与当前时间相差值是否合理
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="min">允许范围。</param>
        public static bool checkTime_days(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime date,int min){
            return Symbol.TypeExtensions.Convert<bool>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                date,min));
        }
        #endregion
        #region notPeriod
        /// <summary>
        /// 判断指定的时间是否到到期，并返回指定的值。 value0小于value1，返回 value1True，反之为value0True。
        /// </summary>
        /// <param name="value0">为空返回value1True</param>
        /// <param name="value1">为空返回value1True</param>
        /// <param name="value0True">value0True</param>
        /// <param name="value1True">value1True</param>
        public static bool notPeriod(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime value0,System.DateTime value1,bool value0True,bool value1True){
            return Symbol.TypeExtensions.Convert<bool>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                value0,value1,value0True,value1True));
        }
        #endregion
        #region getWeekOfDay
        /// <summary>
        /// 获取日期中的星期几，修正数据为程序中的值。 以csharp为参考。
        /// </summary>
        /// <param name="date">当前日期</param>
        public static byte getWeekOfDay(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime date){
            return Symbol.TypeExtensions.Convert<byte>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                date));
        }
        #endregion
        #region isWeekRange_datetime
        /// <summary>
        /// 判断两个日期是否在同一周内
        /// </summary>
        /// <param name="datetime1">第一个日期</param>
        /// <param name="datetime2">第二个日期</param>
        public static bool isWeekRange_datetime(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,System.DateTime datetime1,System.DateTime datetime2){
            return Symbol.TypeExtensions.Convert<bool>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                datetime1,datetime2));
        }
        #endregion
        #region getRandomWeight_2
        /// <summary>
        /// 在基数上随机加权值，和额外2个指定加权值。
        /// </summary>
        /// <param name="base">基数</param>
        /// <param name="b1">加权1</param>
        /// <param name="b2">加权2</param>
        public static double getRandomWeight_2(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,double @base,bool b1,bool b2){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                @base,b1,b2));
        }
        #endregion
        #region getRandomWeight_2int
        /// <summary>
        /// 在基数上随机加权值，和额外2个指定加权值。
        /// </summary>
        /// <param name="base">基数</param>
        /// <param name="b1">加权1</param>
        /// <param name="b2">加权2</param>
        public static double getRandomWeight_2int(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,double @base,int b1,int b2){
            return Symbol.TypeExtensions.Convert<double>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                @base,b1,b2));
        }
        #endregion
        #region getSetting
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="defaultValue">默认值，不宜过长</param>
        public static string getSetting(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string name,string defaultValue){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                name,defaultValue));
        }
        #endregion
        #region getSettingAsInt
        /// <summary>
        /// 获取系统设置（尝试转为int类型）。
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="defaultValue">默认值</param>
        public static int getSettingAsInt(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string name,int defaultValue){
            return Symbol.TypeExtensions.Convert<int>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                name,defaultValue));
        }
        #endregion
        #region getSettingAsBit
        /// <summary>
        /// 获取系统设置（尝试转为bit类型）。
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="defaultValue">默认值</param>
        public static int getSettingAsBit(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string name,bool defaultValue){
            return Symbol.TypeExtensions.Convert<int>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                name,defaultValue));
        }
        #endregion
        #region getUserNameById
        /// <summary>
        /// 获取用户昵称（Id)
        /// </summary>
        /// <param name="id">用户Id</param>
        public static string getUserNameById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,int id){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                id));
        }
        #endregion
        #region getConsumeTypeNameById
        /// <summary>
        /// 获取消费类型名称（Id)
        /// </summary>
        /// <param name="id">id</param>
        public static string getConsumeTypeNameById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,int id){
            return Symbol.TypeExtensions.Convert<string>(dataContext.ExecuteFunction(GetName(System.Reflection.MethodInfo.GetCurrentMethod()),
                id));
        }
        #endregion
        #endregion
        
        #region procedures
        #endregion
        
        #region functions for Table
        #region getUserById
        /// <summary>
        /// 获取用户（按Id）
        /// </summary>
        /// <param name="id">用户 Id，为null将直接返回null。</param>
        public static User getUserById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,int? id){
            if(id==null)  return null;
            return dataContext.CreateQuery<User>("select top 1 * from [User] where [Id]=@p1",id).FirstOrDefault();
        }
        #endregion
        #region getUserByWhere
        /// <summary>
        /// 获取用户（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public static User getUserByWhere(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string whereExpression,params object[] @params){
            if(string.IsNullOrEmpty(whereExpression))  return null;
            return dataContext.CreateQuery<User>("select top 1 * from [User] where "+whereExpression,@params).FirstOrDefault();
        }
        #endregion
        #region getConsumeTypeById
        /// <summary>
        /// 获取消费选项（按Id）
        /// </summary>
        /// <param name="id">消费选项 Id，为null将直接返回null。</param>
        public static ConsumeType getConsumeTypeById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,int? id){
            if(id==null)  return null;
            return dataContext.CreateQuery<ConsumeType>("select top 1 * from [ConsumeType] where [Id]=@p1",id).FirstOrDefault();
        }
        #endregion
        #region getConsumeTypeByWhere
        /// <summary>
        /// 获取消费选项（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public static ConsumeType getConsumeTypeByWhere(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string whereExpression,params object[] @params){
            if(string.IsNullOrEmpty(whereExpression))  return null;
            return dataContext.CreateQuery<ConsumeType>("select top 1 * from [ConsumeType] where "+whereExpression,@params).FirstOrDefault();
        }
        #endregion
        #region getFundsLogById
        /// <summary>
        /// 获取资金记录（按Id）
        /// </summary>
        /// <param name="id">资金记录 Id，为null将直接返回null。</param>
        public static FundsLog getFundsLogById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,long? id){
            if(id==null)  return null;
            return dataContext.CreateQuery<FundsLog>("select top 1 * from [FundsLog] where [Id]=@p1",id).FirstOrDefault();
        }
        #endregion
        #region getFundsLogByWhere
        /// <summary>
        /// 获取资金记录（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public static FundsLog getFundsLogByWhere(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string whereExpression,params object[] @params){
            if(string.IsNullOrEmpty(whereExpression))  return null;
            return dataContext.CreateQuery<FundsLog>("select top 1 * from [FundsLog] where "+whereExpression,@params).FirstOrDefault();
        }
        #endregion
        #region getFundsConsumeTypeDayById
        /// <summary>
        /// 获取资金消费统计（天）（按Id）
        /// </summary>
        /// <param name="id">资金消费统计（天） Id，为null将直接返回null。</param>
        public static FundsConsumeTypeDay getFundsConsumeTypeDayById(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,long? id){
            if(id==null)  return null;
            return dataContext.CreateQuery<FundsConsumeTypeDay>("select top 1 * from [FundsConsumeTypeDay] where [Id]=@p1",id).FirstOrDefault();
        }
        #endregion
        #region getFundsConsumeTypeDayByWhere
        /// <summary>
        /// 获取资金消费统计（天）（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public static FundsConsumeTypeDay getFundsConsumeTypeDayByWhere(
#if !CSharp20
            this 
#endif
            Symbol.Data.DataContext dataContext,string whereExpression,params object[] @params){
            if(string.IsNullOrEmpty(whereExpression))  return null;
            return dataContext.CreateQuery<FundsConsumeTypeDay>("select top 1 * from [FundsConsumeTypeDay] where "+whereExpression,@params).FirstOrDefault();
        }
        #endregion
        #endregion
        
    }
}