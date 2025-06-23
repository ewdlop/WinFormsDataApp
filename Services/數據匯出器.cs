using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WinFormsApp3.Models;

namespace WinFormsApp3.Services
{
    public static class 數據匯出器
    {
        // 匯出為CSV格式
        public static void 匯出CSV(List<金融數據> 數據, string 檔案路徑)
        {
            var csv = new StringBuilder();
            
            // 寫入標題行
            csv.AppendLine("日期,開盤價,最高價,最低價,收盤價,成交量,報酬率");
            
            // 寫入數據行
            foreach (var 項目 in 數據)
            {
                csv.AppendLine($"{項目.日期:yyyy-MM-dd}," +
                              $"{項目.開盤價:F2}," +
                              $"{項目.最高價:F2}," +
                              $"{項目.最低價:F2}," +
                              $"{項目.收盤價:F2}," +
                              $"{項目.成交量:F0}," +
                              $"{項目.報酬率:F6}");
            }
            
            File.WriteAllText(檔案路徑, csv.ToString(), Encoding.UTF8);
        }

        // 匯出技術指標為CSV
        public static void 匯出技術指標CSV(
            List<金融數據> 數據, 
            List<double>? 移動平均值 = null,
            List<double>? RSI值 = null,
            (List<double> MACD, List<double> 信號線, List<double> 直方圖)? MACD數據 = null,
            (List<double> 上軌, List<double> 中軌, List<double> 下軌)? 布林通道數據 = null,
            string 檔案路徑 = "技術指標分析.csv")
        {
            var csv = new StringBuilder();
            
            // 建立標題行
            var 標題 = new List<string> { "日期", "收盤價", "報酬率" };
            
            if (移動平均值 != null) 標題.Add("移動平均");
            if (RSI值 != null) 標題.Add("RSI");
            if (MACD數據.HasValue)
            {
                標題.AddRange(new[] { "MACD", "MACD信號線", "MACD直方圖" });
            }
            if (布林通道數據.HasValue)
            {
                標題.AddRange(new[] { "布林上軌", "布林中軌", "布林下軌" });
            }
            
            csv.AppendLine(string.Join(",", 標題));
            
            // 寫入數據行
            for (int i = 0; i < 數據.Count; i++)
            {
                var 行 = new List<string>
                {
                    數據[i].日期.ToString("yyyy-MM-dd"),
                    數據[i].收盤價.ToString("F2"),
                    數據[i].報酬率.ToString("F6")
                };
                
                if (移動平均值 != null)
                    行.Add(i < 移動平均值.Count ? 移動平均值[i].ToString("F2") : "");
                    
                if (RSI值 != null)
                    行.Add(i < RSI值.Count ? RSI值[i].ToString("F2") : "");
                    
                if (MACD數據.HasValue)
                {
                    行.Add(i < MACD數據.Value.MACD.Count ? MACD數據.Value.MACD[i].ToString("F4") : "");
                    行.Add(i < MACD數據.Value.信號線.Count ? MACD數據.Value.信號線[i].ToString("F4") : "");
                    行.Add(i < MACD數據.Value.直方圖.Count ? MACD數據.Value.直方圖[i].ToString("F4") : "");
                }
                
                if (布林通道數據.HasValue)
                {
                    行.Add(i < 布林通道數據.Value.上軌.Count ? 布林通道數據.Value.上軌[i].ToString("F2") : "");
                    行.Add(i < 布林通道數據.Value.中軌.Count ? 布林通道數據.Value.中軌[i].ToString("F2") : "");
                    行.Add(i < 布林通道數據.Value.下軌.Count ? 布林通道數據.Value.下軌[i].ToString("F2") : "");
                }
                
                csv.AppendLine(string.Join(",", 行));
            }
            
            File.WriteAllText(檔案路徑, csv.ToString(), Encoding.UTF8);
        }

        // 匯出回測結果
        public static void 匯出回測結果(回測結果 結果, string 檔案路徑)
        {
            var csv = new StringBuilder();
            
            // 摘要資訊
            csv.AppendLine("=== 回測結果摘要 ===");
            csv.AppendLine($"初始資金,{結果.初始資金:C}");
            csv.AppendLine($"最終資產,{結果.最終資產:C}");
            csv.AppendLine($"總報酬率,{結果.總報酬率:P2}");
            csv.AppendLine($"交易次數,{結果.交易次數}");
            csv.AppendLine($"總手續費,{結果.總手續費:C}");
            csv.AppendLine();
            
            // 交易明細
            csv.AppendLine("=== 交易明細 ===");
            csv.AppendLine("日期,交易類型,價格,數量,手續費,交易金額");
            
            foreach (var 交易 in 結果.交易紀錄)
            {
                double 交易金額 = 交易.價格 * 交易.數量;
                csv.AppendLine($"{交易.日期:yyyy-MM-dd}," +
                              $"{交易.類型}," +
                              $"{交易.價格:F2}," +
                              $"{交易.數量:F0}," +
                              $"{交易.手續費:F2}," +
                              $"{交易金額:F2}");
            }
            
            File.WriteAllText(檔案路徑, csv.ToString(), Encoding.UTF8);
        }

