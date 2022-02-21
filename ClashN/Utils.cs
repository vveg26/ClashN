using Microsoft.Win32;
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
        #region 读取Ini文件的操作
        //需要调用GetPrivateProfileString的重载
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);
               
        /// <summary>
        /// 获取指定Section，指定Key的Value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="iniFilePath">ini文件的地址</param>
        /// <returns>Value</returns>
        public string IniReadValue(string Section, string Key,string iniFilePath)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, iniFilePath);
            return temp.ToString();
        }
        /// <summary>
        /// 获取指定Section中的所有键值对
        /// </summary>
        /// <param name="SectionName"></param>
        /// <param name="iniFilePath">ini路径</param>
        /// <returns>键值对字典</returns>
        public Dictionary<string, string> IniReadMap(string SectionName, string iniFilePath)
        {
           // List<string> result = new List<string>();
            Dictionary<string, string> resultMap = new Dictionary<string, string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilePath);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {   
                    //将键值对存入到字典中
                    resultMap.Add(Encoding.Default.GetString(buf, j, i - j), IniReadValue(SectionName, Encoding.Default.GetString(buf, j, i - j), iniFilePath));//获取Key和Valuve
                   // result.Add(IniReadValue(SectionName, Encoding.Default.GetString(buf, j, i - j),iniFilename));//获取Value
                    j = i + 1;
                }
            return resultMap;
        }
        #endregion


        #region 文件操作方法
        /// <summary>
        /// 将文本写入文件的第一行
        /// </summary>
        /// <param name="filenPath">文件名</param>
        /// <param name="str">写入字符串</param>
        public void WriteFirstLine(string filePath,string str)
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
        #endregion

        #region Json操作方法
        /// <summary>
        /// 读取Json文件的Key和Value
        /// </summary>
        /// <param name="jsonstr">json字符串</param>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public string JsonRead(string jsonstr,string key)
        {
           
            JObject obj = JObject.Parse(jsonstr);

            //Console.WriteLine(obj.Count);
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
        /// 获取json字符串中所有的KV
        /// </summary>
        /// <param name="jsonData">json字符串</param>
        /// <returns>map</returns>
        public Dictionary<string, string> JsonGetAllKV(string jsonData)
        {
            JObject obj = JObject.Parse(jsonData);
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (var x in obj)
            {
                map.Add(x.Key, x.Value.ToString());
            }
            return map;
        }
        #endregion





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
        /// 读取yaml文件中的Value
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


        /// <summary>
        /// 设置开机自启动
        /// </summary>
        /// <param name="keyName">目标名</param>
        /// <param name="filePath">exe路径</param>
        /// <param name="AddOrCancel">启动或取消</param>
        /// <returns></returns>
        public  bool SetAutoRun(string keyName, string filePath, bool AddOrCancel)
        {
            try
            {
                RegistryKey Local = Registry.LocalMachine;
                RegistryKey runKey = Local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\");
                if (AddOrCancel)
                {
                    runKey.SetValue(keyName, filePath);
                    Local.Close();
                }
                else
                {
                    if (runKey != null)
                    {
                        runKey.DeleteValue(keyName, false);
                        Local.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }
            return true;
        }


    }
}
