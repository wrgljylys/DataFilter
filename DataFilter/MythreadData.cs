using DataFilter.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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

                    if (Rule.End689.Checked)
                    {
                        if (data.EndsWith("36") || data.EndsWith("38") || data.EndsWith("39"))
                        {
                            string subString = data.Substring(0, data.Length - 1);
                            bool gotoBlue = true;
                            foreach (Rule endRule in Rule.End689Rules)
                            {
                                if (Regex.IsMatch(subString, endRule.Regx, RegexOptions.IgnoreCase))
                                {
                                    gotoBlue = false;
                                    break;
                                }
                            }

                            if (gotoBlue)
                            {
                                continue;
                            }
                        }
                    }

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

                    if (rule.Name == "数字双对前小后大")
                    {
                        if (Regex.IsMatch(data, @"\w*?(\d)\D*[0]\w*?\1\D*[1]|\w*?(\d)\D*[1]\w*?\2\D*[2]|\w*?(\d)\D*[2]\w*?\3\D*[3]|\w*?(\d)\D*[3]\w*?\4\D*[4]|\w*?(\d)\D*[4]\w*?\5\D*[5]|\w*?(\d)\D*[5]\w*?\6\D*?[6]|\w*?(\d)\D*[6]\w*?\7\D*[7]|\w*?(\d)\D*[7]\w*?\8\D*[8]|\w*?(\d)\D*[8]\w*?\9\D*[9]", RegexOptions.IgnoreCase)
                            || Regex.IsMatch(data, @"\w*?[0]\D*(\d)\w*?[1]\D*\1|\w*?[1]\D*(\d)\w*?[2]\D*\2|\w*?[2]\D*(\d)\w*?[3]\D*\3|\w*?[3]\D*(\d)\w*?[4]\D*\4|\w*?[4]\D*(\d)\w*?[5]\D*\5|\w*?[5]\D*(\d)\w*?[6]\D*\6|\w*?[6]\D*(\d)\w*?[7]\D*\7|\w*?[7]\D*(\d)\w*?[8]\D*\8|\w*?[8]\D*(\d)\w*?[9]\D*\9", RegexOptions.IgnoreCase))
                        {
                            if (!RedList.Contains(data))
                            {
                                RedList.Add(data);
                                ++count;
                            }
                            BlueList.Remove(data);
                        }
                    }
                    else if (rule.Name == "数字双对前大后小")
                    {
                        if (Regex.IsMatch(data, @"\w*?(\d)\D*[9]\w*?\1\D*[8]|\w*?(\d)\D*[8]\w*?\2\D*[7]|\w*?(\d)\D*[7]\w*?\3\D*[6]|\w*?(\d)\D*[6]\w*?\4\D*[5]|\w*?(\d)\D*[5]\w*?\5\D*[4]|\w*?(\d)\D*[4]\w*?\6\D*[3]|\w*?(\d)\D*?[3]\w*?\7\D*[2]|\w*?(\d)\D*[2]\w*?\8\D*[1]|\w*?(\d)\D*[1]\w*?\9\D*[0]", RegexOptions.IgnoreCase)
                            || Regex.IsMatch(data, @"\w*?[9]\D*(\d)\w*?[8]\D*\1|\w*?[8]\D*(\d)\w*?[7]\D*\2|\w*?[7]\D*(\d)\w*?[6]\D*\3|\w*?[6]\D*(\d)\w*?[5]\D*\4|\w*?[5]\D*(\d)\w*?[4]\D*\5|\w*?[4]\D*(\d)\w*?[3]\D*\6|\w*?[3]\D*(\d)\w*?[2]\D*\7|\w*?[2]\D*(\d)\w*?[1]\D*\8|\w*?[1]\D*(\d)\w*?[0]\D*\9", RegexOptions.IgnoreCase))
                        {
                            if (!RedList.Contains(data))
                            {
                                RedList.Add(data);                      
                                ++count;
                            }
                            BlueList.Remove(data);
                        }
                    }
                    else if (Regex.IsMatch(data, rex, RegexOptions.IgnoreCase) && attPass)
                    {
                        if (!RedList.Contains(data))
                        {
                            RedList.Add(data);
                            ++count;
                        }
                        BlueList.Remove(data);
                    }
                }

                Window.Dispatcher.Invoke(new Action(() =>
                {
                    if (rule != Rule.End689)
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
