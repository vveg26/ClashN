using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClashN
{
    public partial class ClashN : Form
    {
        Utils utils = new Utils();  //工具类
        YML yml = new YML(Application.StartupPath + @"/config.yaml");//YML
        RestfulGo restfulGo = new RestfulGo();


        string subConvert = string.Empty; //订阅转换网址
        string localUI = string.Empty; //本地UI

        string port         = string.Empty;
        string socksport    = string.Empty;
        string mode         = string.Empty;
        string allowlan     = string.Empty;
        string loglevel = string.Empty;
        string mixedport = string.Empty;
        public ClashN()
        {
            InitializeComponent();
        }

/// <summary>
/// 可选菜单
/// </summary>
/// <param name="cms">小项</param>
/// <param name="ocms">大项</param>
        public void IsCheckedControl(ToolStripMenuItem cms,ToolStripMenuItem ocms)
        {
            //这里写父容器的集合 --可自动判断。这里我采用手写。提高效率
            foreach (ToolStripMenuItem item in ocms.DropDownItems)
            {
                //不是当前项的取消选择
                if (item.Name == cms.Name)
                {
                    item.Checked = true; //设选中状态为true
                }
                else 
                {
                    item.Checked = false; //设选中状态为false
                }
            }
        }


        /// <summary>
        /// 全局代理
        /// </summary>
        /// <param name="url"></param>
        private void SystemProxySetting(string url)
        {
            Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", url);//所有HTTP流量走这个端口，主要是通过设置IE的代理来接管
        }

        private void ClashN_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuRight.Show(MousePosition.X, MousePosition.Y);
            }
        }



        private void ClashN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void trayico_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)//当程序是最小化的状态时显示程序页面
                {
                    this.WindowState = FormWindowState.Normal;
                }
                this.Activate();
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void trayExit_Click(object sender, EventArgs e)
        { 
           string url = "http://127.0.0.1:9090/configs";

           string message =restfulGo.WebGet(url);
            YML yml = new YML(Application.StartupPath + @"/config.yaml");
            port = utils.JsonRead(message, "port");
            socksport = utils.JsonRead(message, "socks-port");
            mode = utils.JsonRead(message, "mode");
            allowlan = utils.JsonRead(message, "allow-lan");
            loglevel = utils.JsonRead(message, "log-level");
            mixedport = utils.JsonRead(message, "mixed-port");
            yml.modify("port", port);
            yml.modify("socks-port",socksport);
            yml.modify("mode",mode);
            yml.modify("allow-lan", allowlan);
            yml.modify("log-level",loglevel);
            yml.modify("mixed-port",mixedport);
            
            yml.save();

            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
                utils.KillProcess("clash");
            }
        }

        private void 设置系统代理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsCheckedControl(设置系统代理ToolStripMenuItem, 系统代理ToolStripMenuItem);
            
            
            string port = yml.read("port");
            SystemProxySetting("127.0.0.1:" + port);//HTTP流量接管
            yml.modify("system-proxy", "true");
            yml.save();

        }

        private void 清除系统代理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsCheckedControl(清除系统代理ToolStripMenuItem, 系统代理ToolStripMenuItem);
            SystemProxySetting("");//清除代理
            yml.modify("system-proxy", "false");
            yml.save();
        }

        private void 本地UIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(localUI);
        }

        private void 订阅管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new ConfigManager().Show();

            new ConfigManager(configChoose).Show();

        }

        private void 订阅转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            System.Diagnostics.Process.Start(subConvert);
        }
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
                configChoose.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    configChoose.Items.Add(list[i].Name);
                }


        }
        //重载配置文件combobox
        public void ReloadCombobox()
        {
            //界面显示部分（Combobox）
            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();
            string path = @"./profiles";//配置文件文件夹
            GetFiles(path, ".yaml", ref files);//获取文件夹下的文件
            ListShow(files); //显示配置文件列表
        }

        private void ClashN_Load(object sender, EventArgs e)
        {
           
            YML yml = new YML(Application.StartupPath + @"/config.yaml");//YML

            ReloadCombobox();

            开机自启ToolStripMenuItem.Checked = bool.Parse( yml.read("auto-run"));


            localUI = yml.read("localUI");
            subConvert =yml.read("subConvert");

            utils.KillProcess("clash");
        
            string yamlName = yml.read("last-yaml");
           
           
            string cmdStr = @"clash -d ./ -f ./profiles/"+yamlName +" -ext-ctl 127.0.0.1:9090 -ext-ui ui";
            utils.CmdLine(cmdStr);
            //TODO 需要改进删除之后异常的bug
            /*            int configindex = int.Parse(yml.read("last-yamlIndex"));

                        if (configChoose.Items.Count >= configindex)
                        {
                            configChoose.SelectedIndex = configindex;
                        }*/
            ReloadNodeChoose();//初始化节点
            string configItem = yml.read("last-yaml");
            for(int i=0; i < configChoose.Items.Count; i++)
            {
                if (configChoose.Items[i].ToString().Equals(configItem))
                {
                    configChoose.SelectedIndex = i;
                }
            }
           // MessageBox.Show(configChoose.Items[1].ToString());
            string configUrl = "http://127.0.0.1:9090/configs";

            port = yml.read("port");
            socksport = yml.read("socks-port");
            mode = yml.read("mode");
            allowlan = yml.read("allow-lan");
            loglevel = yml.read("log-level");
            if (allowlan.Equals("True"))
            {
                allowlan = "true";
            }
            else
            {
                allowlan = "false";
            }
            mixedport = yml.read("mixed-port");
            string jsonConfigData = "{\"port\":"+port+",\"socks-port\":"+socksport+",\"mode\":\""+mode+"\",\"allow-lan\":"+allowlan+",\"log-level\":\""+loglevel+ "\",\"mixed-port\":" + mixedport + "}";//基础配置信息
            //string jsonConfigData = "{\"port\":7890,\"socks-port\":7891,\"mode\":\"Rule\",\"allow-lan\":false,\"log-level\":\"info\",\"mixed-port\": 7893}";//基础配置信息
            
            string a = restfulGo.WebPatch(configUrl, jsonConfigData);
            //MessageBox.Show(a);
            //Console.WriteLine(jsonConfigData1);
            bool systemproxy = bool.Parse( yml.read("system-proxy"));
            if (systemproxy)
            {
                设置系统代理ToolStripMenuItem.PerformClick();
            }
            else
            {
                清除系统代理ToolStripMenuItem.PerformClick();
            }

            if (mode.Equals("rule"))
            {
                rulecbx.SelectedIndex = 1;
            }else if (mode.Equals("global"))
            {
                rulecbx.SelectedIndex = 0;
            }
            else
            {
                rulecbx.SelectedIndex = 2;
            }

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            SetVisibleCore(false);


        }

        private void 开机自启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("涉及到注册表修改，已移除");
        }


        //切换配置文件
        private void configChoose_SelectedIndexChanged(object sender, EventArgs e)
        {

            //TODO需要优化
            string fileName = configChoose.Text;
            string path = Application.StartupPath+@"/profiles/" + fileName;
            string jsonReloadData = JsonConvert.SerializeObject(new
            {
                path = path //配置文件路径

            });
            try 
            {
                restfulGo.WebPut("http://127.0.0.1:9090/configs?force=false", jsonReloadData);//切换配置文件
            } catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
           
            
            string yamlPath = Application.StartupPath + @"/config.yaml";
            YML yml = new YML(yamlPath);
            yml.modify("last-yaml", fileName);
           // yml.modify("last-yamlIndex", configChoose.SelectedIndex.ToString());
            yml.save();
            
        }
       



        private void 控制面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(localUI);
        }

        private void rulecbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string configUrl = "http://127.0.0.1:9090/configs";

            string mode;

            if (rulecbx.SelectedIndex == 0)
            {
                mode = "global";
                string jsonConfigData = "{\"mode\":\"" + mode + "\"}";//基础配置信息
                restfulGo.WebPatch(configUrl, jsonConfigData);
            }else if(rulecbx.SelectedIndex == 1)
            {
                mode = "rule";
                string jsonConfigData = "{\"mode\":\"" + mode + "\"}";//基础配置信息
                restfulGo.WebPatch(configUrl, jsonConfigData);
            }
            else
            {
                mode = "direct";
                string jsonConfigData = "{\"mode\":\"" + mode + "\"}";//基础配置信息
                restfulGo.WebPatch(configUrl, jsonConfigData);
            }
            
        }



        /// <summary>
        /// 菜单栏点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DemoClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripMenuItem item1 = (ToolStripMenuItem)item.OwnerItem;
            for(int i = 0; i < item1.DropDownItems.Count; i++)
            {
                ToolStripMenuItem item3 = (ToolStripMenuItem)item1.DropDownItems[i];
                item3.Checked = false;
            }
            string url = "http://127.0.0.1:9090/proxies/";

            string selector = item1.Text;

            string url1 = url + selector;
            string jsonData = "{\"name\":\"" + item.Text + "\"}";
            string a = restfulGo.WebPut(url1,jsonData);
            item.Checked = true;
        }
        //获取节点信息，并初始化界面
        public void ReloadNodeChoose()
        {
            节点选择ToolStripMenuItem.DropDownItems.Clear();
            //获取Key
            string proxiesUrl = "http://127.0.0.1:9090/proxies";
            string jsonData = restfulGo.WebGet(proxiesUrl);
            string jsontest = utils.JsonRead(jsonData, "proxies");
            Dictionary<string, string> map = new Dictionary<string, string>();
            map = utils.JsonGetAllKV(jsontest);

            foreach (var x in map)

            {


                ToolStripMenuItem item = new ToolStripMenuItem();
                string str = x.Key;

                string str1 = x.Value;
                JArray jarray = (JArray)utils.JsonReadAll(str1, "all");//全部节点
                string now = utils.JsonRead(str1, "now");//当前选择节点
                if (jarray != null)
                {
                    foreach (string node in jarray)
                    {   
                        ToolStripMenuItem secondItem = new ToolStripMenuItem(node);
                        secondItem.Text = node;
                        secondItem.Click += DemoClick;
                        if(secondItem .Text.Equals(now))
                        {
                            secondItem.Checked = true;
                        }

                        item.DropDownItems.Add(secondItem);
                    }
                }
                else
                {
                    continue;
                }
                

                item.Text = str;
                item.Name = str;
                节点选择ToolStripMenuItem.DropDownItems.Add(item);

            }
        }



        private void trayIco_Click(object sender, EventArgs e)
        {
            ReloadNodeChoose();//重载节点选择
            ReloadCombobox();//重载规则
            
        }
    }
}
