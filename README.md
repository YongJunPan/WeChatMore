# 煎鱼微信多开

1. 多种多开方式：

   1）右键菜单

   2）创建微信快捷方式

   3）双击界面任意位置

2. 右键菜单：写注册表  `HKEY_CURRENT_USER\Software\Classes\exefile\shell\WeChatMore`

3. 读取微信安装目录：`HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\WeChat`



## 版本更新：

**3.0**

1. 修复：win7用不了问题（.NET Framework版本改为3.5）
2. 去除：拖放启动

**v2.0**

1. 新增：读取注册表获取微信安装目录
2. 新增：创建微信快捷方式，启动快捷方式多开
3. 新增：双击界面任意位置多开

**v1.0**

1. 初始版本



