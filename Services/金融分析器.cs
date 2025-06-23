using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp3.Models;

namespace WinFormsApp3.Services
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

        // 計算VaR (風險價值)
        public static double 計算VaR(List<金融數據> 數據, double 信心水準 = 0.95)
        {
            var 報酬率列表 = 數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r)).OrderBy(r => r).ToList();
            if (報酬率列表.Count == 0) return 0;

            int 索引 = (int)Math.Floor((1 - 信心水準) * 報酬率列表.Count);
            索引 = Math.Max(0, Math.Min(索引, 報酬率列表.Count - 1));
            return -報酬率列表[索引]; // 負值表示損失
        }

        // 計算CVaR (條件風險價值)
        public static double 計算CVaR(List<金融數據> 數據, double 信心水準 = 0.95)
        {
            var 報酬率列表 = 數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r)).OrderBy(r => r).ToList();
            if (報酬率列表.Count == 0) return 0;

            int VaR索引 = (int)Math.Floor((1 - 信心水準) * 報酬率列表.Count);
            if (VaR索引 == 0) return 報酬率列表.Count > 0 ? -報酬率列表[0] : 0;

            double CVaR = 0;
            for (int i = 0; i < VaR索引; i++)
            {
                CVaR += 報酬率列表[i];
            }
            return -CVaR / VaR索引; // 負值表示損失
        }

        // 計算RSI (相對強弱指標)
        public static List<double> 計算RSI(List<金融數據> 數據, int 週期 = 14)
        {
            var RSI列表 = new List<double>();
            if (數據.Count < 週期 + 1)
            {
                return Enumerable.Repeat(double.NaN, 數據.Count).ToList();
            }

            var 價格變化 = new List<double>();
            for (int i = 1; i < 數據.Count; i++)
            {
                價格變化.Add(數據[i].收盤價 - 數據[i - 1].收盤價);
            }

            // 初始化
            RSI列表.Add(double.NaN); // 第一個值無法計算

            for (int i = 0; i < 價格變化.Count; i++)
            {
                if (i < 週期 - 1)
                {
                    RSI列表.Add(double.NaN);
                }
                else
                {
                    double 平均漲幅 = 0;
                    double 平均跌幅 = 0;

                    for (int j = i - 週期 + 1; j <= i; j++)
                    {
                        if (價格變化[j] > 0)
                            平均漲幅 += 價格變化[j];
                        else
                            平均跌幅 += Math.Abs(價格變化[j]);
                    }

                    平均漲幅 /= 週期;
                    平均跌幅 /= 週期;

                    double RS = 平均跌幅 == 0 ? 100 : 平均漲幅 / 平均跌幅;
                    double RSI = 100 - (100 / (1 + RS));
                    RSI列表.Add(RSI);
                }
            }

            return RSI列表;
        }

        // 計算MACD
        public static (List<double> MACD, List<double> 信號線, List<double> 直方圖) 計算MACD(
            List<金融數據> 數據, int 快線週期 = 12, int 慢線週期 = 26, int 信號週期 = 9)
        {
            var 快線EMA = 計算EMA(數據, 快線週期);
            var 慢線EMA = 計算EMA(數據, 慢線週期);
            
            var MACD = new List<double>();
            for (int i = 0; i < 數據.Count; i++)
            {
                if (double.IsNaN(快線EMA[i]) || double.IsNaN(慢線EMA[i]))
                    MACD.Add(double.NaN);
                else
                    MACD.Add(快線EMA[i] - 慢線EMA[i]);
            }

            // 計算信號線 (MACD的EMA)
            var 信號線 = 計算EMA_從列表(MACD, 信號週期);
            
            // 計算直方圖 (MACD - 信號線)
            var 直方圖 = new List<double>();
            for (int i = 0; i < MACD.Count; i++)
            {
                if (double.IsNaN(MACD[i]) || double.IsNaN(信號線[i]))
                    直方圖.Add(double.NaN);
                else
                    直方圖.Add(MACD[i] - 信號線[i]);
            }

            return (MACD, 信號線, 直方圖);
        }

        // 計算EMA (指數移動平均)
        public static List<double> 計算EMA(List<金融數據> 數據, int 週期)
        {
            var EMA列表 = new List<double>();
            if (數據.Count == 0) return EMA列表;

            double α = 2.0 / (週期 + 1);
            
            // 第一個值使用收盤價
            EMA列表.Add(數據[0].收盤價);
            
            for (int i = 1; i < 數據.Count; i++)
            {
                double EMA = α * 數據[i].收盤價 + (1 - α) * EMA列表[i - 1];
                EMA列表.Add(EMA);
            }

            return EMA列表;
        }

        // 從數值列表計算EMA
        private static List<double> 計算EMA_從列表(List<double> 數據, int 週期)
        {
            var EMA列表 = new List<double>();
            if (數據.Count == 0) return EMA列表;

            double α = 2.0 / (週期 + 1);
            
            // 找到第一個非NaN值
            int 起始索引 = 0;
            for (int i = 0; i < 數據.Count; i++)
            {
                if (!double.IsNaN(數據[i]))
                {
                    起始索引 = i;
                    break;
                }
                EMA列表.Add(double.NaN);
            }

            if (起始索引 >= 數據.Count) return EMA列表;

            // 第一個有效值
            EMA列表.Add(數據[起始索引]);
            
            for (int i = 起始索引 + 1; i < 數據.Count; i++)
            {
                if (double.IsNaN(數據[i]))
                {
                    EMA列表.Add(double.NaN);
                }
                else
                {
                    double EMA = α * 數據[i] + (1 - α) * EMA列表[i - 1];
                    EMA列表.Add(EMA);
                }
            }

            return EMA列表;
        }

        // 計算布林通道
        public static (List<double> 上軌, List<double> 中軌, List<double> 下軌) 計算布林通道(
            List<金融數據> 數據, int 週期 = 20, double 標準差倍數 = 2.0)
        {
            var 移動平均 = 計算移動平均(數據, 週期);
            var 上軌 = new List<double>();
            var 下軌 = new List<double>();

            for (int i = 0; i < 數據.Count; i++)
            {
                if (i < 週期 - 1 || double.IsNaN(移動平均[i]))
                {
                    上軌.Add(double.NaN);
                    下軌.Add(double.NaN);
                }
                else
                {
                    // 計算標準差
                    double 平方差總和 = 0;
                    for (int j = i - 週期 + 1; j <= i; j++)
                    {
                        平方差總和 += Math.Pow(數據[j].收盤價 - 移動平均[i], 2);
                    }
                    double 標準差 = Math.Sqrt(平方差總和 / 週期);

                    上軌.Add(移動平均[i] + 標準差倍數 * 標準差);
                    下軌.Add(移動平均[i] - 標準差倍數 * 標準差);
                }
            }

            return (上軌, 移動平均, 下軌);
        }

        // 計算β值 (相對於市場指數)
        public static double 計算Beta(List<金融數據> 股票數據, List<金融數據> 市場數據)
        {
            if (股票數據.Count != 市場數據.Count || 股票數據.Count < 2) return 0;

            var 股票報酬率 = 股票數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r)).ToList();
            var 市場報酬率 = 市場數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r)).ToList();

            if (股票報酬率.Count != 市場報酬率.Count || 股票報酬率.Count < 2) return 0;

            double 股票平均 = 股票報酬率.Average();
            double 市場平均 = 市場報酬率.Average();

            double 協方差 = 0;
            double 市場變異數 = 0;

            for (int i = 0; i < 股票報酬率.Count; i++)
            {
                協方差 += (股票報酬率[i] - 股票平均) * (市場報酬率[i] - 市場平均);
                市場變異數 += Math.Pow(市場報酬率[i] - 市場平均, 2);
            }

            return 市場變異數 != 0 ? 協方差 / 市場變異數 : 0;
        }

        // 計算最大回撤持續期間
        public static int 計算最大回撤期間(List<金融數據> 數據)
        {
            if (數據.Count == 0) return 0;

            double 累積最高 = 數據[0].收盤價;
            int 最大回撤期間 = 0;
            int 當前回撤期間 = 0;

            foreach (var 項目 in 數據)
            {
                if (項目.收盤價 > 累積最高)
                {
                    累積最高 = 項目.收盤價;
                    當前回撤期間 = 0;
                }
                else
                {
                    當前回撤期間++;
                    最大回撤期間 = Math.Max(最大回撤期間, 當前回撤期間);
                }
            }

            return 最大回撤期間;
        }

        // 簡單回測功能
        public static 回測結果 執行簡單回測(List<金融數據> 數據, WinFormsApp3.Strategies.交易策略 策略, double 初始資金 = 100000)
        {
            var 交易紀錄 = new List<交易記錄>();
            double 當前現金 = 初始資金;
            double 持股數量 = 0;
            double 總手續費 = 0;
            const double 手續費率 = 0.001425; // 0.1425%

            for (int i = 1; i < 數據.Count; i++)
            {
                var 信號 = 策略.產生信號(數據, i);
                
                switch (信號)
                {
                    case 交易信號.買入 when 當前現金 > 數據[i].收盤價:
                    {
                        double 可買股數 = Math.Floor(當前現金 / 數據[i].收盤價 * 0.99); // 保留1%資金
                        if (可買股數 > 0)
                        {
                            double 交易金額 = 可買股數 * 數據[i].收盤價;
                            double 手續費 = 交易金額 * 手續費率;
                            
                            當前現金 -= (交易金額 + 手續費);
                            持股數量 += 可買股數;
                            總手續費 += 手續費;

                            交易紀錄.Add(new 交易記錄
                            {
                                日期 = 數據[i].日期,
                                類型 = 交易信號.買入,
                                價格 = 數據[i].收盤價,
                                數量 = 可買股數,
                                手續費 = 手續費
                            });
                        }

                        break;
                    }

                    case 交易信號.賣出 when 持股數量 > 0:
                    {
                        double 交易金額 = 持股數量 * 數據[i].收盤價;
                        double 手續費 = 交易金額 * 手續費率;
                        
                        當前現金 += (交易金額 - 手續費);
                        總手續費 += 手續費;

                        交易紀錄.Add(new 交易記錄
                        {
                            日期 = 數據[i].日期,
                            類型 = 交易信號.賣出,
                            價格 = 數據[i].收盤價,
                            數量 = 持股數量,
                            手續費 = 手續費
                        });

                        持股數量 = 0;
                        break;
                    }
                }
            }

            // 計算最終價值
            double 最終股票價值 = 持股數量 * 數據.Last().收盤價;
            double 總資產 = 當前現金 + 最終股票價值;
            double 總報酬 = (總資產 - 初始資金) / 初始資金;

            return new 回測結果
            {
                初始資金 = 初始資金,
                最終資產 = 總資產,
                總報酬率 = 總報酬,
                交易次數 = 交易紀錄.Count,
                總手續費 = 總手續費,
                交易紀錄 = 交易紀錄
            };
        }
    }
} 