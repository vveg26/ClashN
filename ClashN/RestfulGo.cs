using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClashN
{
    class RestfulGo
    {
        /// <summary>
        /// GET用法
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public string WebGet(string url)
        {
            using (WebClient webClient = new WebClient())
            // 從 url 讀取資訊至 stream
            using (Stream stream = webClient.OpenRead(url))
            // 使用 StreamReader 讀取 stream 內的字元
            using (StreamReader reader = new StreamReader(stream))
            {
                // 將 StreamReader 所讀到的字元轉為 string
                string request = reader.ReadToEnd();
                return request;

            }
        }

        public string WebPut(string url,string jsonData)
        {
            //创建一个HTTP请求  

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式  
            request.Method = "PUT";
            //内容类型
            request.ContentType = "application/json";

            //设置参数，并进行URL编码 

            string paraUrlCoded = jsonData;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流
                           // String strValue = "";//strValue为http响应所返回的字符流
            string postContent = null;
            //如果出现异常，可以将下面的部分注释掉--------------------------------------------------------------------



            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream s = response.GetResponseStream();
                    StreamReader sRead = new StreamReader(s);
                    postContent = sRead.ReadToEnd();
                    sRead.Close();
                    s.Close();
                    response.Close();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }



//-----------------------------------------------------------------------------------------------------------------------------------------           
           return postContent;//返回Json数据
           

        }


            public string WebPatch(string url, string jsonData)
            {
                //创建一个HTTP请求  

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //Post请求方式  
                request.Method = "PATCH";
                //内容类型
                request.ContentType = "application/json";

                //设置参数，并进行URL编码 

                string paraUrlCoded = jsonData;//System.Web.HttpUtility.UrlEncode(jsonParas);   

                byte[] payload;
                //将Json字符串转化为字节  
                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                //设置请求的ContentLength   
                request.ContentLength = payload.Length;
                //发送请求，获得请求流 

                Stream writer;
                try
                {
                    writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
                }
                catch (Exception)
                {
                    writer = null;
                    Console.Write("连接服务器失败!");
                }
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                writer.Close();//关闭请求流
                               // String strValue = "";//strValue为http响应所返回的字符流
                HttpWebResponse response;
                try
                {
                    //获得响应流
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = ex.Response as HttpWebResponse;
                }
                Stream s = response.GetResponseStream();
                //  Stream postData = Request.InputStream;
                StreamReader sRead = new StreamReader(s);
                string postContent = sRead.ReadToEnd();
                sRead.Close();
                return postContent;//返回Json数据

            
        }
    }
}



