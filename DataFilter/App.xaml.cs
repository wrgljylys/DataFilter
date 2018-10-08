using DataFilter.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace DataFilter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public DateTime Date { get; set; }
        public App()
        {
            this.Startup += App_Startup;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\regData.data";

            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    byte[] data = new byte[file.Length];
                    file.Read(data, 0, (int)file.Length);
                    string src = Encoding.Default.GetString(data);

                    string mac = NetworkAdapter.DefaultNa.Mac.Replace(":", "").Replace("：", "").Replace(" ", "").Replace("-", "").ToUpper();

                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    des.Key = ASCIIEncoding.ASCII.GetBytes(mac.Substring(0, 8));
                    des.IV = ASCIIEncoding.ASCII.GetBytes(mac.Substring(4, 8));

                    MemoryStream ms = new MemoryStream();
                    byte[] inputByteArray = new byte[src.Length / 2];
                    for(int i = 0; i < inputByteArray.Length; i++)
                    {
                        inputByteArray[i] = Convert.ToByte(src.Substring(i * 2, 2), 16);
                    }
                    
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Encoding.Default.GetString(ms.ToArray());

                    DateTime date = DateTime.ParseExact(str, "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture);

                    if(date <= DateTime.Now)
                    {
                        MessageBox.Show("当前注册已过期，请联系服务商注册本软件");
                        Shutdown();
                        return;
                    }

                    Date = date;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无注册信息或注册信息不合法，联系服务商注册本软件");
                Shutdown();
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show("程序出现异常: " + ex.Message + (e.IsTerminating ? "  请重启系统" : ""));
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("程序出现Dispatcher异常: " + e.Exception.Message);
            e.Handled = true;
        }
    }
}
