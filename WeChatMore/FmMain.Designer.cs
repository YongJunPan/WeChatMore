namespace WeChatMore
{
    partial class FmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.cbRegedit = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShortcut = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbRegedit
            // 
            this.cbRegedit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRegedit.AutoSize = true;
            this.cbRegedit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRegedit.Location = new System.Drawing.Point(225, 253);
            this.cbRegedit.Name = "cbRegedit";
            this.cbRegedit.Size = new System.Drawing.Size(125, 25);
            this.cbRegedit.TabIndex = 1;
            this.cbRegedit.Text = "注册右键菜单";
            this.cbRegedit.UseVisualStyleBackColor = true;
            this.cbRegedit.CheckedChanged += new System.EventHandler(this.cbRegedit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(80, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 54);
            this.label1.TabIndex = 2;
            this.label1.Text = "拖拽文件或快捷方式到此\r\n或双击界面任意位置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FmMain_DragDrop);
            this.label1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FmMain_DragEnter);
            this.label1.DoubleClick += new System.EventHandler(this.FmMain_DoubleClick);
            // 
            // cbShortcut
            // 
            this.cbShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShortcut.AutoSize = true;
            this.cbShortcut.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbShortcut.Location = new System.Drawing.Point(225, 284);
            this.cbShortcut.Name = "cbShortcut";
            this.cbShortcut.Size = new System.Drawing.Size(157, 25);
            this.cbShortcut.TabIndex = 1;
            this.cbShortcut.Text = "创建微信快捷方式";
            this.cbShortcut.UseVisualStyleBackColor = true;
            this.cbShortcut.CheckedChanged += new System.EventHandler(this.cbShortcut_CheckedChanged);
            // 
            // FmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShortcut);
            this.Controls.Add(this.cbRegedit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WeChatMore";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FmMain_DragEnter);
            this.DoubleClick += new System.EventHandler(this.FmMain_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbRegedit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbShortcut;
    }
}

