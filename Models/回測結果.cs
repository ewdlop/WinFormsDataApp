using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp3.Models
{
    public class 回測結果
    {
        public double 初始資金 { get; set; }
        public double 最終資產 { get; set; }
        public double 總報酬率 { get; set; }
        public int 交易次數 { get; set; }
        public double 總手續費 { get; set; }
        public List<交易記錄> 交易紀錄 { get; set; } = new List<交易記錄>();
    }
} 