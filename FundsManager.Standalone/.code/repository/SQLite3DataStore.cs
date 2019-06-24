using Symbol.Data;
using Symbol;

namespace FundsManager.Standalone {
    public class SQLite3DataStore : IDataStore {

        #region fields
        private Symbol.Data.IDataContext _dataContext = null;
        #endregion

        #region ctor
        public SQLite3DataStore() {
            string file = Symbol.Forms.ProgramHelper.MapPath("FundsManager.Standalone.o");
            var connectionStringBuilder = Symbol.Data.SQLite.SQLiteHelper.CreateConnectionStringBuilder(file, "&IHLIfdhsi376zhd");
            connectionStringBuilder["FailIfMissing"] = false;
            connectionStringBuilder["Pooling"] = true;
            string connectionString = connectionStringBuilder.ConnectionString;
            _dataContext = Symbol.Data.Provider.CreateDataContext("sqlite", connectionString);
            Symbol.Data.SQLite.SQLiteHelper.CreateFile(file);
        }
        #endregion

        #region IDataStore 成员

        public IDataContext DataContext {
            get { return _dataContext; }
        }

        public decimal TotalInMoney {
            get { return TypeExtensions.Convert<decimal>(_dataContext.ExecuteScalar("select sum([Money]) as p from [FundsConsumeTypeDay] where [UserId]=@p1 and [IsOut]=0", Program.CurrentUser.Id), 0M); }
        }

        public decimal TotalOutMoney {
            get { return TypeExtensions.Convert<decimal>(_dataContext.ExecuteScalar("select sum([Money]) as p from [FundsConsumeTypeDay] where [UserId]=@p1 and [IsOut]=1", Program.CurrentUser.Id), 0M); }
        }

        public void Add(ConsumeType model) {
            using (var builder = _dataContext.CreateInsert("ConsumeType")) {
                builder.Fields.SetValues(model);
                builder.Fields.Remove("Id");
                model.Id = TypeExtensions.Convert<int>(_dataContext.ExecuteScalar(builder.CommandText, builder.Values), 0);
            }
        }

        public void Edit(ConsumeType model) {
            using (var builder = _dataContext.CreateUpdate("ConsumeType")) {
                builder.Fields.SetValues(model);
                builder.Fields.Remove("Id");
                builder.Fields.Remove("IsOut");
                builder.Fields.Remove("UserId");
                _dataContext.ExecuteNonQuery(builder.CommandText + " where [Id]=" + model.Id, builder.Values);
            }
        }

        public void Remove(ConsumeType model) {
            _dataContext.ExecuteNonQuery("delete from [ConsumeType] where [Id]=@p1", model.Id);
        }

        public bool ExistsConsumeTypeWithName(string name, long id) {
            return TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [ConsumeType] where [UserId]=@p1 and [Name]=@p2 and [Id]<>@p3 limit 0,1", Program.CurrentUser.Id, name, id), 0) > 0;
        }

