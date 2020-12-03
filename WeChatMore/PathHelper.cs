using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Text;

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


        /// <summary>
        /// 将文本写入txt文件中
        /// </summary>
        /// <param name="DirPath">文件路径</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="Strs">字符串</param>
        /// <param name="IsCleanFile">是否先清空文件</param>
        // public static void WriteTxtToFile(string DirPath, string FileName, string Strs, bool IsCleanFile = false)
        // 测试用
        public static void WriteTxtToFile(string Strs, bool IsCleanFile = false)
        {
            string DirPath = DeskTopPath;
            string FileName = "\\test.txt";
            if (string.IsNullOrEmpty(Strs))
                return;
            if (!Directory.Exists(DirPath))  //如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(DirPath);
            }
            if (!System.IO.File.Exists(DirPath + FileName))
            {
                System.IO.File.Create(DirPath + FileName);
            }

            FileStream fs = null;
            //将待写的入数据从字符串转换为字节数组
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes(Strs + "\r\n");
            try
            {
                fs = System.IO.File.OpenWrite(DirPath + FileName);
                //设定书写的开始位置为文件的末尾
                fs.Position = fs.Length;
                //将待写入内容追加到文件末尾
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("文件打开失败{0}", ex.ToString());
                throw ex;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
