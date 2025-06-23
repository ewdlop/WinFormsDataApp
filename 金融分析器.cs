namespace WinFormsApp3
{
    public static class 金融分析器
    {
        // 計算移動平均
        public static List<double> 計算移動平均(List<金融數據> 數據, int 週期)
        {
            var 移動平均列表 = new List<double>();
            
            for (int i = 0; i < 數據.Count; i++)
            {
                if (i < 週期 - 1)
                {
                    移動平均列表.Add(double.NaN);
                }
                else
                {
                    double 總和 = 0;
                    for (int j = i - 週期 + 1; j <= i; j++)
                    {
                        總和 += 數據[j].收盤價;
                    }
                    移動平均列表.Add(總和 / 週期);
                }
            }
            
            return 移動平均列表;
        }

        // 計算波動率
        public static double 計算波動率(List<金融數據> 數據)
        {
            var 報酬率列表 = 數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r) && r != 0).ToList();
            if (報酬率列表.Count < 2) return 0;

            double 平均值 = 報酬率列表.Average();
            double 平方差總和 = 報酬率列表.Sum(r => Math.Pow(r - 平均值, 2));
            return Math.Sqrt(平方差總和 / (報酬率列表.Count - 1)) * Math.Sqrt(252); // 年化波動率
        }

        // 計算夏普比率
        public static double 計算夏普比率(List<金融數據> 數據, double 無風險利率 = 0.02)
        {
            var 報酬率列表 = 數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r) && r != 0).ToList();
            if (報酬率列表.Count < 2) return 0;

            double 平均報酬率 = 報酬率列表.Average() * 252; // 年化收益率
            double 波動率 = 計算波動率(數據);
            
            return 波動率 != 0 ? (平均報酬率 - 無風險利率) / 波動率 : 0;
        }
    }
} 