namespace Symbol.Data {
    [Const("Version", "1")]
    class DatabaseSchemaWithBase : DatabaseSchema {
        #region ctor
        public DatabaseSchemaWithBase(IDataContext dataContext)
            : base(dataContext) {
            _items.Add(new DatabaseSchemaCheckItem("Gouring_SchemaVersion", DatabaseSchemaTypes.Table, 0, (s, i) => {
                _dataContext.ExecuteNonQuery("CREATE TABLE [Gouring_SchemaVersion]([Id] integer PRIMARY KEY AUTOINCREMENT not null, [Name] varchar(255) NOT NULL,[Type] tinyint NOT NULL default 0,[Version] int NOT NULL default 1)");
                _dataContext.ExecuteNonQuery("CREATE INDEX [idx_Gouring_SchemaVersion_Name_Type] on [Gouring_SchemaVersion]([Name],[Type])");
            }));
            _items.Add(new DatabaseSchemaCheckItem("Gouring_Setting", DatabaseSchemaTypes.Table, 0, (s, i) => {
                _dataContext.ExecuteNonQuery("CREATE TABLE [Gouring_Setting] ([Id] integer PRIMARY KEY AUTOINCREMENT not null, [Name] varchar(255) NOT NULL,[GroupName] nvarchar(64) not null,[Permission] varchar(255) not null,[Order] float not null,[Value] ntext,[DisplayName] nvarchar(64),[Description] ntext,[Editor] varchar(255),[CreateDate] datetime default(getDate()))");
                _dataContext.ExecuteNonQuery("CREATE INDEX [idx_Gouring_Setting_Name] on [Gouring_Setting]([Name])");
                _dataContext.ExecuteNonQuery("CREATE INDEX [idx_Gouring_Setting_GroupName] on [Gouring_Setting]([GroupName])");
            }));

        }
        #endregion
    }

    /// <summary>
    /// 数据库架构
    /// </summary>
    public abstract class DatabaseSchema {
        #region fields
        protected IDataContext _dataContext;
        protected System.Collections.Generic.List<DatabaseSchemaCheckItem> _items;

        #endregion

        #region ctor
        public DatabaseSchema(IDataContext dataContext) {
            _dataContext = dataContext;
            _items = new System.Collections.Generic.List<DatabaseSchemaCheckItem>();
        }
        #endregion

        #region methods

        #region GetSchemaVersion
        /// <summary>
        /// 获取架构版本
        /// </summary>
        /// <param name="name">架构名称</param>
        /// <param name="type">架构类型</param>
        /// <returns>返回版本，如果未找到对应的记录将返回0。</returns>
        public int GetSchemaVersion(string name, DatabaseSchemaTypes type = DatabaseSchemaTypes.Table) {
            return TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Version] from [Gouring_SchemaVersion] where [Name]=@p1 and [Type]=@p2 limit 0,1", name, type), 0);
        }
        #endregion

        #region SetSchemaVersion
        /// <summary>
        /// 设置架构版本
        /// </summary>
        /// <param name="name">架构名称</param>
        /// <param name="type">架构类型</param>
        /// <param name="version">架构版本</param>
        public void SetSchemaVersion(string name, DatabaseSchemaTypes type, int version) {
            int id = TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [Gouring_SchemaVersion] where [Name]=@p1 and [Type]=@p2 limit 0,1", name, type), 0);
            if (id > 0)
                _dataContext.ExecuteNonQuery("update [Gouring_SchemaVersion] set [Version]=@p1 where [Id]=@p2", version, id);
            else
                _dataContext.ExecuteNonQuery("insert into [Gouring_SchemaVersion]([Name],[Type],[Version]) values(@p1,@p2,@p3)", name, type, version);
        }
        #endregion

        #region TriggerExists
        /// <summary>
        /// 检查触发器是否存在
        /// </summary>
        /// <param name="triggerName">需要检查的触发器名称。</param>
        /// <returns>返回是否存在。</returns>
        public bool TriggerExists(string triggerName) {
            return TypeExtensions.Convert<bool>(_dataContext.ExecuteScalar("select count(*) from sqlite_master where type='trigger' and name='" + triggerName + "'"), false);
        }
        #endregion

        #region SettingExists
        /// <summary>
        /// 检查指定的系统设置项是否存在
        /// </summary>
        /// <param name="settingName">设置名称</param>
        /// <returns>返回是否存在。注意：若Gouring_Setting表不存在时，直接返回false。</returns>
        public bool SettingExists(string settingName) {
            if (_dataContext.TableExists("Gouring_Setting")) {
                return TypeExtensions.Convert<int>(_dataContext.ExecuteScalar("select [Id] from [Gouring_Setting] where [Name]=@p1", settingName), 0) > 0;
            }
            return false;
        }
        #endregion

        #region Check
        public virtual void Check() {
            foreach (DatabaseSchemaCheckItem item in _items) {
                bool exists = false;
                if (item.Type == DatabaseSchemaTypes.Table)
                    exists = _dataContext.TableExists(item.Name);
                //else if (item.Type == DatabaseSchemaTypes.Trigger)
                //    exists = TriggerExists(item.Name);
                else
                    exists = false;
                if (item.Version == 0 && exists)
                    continue;
                if (item.Version == 0 || GetSchemaVersion(item.Name, item.Type) < item.Version) {
                    item.Creater(this, item);
                    if (item.Version != 0)
                        SetSchemaVersion(item.Name, item.Type, item.Version);
                }
            }
        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 数据库架构创建器
    /// </summary>
    /// <param name="schema">架构实例</param>
    /// <param name="item">检查项</param>
    public delegate void DatabaseSchemaCreater(DatabaseSchema schema, DatabaseSchemaCheckItem item);
    /// <summary>
    /// 数据库架构检查项
    /// </summary>
    public class DatabaseSchemaCheckItem {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 架构类型
        /// </summary>
        public DatabaseSchemaTypes Type { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 创建器
        /// </summary>
        public DatabaseSchemaCreater Creater { get; set; }
        public DatabaseSchemaCheckItem(string name, DatabaseSchemaTypes type, int version, DatabaseSchemaCreater creater) {
            Name = name;
            Type = type;
            Version = version;
            Creater = creater;
        }
    }
  
    
}