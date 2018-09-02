using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DataFilter.Entity
{
    public class Rule : DependencyObject
    {
        public static DependencyProperty CheckedProperty;
        public static List<Rule> Rules;
        public static Rule No4;
        private const bool initChecked = false;
        static Rule()
        {
            CheckedProperty = DependencyProperty.Register("Checked1", typeof(bool), typeof(Rule));
            No4 = new Rule { GroupName = "数字2", Name = "所有数据不带4", Checked = initChecked, IsAttached = false, Regx = @"" };
            Rules = new List<Rule>();
            Rules.Add(new Rule() { GroupName = "数字1", Name = "三同", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\d)\w*?\1\w*?\1" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "特顺", Checked = initChecked, IsAttached = false, Regx = @"\w*?0\D*1\D*2\D*3\D*4|\W*?1\D*2\D*3\D*4\D*5|\w*?2\D*3\D*4\D*5\D*6|\w*?2\D*3\D*4\D*5\D*6|\w*?3\D*4\D*5\D*6\D*7|\w*?4\D*5\D*6\D*7\D*8|\w*?5\D*6\D*7\D*8\D*9|\w*?6\D*7\D*8\D*9\D*0|\w*?9\D*8\D*7\D*6\D*5|\w*?8\D*7\D*6\D*5\D*4|\w*?7\D*6\D*5\D*4\D*3|\w*?6\D*5\D*4\D*3\D*2|\w*?5\D*4\D*3\D*2\D*1|\w*?4\D*3\D*2\D*1\D*0|\w*?0\D*1\D*2\D*3|\w*?1\D*2\D*3\D*4|\w*?2\D*3\D*4\D*5|\w*?3\D*4\D*5\D*6|\w*?4\D*5\D*6\D*7|\w*?5\D*6\D*7\D*8|\w*?6\D*7\D*8\D*9|\w*?7\D*8\D*9\D*0|\w*?9\D*8\D*7\D*6|\w*?8\D*7\D*6\D*5|\w*?7\D*6\D*5\D*4|\w*?6\D*5\D*4\D*3|\w*?5\D*4\D*3\D*2|\w*?4\D*3\D*2\D*1|\w*?3\D*2\D*1\D*0" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "顺子", Checked = initChecked, IsAttached = false, Regx = @"\w*?1\D*2\D*3|\w*?5\D*6\D*7|\w*?6\D*7\D*8|\w*?7\D*8\D*9" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "带4顺子", Checked = initChecked, IsAttached = false, Regx = @"\w*?2\D*3\D*4|\w*?3\D*4\D*5|\w*?4\D*5\D*6" }); 
            Rules.Add(new Rule() { GroupName = "数字1", Name = "倒顺", Checked = initChecked, IsAttached = false, Regx = @"\w*?3\D*2\D*1|\w*?7\D*6\D*5|\w*?8\D*7\D*6|\w*?9\D*8\D*7" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "双对", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\w)\w*?(\w)\w*?\1\w*?\2|\w*?(\w)\w*?(\w)\w*?\4\w*?\3|\w*?(\w)\w*?\5\w*?(\w)\w*?\6" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "带4倒顺", Checked = initChecked, IsAttached = false, Regx = @"\w*?4\D*3\D*2|\w*?5\D*4\D*3|\w*?6\D*5\D*4" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "0689对子", Checked = initChecked, IsAttached = false, Regx = @"\w*?0\D*0|\w*?6\D*6|\w*?8\D*8|\w*?9\D*9" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "168、520", Checked = initChecked, IsAttached = false, Regx = @"\w*?1\D*6\D*8|\w*?5\D*2\D*0" });
                       
            List<Rule> att3 = new List<Rule>();
            att3.Add(new Rule() { GroupName = "0689ABA", Name = "含6", Checked = true, IsAttached = true, Regx = "6" });
            att3.Add(new Rule() { GroupName = "0689ABA", Name = "含8", Checked = true, IsAttached = true, Regx = "8" });
            att3.Add(new Rule() { GroupName = "0689ABA", Name = "含9", Checked = true, IsAttached = true, Regx = "9" });
            att3.Add(new Rule() { GroupName = "0689ABA", Name = "含0", Checked = true, IsAttached = true, Regx = "0" });
            Rules.Add(new Rule() { GroupName = "数字1", Name = "0689ABA", Checked = initChecked, IsAttached = false, Children = att3, Regx = @"\w*?{0}\w*?(\d)\w+\1|\w*?(\d)\w*?{1}\w*?\2|\w*?(\d)\w+\1\w*?{2}" });

            Rules.Add(new Rule() { GroupName = "数字1", Name = "尾数689", Checked = initChecked, IsAttached = false, Regx = @"-----------" });
            Rules.Add(No4);
            Rules.Add(new Rule() { GroupName = "数字3", Name = "顺子", Checked = initChecked, IsAttached = false, Regx = @"\w*?1\D*2\D*3|\w*?5\D*6\D*7|\w*?6\D*7\D*8|\w*?7\D*8\D*9" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "倒顺", Checked = initChecked, IsAttached = false, Regx = @"\w*?3\D*2\D*1|\w*?7\D*6\D*5|\w*?8\D*7\D*6|\w*?9\D*8\D*7" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "168", Checked = initChecked, IsAttached = false, Regx = @"\w*?1\D*6\D*8" });            
            Rules.Add(new Rule() { GroupName = "数字3", Name = "520", Checked = initChecked, IsAttached = false, Regx = @"\w*?5\D*2\D*0" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "518", Checked = initChecked, IsAttached = false, Regx = @"\w*?5\D*1\D*8" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "258", Checked = initChecked, IsAttached = false, Regx = @"\w*?2\D*5\D*8" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "369", Checked = initChecked, IsAttached = false, Regx = @"\w*?3\D*6\D*9" });
            Rules.Add(new Rule() { GroupName = "数字3", Name = "521", Checked = initChecked, IsAttached = false, Regx = @"\w*?5\D*2\D*1" });

            List<Rule> att1 = new List<Rule>();
            att1.Add(new Rule() { GroupName = "对子", Name = "含6", Checked = initChecked, IsAttached = true, Regx = "6" });
            att1.Add(new Rule() { GroupName = "对子", Name = "含8", Checked = initChecked, IsAttached = true, Regx = "8" });
            att1.Add(new Rule() { GroupName = "对子", Name = "含9", Checked = initChecked, IsAttached = true, Regx = "9" });
            att1.Add(new Rule() { GroupName = "对子", Name = "含0", Checked = initChecked, IsAttached = true, Regx = "0" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "00", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?0\D*0|\w*?0\D*0\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "11", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?1\D*1|\w*?1\D*1\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "22", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?2\D*2|\w*?2\D*2\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "33", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?3\D*3|\w*?3\D*3\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "44", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?4\D*4|\w*?4\D*4\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "55", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?5\D*5|\w*?5\D*5\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "66", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?6\D*6|\w*?6\D*6\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "77", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?7\D*7|\w*?7\D*7\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "88", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?8\D*8|\w*?8\D*8\w*?{1}", Children = att1 });
            Rules.Add(new Rule() { GroupName = "对子", Name = "99", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?9\D*9|\w*?9\D*9\w*?{1}", Children = att1 });
            Rules.AddRange(att1);

            List<Rule> att2 = new List<Rule>();
            att2.Add(new Rule() { GroupName = "ABA格式", Name = "含6", Checked = initChecked, IsAttached = true, Regx = "6" });
            att2.Add(new Rule() { GroupName = "ABA格式", Name = "含8", Checked = initChecked, IsAttached = true, Regx = "8" });
            att2.Add(new Rule() { GroupName = "ABA格式", Name = "含9", Checked = initChecked, IsAttached = true, Regx = "9" });
            att2.Add(new Rule() { GroupName = "ABA格式", Name = "含0", Checked = initChecked, IsAttached = true, Regx = "0" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "0*0", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?0\w*?0|\w*?0\w*?{1}\w*?0|\w*?0\w*?0\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "1*1", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?1\w*?1|\w*?1\w*?{1}\w*?1|\w*?1\w*?1\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "2*2", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?2\w*?2|\w*?2\w*?{1}\w*?2|\w*?2\w*?2\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "3*3", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?3\w*?3|\w*?3\w*?{1}\w*?3|\w*?3\w*?3\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "4*4", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?4\w*?4|\w*?4\w*?{1}\w*?4|\w*?4\w*?4\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "5*5", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?5\w*?5|\w*?5\w*?{1}\w*?5|\w*?5\w*?5\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "6*6", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?6\w*?6|\w*?6\w*?{1}\w*?6|\w*?6\w*?6\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "7*7", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?7\w*?7|\w*?7\w*?{1}\w*?7|\w*?7\w*?7\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "8*8", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?8\w*?8|\w*?8\w*?{1}\w*?8|\w*?8\w*?8\w*?{2}", Children = att2 });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "9*9", Checked = initChecked, IsAttached = false, Regx = @"\w*?{0}\w*?9\w*?9|\w*?9\w*?{1}\w*?9|\w*?9\w*?9\w*?{2}", Children = att2 });
            Rules.AddRange(att2);

            Rules.Add(new Rule() { GroupName = "特定格式", Name = "6*8", Checked = initChecked, IsAttached = false, Regx = @"\w*?6\w*?8" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "6*9", Checked = initChecked, IsAttached = false, Regx = @"\w*?6\w*?9" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "8*6", Checked = initChecked, IsAttached = false, Regx = @"\w*?8\w*?6" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "8*9", Checked = initChecked, IsAttached = false, Regx = @"\w*?8\w*?9" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "9*8", Checked = initChecked, IsAttached = false, Regx = @"\w*?9\w*?8" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "9*6", Checked = initChecked, IsAttached = false, Regx = @"\w*?9\w*?6" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "1*8", Checked = initChecked, IsAttached = false, Regx = @"\w*?1\w*?8" });
            //Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\d)\D*?(\d)\w*?\1\D*?\2|\w*?(\d)\D*?\3\w*?(\d)\D*?\4" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "双对", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\w)\w*?(\w)\w*?\1\w*?\2|\w*?(\w)\w*?(\w)\w*?\4\w*?\3|\w*?(\w)\w*?\5\w*?(\w)\w*?\6" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对前小后大", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\d)\D*?[0]\w*?\1\D*?[1]|\w*?(\d)\D*?[1]\w*?\2\D*?[2]|\w*?(\d)\D*?[2]\w*?\3\D*?[3]|\w*?(\d)\D*?[3]\w*?\4\D*?[4]|\w*?(\d)\D*?[4]\w*?\5\D*?[5]|\w*?(\d)\D*?[5]\w*?\6\D*?[6]|\w*?(\d)\D*?[6]\w*?\7\D*?[7]|\w*?(\d)\D*?[7]\w*?\8\D*?[8]|\w*?(\d)\D*?[8]\w*?\9\D*?[9]|\w*?\[0]\D*(\d)\w*?\[1]\D*\10|\w*?\[1]\D*(\d)\w*?\[2]\D*\11|\w*?\[2]\D*(\d)\w*?\[3]\D*\12|\w*?\[3]\D*(\d)\w*?\[4]\D*\13|\w*?\[4]\D*(\d)\w*?\[5]\D*\14|\w*?\[5]\D*(\d)\w*?\[6]\D*\15|\w*?\[6]\D*(\d)\w*?\[7]\D*\16|\w*?\[7]\D*(\d)\w*?\[8]\D*\17|\w*?\[8]\D*(\d)\w*?\[9]\D*\18" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对前大后小", Checked = initChecked, IsAttached = false, Regx = @"\w*?(\d)\D*?[9]\w*?\1\D*?[8]|\w*?(\d)\D*?[8]\w*?\2\D*?[7]|\w*?(\d)\D*?[7]\w*?\3\D*?[6]|\w*?(\d)\D*?[6]\w*?\4\D*?[5]|\w*?(\d)\D*?[5]\w*?\5\D*?[4]|\w*?(\d)\D*?[4]\w*?\6\D*?[3]|\w*?(\d)\D*?[3]\w*?\7\D*?[2]|\w*?(\d)\D*?[2]\w*?\8\D*?[1]|\w*?(\d)\D*?[1]\w*?\9\D*?[0]|\w*?\[9]\D*(\d)\w*?\[8]\D*\10|\w*?\[8]\D*(\d)\w*?\[7]\D*\11|\w*?\[7]\D*(\d)\w*?\[6]\D*\12|\w*?\[6]\D*(\d)\w*?\[5]\D*\13|\w*?\[5]\D*(\d)\w*?\[4]\D*\14|\w*?\[4]\D*(\d)\w*?\[3]\D*\15|\w*?\[3]\D*(\d)\w*?\[2]\D*\16|\w*?\[2]\D*(\d)\w*?\[1]\D*\17|\w*?\[1]\D*(\d)\w*?\[0]\D*\18" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "双字母", Checked = initChecked, IsAttached = false, Regx = @"\w*?A\d*A|\w*?B\d*B|\w*?C\d*C|\w*?D\d*D|\w*?E\d*E|\w*?F\d*F|\w*?G\d*G|\w*?H\d*H|\w*?I\d*I|\w*?J\d*J|\w*?K\d*K|\w*?L\d*L|\w*?M\d*M|\w*?N\d*N|\w*?O\d*O|\w*?P\d*P|\w*?Q\d*Q|\w*?R\d*R|\w*?S\d*S|\w*?T\d*T|\w*?U\d*U|\w*?V\d*V|\w*?W\d*W|\w*?X\d*X|\w*?Y\d*Y|\w*?Z\d*Z" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "字母顺子", Checked = initChecked, IsAttached = false, Regx = @"\w*?ABC|\w*?BCD|\w*?CDE|\w*?DEF|\w*?EFG|\w*?FGH|\w*?GHI|\w*?HIJ|\w*?IJK|\w*?JKL|\w*?KLM|\w*?LMN|\w*?MNO|\w*?NOP|\w*?OPQ|\w*?PQR|\w*?QRS|\w*?RST|\w*?STU|\w*?TUV|\w*?UVW|\w*?VWX|\w*?WXY|\w*?XYZ" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "ABA格式字母", Checked = initChecked, IsAttached = false, Regx = @"\w*(\D)\w*\1" });
        }
        public string Name { get; set; }
        public bool Checked
        {
            get;
            set;
        }
        public string Regx { get; set; }
        public string GroupName { get; set; }
        public List<Rule> Children { get; set; }
        public bool IsAttached { get; set; }
    }
}