        public bool HasRefencenConsumeType(long id) {
            return (TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [FundsLog] where [UserId]=@p1 and [ConsumeTypeId]=@p2 limit 0,1", Program.CurrentUser.Id, id), 0) > 0);
        }

        public IDataQuery<ConsumeType> FindAllConsumeType(string columns = null) {
            return _dataContext.CreateQuery<ConsumeType>("select " + (string.IsNullOrEmpty(columns) ? "*" : columns) + " from [ConsumeType] where [UserId]=" + Program.CurrentUser.Id + " order by [Order],[Id]");
        }

        public void Add(FundsLog model) {
            using (var builder = _dataContext.CreateInsert("FundsLog")) {
                builder.Fields.SetValues(model);
                builder.Fields.Remove("Id");
                builder.Fields.Remove("ByDate");
                builder.Fields.Add("ByDateLong", model.ByDate.Ticks);

                model.Id = TypeExtensions.Convert<int>(_dataContext.ExecuteScalar(builder.CommandText, builder.Values), 0);
            }

            onFundsConsumeTypeDay(model);
        }

        public void Edit(FundsLog model) {
            FundsLog original = FindFundsLog(model.Id);
            using (var builder = _dataContext.CreateUpdate("FundsLog")) {
                builder.Fields.SetValues(model);
                builder.Fields.Remove("Id");
                builder.Fields.Remove("UserId");
                builder.Fields.Remove("ConsumeTypeId");
                builder.Fields.Remove("ByDate");
                builder.Fields.Add("ByDateLong", model.ByDate.Ticks);
                _dataContext.ExecuteNonQuery(builder.CommandText + " where [id]=" + model.Id, builder.Values);
            }
            onFundsConsumeTypeDay(original, true);
            onFundsConsumeTypeDay(model, false);
        }

        public void Remove(FundsLog model) {
            onFundsConsumeTypeDay(model, true);
            _dataContext.ExecuteNonQuery("delete from [FundsLog] where [Id]=@p1", model.Id);
        }
        delegate string NextParamHandler(object value);
        public FundsLog FindFundsLog(long id) {
            Symbol.Data.IDataQuery<FundsLog> q = _dataContext.CreateQuery<FundsLog>("select [t0].*,[t1].[Name] as [ConsumeTypeName],(toDate_long([CreateDateLong])) as [CreateDate],(toDate_long([ByDateLong])) as [ByDate] from [FundsLog] as [t0] left join [ConsumeType] as [t1] on [t1].[Id]=[t0].[ConsumeTypeId] where [t0].[id]=@p1 and [t0].[UserId]=" + Program.CurrentUser.Id,id);
            return q.FirstOrDefault();
        }
        public Paging<FundsLog> FindAllFundsLog(int page, System.DateTime? beginDate = null, System.DateTime? endDate = null, long? consumeTypeId = null, string keyword = null) {
            var builder = _dataContext.CreateSelect("FundsLog as log");
            builder.Refer(Symbol.Data.NoSQL.Refer.Begin().Refence("consumeType", "ConsumeType", "id", "log", "ConsumeTypeId").Json());

            builder.Select(
                   "log.*",
                   "consumeType.[Name] as [ConsumeTypeName]",
                   "(toDate_long(log.[CreateDateLong])) as [CreateDate]",
                   "(toDate_long(log.[ByDateLong])) as [ByDate]");

            if (beginDate != null) {
                builder.Gte("log.ByDateDay", System.DateTimeExtensions.ToDayNumber(beginDate.Value));
            }
            if (endDate != null) {
                builder.Lt("log.ByDateDay", System.DateTimeExtensions.ToDayNumber(endDate.Value.AddDays(1)));
            }

            builder.Eq("log.ConsumeTypeId", consumeTypeId);
            if (!string.IsNullOrEmpty(keyword)) {
                keyword = "%" + keyword + "%";
                var arg_name = builder.AddCommandParameter(keyword);
                builder.Where(
                    $"(log.RelatedPerson like {arg_name} or log.Comment like {arg_name})");
            }
            if (page != -1) {
                builder.Paging(Program.Config.ItemPerPage, page);
            }
            builder.OrderBy("[log].[ByDateLong] desc", "[log].[Id] desc");

            var paging = new Paging<FundsLog>();
            paging.Query = builder.CreateQuery<FundsLog>();
            if (page != -1) {
                paging.Page = page;
                paging.Size = Program.Config.ItemPerPage;
                builder.Count();
                paging.Count = builder.FirstOrDefault<int>();
                builder.Skip(0).Take(-1);
                paging.TotalCount = builder.FirstOrDefault<int>();
                paging.PageCount = (int)System.Math.Ceiling((decimal)paging.TotalCount / paging.Size);

            } else {
                paging.Page = 0;
                paging.Size = -1;
                builder.Count();
                paging.TotalCount = builder.FirstOrDefault<int>();
                paging.PageCount = 1;
                paging.Count = paging.TotalCount;
            }

            return paging;
        }

        public User Register(string account, string password, string name) {
            User result = new User() {
                Account = account,
                Password = Symbol.Encryption.MD5EncryptionHelper.Encrypt(password),
                Name = name
            };
            using (var builder = _dataContext.CreateInsert("User")) {
                builder.Fields.SetValues(result);
                builder.Fields.Remove("Id");
                result.Id = TypeExtensions.Convert<int>(_dataContext.ExecuteScalar(builder.CommandText, builder.Values), 0);
            }

            return result;
        }

        public User FindUserByAccount(string account) {
            if (string.IsNullOrEmpty(account))
                return null;
            return _dataContext.CreateQuery<User>("select * from [User] where [Account]=@p1 limit 0,1", account).FirstOrDefault();
        }

        public bool ExistsUserWithAccount(string account) {
            if (string.IsNullOrEmpty(account))
                return false;
            return TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [User] where [Account]=@p1 limit 0,1", account), 0) > 0;
        }

        public bool ExistsUserWithName(string name) {
            if (string.IsNullOrEmpty(name))
                return false;
            return TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [User] where [Name]=@p1 limit 0,1", name), 0) > 0;
        }

        public void ChangePassword(string account, string newPassword) {
            if (string.IsNullOrEmpty(newPassword))
                return;
            User model = FindUserByAccount(account);
            string md5 = Symbol.Encryption.MD5EncryptionHelper.Encrypt(newPassword);
            _dataContext.ExecuteNonQuery("update [User] set [Password]=@p1 where [Id]=@p2", md5, model.Id);
        }

        public decimal GetTotalMoneyByMonth(int year, int month, bool isOut) {
            System.DateTime beginDate = new System.DateTime(year, month, 01);
            System.DateTime endDate = beginDate.AddMonths(1);
            int beginDay = System.DateTimeExtensions.ToDayNumber(beginDate);
            int endDay = System.DateTimeExtensions.ToDayNumber(endDate);
            return TypeExtensions.Convert<decimal>(_dataContext.ExecuteScalar("select sum([Money]) as p from [FundsConsumeTypeDay] where [UserId]=@p1 and [Day]>=@p2 and [Day]<@p3 and [IsOut]=@p4", Program.CurrentUser.Id, beginDay, endDay, isOut), 0M);
        }

        public decimal GetTotalMoneyByConsumeType(long consumeTypeId, System.DateTime? beginDate = null, System.DateTime? endDate = null) {
            string sql = "select sum([Money]) as p from [FundsConsumeTypeDay] where [UserId]=" + Program.CurrentUser.Id + " and [ConsumeTypeId]=" + consumeTypeId;
            if (beginDate != null) {
                sql += " and [Day]>=" + System.DateTimeExtensions.ToDayNumber(beginDate.Value);
            }
            if (endDate != null) {
                sql += " and [Day]<" + System.DateTimeExtensions.ToDayNumber(endDate.Value.AddDays(1));
            }
            return TypeExtensions.Convert<decimal>(_dataContext.ExecuteScalar(sql), 0M);
        }

        public void CheckSchema() {
            new DatabaseSchemaWithBase(_dataContext).Check();
            new Schema_1(_dataContext).Check();
            new Schema_2(_dataContext).Check();
            new Schema_3(_dataContext).Check();
        }

        #endregion
        #region methods

        #region onFundsConsumeTypeDay
        void onFundsConsumeTypeDay(FundsLog model, bool undo = false) {
            int day = model.ByDateDay;
            //if (undo) {
            //    dataContext.ExecuteNonQuery("update [Funds] set [Money]=[Money]" + (model.IsOut ? "+" : "-") + "@p1 where [Id]=@p2", model.Money, model.FundsId);
            //} else {
            //    dataContext.ExecuteNonQuery("update [Funds] set [Money]=[Money]" + (model.IsOut ? "-" : "+") + "@p1 where [Id]=@p2", model.Money, model.FundsId);
            //}
            //FundsConsumeTypeDay item = dataContext.getFundsConsumeTypeDayByWhere("[FundsId]=@p1 and [ConsumeTypeId]=@p2 and [Day]=@p3", model.FundsId, model.ConsumeTypeId, model.ByDateDay);
            FundsConsumeTypeDay item = getFundsConsumeTypeDayByWhere("[ConsumeTypeId]=@p1 and [Day]=@p2", model.ConsumeTypeId, model.ByDateDay);
            if (item == null) {
                if (undo)
                    return;
                item = new FundsConsumeTypeDay() {
                    UserId = model.UserId,
                    //FundsId = model.FundsId,
                    ConsumeTypeId = model.ConsumeTypeId,
                    Day = model.ByDateDay,
                    IsOut = model.IsOut,
                    Money = model.Money,
                };
                using (var builder = _dataContext.CreateInsert("FundsConsumeTypeDay")) {
                    builder.Fields.SetValues(item);
                    builder.Fields.Remove("Id");
                    _dataContext.ExecuteNonQuery(builder.CommandText, builder.Values);
                }
            } else {
                _dataContext.ExecuteNonQuery("update [FundsConsumeTypeDay] set [Money]=[Money]" + (undo ? "-" : "+") + "@p1 where [Id]=@p2", model.Money, item.Id);
            }
        }
        #endregion
        #region getUserById
        /// <summary>
        /// 获取用户（按Id）
        /// </summary>
        /// <param name="id">用户 Id，为null将直接返回null。</param>
        public User getUserById(int? id) {
            if (id == null) return null;
            return _dataContext.CreateQuery<User>("select *,(toDate_long([CreateDateLong])) as [CreateDate] from [User] where [Id]=@p1 limit 0,1", id).FirstOrDefault();
        }
        #endregion
        #region getUserByWhere
        /// <summary>
        /// 获取用户（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public User getUserByWhere(string whereExpression, params object[] @params) {
            if (string.IsNullOrEmpty(whereExpression)) return null;
            return _dataContext.CreateQuery<User>("select *,(toDate_long([CreateDateLong])) as [CreateDate] from [User] where " + whereExpression + " limit 0,1", @params).FirstOrDefault();
        }
        #endregion
        #region getConsumeTypeById
        /// <summary>
        /// 获取消费选项（按Id）
        /// </summary>
        /// <param name="id">消费选项 Id，为null将直接返回null。</param>
        public ConsumeType getConsumeTypeById(int? id) {
            if (id == null) return null;
            return _dataContext.CreateQuery<ConsumeType>("select * from [ConsumeType] where [Id]=@p1 limit 0,1", id).FirstOrDefault();
        }
        #endregion
        #region getConsumeTypeByWhere
        /// <summary>
        /// 获取消费选项（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public ConsumeType getConsumeTypeByWhere(string whereExpression, params object[] @params) {
            if (string.IsNullOrEmpty(whereExpression)) return null;
            return _dataContext.CreateQuery<ConsumeType>("select * from [ConsumeType] where " + whereExpression+" limit 0,1", @params).FirstOrDefault();
        }
        #endregion
        #region getFundsConsumeTypeDayById
        /// <summary>
        /// 获取资金消费统计（天）（按Id）
        /// </summary>
        /// <param name="id">资金消费统计（天） Id，为null将直接返回null。</param>
        public FundsConsumeTypeDay getFundsConsumeTypeDayById(long? id) {
            if (id == null) return null;
            return _dataContext.CreateQuery<FundsConsumeTypeDay>("select * from [FundsConsumeTypeDay] where [Id]=@p1 limit 0,1", id).FirstOrDefault();
        }
        #endregion
        #region getFundsConsumeTypeDayByWhere
        /// <summary>
        /// 获取资金消费统计（天）（按条件）
        /// </summary>
        /// <param name="whereExpression">where条件，为空或空字符串，将直接返回null。</param>
        /// <param name="params">参数列表</param>
        public FundsConsumeTypeDay getFundsConsumeTypeDayByWhere(string whereExpression, params object[] @params) {
            if (string.IsNullOrEmpty(whereExpression)) return null;
            return _dataContext.CreateQuery<FundsConsumeTypeDay>("select * from [FundsConsumeTypeDay] where " + whereExpression + " limit 0,1", @params).FirstOrDefault();
        }
        #endregion

