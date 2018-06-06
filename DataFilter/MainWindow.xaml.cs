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

            Log(LogType.导入, fileName, dataList.Count);
        }

        private void btnFilt_Click(object sender, RoutedEventArgs e)
        {
            if (cbNo4Only.IsChecked == true)
            {
                no4Only();
                return;
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
            Log(LogType.筛选, "唯一条件 所有数字不带4", redList.Count);
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
                Log(LogType.导出, fileName, redList.Count);
            }
            if (blueList.Count > 0)
            {
                fileName = filePath + "蓝" + blueList[0] + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx";
                new ExcelWriter().Write(fileName, blueList);
                Log(LogType.导出, fileName, blueList.Count);
            }
        }

        private void Log(LogType logType, string message, int dataCount)
        {
            logInfoList.Add(new LogInfo() { Time = DateTime.Now, LogType = logType, Message = message, DataCount = dataCount });
        }
    }
}
