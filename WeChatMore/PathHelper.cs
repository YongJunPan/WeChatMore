using System;
using IWshRuntimeLibrary;

namespace WeChatMore
{
    class PathHelper
    {
        public static string[] AutoPath;
        public static string FilePath;

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
    }
}