        #region upload_LastMoney_ConsumeType
        public void upload_LastMoney_ConsumeType() {
            Symbol.Data.IDataQuery<m_lm_ConsumeType_Input> q = _dataContext.CreateQuery<m_lm_ConsumeType_Input>("select [name],[order],[isOut] from [ConsumeType] where [UserId]=" + Program.CurrentUser.Id + " order by [Order],[Id]");
            int count = Program.LastMoney.import(q.ToList());
            if (Program.LastMoney.lastResult.success) {
                Symbol.Forms.ProgramHelper.ShowInformation("同步成功，共 {0} 条消费选项！", count);
            } else {
                Symbol.Forms.ProgramHelper.ShowInformation(Program.LastMoney.lastMessage);
            }
        }
        #endregion

        #region upload_LastMoney_FundsLog
        public void upload_LastMoney_FundsLog() {
            Symbol.Data.IDataQuery<m_lm_MoneyRecord_Import> q = _dataContext.CreateQuery<m_lm_MoneyRecord_Import>("select [t0].*,[t1].[Name] as [ConsumeTypeName],(toDate_long([CreateDateLong])) as [CreateDate],(toDate_long([ByDateLong])) as [ByDate] from [FundsLog] as [t0] left join [ConsumeType] as [t1] on [t1].[Id]=[t0].[ConsumeTypeId] where [t0].[UserId]=@p1 and [SourceId] like @p2",Program.CurrentUser.Id,"funds_%");
            int count=Program.LastMoney.import(q.ToList());
            if (Program.LastMoney.lastResult.success) {
                Symbol.Forms.ProgramHelper.ShowInformation("同步成功，共 {0} 条账目！", count);
            } else {
                Symbol.Forms.ProgramHelper.ShowInformation(Program.LastMoney.lastMessage);
            }
        }
        #endregion

