using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WeChatMore
{
    public partial class FmMain : Form
    {
        public FmMain()
        {
            InitializeComponent();
        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            int argLeng = PathHelper.AutoPath.Length;
            if (argLeng != 0)
            {
                this.Visible = false;
                for (int i = 0; i < argLeng; i++)
                {
                    PathHelper.FilePath += PathHelper.AutoPath[i] + " ";
                }
                PathHelper.FilePath = PathHelper.FilePath.Trim();
                runWeChat(PathHelper.FilePath);
                this.Close();
                this.Dispose();
                Environment.Exit(Environment.ExitCode);
            }

            // 读取注册表微信安装目录
            regHelper.GetSoftWare("微信", out PathHelper.WeChatPath);
            PathHelper.WeChatPath = PathHelper.WeChatPath + "\\WeChat.exe";

            // 判断是否已注册右键菜单
            if (regHelper.IsRegeditItemExist("WeChatMore"))
            {
                this.cbRegedit.Checked = true;
            }

            // 判断是否已创建快捷方式
            if (File.Exists(PathHelper.LnkPath))
            {
                this.cbShortcut.Checked = true;
            }
        }


        private void FmMain_DoubleClick(object sender, EventArgs e)
        {
            runWeChat(PathHelper.WeChatPath);
        }

        #region 启动微信函数
        private void runWeChat(string path)
        {
            ClearMemory();
            string name = Path.GetFileName(path);
            if (name.Equals("WeChat.exe"))
            {
                Process[] localByName = Process.GetProcessesByName("WeChat");
                foreach (Process pro in localByName)
                {
                    // WeChat_App_Instance_Identity_Mutex_Name
                    checkProcessAndClose(pro, "WeChat_App_Instance_Identity_Mutex_Name");
                }
                Process.Start(path);
                ClearMemory();
            }
        }
        #endregion

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary> 
        /// 释放内存
        /// </summary> 
        public void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        #region 查找句柄并关闭
        private void checkProcessAndClose(Process pro, string name)
        {
            ushort handle = RefreshHandles(pro, name);
            if (handle != 0)
            {
                HandleModle.CloseProcessHandle(pro.Id, handle);
            }
        }
        #endregion

        #region 枚举句柄
        private ushort RefreshHandles(Process pro, string check = "")
        {
            List<Win32API.SYSTEM_HANDLE_INFORMATION> lws = HandleModle.GetHandles(pro);
            foreach (Win32API.SYSTEM_HANDLE_INFORMATION lw in lws)
            {
                string str_handle_name = HandleModle.GetFilePath(lw, pro);
                if (str_handle_name == "")
                {
                    continue;
                }
                if (str_handle_name == null)
                {
                    continue;
                }
                if (str_handle_name.Contains(check))
                {
                    return lw.Handle;
                }
            }
            return 0;
        }
        #endregion

        #region 注册右键菜单
        private void cbRegedit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRegedit.Checked)
            {
                regHelper.AddMenuItem("WeChatMore", Application.ExecutablePath);
            }
            else
            {
                regHelper.DelMenuItem("WeChatMore");
            }
        }
        #endregion

        #region 创建微信快捷方式
        private void cbShortcut_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShortcut.Checked)
            {   
                if (!File.Exists(PathHelper.LnkPath))
                {
                    PathHelper.CreateShortcut(PathHelper.DeskTopPath, "微信", Assembly.GetEntryAssembly().Location, PathHelper.WeChatPath, "", PathHelper.WeChatPath);
                }
            }
            else
            {
                if (File.Exists(PathHelper.LnkPath))
                {
                    File.Delete(PathHelper.LnkPath);
                }
            }
        }
        #endregion

    }
}
