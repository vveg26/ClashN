using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashN
{
    public partial class ClashN : Form
    {
        public ClashN()
        {
            InitializeComponent();
        }
        string cmdStr = @"clash -d ./ -f ./profiles/self.yaml -ext-ctl 127.0.0.1:12344";//打开clash
        

        /// <summary>
        /// 命令行
        /// </summary>
        /// <param name="cmdStr">cmd命令</param>
        private void CmdLine(string cmdStr)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";//要启动的应用程序
            p.StartInfo.UseShellExecute = false;//不使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//允许输出信息
            p.StartInfo.RedirectStandardError = true;//允许输出错误
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine(cmdStr);//向cmd窗口发送输入命令
            p.StandardInput.AutoFlush = true;//自动刷新
            p.Close();//关闭进程

        }
        private void KillProcess()
        {

        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            if(btn_start.Text == "启动")
            {
                CmdLine(cmdStr);//调用cmd
                btn_start.Text = "关闭";
            }
            else
            {
                KillProcess();
                btn_start.Text = "启动";
            }
            
        }


    }
}
