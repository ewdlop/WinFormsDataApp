using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp3.Models;
using WinFormsApp3.Services;
using WinFormsApp3.Strategies;

namespace WinFormsApp3.Tests
{
    public static class 功能測試程式
    {
        public static void 執行完整功能測試()
        {
            Console.WriteLine("=== 金融分析器完整功能測試 ===");
            Console.WriteLine();

            // 1. 生成測試數據
            Console.WriteLine("1. 生成測試數據...");
            var 數據生成器 = new 隨機數據生成器();
            var 測試數據 = 數據生成器.生成幾何布朗運動(100, 0.05, 0.2, 100, DateTime.Today.AddDays(-100));
            Console.WriteLine($"   生成了 {測試數據.Count} 筆數據");
            Console.WriteLine();

            // 2. 基本統計測試
            Console.WriteLine("2. 基本統計分析...");
            double 波動率 = 金融分析器.計算波動率(測試數據);
            double 夏普比率 = 金融分析器.計算夏普比率(測試數據);
            Console.WriteLine($"   年化波動率: {波動率:P2}");
            Console.WriteLine($"   夏普比率: {夏普比率:F3}");
            Console.WriteLine();

            // 3. 風險指標測試
            Console.WriteLine("3. 風險指標分析...");
            double VaR95 = 金融分析器.計算VaR(測試數據, 0.95);
            double CVaR95 = 金融分析器.計算CVaR(測試數據, 0.95);
            int 最大回撤期間 = 金融分析器.計算最大回撤期間(測試數據);
            Console.WriteLine($"   VaR (95%): {VaR95:P4}");
            Console.WriteLine($"   CVaR (95%): {CVaR95:P4}");
            Console.WriteLine($"   最大回撤期間: {最大回撤期間} 天");
            Console.WriteLine();

            // 4. 技術指標測試
            Console.WriteLine("4. 技術指標計算...");
            
            // RSI
            var RSI值 = 金融分析器.計算RSI(測試數據, 14);
            var 最新RSI = RSI值.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   最新RSI: {最新RSI:F2}");

            // MACD
            var MACD數據 = 金融分析器.計算MACD(測試數據, 12, 26, 9);
            var 最新MACD = MACD數據.MACD.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   最新MACD: {最新MACD:F4}");

            // 布林通道
            var 布林通道 = 金融分析器.計算布林通道(測試數據, 20, 2.0);
            var 最新布林上軌 = 布林通道.上軌.LastOrDefault(x => !double.IsNaN(x));
            var 最新布林下軌 = 布林通道.下軌.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   布林上軌: {最新布林上軌:F2}");
            Console.WriteLine($"   布林下軌: {最新布林下軌:F2}");

            // EMA
            var EMA12 = 金融分析器.計算EMA(測試數據, 12);
            var 最新EMA = EMA12.LastOrDefault();
            Console.WriteLine($"   EMA(12): {最新EMA:F2}");
            Console.WriteLine();

            // 5. 回測功能測試
            Console.WriteLine("5. 回測功能測試...");
            var 移動平均策略 = new 移動平均交叉策略(5, 20);
            var 回測結果 = 金融分析器.執行簡單回測(測試數據, 移動平均策略, 100000);
            
            Console.WriteLine($"   初始資金: {回測結果.初始資金:C0}");
            Console.WriteLine($"   最終資產: {回測結果.最終資產:C0}");
            Console.WriteLine($"   總報酬率: {回測結果.總報酬率:P2}");
            Console.WriteLine($"   交易次數: {回測結果.交易次數}");
            Console.WriteLine($"   總手續費: {回測結果.總手續費:C0}");
            Console.WriteLine();

            // 6. 數據匯出測試
            Console.WriteLine("6. 數據匯出功能測試...");
            try
            {
                string CSV檔案 = "測試數據.csv";
                數據匯出器.匯出CSV(測試數據, CSV檔案);
                Console.WriteLine($"   CSV數據已匯出至: {CSV檔案}");

                string 技術指標檔案 = "技術指標測試.csv";
                數據匯出器.匯出技術指標CSV(測試數據, 
                    金融分析器.計算移動平均(測試數據, 20), 
                    RSI值, MACD數據, 布林通道, 技術指標檔案);
                Console.WriteLine($"   技術指標已匯出至: {技術指標檔案}");

                string 統計報告檔案 = "統計報告測試.csv";
                數據匯出器.匯出統計報告(測試數據, 統計報告檔案);
                Console.WriteLine($"   統計報告已匯出至: {統計報告檔案}");

                string 回測報告檔案 = "回測結果測試.csv";
                數據匯出器.匯出回測結果(回測結果, 回測報告檔案);
                Console.WriteLine($"   回測結果已匯出至: {回測報告檔案}");

                string JSON檔案 = "測試數據.json";
                數據匯出器.匯出JSON(測試數據, JSON檔案);
                Console.WriteLine($"   JSON數據已匯出至: {JSON檔案}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   匯出錯誤: {ex.Message}");
            }
            Console.WriteLine();

            // 7. 進階分析功能測試
            Console.WriteLine("7. 進階分析功能測試...");
            
            // 生成第二個數據集作為市場指數
            var 市場數據 = 數據生成器.生成幾何布朗運動(100, 0.03, 0.15, 100, DateTime.Today.AddDays(-100));
            
            if (測試數據.Count == 市場數據.Count)
            {
                double Beta值 = 金融分析器.計算Beta(測試數據, 市場數據);
                Console.WriteLine($"   Beta值 (相對市場): {Beta值:F3}");
            }

            // 測試雜訊添加功能
            var 原始價格 = 測試數據.Last().收盤價;
            數據生成器.添加白雜訊(測試數據, 0.01); // 1%雜訊
            var 雜訊後價格 = 測試數據.Last().收盤價;
            Console.WriteLine($"   雜訊前價格: {原始價格:F2}");
            Console.WriteLine($"   雜訊後價格: {雜訊後價格:F2}");
            Console.WriteLine();

            // 8. 性能測試
            Console.WriteLine("8. 性能測試...");
            var 計時器 = System.Diagnostics.Stopwatch.StartNew();
            
            // 大數據集測試
            var 大數據集 = 數據生成器.生成幾何布朗運動(100, 0.05, 0.2, 1000, DateTime.Today.AddDays(-1000));
            計時器.Stop();
            Console.WriteLine($"   生成1000筆數據耗時: {計時器.ElapsedMilliseconds} 毫秒");

            計時器.Restart();
            var 大數據RSI = 金融分析器.計算RSI(大數據集, 14);
            var 大數據MACD = 金融分析器.計算MACD(大數據集, 12, 26, 9);
            var 大數據布林 = 金融分析器.計算布林通道(大數據集, 20, 2.0);
            計時器.Stop();
            Console.WriteLine($"   計算大數據集技術指標耗時: {計時器.ElapsedMilliseconds} 毫秒");

            Console.WriteLine();
            Console.WriteLine("=== 所有功能測試完成 ===");
        }

