using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Register
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbMac.Text = NetworkAdapter.DefaultNa.Mac;
        }

        private void btnLoaclMac_Click(object sender, RoutedEventArgs e)
        {
            tbMac.Text = NetworkAdapter.DefaultNa.Mac;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbMac.Text))
            {
                MessageBox.Show("请先填写Mac地址");
                tbMac.Focus();
                return;
            }
            if(dpDate.SelectedDate == null)
            {
                MessageBox.Show("请先选择日期");
                dpDate.Focus();
                return;
            }
            string mac = tbMac.Text.Replace(":", "").Replace("：", "").Replace(" ", "").Replace("-", "").ToUpper();
            DateTime date = (DateTime)dpDate.SelectedDate;
            byte[] inputByteArray = Encoding.Default.GetBytes(date.ToString("MMddyyyy"));

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();         
            des.Key = ASCIIEncoding.ASCII.GetBytes(mac.Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(mac.Substring(4, 8));

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            tbRegCode.Text = ret.ToString();  
        }

        private void btnWriteFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbRegCode.Text))
            {
                MessageBox.Show("请先生成注册码");
                return;
            }

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\regData.data", FileMode.Create))
            {
                fs.Write(Encoding.Default.GetBytes(tbRegCode.Text), 0, Encoding.Default.GetBytes(tbRegCode.Text).Length);
                fs.Flush();
            }

            MessageBox.Show("注册文件写入完成！");
        }
    }
}
