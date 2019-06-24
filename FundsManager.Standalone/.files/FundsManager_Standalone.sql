use [master]

GO
create database [FundsManager_Standalone] ON  PRIMARY (
    NAME = N'FundsManager_Standalone', 
    FILENAME = N'd:\.system\database\$\data\FundsManager_Standalone.mdf' , SIZE = 3072KB , FILEGROWTH = 1024KB 
)
LOG ON ( 
    NAME = N'log', 
    FILENAME = N'd:\.system\database\$\data\FundsManager_Standalone_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10% 
)
GO
exec dbo.sp_dbcmptlevel @dbname=N'FundsManager_Standalone', @new_cmptlevel=90
GO
if (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
exec [FundsManager_Standalone].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
alter database [FundsManager_Standalone] set ANSI_NULL_DEFAULT OFF 
GO
alter database [FundsManager_Standalone] set ANSI_NULLS OFF 
GO
alter database [FundsManager_Standalone] set ANSI_PADDING OFF 
GO
alter database [FundsManager_Standalone] set ANSI_WARNINGS OFF 
GO
alter database [FundsManager_Standalone] set ARITHABORT OFF 
GO
alter database [FundsManager_Standalone] set AUTO_CLOSE OFF 
GO
alter database [FundsManager_Standalone] set AUTO_CREATE_STATISTICS ON 
GO
alter database [FundsManager_Standalone] set AUTO_SHRINK ON 
GO
alter database [FundsManager_Standalone] set CURSOR_CLOSE_ON_COMMIT OFF 
GO
alter database [FundsManager_Standalone] set CURSOR_DEFAULT GLOBAL 
GO
alter database [FundsManager_Standalone] set CONCAT_NULL_YIELDS_NULL OFF 
GO
alter database [FundsManager_Standalone] set NUMERIC_ROUNDABORT OFF 
GO
alter database [FundsManager_Standalone] set QUOTED_IDENTIFIER OFF 
GO
alter database [FundsManager_Standalone] set RECURSIVE_TRIGGERS OFF 
GO
alter database [FundsManager_Standalone] set AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
alter database [FundsManager_Standalone] set DATE_CORRELATION_OPTIMIZATION OFF 
GO
alter database [FundsManager_Standalone] set PARAMETERIZATION SIMPLE 
GO
alter database [FundsManager_Standalone] set READ_WRITE 
GO
alter database [FundsManager_Standalone] set RECOVERY SIMPLE 
GO
alter database [FundsManager_Standalone] set MULTI_USER 
GO
alter database [FundsManager_Standalone] set PAGE_VERIFY CHECKSUM 
GO
use [FundsManager_Standalone]
GO
if not exists (select name from sys.filegroups where is_default=1 and name = N'PRIMARY') alter database [FundsManager_Standalone] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
use [master]

GO
create LOGIN [FundsManager_Standalone] WITH PASSWORD=N'0390864C8C8BDF26B65696F942BFC500', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
use [FundsManager_Standalone]
GO
create USER [FundsManager_Standalone] FOR LOGIN [FundsManager_Standalone]
GO
use [FundsManager_Standalone]
GO
exec sp_addrolemember N'db_owner', N'FundsManager_Standalone'
GO
GO
-- 包装系统的rand函数
create function [dbo].[randX]() returns float as begin
    return (SELECT [Expr1] FROM [dbo].[RandView])
end
GO
-- 求随机值
create function [dbo].[randomNext](
    @min              float                       ,-- 最小值
    @max              float                       ,-- 最大值,输出的值，始终小于此值
    @asInteger        bit             =0           -- 为1表示输出为整数
) returns float as begin
    declare @value float
    if (@asInteger=1)
        set @value= cast( floor([dbo].[randX]()* (@max-@min)+@min )as int) 
    else
        set @value= ([dbo].[randX]()* (@max-@min)+@min ) 
    return @value
end
GO
-- 将数字左边追加一些0
create function [dbo].[valuePaddingLeft](
    @value            sql_variant                 ,-- 需要处理的数据
    @length           int                         ,-- 期望长度
    @char             char                         -- 填充字符
) returns nvarchar(255) as begin
    declare @result nvarchar(255)
    set @result=right( replace(space(@length),' ',@char)+convert(nvarchar(255),@value),@length)
    return @result
end
GO
-- 获取日期的数字格式：20120720
create function [dbo].[getDayNumber](
    @date             datetime                     -- 日期
) returns int as begin
    declare @result int
    if (@date is null)
        set @date=getDate()
    set @result=datepart(yyyy,@date) * 10000 + datepart(MM,@date) * 100+ datepart(dd,@date)
    return @result
end
GO
-- 求字符串长度
create function [dbo].[getLength](
    @value            nvarchar(max)                -- 值
) returns int as begin
    if (@value is null)
    	return 0
    declare @length int
    set @length= len(@value)
    return @length
end
GO
-- guid 转为字符串，32位char
create function [dbo].[guidToString](
    @guid             uniqueidentifier             -- GUID
) returns char(32) as begin
    return replace(@guid,'-','')
end
GO
-- 判断第一个长度大于0，返回第一个，反之第二个 3 4 
create function [dbo].[anyValue_4](
    @value0           nvarchar(255)               ,-- 待检查的参数1
    @value1           nvarchar(255)               ,-- 待检查的参数2
    @value2           nvarchar(255)               ,-- 待检查的参数3
    @value3           nvarchar(255)                -- 默认的参数
) returns nvarchar(255) as begin
    if (len( @value0 ) >0)
        return @value0
    if (len( @value1 ) >0)
        return @value1
    if (len( @value2 ) >0)
        return @value2
    return @value3
end
GO
-- 判断第一个长度大于0，返回第一个，反之第二个
create function [dbo].[anyValue_2](
    @value0           nvarchar(255)               ,-- 待检查的参数1
    @value1           nvarchar(255)                -- 默认的参数
) returns nvarchar(255) as begin
    if (len( @value0 ) >0)
        return @value0
    return @value1
end
GO
-- 表求值专用,前3个参数和后3个参数完全相等返回1，反之为0。
create function [dbo].[all_bool_3](
    @value0           bit                         ,
    @value1           bit                         ,
    @value2           bit                         ,
    @value0True       bit                         ,
    @value1True       bit                         ,
    @value2True       bit                          
) returns bit as begin
    if(@value0=@value0True and @value1=@value1True and @value2=@value2True)
        return 1
    return 0
end
GO
-- 求时间差，以小时为单，小数，求第二个与第一个之间的相差。
create function [dbo].[getDateOffset](
    @dateX            datetime                    ,-- 第一个日期
    @dateY            datetime                     -- 第二个日期
) returns float as begin
    declare @f float
    declare @f2 float
    set @f=DATEDIFF(DAY,@dateY,@dateX) * 24
    set @f2=DATEDIFF(MILLISECOND,DATEADD(DAY, DATEDIFF(DAY, @dateY, @dateX), @dateY),@dateX)
    set @f2=@f2/3600000
    return @f+@f2
end
GO
-- 求时间差，以天为单位
create function [dbo].[getDateOffset_days](
    @dateX            datetime                    ,-- 第一个日期
    @dateY            datetime                     -- 第二个日期
) returns int as begin
    return DATEDIFF(DAY,@dateY,@dateX)
end
GO
-- （分钟）求时间差，以小时为单，小数
create function [dbo].[getDateOffset_minutes](
    @dateX            datetime                    ,-- 第一个日期
    @dateY            datetime                     -- 第二个日期
) returns float as begin
    declare @f float
    declare @f2 float
    set @f=DATEDIFF(DAY,@dateY,@dateX) * 1440
    set @f2=DATEDIFF(minute,DATEADD(DAY, DATEDIFF(DAY, @dateY, @dateX), @dateY),@dateX)
    return @f+@f2
end
GO
-- （秒数）求时间差，以小时为单，小数
create function [dbo].[getDateOffset_seconds](
    @dateX            datetime                    ,-- 第一个日期
    @dateY            datetime                     -- 第二个日期
) returns float as begin
    declare @f float
    declare @f2 float
    set @f=DATEDIFF(DAY,@dateY,@dateX) * 86400
    set @f2=DATEDIFF(SECOND,DATEADD(DAY, DATEDIFF(DAY, @dateY, @dateX), @dateY),@dateX)
    return @f+@f2
end
GO
-- （按分钟）判断指定的时间与当前时间相差值是否合理
create function [dbo].[checkTime_minutes](
    @date             datetime                    ,-- 当前时间
    @min              int                          -- 允许范围。
) returns bit as begin
    if (@date is null)
        return 0
    declare @offset float
    set @offset=[dbo].[getDateOffset_minutes](getDate(),@date)
    if (@offset>@min)
        return 0
    return 1
end
GO
-- （按天数）判断指定的时间与当前时间相差值是否合理
create function [dbo].[checkTime_days](
    @date             datetime                    ,-- 当前时间
    @min              int                          -- 允许范围。
) returns bit as begin
    if (@date is null)
        return 0
    declare @offset float
    set @offset=[dbo].[getDateOffset_days](getDate(),@date)
    if (@offset>@min)
        return 0
    return 1
end
GO
-- 判断指定的时间是否到到期，并返回指定的值。
-- value0小于value1，返回 value1True，反之为value0True。
create function [dbo].[notPeriod](
    @value0           datetime                    ,-- 为空返回value1True
    @value1           datetime                    ,-- 为空返回value1True
    @value0True       bit                         ,
    @value1True       bit                          
) returns bit as begin
    if (@value0 is null or @value1 is null) 
    	return @value1True
    if (@value0 <@value1)
    	return @value1True
    return @value0True
end
GO
-- 获取日期中的星期几，修正数据为程序中的值。
-- 以csharp为参考。
create function [dbo].[getWeekOfDay](
    @date             datetime                     -- 当前日期
) returns tinyint as begin
    return case ((DATEPART(dw, @date)) + @@DATEFIRST) % 7
    		when 1 then 0  --Sunday
    		when 2 then 1  --Monday
    		when 3 then 2  --Tuesday
    		when 4 then 3  --Wednesday
    		when 5 then 4  --Thursday
    		when 6 then 5  --Friday
    		when 0 then 6  --Saturday
        end
    
end
GO
-- 判断两个日期是否在同一周内
create function [dbo].[isWeekRange_datetime](
    @datetime1        datetime                    ,-- 第一个日期
    @datetime2        datetime                     -- 第二个日期
) returns bit as begin
    -- eq(all. year,month,day) return 1
    --day1{year,month} eq day2{ year,month}
    --  !eq: 0
    --   0(7) 1 2 3 4 5 6
    --    1   2 3 4 5 6 7
    --          ^
    --                  ^
    --    ^
    --               ^
    --    if(0) this
    --      else date offset - wd value => dx1 ,dx2 datetime  dateadd
    --   reset year,month,day   by  dx1 ,dx2
    --  eq all( year,month ,day) return 1
    --  return 0
    declare @year1 int,@month1 int,@day1 int,@weekDays1 tinyint
    declare @year2 int,@month2 int,@day2 int,@weekDays2 tinyint
    set @year1=datepart(yyyy,@datetime1)
    set @month1=datepart(MM,@datetime1)
    set @day1=datepart(dd,@datetime1)
    set @weekDays1=[dbo].[getWeekOfDay](@datetime1)
    
    set @year2=datepart(yyyy,@datetime2)
    set @month2=datepart(MM,@datetime2)
    set @day2=datepart(dd,@datetime2)
    set @weekDays2=[dbo].[getWeekOfDay](@datetime2)
    
    if(@year1=@year2 and @month1=@month2 and @day1=@day2) -- eq all
        return 1
    declare @dx1 datetime,@dx2 datetime
    set @dx1=convert(varchar(4),@year1)+'-'+convert(varchar(2),@month1)+'-'+convert(varchar(2),@day1)
    set @dx2=convert(varchar(4),@year2)+'-'+convert(varchar(2),@month2)+'-'+convert(varchar(2),@day2)
    set @dx1=dateadd(day,-@weekDays1,@dx1)
    set @dx2=dateadd(day,-@weekDays2,@dx2)
    if(@dx1=@dx2)
        return 1
    return 0
end
GO
-- 在基数上随机加权值，和额外2个指定加权值。
create function [dbo].[getRandomWeight_2](
    @base             float                       ,-- 基数
    @b1               bit                         ,-- 加权1
    @b2               bit                          -- 加权2
) returns float as begin
    declare @weight float
    set @weight=@base+[dbo].[randX]()
    if(@b1=1)
        set @weight=@weight+0.05
    if(@b2=1)
        set @weight=@weight+0.08
    return @weight
end
GO
-- 在基数上随机加权值，和额外2个指定加权值。
create function [dbo].[getRandomWeight_2int](
    @base             float                       ,-- 基数
    @b1               int                         ,-- 加权1
    @b2               int                          -- 加权2
) returns float as begin
    declare @weight float
    set @weight=@base+[dbo].[randX]()
    if(@b1=1)
        set @weight=@weight+0.05
    if(@b2=1)
        set @weight=@weight+0.08
    return @weight
end
GO
-- 获取系统设置
create function [dbo].[getSetting](
    @name             nvarchar(255)               ,-- 设置名称
    @defaultValue     nvarchar(64)    =''          -- 默认值，不宜过长
) returns nvarchar(max) as begin
    --参数不合法
    if (@name is null or len(@name)=0)
        return ''
    
    --找到这个设置
    declare @value nvarchar(Max)
    set @value=(select top 1 [Value] from [Gouring_Setting] where [Name]=@name)
    --没有值，设为默认值
    if (@value is null or len(@value)=0)
        set @value=@defaultValue
    --以免转换无效
    if (@value is null) 
        set @value=''
    return @value
end
GO
-- 获取系统设置（尝试转为int类型）。
create function [dbo].[getSettingAsInt](
    @name             nvarchar(255)               ,-- 设置名称
    @defaultValue     int             =0           -- 默认值
) returns int as begin
    --参数不合法
    if (len(@name)=0)
        return 0
    
    --找到这个设置
    declare @value nvarchar(Max)
    set @value=[dbo].[getSetting](@name,@defaultValue)
    if (@value='')
        begin
    	if(@defaultValue is null)
    	    set @value=0
    	else
    	    set @value=@defaultValue
        end
    return convert(int, @value)
end
GO
-- 获取系统设置（尝试转为bit类型）。
create function [dbo].[getSettingAsBit](
    @name             nvarchar(255)               ,-- 设置名称
    @defaultValue     bit             =0           -- 默认值
) returns int as begin
    --参数不合法
    if (len(@name)=0)
        return 0
    
    --找到这个设置
    declare @value nvarchar(Max)
    declare @result bit
    set @value=[dbo].[getSetting](@name,@defaultValue)
    if (@value='')
        begin
    	if(@defaultValue is null)
    	    set @result=0
    	else
    	    set @result=@defaultValue
        end
    else if(@value like 'true%')
        set @result=1
    else if(@value like 'yes%')
        set @result=1
    else if(@value ='1')
        set @result=1
    else if(@value ='-1')
        set @result=1
    else
        set @result=0
    return @result
end
GO
-- 获取用户昵称（Id)
create function [dbo].[getUserNameById](
    @id               int                          -- 用户Id
) returns nvarchar(16) as begin
    declare @result nvarchar(16)
    set @result=(select top 1 [Name] from [User] where [Id]=@id)
    return @result
