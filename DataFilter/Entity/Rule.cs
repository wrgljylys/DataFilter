using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFilter.Entity
{
    public class Rule
    {
        public static List<Rule> Rules;

        static Rule()
        {
            Rules = new List<Rule>();
            Rules.Add(new Rule() { GroupName = "数字", Name = "444.456.345.234", Checked = false, IsAttached = false, Regx = @"\D*4\D*4\D*4\D*|\D*4\D*5\D*6\D*|\D*3\D*4\D*5\D*|\D*2\D*3\D*4\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "654.543.432.488.466.499.400.004", Checked = false, IsAttached = false, Regx = @"\D*6\D*5\D*4\D*|\D*5\D*4\D*3\D*|\D*4\D*3\D*2\D*|\D*4\D*8\D*8\D*|\D*4\D*6\D*6\D*|\D*4\D*9\D*9\D*|\D*4\D*0\D*0\D*|\D*0\D*0\D*4\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "三同", Checked = false, IsAttached = false, Regx = @"\w*?0\w*?0\w*?0\w*?|\w*?1\w*?1\w*?1\w*?|\w*?2\w*?2\w*?2\w*?|\w*?3\w*?3\w*?3\w*?|\w*?4\w*?4\w*?4\w*?|\w*?5\w*?5\w*?5\w*?|\w*?6\w*?6\w*?6\w*?|\w*?7\w*?7\w*?7\w*?|\w*?8\w*?8\w*?8\w*?|\w*?9\w*?9\w*?9\w*?" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "顺子", Checked = false, IsAttached = false, Regx = @"\D*0\D*1\D*2\D*|\D*1\D*2\D*3\D*|\D*2\D*3\D*4\D*|\D*3\D*4\D*5\D*|\D*4\D*5\D*6\D*|\D*5\D*6\D*7\D*|\D*6\D*7\D*8\D*|\D*7\D*8\D*9\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "其他数字", Checked = false, IsAttached = false, Regx = "" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "168", Checked = false, IsAttached = false,  Regx = @"\D*1\D*6\D*8\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "518", Checked = false, IsAttached = false, Regx = @"\D*5\D*1\D*8\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "520", Checked = false, IsAttached = false, Regx = @"\D*5\D*2\D*0\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "258", Checked = false, IsAttached = false, Regx = @"\D*2\D*5\D*8\D*" });
            Rules.Add(new Rule() { GroupName = "数字", Name = "369", Checked = false, IsAttached = false, Regx = @"\D*3\D*6\D*9\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "987", Checked = false, IsAttached = false, Regx = @"\D*9\D*8\D*7\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "876", Checked = false, IsAttached = false, Regx = @"\D*8\D*7\D*6\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "765", Checked = false, IsAttached = false, Regx = @"\D*7\D*6\D*5\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "654", Checked = false, IsAttached = false, Regx = @"\D*6\D*5\D*4\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "543", Checked = false, IsAttached = false, Regx = @"\D*5\D*4\D*3\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "432", Checked = false, IsAttached = false, Regx = @"\D*4\D*3\D*2\D*" });
            Rules.Add(new Rule() { GroupName = "倒顺子", Name = "321", Checked = false, IsAttached = false, Regx = @"\D*3\D*2\D*1\D*" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "含6", Checked = false, IsAttached = true,  Regx = "6" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "含8", Checked = false, IsAttached = true,  Regx = "8" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "含9", Checked = false, IsAttached = true,  Regx = "9" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "含0", Checked = false, IsAttached = true,  Regx = "0" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "00", Checked = false, IsAttached = false,  Regx = "00" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "11", Checked = false, IsAttached = false,  Regx = "11" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "22", Checked = false, IsAttached = false,  Regx = "22" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "33", Checked = false, IsAttached = false,  Regx = "33" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "44", Checked = false, IsAttached = false,  Regx = "44" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "55", Checked = false, IsAttached = false,  Regx = "55" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "66", Checked = false, IsAttached = false,  Regx = "66" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "77", Checked = false, IsAttached = false,  Regx = "77" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "88", Checked = false, IsAttached = false,  Regx = "88" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "99", Checked = false, IsAttached = false,  Regx = "99" });
            Rules.Add(new Rule() { GroupName = "对子", Name = "00", Checked = false, IsAttached = false,  Regx = "00" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "含6", Checked = false, IsAttached = true, Regx = "6" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "含8", Checked = false, IsAttached = true, Regx = "8" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "含9", Checked = false, IsAttached = true, Regx = "9" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "含0", Checked = false, IsAttached = true, Regx = "0" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "0*0", Checked = false, IsAttached = false, Regx = @"0\w*?0" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "1*1", Checked = false, IsAttached = false, Regx = @"1\w*?1" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "2*2", Checked = false, IsAttached = false, Regx = @"2\w*?2" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "3*3", Checked = false, IsAttached = false, Regx = @"3\w*?3" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "4*4", Checked = false, IsAttached = false, Regx = @"4\w*?4" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "5*5", Checked = false, IsAttached = false, Regx = @"5\w*?5" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "6*6", Checked = false, IsAttached = false, Regx = @"6\w*?6" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "7*7", Checked = false, IsAttached = false, Regx = @"7\w*?7" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "8*8", Checked = false, IsAttached = false, Regx = @"8\w*?8" });
            Rules.Add(new Rule() { GroupName = "ABA格式", Name = "9*9", Checked = false, IsAttached = false, Regx = @"9\w*?9" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "6*8", Checked = false, IsAttached = false,  Regx = @"6\w*?8" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "6*9", Checked = false, IsAttached = false, Regx = @"6\w*?9" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "8*6", Checked = false, IsAttached = false, Regx = @"8\w*?6" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "8*9", Checked = false, IsAttached = false, Regx = @"8\w*?9" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "9*8", Checked = false, IsAttached = false, Regx = @"9\w*?8" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "9*6", Checked = false, IsAttached = false, Regx = @"9\w*?6" });
            Rules.Add(new Rule() { GroupName = "特定格式", Name = "1*8", Checked = false, IsAttached = false, Regx = @"1\w*?8" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对", Checked = false, IsAttached = false, Regx = @"\w*?(\d)\w*?\1\w*?(\d)\2|\w*?(\d)\w*?(\d)\w*?\1\w*?\2|\w*?(\d)\w*?(\d)\w*?\2\w*?\1" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对前小后大", Checked = false, IsAttached = false, Regx = @"\w*?(\d)\w*?[0]\w*?\1\w*?[1]|\w*?(\d)\w*?[1]\w*?\1\w*?[2]|\w*?(\d)\w*?[2]\w*?\1\w*?[3]|\w*?(\d)\w*?[3]\w*?\1\w*?[4]|\w*?(\d)\w*?[4]\w*?\1\w*?[5]|\w*?(\d)\w*?[5]\w*?\1\w*?[6]|\w*?(\d)\w*?[6]\w*?\1\w*?[7]|\w*?(\d)\w*?[7]\w*?\1\w*?[8]|\w*?(\d)\w*?[8]\w*?\1\w*?[9]" });
            Rules.Add(new Rule() { GroupName = "其他格式", Name = "数字双对前大后小", Checked = false, IsAttached = false, Regx = @"\w*?(\d)\w*?[9]\w*?\1\w*?[8]|\w*?(\d)\w*?[8]\w*?\1\w*?[7]|\w*?(\d)\w*?[7]\w*?\1\w*?[6]|\w*?(\d)\w*?[6]\w*?\1\w*?[5]|\w*?(\d)\w*?[5]\w*?\1\w*?[4]|\w*?(\d)\w*?[4]\w*?\1\w*?[3]|\w*?(\d)\w*?[3]\w*?\1\w*?[2]|\w*?(\d)\w*?[2]\w*?\1\w*?[1]|\w*?(\d)\w*?[1]\w*?\1\w*?[0]" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "双字母", Checked = false, IsAttached = false, Regx = "AA|BB|CC|DD|EE|FF|GG|HH|II|JJ|KK|LL|MM|NN|OO|PP|QQ|RR|SS|TT|UU|VV|WW|XX|YY|ZZ" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "字母顺子", Checked = false, IsAttached = false, Regx = @"ABC|BCD|CDE|DEF|EFG|FGH|GHI|HIJ|IJK|JKL|KLM|LMN|MNO|NOP|OPQ|PQR|RST|STU|TUV|UVW|VWX|WXY|XYZ" });
            Rules.Add(new Rule() { GroupName = "字母", Name = "ABA格式字母", Checked = false, IsAttached = false, Regx = @"A\w*?A|B\w*?B|C\w*?C|D\w*?D|E\w*?E|F\w*?F|G\w*?G|H\w*?H|I\w*?I|J\w*?J|K\w*?K|L\w*?L|M\w*?M|N\w*?N|O\w*?O|P\w*?P|Q\w*?Q|R\w*?R|S\w*?S|T\w*?T|U\w*?U|V\w*?V|W\w*?W|X\w*?X|Y\w*?Y|Z\w*?Z" });
        }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Regx { get; set; }
        public string GroupName { get; set; }
        public List<Rule> Children { get; set; }
        public bool IsAttached { get; set; }
    }
}
