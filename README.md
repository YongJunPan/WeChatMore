# 煎鱼微信多开

1. 多种多开方式：

   1）拖动文件或快捷方式到窗体

   2）右键菜单

   3）创建微信快捷方式

   4）双击界面任意位置

2. 右键菜单：写注册表  `HKEY_CURRENT_USER\Software\Classes\exefile\shell\WeChatMore`

3. 读取微信安装目录：`HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\WeChat`



## 版本更新：

**v2.0**

1. 新增：读取注册表获取微信安装目录
2. 新增：创建微信快捷方式，启动快捷方式多开
3. 新增：双击界面任意位置多开

**v1.0**

1. 初始版本



