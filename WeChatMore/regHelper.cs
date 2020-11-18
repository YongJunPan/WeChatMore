using Microsoft.Win32;

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
    }
}