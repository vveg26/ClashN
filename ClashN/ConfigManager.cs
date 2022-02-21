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
        Form clashN = new Form();
        public ConfigManager()
        {
            
            InitializeComponent();
        }

        public ConfigManager(ToolStripComboBox tscb)
        {
            this.tscb = tscb;
            InitializeComponent();
        }

        ToolStripComboBox tscb = new ToolStripComboBox();
        Utils utils = new Utils();
        List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();
        string path = @"./profiles";
        /// <summary>
        /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
        /// <returns>List<FileInfo></returns>
        public static List<System.IO.FileInfo> GetFiles(string path, string ExtName, ref List<System.IO.FileInfo> lst)
        {

            try
            {
                //List<FileInfo> lst = new List<FileInfo>();
                string[] dir = System.IO.Directory.GetDirectories(path);// 文件夹列表
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
                System.IO.FileInfo[] files = directoryInfo.GetFiles();
                if (files.Length != 0 || dir.Length != 0) // 当前目录文件或文件夹不能为空
                {
                    foreach (System.IO.FileInfo f in files)
                    {
                        if (ExtName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }
                    foreach (string d in dir)
                    {
                        GetFiles(d, ExtName, ref lst);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //LIstview的显示设置
        public void ListShow(List<System.IO.FileInfo> list)
        {

                Utils utils = new Utils();
                listView1.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] item = new string[4];
                    item[0] = list[i].Name;
                    item[1] = list[i].LastWriteTime.ToString();
                    YML yml = new YML(list[i].FullName, true);
                    item[2] = yml.read("clash-sub-url");//订阅链接
                                                        //item[2] = utils.ReadYamlValue("clash-sub-url", list[i].FullName);
                    item[3] = " ";
                    listView1.Items.Add(new ListViewItem(item));
                }




        }
        public void ReloadListView()
        {
            GetFiles(path, ".yaml", ref files);
            ListShow(files);
            string lastyaml = new YML(Application.StartupPath+@"/config.yaml").read("last-yaml");
            for(int i=0; i < listView1.Items.Count; i++)
            {
                if (lastyaml.Equals(listView1.Items[i].SubItems[0].Text))
                {
                    listView1.Items[i].SubItems[3].Text = "🐱";
                    
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
                utils.CopyToFile(file, Application.StartupPath + @"/profiles");
                MessageBox.Show("添加成功");
            }
            

        }

        private void btnGetFree_Click(object sender, EventArgs e)
        {

            backgroundWorker1.RunWorkerAsync();

        }

        private void 添加订阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new AddSub().Show();
        }
        //根据配置启动
        private void 启用配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                RestfulGo restfulGo = new RestfulGo();
                string fileName = listView1.SelectedItems[0].SubItems[0].Text;
                string path = Application.StartupPath + @"/profiles/" + fileName;
                string jsonReloadData = JsonConvert.SerializeObject(new
                {
                    path = path //配置文件路径

                });
                string message = restfulGo.WebPut("http://127.0.0.1:9090/configs?force=false", jsonReloadData);//切换配置文件
               // string yamlPath = Application.StartupPath + @"/config.yaml";
                //YML yml = new YML(yamlPath);
                //yml.modify("last-yaml", fileName);
                //yml.modify("last-yamlIndex", listView1.SelectedItems[0].Index.ToString());
                //yml.save();
                //new ClashN().configChoose
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].SubItems[3].Text = " ";
                }
                listView1.SelectedItems[0].SubItems[3].Text = "🐱";
                //再次调用时 会导致读取两次文件yaml，会造成冲突，一次是listview，一次是combobox，取消掉listview的改变就可
               tscb.SelectedIndex = listView1.SelectedItems[0].Index;

            }




        }

        private void 更新配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string path = Application.StartupPath + @"/profiles/" + listView1.SelectedItems[0].SubItems[0].Text;
                string url = listView1.SelectedItems[0].SubItems[2].Text;
                if (url != "")
                {
                    utils.DownloadFile(listView1.SelectedItems[0].SubItems[2].Text, path);
                    //将如下保存到每个订阅的配置文件中 
                    //ClashNurl ： url
                    string str = "clash-sub-url: " + url;
                    utils.WriteFirstLine(path, str);
                }
            }
            
        }

        private void btnOneClickUpdate_Click(object sender, EventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            string v_OpenFolderPath = Application.StartupPath+@"\profiles";
            System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
        }

        private void 删除配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string path1 = Application.StartupPath + @"/profiles/" + listView1.SelectedItems[0].SubItems[0].Text;
                // ...or by using FileInfo instance method.
                System.IO.FileInfo fi = new System.IO.FileInfo(path1);
                try
                {
                    fi.Delete();
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Diagnostics.Process.Start("explorer.exe", "https://github.com/vveg26/SelfConfig");
            string filePath = Application.StartupPath + @"/config.ini"; //配置文件
           
            Dictionary<string, string> map= new Dictionary<string, string>();
            map = utils.IniReadMap("Clash", filePath);



            for (int i = 0; i < map.Count; i++)
            {
                string downloadSavePath = Application.StartupPath + @"/profiles/free" + i + @".yaml";
                var item = map.ElementAt(i);
                utils.DownloadFile(item.Value, downloadSavePath);
                //MessageBox.Show(item.Value);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string path = Application.StartupPath + @"/profiles/" + listView1.Items[i].SubItems[0].Text;
                string url = listView1.Items[i].SubItems[2].Text;
                if (url != "")
                {
                    utils.DownloadFile(listView1.Items[i].SubItems[2].Text, path);
                    //将如下保存到每个订阅的配置文件中 
                    //ClashNurl ： url
                    string str = "clash-sub-url: " + url;
                    utils.WriteFirstLine(path, str);
                }
            }

            ReloadListView();
        }

        private void 更新免费订阅源文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YML yml = new YML(Application.StartupPath + @"/config.yaml");
            string url = yml.read("configIni");

            utils.DownloadFile(url, Application.StartupPath + @"/config.ini");
        }


    }
}
