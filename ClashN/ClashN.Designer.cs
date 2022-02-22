
namespace ClashN
{
    partial class ClashN
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClashN));
            this.menuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.llllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.llllllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.系统代理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置系统代理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除系统代理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulecbx = new System.Windows.Forms.ToolStripComboBox();
            this.控制面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sp1 = new System.Windows.Forms.ToolStripSeparator();
            this.订阅管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configChoose = new System.Windows.Forms.ToolStripComboBox();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.其他设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机自启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订阅转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.节点选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuRight.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuRight
            // 
            this.menuRight.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.llllToolStripMenuItem,
            this.llllllToolStripMenuItem});
            this.menuRight.Name = "contextMenuStrip2";
            this.menuRight.Size = new System.Drawing.Size(132, 48);
            // 
            // llllToolStripMenuItem
            // 
            this.llllToolStripMenuItem.Name = "llllToolStripMenuItem";
            this.llllToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.llllToolStripMenuItem.Text = "测试功能1";
            // 
            // llllllToolStripMenuItem
            // 
            this.llllllToolStripMenuItem.Name = "llllllToolStripMenuItem";
            this.llllllToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.llllllToolStripMenuItem.Text = "测试功能2";
            // 
            // trayIco
            // 
            this.trayIco.ContextMenuStrip = this.trayMenu;
            this.trayIco.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIco.Icon")));
            this.trayIco.Text = "clashN";
            this.trayIco.Visible = true;
            this.trayIco.Click += new System.EventHandler(this.trayIco_Click);
            this.trayIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayico_MouseDoubleClick);
            // 
            // trayMenu
            // 
            this.trayMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统代理ToolStripMenuItem,
            this.rulecbx,
            this.控制面板ToolStripMenuItem,
            this.sp1,
            this.订阅管理ToolStripMenuItem,
            this.configChoose,
            this.sp2,
            this.其他设置ToolStripMenuItem,
            this.trayExit,
            this.toolStripSeparator1,
            this.节点选择ToolStripMenuItem});
            this.trayMenu.Name = "contextMenuStrip1";
            this.trayMenu.Size = new System.Drawing.Size(182, 212);
            this.trayMenu.Text = "trayIcoMenu";
            // 
            // 系统代理ToolStripMenuItem
            // 
            this.系统代理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置系统代理ToolStripMenuItem,
            this.清除系统代理ToolStripMenuItem});
            this.系统代理ToolStripMenuItem.Name = "系统代理ToolStripMenuItem";
            this.系统代理ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.系统代理ToolStripMenuItem.Text = "系统代理";
            // 
            // 设置系统代理ToolStripMenuItem
            // 
            this.设置系统代理ToolStripMenuItem.Name = "设置系统代理ToolStripMenuItem";
            this.设置系统代理ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.设置系统代理ToolStripMenuItem.Text = "设置系统代理";
            this.设置系统代理ToolStripMenuItem.Click += new System.EventHandler(this.设置系统代理ToolStripMenuItem_Click);
            // 
            // 清除系统代理ToolStripMenuItem
            // 
            this.清除系统代理ToolStripMenuItem.Checked = true;
            this.清除系统代理ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.清除系统代理ToolStripMenuItem.Name = "清除系统代理ToolStripMenuItem";
            this.清除系统代理ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除系统代理ToolStripMenuItem.Text = "清除系统代理";
            this.清除系统代理ToolStripMenuItem.Click += new System.EventHandler(this.清除系统代理ToolStripMenuItem_Click);
            // 
            // rulecbx
            // 
            this.rulecbx.Items.AddRange(new object[] {
            "全局",
            "规则",
            "直连"});
            this.rulecbx.Name = "rulecbx";
            this.rulecbx.Size = new System.Drawing.Size(121, 25);
            this.rulecbx.SelectedIndexChanged += new System.EventHandler(this.rulecbx_SelectedIndexChanged);
            // 
            // 控制面板ToolStripMenuItem
            // 
            this.控制面板ToolStripMenuItem.Name = "控制面板ToolStripMenuItem";
            this.控制面板ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.控制面板ToolStripMenuItem.Text = "控制面板";
            this.控制面板ToolStripMenuItem.Click += new System.EventHandler(this.控制面板ToolStripMenuItem_Click);
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(178, 6);
            // 
            // 订阅管理ToolStripMenuItem
            // 
            this.订阅管理ToolStripMenuItem.Name = "订阅管理ToolStripMenuItem";
            this.订阅管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.订阅管理ToolStripMenuItem.Text = "订阅管理";
            this.订阅管理ToolStripMenuItem.Click += new System.EventHandler(this.订阅管理ToolStripMenuItem_Click);
            // 
            // configChoose
            // 
            this.configChoose.Name = "configChoose";
            this.configChoose.Size = new System.Drawing.Size(121, 25);
            this.configChoose.SelectedIndexChanged += new System.EventHandler(this.configChoose_SelectedIndexChanged);
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(178, 6);
            // 
            // 其他设置ToolStripMenuItem
            // 
            this.其他设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开机自启ToolStripMenuItem,
            this.订阅转换ToolStripMenuItem});
            this.其他设置ToolStripMenuItem.Name = "其他设置ToolStripMenuItem";
            this.其他设置ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.其他设置ToolStripMenuItem.Text = "其他设置";
            // 
            // 开机自启ToolStripMenuItem
            // 
            this.开机自启ToolStripMenuItem.Checked = true;
            this.开机自启ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.开机自启ToolStripMenuItem.Name = "开机自启ToolStripMenuItem";
            this.开机自启ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.开机自启ToolStripMenuItem.Text = "开机自启";
            this.开机自启ToolStripMenuItem.Click += new System.EventHandler(this.开机自启ToolStripMenuItem_Click);
            // 
            // 订阅转换ToolStripMenuItem
            // 
            this.订阅转换ToolStripMenuItem.Name = "订阅转换ToolStripMenuItem";
            this.订阅转换ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.订阅转换ToolStripMenuItem.Text = "订阅转换";
            this.订阅转换ToolStripMenuItem.Click += new System.EventHandler(this.订阅转换ToolStripMenuItem_Click);
            // 
            // trayExit
            // 
            this.trayExit.Name = "trayExit";
            this.trayExit.Size = new System.Drawing.Size(181, 22);
            this.trayExit.Text = "退出";
            this.trayExit.Click += new System.EventHandler(this.trayExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // 节点选择ToolStripMenuItem
            // 
            this.节点选择ToolStripMenuItem.Name = "节点选择ToolStripMenuItem";
            this.节点选择ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.节点选择ToolStripMenuItem.Text = "节点选择";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(375, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ClashN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 251);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClashN";
            this.Text = "ClashN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClashN_FormClosing);
            this.Load += new System.EventHandler(this.ClashN_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClashN_MouseClick);
            this.menuRight.ResumeLayout(false);
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip menuRight;
        private System.Windows.Forms.ToolStripMenuItem llllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem llllllToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon trayIco;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem trayExit;
        private System.Windows.Forms.ToolStripMenuItem 订阅管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统代理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置系统代理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 控制面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除系统代理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.ToolStripMenuItem 其他设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机自启ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订阅转换ToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox configChoose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox rulecbx;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 节点选择ToolStripMenuItem;
    }
}

