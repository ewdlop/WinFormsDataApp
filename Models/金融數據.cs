using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp3.Models
{
    public class 金融數據
    {
        public DateTime 日期 { get; set; }
        public double 開盤價 { get; set; }
        public double 最高價 { get; set; }
        public double 最低價 { get; set; }
        public double 收盤價 { get; set; }
        public double 成交量 { get; set; }
        public double 報酬率 { get; set; }
    }
} 