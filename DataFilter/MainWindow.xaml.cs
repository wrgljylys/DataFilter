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
                gb.DataContext = from rule in rules
                                 where rule.GroupName == gb.Header.ToString()
                                 select rule; 
            }
        }


        void MainWindow_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            dataList = new ExcelReader().Load(fileName);
            MessageBox.Show("导入" + dataList.Count + "条数据");

            redList = new List<string>();
            blueList = new List<string>();
        }

        private void btnFilt_Click(object sender, RoutedEventArgs e)
        {

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
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
