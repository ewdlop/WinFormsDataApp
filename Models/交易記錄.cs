using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp3.Models
{
    public class 交易記錄
    {
        public DateTime 日期 { get; set; }
        public 交易信號 類型 { get; set; }
        public double 價格 { get; set; }
        public double 數量 { get; set; }
        public double 手續費 { get; set; }
    }
} 