        public static void 展示交易策略效果()
        {
            Console.WriteLine("=== 交易策略效果比較 ===");
            
            var 數據生成器 = new 隨機數據生成器();
            var 測試數據 = 數據生成器.生成幾何布朗運動(100, 0.08, 0.25, 252, DateTime.Today.AddDays(-252));

            Console.WriteLine($"測試期間: {測試數據.First().日期:yyyy-MM-dd} 至 {測試數據.Last().日期:yyyy-MM-dd}");
            Console.WriteLine($"初始價格: {測試數據.First().收盤價:F2}");
            Console.WriteLine($"最終價格: {測試數據.Last().收盤價:F2}");
            
            double 買入持有報酬 = (測試數據.Last().收盤價 - 測試數據.First().收盤價) / 測試數據.First().收盤價;
            Console.WriteLine($"買入持有策略報酬率: {買入持有報酬:P2}");
            Console.WriteLine();

            // 測試不同移動平均參數
            var 策略組合 = new List<(int 短期, int 長期, string 名稱)>
            {
                (5, 10, "激進策略(5,10)"),
                (5, 20, "平衡策略(5,20)"),
                (10, 30, "保守策略(10,30)"),
                (20, 50, "長期策略(20,50)")
            };

            foreach (var (短期, 長期, 名稱) in 策略組合)
            {
                if (測試數據.Count > 長期)
                {
                    var 策略 = new 移動平均交叉策略(短期, 長期);
                    var 結果 = 金融分析器.執行簡單回測(測試數據, 策略, 100000);
                    
                    Console.WriteLine($"{名稱}:");
                    Console.WriteLine($"  總報酬率: {結果.總報酬率:P2}");
                    Console.WriteLine($"  交易次數: {結果.交易次數}");
                    Console.WriteLine($"  手續費: {結果.總手續費:C0}");
                    
                    double 相對表現 = 結果.總報酬率 - 買入持有報酬;
                    Console.WriteLine($"  相對表現: {相對表現:P2}");
                    Console.WriteLine();
                }
            }
        }