        #endregion

        #region types
        class Schema_1 : DatabaseSchema {
            #region ctor
            public Schema_1(IDataContext dataContext)
                : base(dataContext) {
                _items.Add(new DatabaseSchemaCheckItem("User", DatabaseSchemaTypes.Table, 0, (s, i) => {
                    _dataContext.ExecuteNonQuery("CREATE TABLE [User]([Id] integer PRIMARY KEY AUTOINCREMENT not null, [CreateDateLong] bigint NOT NULL default(getDate_long()), [LastLoginDateLong] bigint NOT NULL default(getDate_long()), [Account] nvarchar(64) NOT NULL,[Password] char(32) NOT NULL,[Name] nvarchar(16) NOT NULL)");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_User_Account] on [User]([Account])");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_User_Name] on [User]([Name])");
                }));
                _items.Add(new DatabaseSchemaCheckItem("ConsumeType", DatabaseSchemaTypes.Table, 0, (s, i) => {
                    _dataContext.ExecuteNonQuery("CREATE TABLE [ConsumeType] ([Id] integer PRIMARY KEY AUTOINCREMENT not null,[UserId] int NOT NULL,[Order] float NOT NULL default(0),[IsOut] bit NOT NULL,[Name] nvarchar(16) NOT NULL)");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_ConsumeType_UserId_Order_Id] on [ConsumeType]([UserId],[Order],[Id])");
                }));
                _items.Add(new DatabaseSchemaCheckItem("FundsLog", DatabaseSchemaTypes.Table, 0, (s, i) => {
                    _dataContext.ExecuteNonQuery("CREATE TABLE [FundsLog] ([Id] integer PRIMARY KEY AUTOINCREMENT not null,[UserId] int NOT NULL,[ConsumeTypeId] int NOT NULL,[ByDateDay] int NOT NULL,[CreateDateDay] int NOT NULL default(getDayNumber(getDate())),[ByDateLong] bigint NOT NULL,[CreateDateLong] bigint NOT NULL default(getDate_long()),[IsOut] bit NOT NULL,[Money] decimal(18,2) NOT NULL,[LastMoney] decimal(18,2) NOT NULL,[RelatedPerson] nvarchar(64) NULL,[Comment] ntext NULL)");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsLog_UserId_ConsumeTypeId] on [FundsLog]([UserId],[ConsumeTypeId])");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsLog_ConsumeTypeId] on [FundsLog]([ConsumeTypeId])");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsLog_ByDateDay] on [FundsLog]([ByDateDay])");
                }));
                _items.Add(new DatabaseSchemaCheckItem("FundsConsumeTypeDay", DatabaseSchemaTypes.Table, 0, (s, i) => {
                    _dataContext.ExecuteNonQuery("CREATE TABLE [FundsConsumeTypeDay] ([Id] integer PRIMARY KEY AUTOINCREMENT not null,[UserId] int NOT NULL,[ConsumeTypeId] int NOT NULL,[Day] int NOT NULL,[IsOut] bit NOT NULL,[Money] decimal(18,2) NOT NULL)");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsConsumeTypeDay_UserId_ConsumeTypeId] on [FundsConsumeTypeDay]([UserId],[ConsumeTypeId])");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsConsumeTypeDay_ConsumeTypeId] on [FundsConsumeTypeDay]([ConsumeTypeId])");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsConsumeTypeDay_Day] on [FundsConsumeTypeDay]([Day])");
                }));

            }
            #endregion

        }
        class Schema_2 : DatabaseSchema {
                        #region ctor
            public Schema_2(IDataContext dataContext)
                : base(dataContext) {
                //if (TypeExtensions.Convert<long>(dataContext.ExecuteScalar("select [Id] from [FundsLog] where [ByDateLong]<0 limit 0,1"), 0L) > 0L) {
                //    foreach (FundsLog item in dataContext.CreateQuery<FundsLog>("select [Id],(toDate_long_binary([CreateDateLong])) as [CreateDate],(toDate_long_binary([ByDateLong])) from [FundsLog] where [ByDateLong]<0")) {
                //        dataContext.ExecuteNonQuery("update [FundsLog] set [CreateDateLog]=@p1,[ByDateLong]=@p2 where [Id]=@p3",item.CreateDate.Ticks,item.ByDate.Ticks,item.Id);
                //    }
                dataContext.ExecuteNonQuery("update [FundsLog] set [CreateDateLong]=getDate_long_value(toDate_long_binary([CreateDateLong])),[ByDateLong]=getDate_long_value(toDate_long_binary([ByDateLong]))");
                //}
            }
            #endregion

        }
        class Schema_3 : DatabaseSchema {
                        #region ctor
            public Schema_3(IDataContext dataContext)
                : base(dataContext) {
                //if (TypeExtensions.Convert<long>(dataContext.ExecuteScalar("select [Id] from [FundsLog] where [ByDateLong]<0 limit 0,1"), 0L) > 0L) {
                //    foreach (FundsLog item in dataContext.CreateQuery<FundsLog>("select [Id],(toDate_long_binary([CreateDateLong])) as [CreateDate],(toDate_long_binary([ByDateLong])) from [FundsLog] where [ByDateLong]<0")) {
                //        dataContext.ExecuteNonQuery("update [FundsLog] set [CreateDateLog]=@p1,[ByDateLong]=@p2 where [Id]=@p3",item.CreateDate.Ticks,item.ByDate.Ticks,item.Id);
                //    }
                _items.Add(new DatabaseSchemaCheckItem("FundsLog", DatabaseSchemaTypes.Table, 1, (s, i) => {
                    if (_dataContext.GetColumnInfo("FundsLog", "sourceId").Exists)
                        return;
                    _dataContext.ExecuteNonQuery("alter TABLE [FundsLog] add column sourceId nvarchar(32) null");
                    _dataContext.ExecuteNonQuery("CREATE INDEX [idx_FundsLog_sourceId] on [FundsLog]([sourceId])");
                    _dataContext.ExecuteNonQuery("update [FundsLog] set [sourceId]='funds_'||[id]");
                }));
                //}
            }
            #endregion

        }
        #endregion

    }

}