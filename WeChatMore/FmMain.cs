using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Water.Open2;

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
                    PathHelper.FilePath += PathHelper.AutoPath[i];
                }
                runWeChat(PathHelper.FilePath);
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }

            if (regHelper.IsRegeditItemExist("WeChatMore"))
            {
                this.checkBox1.Checked = true;
            }
        }

        #region 文件拖动启动
        private void FmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FmMain_DragDrop(object sender, DragEventArgs e)
        {
            PathHelper.FilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            runWeChat(PathHelper.FilePath);
        }
        #endregion

        #region 启动微信函数
        private void runWeChat(string path)
        {
            ClearMemory();
            if (path.ToLower().Contains(".lnk"))
            {
                path = PathHelper.getLnkPath(path);
            }
            string name = System.IO.Path.GetFileName(path);
            if (name.Equals("WeChat.exe"))
            {
                Process[] localByName = Process.GetProcessesByName("WeChat");
                foreach (Process pro in localByName)
                {
                    //WeChat_App_Instance_Identity_Mutex_Name
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                regHelper.AddMenuItem("WeChatMore", Application.ExecutablePath);
            }
            else
            {
                regHelper.DelMenuItem("WeChatMore");
            }
        }
    }
}
