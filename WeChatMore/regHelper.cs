using Microsoft.Win32;
using System.Collections.Generic;

namespace WeChatMore
{
    class regHelper
    {
        public static void AddMenuItem(string itemName, string associatedProgramFullPath)
        {
            //创建项：exefile 
            RegistryKey shellKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\exefile", true);
            if (shellKey == null)
            {
                shellKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\exefile");
                shellKey.Close();
            }

            //创建项：shell 
            shellKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\exefile\shell", true);
            if (shellKey == null)
            {
                shellKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\exefile\shell");
            }

            //创建项：右键显示的菜单名称
            RegistryKey rightCommondKey = shellKey.CreateSubKey(itemName);
            rightCommondKey.SetValue("", "微信多开");
            rightCommondKey.SetValue("Icon", associatedProgramFullPath + ",0");

            //创建默认值：关联的程序
            RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");
            associatedProgramKey.SetValue("", associatedProgramFullPath + " %1");

            //刷新到磁盘并释放资源
            associatedProgramKey.Close();
            rightCommondKey.Close();
            shellKey.Close();
        }

        public static void DelMenuItem(string itemName)
        {
            if (IsRegeditItemExist(itemName))
            {
                RegistryKey delKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\exefile\shell", true);
                //delKey.DeleteSubKey(itemName, true);
                delKey.DeleteSubKeyTree(itemName, true); 
                delKey.Close();
            }
        }

        public static bool IsRegeditItemExist(string itemName)
        {
            RegistryKey hkml = Registry.CurrentUser;
            RegistryKey software = hkml.OpenSubKey(@"Software\Classes\exefile\shell");
            if (software != null)
            {
                string[] subkeyNames = software.GetSubKeyNames();
                //取得该项下所有子项的名称的序列，并传递给预定的数组中
                foreach (string keyName in subkeyNames)  //遍历整个数组
                {
                    if (keyName == itemName) //判断子项的名称
                    {
                        hkml.Close();
                        return true;
                    }
                }
            }
            hkml.Close();
            return false;
        }

        /// <summary>
        /// 软件是否安装
        /// </summary>
        /// <param name="SoftWareName"> 软件名称</param>
        /// <param name="SoftWarePath "> 安装路径</param>
        /// <returns> true or false </returns>
        public static bool GetSoftWare(string SoftWareName, out string SoftWarePath)
        {
            SoftWarePath = null;
            List<RegistryKey> RegistryKeys = new List<RegistryKey>();
            RegistryKeys.Add(Registry.ClassesRoot);
            RegistryKeys.Add(Registry.CurrentConfig);
            RegistryKeys.Add(Registry.CurrentUser);
            RegistryKeys.Add(Registry.LocalMachine);
            RegistryKeys.Add(Registry.PerformanceData);
            RegistryKeys.Add(Registry.Users);
            Dictionary<string, string> Softwares = new Dictionary<string, string>();
            string SubKeyName = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            foreach (RegistryKey Registrykey in RegistryKeys)
            {
                using (RegistryKey RegistryKey1 = Registrykey.OpenSubKey(SubKeyName, false))
                {
                    if (RegistryKey1 == null) // 判断对象不存在
                        continue;
                    if (RegistryKey1.GetSubKeyNames() == null)
                        continue;
                    string[] KeyNames = RegistryKey1.GetSubKeyNames();
                    foreach (string KeyName in KeyNames)// 遍历子项名称的字符串数组
                    {
                        using (RegistryKey RegistryKey2 = RegistryKey1.OpenSubKey(KeyName, false)) // 遍历子项节点
                        {
                            if (RegistryKey2 == null)
                                continue;
                            string SoftwareName = RegistryKey2.GetValue("DisplayName", "").ToString(); // 获取软件名
                            string InstallLocation = RegistryKey2.GetValue("InstallLocation", "").ToString(); // 获取安装路径
                            if (!string.IsNullOrEmpty(InstallLocation) && !string.IsNullOrEmpty(SoftwareName))
                            {
                                if (!Softwares.ContainsKey(SoftwareName))
                                    Softwares.Add(SoftwareName, InstallLocation);
                            }
                        }
                    }
                }
            }
            if (Softwares.Count <= 0)
                return false;
            foreach (string SoftwareName in Softwares.Keys)
            {
                if (SoftwareName.Contains(SoftWareName))
                {
                    SoftWarePath = Softwares[SoftwareName];
                    return true;
                }
            }
            return false;
        }

    }
}