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

namespace DataFilter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> dataList;
        private List<string> redList;
        private List<string> blueList;
        private ObservableCollection<LogInfo> logInfoList;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Drop += MainWindow_Drop;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        { 
            List<Rule> rules = Rule.Rules;
            StackPanel sp = expander.Content as StackPanel;
            foreach (GroupBox gb in sp.Children)
            {
                gb.DataContext = (from rule in rules
                                 where rule.GroupName == gb.Header.ToString()
                                 select rule).ToList(); 
            }
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
        }

        private void btnFilt_Click(object sender, RoutedEventArgs e)
        {
            if (cbNo4Only.IsChecked == true)
            {
                no4Only();
                return;
            }

            filterByRules();
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

        private void filterByRules()
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

                Log(LogType.筛选, rule.Name, count, redList.Count, blueList.Count);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Export\\";
            string fileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (redList.Count > 0)
            {
                fileName = filePath + "红" + redList[0] + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx";
                new ExcelWriter().Write(fileName, redList);
                Log(LogType.导出, fileName, redList.Count, redList.Count, blueList.Count);
            }
            if (blueList.Count > 0)
            {
                fileName = filePath + "蓝" + blueList[0] + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx";
                new ExcelWriter().Write(fileName, blueList);
                Log(LogType.导出, fileName, blueList.Count, redList.Count, blueList.Count);
            }
        }

        private void Log(LogType logType, string message, int dataCount, int redCount, int blueCount)
        {
            logInfoList.Add(new LogInfo() { Time = DateTime.Now, LogType = logType, Message = message, DataCount = dataCount, RedCount = redCount, BlueCount = blueCount });
        }
    }
}
