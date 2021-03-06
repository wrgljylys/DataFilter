﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFilter.Entity
{
    public enum LogType { 导入, 筛选, 导出, 线程启动 }
    public class LogInfo
    {
        public LogType LogType { get; set; }
        public string Message { get; set; }
        public int DataCount { get; set; }
        public DateTime Time { get; set; }
        public int RedCount { get; set; }
        public int BlueCount { get; set; }
    }
}
