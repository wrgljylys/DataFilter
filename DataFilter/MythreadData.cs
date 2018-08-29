using DataFilter.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataFilter
{
    public class MythreadData
    {
        public MythreadData(MainWindow window, int seq, List<string> blueList, string num, string mychar)
        {
            Worker = new BackgroundWorker();
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += worker_DoWork;
            Worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            RedList = new List<string>();
            Seq = seq;
            BlueList = blueList;
            Num = num;
            MyChar = mychar;
            Window = window;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> temp;
            if (!string.IsNullOrEmpty(Num))
            {
                string[] regStrs = Num.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string str in regStrs)
                {
                    sb.Append(@"\w*?").Append(str.Trim()).Append("|");
                }
                sb.Remove(sb.Length - 1, 1);

                temp = new List<string>(BlueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (Regex.IsMatch(data, sb.ToString(), RegexOptions.IgnoreCase))
                    {
                        if (!RedList.Contains(data))
                            RedList.Add(data);
                        BlueList.Remove(data);
                        ++count;
                    }
                }

                Window.Dispatcher.Invoke(new Action(() =>
                {
                    Window.Log(LogType.筛选, "线程" + Seq + ": 自定义数字", count, RedList.Count, BlueList.Count);
                }));
            }

            if (!string.IsNullOrEmpty(MyChar))
            {
                string[] regStrs = MyChar.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string str in regStrs)
                {
                    sb.Append(@"\w*?").Append(str.Trim()).Append("|");
                }
                sb.Remove(sb.Length - 1, 1);

                temp = new List<string>(BlueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (Regex.IsMatch(data, sb.ToString(), RegexOptions.IgnoreCase))
                    {
                        if (!RedList.Contains(data))
                            RedList.Add(data);
                        BlueList.Remove(data);
                        ++count;
                    }
                }
                Window.Dispatcher.Invoke(new Action(() =>
                {
                    Window.Log(LogType.筛选, "线程" + Seq + ": 自定义数字", count, RedList.Count, BlueList.Count);
                }));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MythreadData thread = e.Argument as MythreadData;
            thread.filterByRules();
        }

        public void filterByRules()
        {
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

                List<string> temp = new List<string>(BlueList);
                int count = 0;
                foreach (string data in temp)
                {
                    if (isNo4 && data.IndexOf('4') >= 0)
                        continue;

                    bool attPass = true;
                    string rex = rule.Regx;

                    if (rule.Children != null)
                    {
                        List<Rule> children = (from r in rule.Children
                                               where r.Checked
                                               select r).ToList();
                        attPass = children.Count <= 0;
                        bool needFormat = rule.Regx.Contains("{0}");
                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (Rule r in children)
                        {
                            if(!r.Checked)
                                continue;

                            if (needFormat)
                            {
                                sb.Append(r.Regx);
                            }
                        }

                        foreach(Rule r in children)
                        {
                            if (data.IndexOf(r.Regx) > 0)
                            {
                                attPass = true;
                                break;
                            }
                            else
                                attPass = false;
                        }

                        sb.Append("]");
                        if (sb.Length <= 2)
                            sb = new StringBuilder();
                        if (needFormat)
                        {
                            rex = string.Format(rule.Regx, sb, sb, sb);
                        }
                    }

                    if (Regex.IsMatch(data, rex, RegexOptions.IgnoreCase) && attPass)
                    {
                        if (!RedList.Contains(data))
                            RedList.Add(data);
                        BlueList.Remove(data);
                        ++count;
                    }
                }

                Window.Dispatcher.Invoke(new Action(() =>
                {
                    Window.Log(LogType.筛选, "线程" + Seq + ": " + rule.Name, count, RedList.Count, BlueList.Count);
                }));
            }
        }

        public MainWindow Window { get; set; }
        public List<string> RedList { get; set; }
        public List<string> BlueList { get; set; }
        public BackgroundWorker Worker { get; set; }
        public int Seq { get; set; }
        public string Num { get; set; }
        public string MyChar { get; set; }
    }
}
