using System.Collections.Generic;
using System.Linq;
using WinFormsApp3.Models;
using WinFormsApp3.Services;

namespace WinFormsApp3.Strategies
{
    // 簡單移動平均交叉策略
    public class 移動平均交叉策略 : 交易策略
    {
        private readonly int _短期週期;
        private readonly int _長期週期;

        public 移動平均交叉策略(int 短期週期 = 5, int 長期週期 = 20)
        {
            _短期週期 = 短期週期;
            _長期週期 = 長期週期;
        }

        public override 交易信號 產生信號(List<金融數據> 數據, int 當前索引)
        {
            if (當前索引 < _長期週期) return 交易信號.持有;

            var 短期MA = 金融分析器.計算移動平均(數據.Take(當前索引 + 1).ToList(), _短期週期);
            var 長期MA = 金融分析器.計算移動平均(數據.Take(當前索引 + 1).ToList(), _長期週期);

            if (短期MA.Count < 2 || 長期MA.Count < 2) return 交易信號.持有;

            double 當前短期 = 短期MA.Last();
            double 當前長期 = 長期MA.Last();
            double 前期短期 = 短期MA[短期MA.Count - 2];
            double 前期長期 = 長期MA[長期MA.Count - 2];

            // 黃金交叉 (短期MA向上穿越長期MA)
            if (前期短期 <= 前期長期 && 當前短期 > 當前長期)
                return 交易信號.買入;

            // 死亡交叉 (短期MA向下穿越長期MA)
            if (前期短期 >= 前期長期 && 當前短期 < 當前長期)
                return 交易信號.賣出;

            return 交易信號.持有;
        }
    }
} 