        public static void 風險分析示例()
        {
            Console.WriteLine("=== 風險分析示例 ===");
            
            var 數據生成器 = new 隨機數據生成器();
            
            // 生成不同風險特性的資產
            var 低風險資產 = 數據生成器.生成幾何布朗運動(100, 0.03, 0.10, 252, DateTime.Today.AddDays(-252));
            var 中風險資產 = 數據生成器.生成幾何布朗運動(100, 0.06, 0.20, 252, DateTime.Today.AddDays(-252));
            var 高風險資產 = 數據生成器.生成幾何布朗運動(100, 0.10, 0.35, 252, DateTime.Today.AddDays(-252));

            var 資產列表 = new List<(List<金融數據> 數據, string 名稱)>
            {
                (低風險資產, "低風險資產"),
                (中風險資產, "中風險資產"),
                (高風險資產, "高風險資產")
            };

            foreach (var (數據, 名稱) in 資產列表)
            {
                Console.WriteLine($"{名稱}:");
                Console.WriteLine($"  年化波動率: {金融分析器.計算波動率(數據):P2}");
                Console.WriteLine($"  夏普比率: {金融分析器.計算夏普比率(數據):F3}");
                Console.WriteLine($"  VaR(95%): {金融分析器.計算VaR(數據, 0.95):P2}");
                Console.WriteLine($"  CVaR(95%): {金融分析器.計算CVaR(數據, 0.95):P2}");
                Console.WriteLine($"  最大回撤期間: {金融分析器.計算最大回撤期間(數據)} 天");
                
                double 總報酬 = (數據.Last().收盤價 - 數據.First().收盤價) / 數據.First().收盤價;
                Console.WriteLine($"  總報酬率: {總報酬:P2}");
                Console.WriteLine();
            }
        }

