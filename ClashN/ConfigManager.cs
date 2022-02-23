using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashN
{
    public partial class ConfigManager : Form
    {
        
        public ConfigManager()
        {
            
            InitializeComponent();
        }
        public ConfigManager(ClashN clash)
        {
            this.clashN = clash;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //方便新线访问空间
        }
        ClashN clashN;
       
        Utils utils = new Utils();        
        
        string profilesDir = Application.StartupPath + @"/profiles/"; //配置文件文件夹路径
        string yamlConfigPath = Application.StartupPath + @"/config.yaml";

        //LIstview的显示设置
        private void ListShow(List<System.IO.FileInfo> list)
        {
            
               // Utils utils = new Utils();
                listViewConfigFile.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] item = new string[4];
                    item[0] = list[i].Name;
                    item[1] = list[i].LastWriteTime.ToString();
                    YML yml = new YML(list[i].FullName, true);
                    item[2] = yml.read("clash-sub-url");//订阅链接                                                        
                    item[3] = " ";
                    listViewConfigFile.Items.Add(new ListViewItem(item));
                }

        }
        //重载ListViwe
        public void ReloadListView()
        {

            List<FileInfo> files = utils.GetFiles(profilesDir , ".yaml"); 
            ListShow(files);

            string lastyaml = new YML(yamlConfigPath ).read("last-yaml");
            for(int i=0; i < listViewConfigFile.Items.Count; i++)
            {
                if (lastyaml.Equals(listViewConfigFile.Items[i].SubItems[0].Text))
                {
                    listViewConfigFile.Items[i].SubItems[3].Text = "🐱";
                    
                }
            }


        }
        private void ConfigManager_Load(object sender, EventArgs e)
        {
            ReloadListView();
        }



        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                configMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void 手动添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.yaml)|*.yaml";
            string file = string.Empty;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 file = dialog.FileName;
            }
            if (file != null)
            {
                utils.CopyToFile(file, profilesDir );
                MessageBox.Show("添加成功");
            }
            ReloadListView();
            // clashN.ReloadAll();
            clashN.ReloadCombobox();

        }

        //获取免费订阅
        private void btnGetFree_Click(object sender, EventArgs e)
        {

            backgroundWorkerGetFree.RunWorkerAsync();

        }

        private void 添加订阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new AddSub().Show();
        }
        //根据配置启动
        private void 启用配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listViewConfigFile.SelectedItems.Count > 0)
            {
                RestfulGo restfulGo = new RestfulGo();
                string fileName = listViewConfigFile.SelectedItems[0].SubItems[0].Text;
                string path = profilesDir  + fileName;
                string jsonReloadData = JsonConvert.SerializeObject(new
                {
                    path = path //配置文件路径

                });
                //string message = restfulGo.WebPut("http://127.0.0.1:9090/configs?force=false", jsonReloadData);//切换配置文件
                // string yamlPath = Application.StartupPath + @"/config.yaml";
                //YML yml = new YML(yamlPath);
                //yml.modify("last-yaml", fileName);
                //yml.modify("last-yamlIndex", listView1.SelectedItems[0].Index.ToString());
                //yml.save();
                //new ClashN().configChoose
                for (int i = 0; i < listViewConfigFile.Items.Count; i++)
                {
                    listViewConfigFile.Items[i].SubItems[3].Text = " ";
                }
                listViewConfigFile.SelectedItems[0].SubItems[3].Text = "🐱";
                //再次调用时 会导致读取两次文件yaml，会造成冲突，一次是listview，一次是combobox，取消掉listview的改变就可
                // clashN.ReloadAll();
                //clashN.ReloadAll();
                clashN.ReloadCombobox();
               clashN.configChoose.SelectedIndex = listViewConfigFile.SelectedItems[0].Index;

            }




        }

        private void 更新配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfigFile.SelectedItems.Count > 0)
            {
                string path = profilesDir  + listViewConfigFile.SelectedItems[0].SubItems[0].Text;
                string url = listViewConfigFile.SelectedItems[0].SubItems[2].Text;
                if (url != "")
                {
                    utils.DownloadFile(listViewConfigFile.SelectedItems[0].SubItems[2].Text, path);
                    //将如下保存到每个订阅的配置文件中 
                    //ClashNurl ： url
                    string str = "clash-sub-url: " + url;
                    utils.WriteFirstLine(path, str);
                }
            }
            
        }

        private void btnOneClickUpdate_Click(object sender, EventArgs e)
        {
            backgroundWorkerOneClickUpdate.RunWorkerAsync();

        }

        //打开文件夹
        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            string v_OpenFolderPath = profilesDir ;
            System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
        }

        private void 删除配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfigFile.SelectedItems.Count > 0)
            {
                string filepath = profilesDir  + listViewConfigFile.SelectedItems[0].SubItems[0].Text;
                // ...or by using FileInfo instance method.
                System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
                try
                {
                    fi.Delete();
                    listViewConfigFile.SelectedItems[0].Remove(); //删除一行

                    if (listViewConfigFile.Items.Count  == 0)
                    {   
                        YML yml = new YML(yamlConfigPath);
                        yml.modify("last-yaml", "config.yaml");
                        yml.save();
                    }

                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                

            }

            //clashN.ReloadAll();
            clashN.ReloadCombobox();
           // clashN.configChoose.SelectedIndex = listViewConfigFile.SelectedItems[0].Index;
        }


        private void 更新免费订阅源文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YML yml = new YML(yamlConfigPath );
            string url = yml.read("configIni");

            utils.DownloadFile(url, Application.StartupPath + @"/config.ini");
        }
        //获取免费订阅后台
        private void backgroundWorkerGetFree_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("免费订阅源来自五湖四海，下载成功率与自身的网络环境，资源本身都有关系");
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/vveg26/SelfConfig");

            string filepath = Application.StartupPath + @"/config.ini"; //配置文件

            Dictionary<string, string> map = new Dictionary<string, string>();
            map = utils.IniReadMap("Clash", filepath);

            if (map.Count > 0)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    string fileName = "free"+i+ @".yaml";
                    string downloadSavePath = profilesDir + fileName ;
                    var item = map.ElementAt(i);
                    utils.DownloadFile(item.Value, downloadSavePath);

                }
            }
            ReloadListView ();
            clashN.ReloadCombobox();
            
           // clashN.configChoose.SelectedIndex = listViewConfigFile.SelectedItems[0].Index;
        }
        //后台一键更新
        public void backgroundWorkerOneClickUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < listViewConfigFile.Items.Count; i++)
            {
                string path = profilesDir  + listViewConfigFile.Items[i].SubItems[0].Text;
                string url = listViewConfigFile.Items[i].SubItems[2].Text;
                if (url != "")
                {
                    utils.DownloadFile(listViewConfigFile.Items[i].SubItems[2].Text, path);
                    //将如下保存到每个订阅的配置文件的第一行中 
                    //ClashNurl ： url
                    string str = "clash-sub-url: " + url;
                    utils.WriteFirstLine(path, str);
                }
            }

            ReloadListView();
        }


    }



}