        // 匯出統計報告
        public static void 匯出統計報告(List<金融數據> 數據, string 檔案路徑)
        {
            var csv = new StringBuilder();
            
            // 基本統計
            var 收盤價列表 = 數據.Select(d => d.收盤價).ToList();
            var 報酬率列表 = 數據.Select(d => d.報酬率).Where(r => !double.IsNaN(r) && r != 0).ToList();
            
            csv.AppendLine("=== 金融數據統計報告 ===");
            csv.AppendLine($"數據期間,{數據.First().日期:yyyy-MM-dd} 至 {數據.Last().日期:yyyy-MM-dd}");
            csv.AppendLine($"總天數,{數據.Count}");
            csv.AppendLine();
            
            // 價格統計
            csv.AppendLine("=== 價格統計 ===");
            csv.AppendLine($"初始價格,{收盤價列表.First():F2}");
            csv.AppendLine($"最終價格,{收盤價列表.Last():F2}");
            csv.AppendLine($"最高價格,{收盤價列表.Max():F2}");
            csv.AppendLine($"最低價格,{收盤價列表.Min():F2}");
            csv.AppendLine($"平均價格,{收盤價列表.Average():F2}");
            csv.AppendLine();
            
            // 報酬率統計
            if (報酬率列表.Any())
            {
                csv.AppendLine("=== 報酬率統計 ===");
                csv.AppendLine($"平均日報酬率,{報酬率列表.Average():P4}");
                csv.AppendLine($"最大單日漲幅,{報酬率列表.Max():P4}");
                csv.AppendLine($"最大單日跌幅,{報酬率列表.Min():P4}");
                csv.AppendLine($"報酬率標準差,{計算標準差(報酬率列表):P4}");
                csv.AppendLine($"年化波動率,{金融分析器.計算波動率(數據):P2}");
                csv.AppendLine();
            }
            
            // 風險指標
            csv.AppendLine("=== 風險指標 ===");
            csv.AppendLine($"VaR (95%),{金融分析器.計算VaR(數據, 0.95):P4}");
            csv.AppendLine($"CVaR (95%),{金融分析器.計算CVaR(數據, 0.95):P4}");
            csv.AppendLine($"夏普比率,{金融分析器.計算夏普比率(數據):F3}");
            csv.AppendLine($"最大回撤期間,{金融分析器.計算最大回撤期間(數據)} 天");
            
            File.WriteAllText(檔案路徑, csv.ToString(), Encoding.UTF8);
        }

        private static double 計算標準差(List<double> 數據)
        {
            if (數據.Count < 2) return 0;
            
            double 平均值 = 數據.Average();
            double 平方差總和 = 數據.Sum(x => Math.Pow(x - 平均值, 2));
            return Math.Sqrt(平方差總和 / (數據.Count - 1));
        }

        // 簡化的JSON匯出（用於網頁或API）
        public static void 匯出JSON(List<金融數據> 數據, string 檔案路徑)
        {
            var json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("  \"data\": [");
            
            for (int i = 0; i < 數據.Count; i++)
            {
                var 項目 = 數據[i];
                json.AppendLine("    {");
                json.AppendLine($"      \"date\": \"{項目.日期:yyyy-MM-dd}\",");
                json.AppendLine($"      \"open\": {項目.開盤價:F2},");
                json.AppendLine($"      \"high\": {項目.最高價:F2},");
                json.AppendLine($"      \"low\": {項目.最低價:F2},");
                json.AppendLine($"      \"close\": {項目.收盤價:F2},");
                json.AppendLine($"      \"volume\": {項目.成交量:F0},");
                json.AppendLine($"      \"returns\": {項目.報酬率:F6}");
                json.Append("    }");
                
                if (i < 數據.Count - 1)
                    json.AppendLine(",");
                else
                    json.AppendLine();
            }
            
            json.AppendLine("  ]");
            json.AppendLine("}");
            
            File.WriteAllText(檔案路徑, json.ToString(), Encoding.UTF8);
        }
    }
} 