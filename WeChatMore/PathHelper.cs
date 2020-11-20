using IWshRuntimeLibrary;
using System;
using System.IO;

namespace WeChatMore
{
    class PathHelper
    {
        public static string[] AutoPath;
        public static string FilePath;
        public static string WeChatPath;
        public static string DeskTopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string LnkPath = DeskTopPath + "\\微信.lnk";


        /// <summary>
        /// 获取快捷方式指向的路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string getLnkPath(string path)
        {
            //快捷方式的路径 = @"d:\Test.lnk";
            if (System.IO.File.Exists(path))
            {
                WshShell shell = new WshShell();
                IWshShortcut objectIW = (IWshShortcut)shell.CreateShortcut(path);
                //快捷方式指向的路径.Text = objectIW.TargetPath;
                //快捷方式指向的目标目录.Text = objectIW.WorkingDirectory;
                return objectIW.TargetPath;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="directory">快捷方式所处的文件夹</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="arguments">启动程序时的参数</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
        /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
        /// <remarks></remarks>
        public static void CreateShortcut(string directory, string shortcutName, string targetPath, string arguments = "", string description = null, string iconLocation = null)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Arguments = arguments;
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrEmpty(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }
    }
}
