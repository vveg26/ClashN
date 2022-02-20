namespace ClashN
{
    partial class ConfigManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.启用配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.添加配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加订阅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOneClickUpdate = new System.Windows.Forms.Button();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.btnGetFree = new System.Windows.Forms.Button();
            this.configMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.ContextMenuStrip = this.configMenu;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(23, 30);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(935, 195);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "订阅名称";
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "更新时间";
            this.columnHeader2.Width = 198;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "订阅地址";
            this.columnHeader3.Width = 340;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "-";
            this.columnHeader4.Width = 26;
            // 
            // configMenu
            // 
            this.configMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.configMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启用配置ToolStripMenuItem,
            this.更新配置ToolStripMenuItem,
            this.删除配置文件ToolStripMenuItem,
            this.toolStripSeparator1,
            this.添加配置ToolStripMenuItem});
            this.configMenu.Name = "configMenu";
            this.configMenu.Size = new System.Drawing.Size(211, 134);
            // 
            // 启用配置ToolStripMenuItem
            // 
            this.启用配置ToolStripMenuItem.Name = "启用配置ToolStripMenuItem";
            this.启用配置ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.启用配置ToolStripMenuItem.Text = "启用配置";
            this.启用配置ToolStripMenuItem.Click += new System.EventHandler(this.启用配置ToolStripMenuItem_Click);
            // 
            // 更新配置ToolStripMenuItem
            // 
            this.更新配置ToolStripMenuItem.Name = "更新配置ToolStripMenuItem";
            this.更新配置ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.更新配置ToolStripMenuItem.Text = "更新配置";
            this.更新配置ToolStripMenuItem.Click += new System.EventHandler(this.更新配置ToolStripMenuItem_Click);
            // 
            // 删除配置文件ToolStripMenuItem
            // 
            this.删除配置文件ToolStripMenuItem.Name = "删除配置文件ToolStripMenuItem";
            this.删除配置文件ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.删除配置文件ToolStripMenuItem.Text = "删除配置";
            this.删除配置文件ToolStripMenuItem.Click += new System.EventHandler(this.删除配置文件ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // 添加配置ToolStripMenuItem
            // 
            this.添加配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手动添加ToolStripMenuItem,
            this.添加订阅ToolStripMenuItem});
            this.添加配置ToolStripMenuItem.Name = "添加配置ToolStripMenuItem";
            this.添加配置ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.添加配置ToolStripMenuItem.Text = "添加配置";
            // 
            // 手动添加ToolStripMenuItem
            // 
            this.手动添加ToolStripMenuItem.Name = "手动添加ToolStripMenuItem";
            this.手动添加ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.手动添加ToolStripMenuItem.Text = "添加配置文件";
            this.手动添加ToolStripMenuItem.Click += new System.EventHandler(this.手动添加ToolStripMenuItem_Click);
            // 
            // 添加订阅ToolStripMenuItem
            // 
            this.添加订阅ToolStripMenuItem.Name = "添加订阅ToolStripMenuItem";
            this.添加订阅ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.添加订阅ToolStripMenuItem.Text = "添加订阅";
            this.添加订阅ToolStripMenuItem.Click += new System.EventHandler(this.添加订阅ToolStripMenuItem_Click);
            // 
            // btnOneClickUpdate
            // 
            this.btnOneClickUpdate.Location = new System.Drawing.Point(23, 244);
            this.btnOneClickUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOneClickUpdate.Name = "btnOneClickUpdate";
            this.btnOneClickUpdate.Size = new System.Drawing.Size(152, 76);
            this.btnOneClickUpdate.TabIndex = 1;
            this.btnOneClickUpdate.Text = "一键更新";
            this.btnOneClickUpdate.UseVisualStyleBackColor = true;
            this.btnOneClickUpdate.Click += new System.EventHandler(this.btnOneClickUpdate_Click);
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Location = new System.Drawing.Point(391, 244);
            this.btnOpenDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(172, 76);
            this.btnOpenDir.TabIndex = 4;
            this.btnOpenDir.Text = "打开目录";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // btnGetFree
            // 
            this.btnGetFree.Location = new System.Drawing.Point(778, 244);
            this.btnGetFree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetFree.Name = "btnGetFree";
            this.btnGetFree.Size = new System.Drawing.Size(180, 76);
            this.btnGetFree.TabIndex = 7;
            this.btnGetFree.Text = "获取免费订阅";
            this.btnGetFree.UseVisualStyleBackColor = true;
            this.btnGetFree.Click += new System.EventHandler(this.btnGetFree_Click);
            // 
            // ConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 350);
            this.Controls.Add(this.btnGetFree);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.btnOneClickUpdate);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ConfigManager";
            this.Text = "配置文件管理";
            this.Load += new System.EventHandler(this.ConfigManager_Load);
            this.configMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnOneClickUpdate;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnGetFree;
        private System.Windows.Forms.ContextMenuStrip configMenu;
        private System.Windows.Forms.ToolStripMenuItem 启用配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除配置文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 添加配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加订阅ToolStripMenuItem;
    }
}