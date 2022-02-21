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
    public partial class AddSub : Form
    {
        Utils utils = new Utils();
        public AddSub()
        {
            InitializeComponent();
        }
        public AddSub(ListView lsv)
        {
            this.lsv = lsv;
            InitializeComponent();
        }

        ListView lsv = new ListView();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath+@"/profiles/"+txtYamlName.Text+@".yaml";//配置文件保存路径
            string url = txtSubUrl.Text;           
            utils.DownloadFile(url,path);//下载文件到本地

            if (System.IO.File.Exists(path))
            {
                //将如下保存到每个订阅的配置文件中 
                //ClashNurl ： url
                string str = "clash-sub-url: " + url;
                //utils.WriteLine(str, path);
                utils.WriteFirstLine(path, str);
            }


            
           
           
            
            


        }

        private void AddSub_Load(object sender, EventArgs e)
        {

        }
    }
}
