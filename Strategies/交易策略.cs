using System.Collections.Generic;
using WinFormsApp3.Models;

namespace WinFormsApp3.Strategies
{
    public abstract class 交易策略
    {
        public abstract 交易信號 產生信號(List<金融數據> 數據, int 當前索引);
    }
} 