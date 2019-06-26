# 小型财务管理(单机版)
> 原始需求是写给自己用的，打算记录一下新的一年，所有的收支情况，统计一下自己什么时候花了多少，好做后期的计划。结果写成完整的单机版了，有兴趣的可以拿去用用吧。

# 版本记录
#### 2019-06-24 v2.4.0.25 开源版
* 采用nuget版本的Symbol、Symbol.Data、Symbol.Data.SQLite；
* 精简程序代码；
* 移植到github，欢迎有兴趣的朋友pull；

#### 2017-03-03 v1.3.0.22    [立即下载](/../../raw/master/.files/versions/FundsManager.Standalone-v1.3.0.22-20170303.zip)
* 优化主界面金额显示、日期显示；
* 日期支持精确到时分；
* 允许修改日期、金额，并且自动更正统计结果；
* 主界面随意选中一条记录，按下Ctrl+C，快速复制此记录；
* 优化导出结果；
* 添加/修改界面，增加数据验证；

#### 2017-02-27 v1.3.0.18
* 增加导出查询为csv文件的功能；

#### 2016-08-03 v1.2.0.16
* 增加程序图标，更容易找到；
* 去掉登录界面 默认开启自动登录的问题；
* “注册帐号” 修改为“新用户”，本软件不会访问任何外网； 

#### 2015-04-07 v1.1.0.11    [立即下载](/../../raw/master/.files/versions/FundsManager.Standalone-v1.1.0.11-20150407-1624.7z)
* 新增统计功能“对比统计”，可以对两个时间段内的收支情况进行对比；
默认自动为 本月和上月 的时间段；
* 修正所有统计界面最小化时出现错误的问题；
* 主界面增加一个链接“最新版本”，点击链接到网站的发布页面，方便随时了解新版本的情况；

![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-012.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-013.png)

#### 2015-03-18 v1.1.0.10
* 改进统计图上的“收入”、“支出”，在后面显示当前统计图的总值；

![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-011.png)


#### 2015-03-18 v1.1.0.9
* 优化统计界面的文字颜色，主界面底部汇总的颜色；
* 优化添加/编辑记录的界面，允许在备注中输入换行符；
* 菜单栏加上热键字母，外加“工具”菜单，快速启动“计算器”、“记事本”； 

#### 2015-02-25 v1.1.0.8
* 首次使用需要注册帐号，注册过程很简单，不会连接网络，纯单机版；
* 消费方式需要自己手工添加，也可以在添加财务记录的时候临时添加；
* 财务记录添加后，只能修改相关人和备注，如果发现添加错误，请删除并重新添加；
* 统计功能：按月统计（某年中12个月的收支情况）、各类消费（在指定的日期范围内，各种消费方式收支情况）；
* 设置-界面字体，可以调整界面的字体大小，注意字号越大，界面会自己变得很大，根据自己眼睛的情况来设置；
* 主界面的查询可按日期范围、消费方式、关键词（相关人、备注）进行查询；
* 主界面列表每页显示25条记录，可以翻页；
* 主界面底部的总收入、总支出、总金额是独立的，和查询结果无关；
* 有列表的界面，一般按F5可以直接刷新列表，按F2或Enter进行编辑，按Delete进行删除；
* 统计界面会根据窗口大小自动缩放，如果发现显示的统计图很小，可以适当的调整窗口大小；

![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-001.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-002.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-003.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-004.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-005.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-006.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-007.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-008.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-009.png)
![image](/../../raw/master/.files/screenshots/FundsManager.Standalone-010.png)
