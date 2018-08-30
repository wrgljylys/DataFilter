using DataFilter.Entity;
using DataFilter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace DataFilter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime startTime;
        private DateTime filterTime;
        private List<string> dataList;
        private List<string> redList;
        private List<string> blueList;
        private ObservableCollection<LogInfo> logInfoList;

        List<MythreadData> threadList;
        BackgroundWorker worker;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Drop += MainWindow_Drop;
            Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (worker != null)
                worker.CancelAsync();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<Rule> rules = Rule.Rules;
            StackPanel sp = expander.Content as StackPanel;
            foreach (GroupBox gb in sp.Children)
            {
                gb.DataContext = (from rule in rules
                                 where rule.GroupName == gb.Tag.ToString()
                                 select rule).ToList(); 
            }

            Title = "数据筛选工具            （使用期限至" + (App.Current as App).Date.ToString("yyyy-MM-dd") + "）";

            /*
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {

                sb.Append(@"|\w*?\[" + (9 - i) + @"]\D*(\d)\w*?\[" + (8-i) +@"]\D*\" + (i + 10));
            }
            string str = sb.ToString();
            MessageBox.Show(str);
             * */
        }


        void MainWindow_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            dataList = new ExcelReader().Load(fileName);

            redList = new List<string>();
            blueList = new List<string>();
            logInfoList = new ObservableCollection<LogInfo>();
            dgLog.ItemsSource = logInfoList;

            Log(LogType.导入, fileName, dataList.Count, 0, 0);
            btnFilt_Click(null, null);
        }

        private void btnFilt_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            threadList = new List<MythreadData>();

            if (cbNo4Only.IsChecked == true)
            {
                no4Only();
                return;
            }
  
            int count = 0;
            int i = 0;
            while (count < dataList.Count)
            {
                MythreadData thread = new MythreadData(this, ++i, new List<string>(dataList.GetRange(count, count + 25000 <= dataList.Count ? 25000 : dataList.Count - count)), tbNum.Text, tbChar.Text);
                threadList.Add(thread);
                Log(LogType.线程启动, "线程：" + i, dataList.Count, 0, thread.BlueList.Count);
                thread.Worker.RunWorkerAsync(thread);
                thread.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
                count += thread.BlueList.Count;
            }
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool completed = true;
            if (threadList != null)
            {
                foreach (MythreadData thread in threadList)
                {
                    if (thread.Worker.IsBusy)
                    {
                        completed = false;
                        break;
                    }
                }
            }
            if (completed)
            {
                filterTime = DateTime.Now;
                TimeSpan span = (filterTime - startTime);
                tbTime.Text = string.Format("筛选完成！ 用时：{0}秒", span.TotalSeconds);
                btnExport_Click(null, null);
            }
        }

        private void no4Only()
        {
            foreach (string data in dataList)
            {
                if (data.IndexOf('4') < 0)
                    redList.Add(data);
                else
                    blueList.Add(data);
            }
            Log(LogType.筛选, "唯一条件 所有数字不带4", redList.Count, redList.Count, blueList.Count);
        }

        public void filterByRules()
        {
            blueList = new List<string>(dataList);
            bool isNo4 = false;
            foreach (Rule rule in Rule.Rules)
            {
                if (!rule.Checked || rule.IsAttached)
                    continue;
                if (rule.Checked && rule == Rule.No4)
                {
                    isNo4 = true;
                    continue;
                }

                List<string> temp = new List<string>(blueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (isNo4 && data.IndexOf('4') >= 0)
                        continue;

                    bool attPass = true;
                    
                    if (rule.Children != null)
                    {
                        List<Rule> children = (from r in rule.Children
                                               where r.Checked
                                               select r).ToList();
                        attPass = children.Count <= 0;
                        foreach (Rule r in children)
                        {
                            if (data.IndexOf(r.Regx) > 0)
                            {
                                attPass = true;
                                break;
                            }
                        }
                    }

                    if (Regex.IsMatch(data, rule.Regx, RegexOptions.IgnoreCase) && attPass)
                    {
                        if (!redList.Contains(data))
                            redList.Add(data);
                        blueList.Remove(data);
                        ++count;
                    }
                }

                Dispatcher.Invoke(new Action(() =>
                {
                    Log(LogType.筛选, rule.Name, count, redList.Count, blueList.Count);
                }));  
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> temp;
            if (!string.IsNullOrEmpty(tbNum.Text))
            {
                string[] regStrs = tbNum.Text.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string str in regStrs)
                {
                    sb.Append(@"\w*?").Append(str.Trim()).Append("|");
                }
                sb.Remove(sb.Length - 1, 1);

                temp = new List<string>(blueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (Regex.IsMatch(data, sb.ToString(), RegexOptions.IgnoreCase))
                    {
                        if (!redList.Contains(data))
                            redList.Add(data);
                        blueList.Remove(data);
                        ++count;
                    }
                }
                Dispatcher.Invoke(new Action(() =>
                {
                    Log(LogType.筛选, "自定义数字", count, redList.Count, blueList.Count);
                })); 
            }

            if (!string.IsNullOrEmpty(tbNum.Text))
            {
                string[] regStrs = tbNum.Text.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string str in regStrs)
                {
                    sb.Append(@"\w*?").Append(str.Trim()).Append("|");
                }
                sb.Remove(sb.Length - 1, 1);

                temp = new List<string>(blueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (Regex.IsMatch(data, sb.ToString(), RegexOptions.IgnoreCase))
                    {
                        if (!redList.Contains(data))
                            redList.Add(data);
                        blueList.Remove(data);
                        ++count;
                    }
                }
                Dispatcher.Invoke(new Action(() =>
                {
                    Log(LogType.筛选, "自定义字母", count, redList.Count, blueList.Count);
                }));
            }

            MessageBox.Show("筛选完成");
        }

        /*
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MainWindow mainWindow = e.Argument as MainWindow;
            mainWindow.filterByRules();
        }
         */

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (threadList != null)
            {
                foreach (MythreadData thread in threadList)
                {
                    if (thread.Worker.IsBusy)
                    {
                        MessageBox.Show("尚有线程未完成工作，请等待");
                        return;
                    }
                }
            }

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Export\\";
            string fileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (threadList != null)
            {
                foreach (MythreadData thread in threadList)
                {
                    redList.AddRange(thread.RedList);
                    blueList.AddRange(thread.BlueList);
                }
            }

            if (redList.Count > 0)
            {
                fileName = filePath + "红" + redList[0] + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                new ExcelWriter().Write(fileName, redList);
                Log(LogType.导出, fileName, redList.Count, redList.Count, blueList.Count);
            }
            if (blueList.Count > 0)
            {
                fileName = filePath + "蓝" + blueList[0] + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                new ExcelWriter().Write(fileName, blueList);
                Log(LogType.导出, fileName, blueList.Count, redList.Count, blueList.Count);
            }
        }

        public void Log(LogType logType, string message, int dataCount, int redCount, int blueCount)
        {
            logInfoList.Add(new LogInfo() { Time = DateTime.Now, LogType = logType, Message = message, DataCount = dataCount, RedCount = redCount, BlueCount = blueCount });
        }

        private void cbAllSelect_Checked(object sender, RoutedEventArgs e)
        {
            foreach(Rule rule in Rule.Rules)
            {
                rule.Checked = true;
            }
            List<Rule> rules = Rule.Rules;
            StackPanel sp = expander.Content as StackPanel;
            foreach (GroupBox gb in sp.Children)
            {
                gb.DataContext = (from rule in rules
                                  where rule.GroupName == gb.Tag.ToString()
                                  select rule).ToList();
            }
        }

        private void cbAllSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Rule rule in Rule.Rules)
            {
                rule.Checked = false;
            }
            List<Rule> rules = Rule.Rules;
            StackPanel sp = expander.Content as StackPanel;
            foreach (GroupBox gb in sp.Children)
            {
                gb.DataContext = (from rule in rules
                                  where rule.GroupName == gb.Tag.ToString()
                                  select rule).ToList();
            }
        }
    }
}