end
GO
-- 获取消费类型名称（Id)
create function [dbo].[getConsumeTypeNameById](
    @id               int                          
) returns nvarchar(16) as begin
    declare @result nvarchar(16)
    set @result=(select top 1 [Name] from [ConsumeType] where [Id]=@id)
    return @result
end
GO
GO
-- 用户
create table [dbo].[User](
    [Id]                     int identity(1,1)        not null                        ,
    [CreateDate]             datetime                 not null default(getDate())     ,-- 创建日期
    [LastLoginDate]          datetime                 not null default(getDate())     ,-- 最后登录
    [Account]                nvarchar(64)             not null                        ,-- 帐号
    [Password]               char(32)                 not null                        ,-- 密码
    [Name]                   nvarchar(16)             not null                        ,-- 昵称
    constraint [PK_User] primary key clustered ([Id] asc) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
        on [PRIMARY] 
) on [PRIMARY]
GO
GO
create NONCLUSTERED index [IX_User_Account] on [dbo].[User](
    [Account] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
create NONCLUSTERED index [IX_User_Name] on [dbo].[User](
    [Name] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
GO
-- 消费选项
create table [dbo].[ConsumeType](
    [Id]                     int identity(1,1)        not null                        ,
    [UserId]                 int                      not null                        ,-- 用户Id
    [UserName]               as ([dbo].[getUserNameById]([UserId]))                   ,-- 用户昵称
    [Order]                  float                    not null default(0)             ,-- 顺序
    [IsOut]                  bit                      not null                        ,-- 是否为支出
    [Name]                   nvarchar(16)             not null                        ,-- 名称
    constraint [PK_ConsumeType] primary key clustered ([Id] asc) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
        on [PRIMARY] 
) on [PRIMARY]
GO
GO
create NONCLUSTERED index [IX_ConsumeType_UserId_Order_Id] on [dbo].[ConsumeType](
    [UserId] asc,[Order] asc,[Id] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
GO
-- 资金记录
create table [dbo].[FundsLog](
    [Id]                     bigint identity(1,1)     not null                        ,
    [UserId]                 int                      not null                        ,-- 用户Id
    [UserName]               as ([dbo].[getUserNameById]([UserId]))                   ,-- 用户昵称
    [ConsumeTypeId]          int                      not null                        ,-- 消费选项Id
    [ConsumeTypeName]        as ([dbo].[getConsumeTypeNameById]([ConsumeTypeId]))     ,-- 消费选项名称
    [ByDateDay]              int                      not null                        ,-- 消费日期(20150220)
    [CreateDateDay]          int                      not null default([dbo].[getDayNumber](getDate())),-- 创建日期(20150220)
    [ByDate]                 datetime                 not null                        ,-- 消费日期
    [CreateDate]             datetime                 not null default(getDate())     ,-- 创建日期
    [IsOut]                  bit                      not null                        ,-- 是否为支出
    [Money]                  decimal(18,2)            not null                        ,-- 金额
    [LastMoney]              decimal(18,2)            not null                        ,-- 最后余额（操作前）
    [RelatedPerson]          nvarchar(64)                 null                        ,-- 相关人
    [Comment]                ntext                        null                        ,-- 备注
    constraint [PK_FundsLog] primary key clustered ([Id] asc) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
        on [PRIMARY] 
) on [PRIMARY]
GO
GO
create NONCLUSTERED index [IX_FundsLog_UserId_ConsumeTypeId] on [dbo].[FundsLog](
    [UserId] asc,[ConsumeTypeId] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
create NONCLUSTERED index [IX_FundsLog_ConsumeTypeId] on [dbo].[FundsLog](
    [ConsumeTypeId] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
create NONCLUSTERED index [IX_FundsLog_ByDateDay] on [dbo].[FundsLog](
    [ByDateDay] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
GO
-- 资金消费统计（天）
create table [dbo].[FundsConsumeTypeDay](
    [Id]                     bigint identity(1,1)     not null                        ,
    [UserId]                 int                      not null                        ,-- 用户Id
    [UserName]               as ([dbo].[getUserNameById]([UserId]))                   ,-- 用户昵称
    [ConsumeTypeId]          int                      not null                        ,-- 消费选项Id
    [ConsumeTypeName]        as ([dbo].[getConsumeTypeNameById]([ConsumeTypeId]))     ,-- 消费选项名称
    [Day]                    int                      not null                        ,-- 日期(20150220)
    [IsOut]                  bit                      not null                        ,-- 是否为支出
    [Money]                  decimal(18,2)            not null                        ,-- 金额
    constraint [PK_FundsConsumeTypeDay] primary key clustered ([Id] asc) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
        on [PRIMARY] 
) on [PRIMARY]
GO
GO
create NONCLUSTERED index [IX_FundsConsumeTypeDay_UserId_ConsumeTypeId] on [dbo].[FundsConsumeTypeDay](
    [UserId] asc,[ConsumeTypeId] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
create NONCLUSTERED index [IX_FundsConsumeTypeDay_ConsumeTypeId] on [dbo].[FundsConsumeTypeDay](
    [ConsumeTypeId] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
create NONCLUSTERED index [IX_FundsConsumeTypeDay_Day] on [dbo].[FundsConsumeTypeDay](
    [Day] asc
 ) with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) on [PRIMARY]
GO
GO
-- 包装系统的rand函数
create view [dbo].[RandView] as 
    SELECT RAND() AS Expr1
GO
