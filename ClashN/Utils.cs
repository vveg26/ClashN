using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClashN
{
    class Utils
    {
        #region API声明
        //需要调用GetPrivateProfileString的重载
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);
        #endregion
        //配置文件路径
        

        /// <summary>
        /// 读取ini中的Section中的所有Key和Value
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public List<string> ReadKeys(String SectionName,string filePath)
        {
            return ReadIniKeys(SectionName, filePath);
        }
        //读取INI文件  
        public string IniReadValue(string Section, string Key,string filePath)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
            return temp.ToString();
        }
        public List<string> ReadIniKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    //result.Add(Encoding.Default.GetString(buf, j, i - j)); //获取Key
                    result.Add(IniReadValue(SectionName, Encoding.Default.GetString(buf, j, i - j),iniFilename));//获取Value
                    j = i + 1;
                }
            return result;
        }




        /// <summary>
        /// 写入第一行
        /// </summary>
        /// <param name="filenPath">文件名</param>
        /// <param name="str">写入字符串</param>
        public  void WriteFirstLine(string filePath,string str)
        {
            string tempfile = Path.GetTempFileName();
            using (var writer = new StreamWriter(tempfile))
            using (var reader = new StreamReader(filePath))
            {
                writer.WriteLine(str);
                while (!reader.EndOfStream)
                    writer.WriteLine(reader.ReadLine());
            }
            File.Copy(tempfile, filePath, true);
        }


        /// <summary>
        /// 读取json文件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string JsonRead(string jsonstr,string key)
        {
           
            JObject obj = JObject.Parse(jsonstr);

            Console.WriteLine(obj.Count);
            foreach (var x in obj)
            {
                if(x.Key == key)
                {
                    return x.Value.ToString();
                    break;
                }
            }
            return null;
        }


        /// <summary>
        /// 拷贝文件到另一个文件夹下
        /// </summary>
        /// <param name="sourceName">源文件路径</param>
        /// <param name="folderPath">目标路径（目标文件夹）</param>
        public void CopyToFile(string sourceName, string folderPath)
        {
            //例子：
            //源文件路径
            //string sourceName = @"D:\Source\Test.txt";
            //目标路径:项目下的NewTest文件夹,(如果没有就创建该文件夹)
            //string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewTest");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //当前文件如果不用新的文件名，那么就用原文件文件名
            string fileName = Path.GetFileName(sourceName);
            //这里可以给文件换个新名字，如下：
            //string fileName = string.Format("{0}.{1}", "newFileText", "txt");

            //目标整体路径
            string targetPath = Path.Combine(folderPath, fileName);

            //Copy到新文件下
            FileInfo file = new FileInfo(sourceName);
            if (file.Exists)
            {
                //true 为覆盖已存在的同名文件，false 为不覆盖
                file.CopyTo(targetPath, true);
            }
        }



        /// <summary>
        /// Kill进程
        /// </summary>
        /// <param name="processName"></param>
        /// <exception cref="Exception"></exception>
        public void KillProcess(string processName)
        {
            //获得进程对象，以用来操作  
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程   
            try
            {
                //获得需要杀死的进程名  
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    //立即杀死进程  
                    thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }
        /// <summary>
        /// 读取yaml文件中的key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="yamlPath">yaml文件地址</param>
        /// <returns></returns>
        public string ReadYamlValue(string key,string yamlPath)
        {
            string url = string.Empty;
            System.IO.TextReader reader = System.IO.File.OpenText(yamlPath);
            var yaml = new YamlDotNet.RepresentationModel.YamlStream();
            yaml.Load(reader);
            var mapping = (YamlDotNet.RepresentationModel.YamlMappingNode)yaml.Documents[0].RootNode;
            
            foreach (var entry in mapping.Children)
            {
                if (entry.Key.ToString() == key)
                {
                    url = entry.Value.ToString();
                    break;
                    
                }

            }
            reader.Close();
            return url;
            
        }


        public void WriteLine(string str,string yamlPath)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(yamlPath, append:true);
            sw.WriteLine(str);
            sw.Close();//写入
        }


        /// <summary>
        /// 命令行工具
        /// </summary>
        /// <param name="cmdStr">cmd命令</param>
        public void CmdLine(string cmdStr)
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
            //p.Close();
        }

        internal void AddYamlSub()
        {
            
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public  void DownloadFile(string url, string path)
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                //下载uri文件到desPath本地路径
                try
                { webClient.DownloadFile(url, path);
                   System.Windows.Forms.MessageBox.Show("下载成功");
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show(url+"下载失败");
                }
            }
        }
    }
}