        public static void 測試新增功能()
        {
            Console.WriteLine("=== 新增功能測試 ===");
            
            var 數據生成器 = new 隨機數據生成器();
            var 測試數據 = 數據生成器.生成幾何布朗運動(100, 0.05, 0.2, 100, DateTime.Today.AddDays(-100));
            
            Console.WriteLine("1. 測試技術指標計算:");
            
            // RSI測試
            var RSI值 = 金融分析器.計算RSI(測試數據, 14);
            var 最新RSI = RSI值.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   RSI(14): {最新RSI:F2}");
            
            // MACD測試
            var MACD數據 = 金融分析器.計算MACD(測試數據, 12, 26, 9);
            var 最新MACD = MACD數據.MACD.LastOrDefault(x => !double.IsNaN(x));
            var 最新信號線 = MACD數據.信號線.LastOrDefault(x => !double.IsNaN(x));
            var 最新直方圖 = MACD數據.直方圖.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   MACD: {最新MACD:F4}");
            Console.WriteLine($"   信號線: {最新信號線:F4}");
            Console.WriteLine($"   直方圖: {最新直方圖:F4}");
            
            // 布林通道測試
            var 布林通道 = 金融分析器.計算布林通道(測試數據, 20, 2.0);
            var 最新上軌 = 布林通道.上軌.LastOrDefault(x => !double.IsNaN(x));
            var 最新中軌 = 布林通道.中軌.LastOrDefault(x => !double.IsNaN(x));
            var 最新下軌 = 布林通道.下軌.LastOrDefault(x => !double.IsNaN(x));
            Console.WriteLine($"   布林上軌: {最新上軌:F2}");
            Console.WriteLine($"   布林中軌: {最新中軌:F2}");
            Console.WriteLine($"   布林下軌: {最新下軌:F2}");
            
            Console.WriteLine();
            Console.WriteLine("2. 測試回測功能:");
            
            var 策略 = new 移動平均交叉策略(5, 20);
            var 回測結果 = 金融分析器.執行簡單回測(測試數據, 策略, 100000);
            
            Console.WriteLine($"   總報酬率: {回測結果.總報酬率:P2}");
            Console.WriteLine($"   交易次數: {回測結果.交易次數}");
            Console.WriteLine($"   手續費: {回測結果.總手續費:C0}");
            
            Console.WriteLine();
            Console.WriteLine("3. 測試匯出功能:");
            
            try
            {
                // 測試CSV匯出
                數據匯出器.匯出CSV(測試數據, "測試_基本數據.csv");
                Console.WriteLine("   ✓ CSV基本數據匯出成功");
                
                // 測試技術指標匯出
                數據匯出器.匯出技術指標CSV(測試數據, 
                    金融分析器.計算移動平均(測試數據, 20), 
                    RSI值, MACD數據, 布林通道, "測試_技術指標.csv");
                Console.WriteLine("   ✓ 技術指標匯出成功");
                
                // 測試統計報告匯出
                數據匯出器.匯出統計報告(測試數據, "測試_統計報告.csv");
                Console.WriteLine("   ✓ 統計報告匯出成功");
                
                // 測試回測結果匯出
                數據匯出器.匯出回測結果(回測結果, "測試_回測結果.csv");
                Console.WriteLine("   ✓ 回測結果匯出成功");
                
                // 測試JSON匯出
                數據匯出器.匯出JSON(測試數據, "測試_數據.json");
                Console.WriteLine("   ✓ JSON數據匯出成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ 匯出測試失敗: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("4. 圖表功能測試:");
            Console.WriteLine("   新增功能包括:");
            Console.WriteLine("   - 布林通道視覺化");
            Console.WriteLine("   - 交易信號標記");
            Console.WriteLine("   - 多層次技術指標");
            Console.WriteLine("   - 改進的網格和標籤");
            
            Console.WriteLine();
            Console.WriteLine("=== 新增功能測試完成 ===");
        }

        public static void 展示表單新功能()
        {
            Console.WriteLine("=== 表單新功能展示 ===");
            
            Console.WriteLine("新增的控件組包括:");
            Console.WriteLine();
            
            Console.WriteLine("1. 技術指標組 (groupBoxTechnical):");
            Console.WriteLine("   - RSI指標選擇");
            Console.WriteLine("   - MACD指標選擇");
            Console.WriteLine("   - 布林通道選擇");
            Console.WriteLine("   - EMA指標選擇和參數設定");
            Console.WriteLine("   - 計算技術指標按鈕");
            
            Console.WriteLine();
            Console.WriteLine("2. 回測組 (groupBoxBacktest):");
            Console.WriteLine("   - 短期移動平均天數設定");
            Console.WriteLine("   - 長期移動平均天數設定");
            Console.WriteLine("   - 初始資本設定");
            Console.WriteLine("   - 執行回測按鈕");
            
            Console.WriteLine();
            Console.WriteLine("3. 匯出組 (groupBoxExport):");
            Console.WriteLine("   - 匯出基本數據按鈕");
            Console.WriteLine("   - 匯出技術指標按鈕");
            Console.WriteLine("   - 匯出統計報告按鈕");
            Console.WriteLine("   - 匯出JSON格式按鈕");
            
            Console.WriteLine();
            Console.WriteLine("4. 圖表增強功能:");
            Console.WriteLine("   - 多層次技術指標顯示");
            Console.WriteLine("   - 布林通道視覺化");
            Console.WriteLine("   - 交易信號點標記");
            Console.WriteLine("   - 改進的座標軸和網格");
            Console.WriteLine("   - 動態範圍調整");
            
            Console.WriteLine();
            Console.WriteLine("使用建議:");
            Console.WriteLine("1. 先生成數據");
            Console.WriteLine("2. 選擇需要的技術指標並計算");
            Console.WriteLine("3. 設定回測參數並執行");
            Console.WriteLine("4. 查看圖表上的視覺化結果");
            Console.WriteLine("5. 選擇需要的格式匯出數據");
            
            Console.WriteLine();
            Console.WriteLine("=== 表單新功能展示完成 ===");
        }
    }
} 