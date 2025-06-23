using System.Data;
using System.Text;
using WinFormsApp3.Models;
using WinFormsApp3.Services;
using WinFormsApp3.Strategies;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private List<金融數據>? 當前數據;
        private 隨機數據生成器? 數據生成器;
        private List<double>? 移動平均值;
        
        // 新增欄位
        private List<double>? RSI值;
        private (List<double> MACD, List<double> 信號線, List<double> 直方圖)? MACD數據;
        private (List<double> 上軌, List<double> 中軌, List<double> 下軌)? 布林通道數據;
        private 回測結果? 最新回測結果;

        public Form1()
        {
            InitializeComponent();
            初始化();
        }

        private void 初始化()
        {
            // 初始化數據生成器
            數據生成器 = new 隨機數據生成器();
            
            // 設置默認值
            comboBoxModel.SelectedIndex = 0; // 默認選擇幾何布朗運動
            
            // 初始化數據網格
            設置數據網格();
        }

        private void 設置數據網格()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // 添加欄位
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Date", 
                HeaderText = "日期", 
                DataPropertyName = "日期",
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Close", 
                HeaderText = "收盤價", 
                DataPropertyName = "收盤價",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" },
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Returns", 
                HeaderText = "報酬率", 
                DataPropertyName = "報酬率",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "P4" },
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Volume", 
                HeaderText = "成交量", 
                DataPropertyName = "成交量",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" },
                Width = 100
            });
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (數據生成器 == null) return;

                double 初始價格 = (double)numericUpDownInitialPrice.Value;
                double 漂移率 = (double)numericUpDownDrift.Value;
                double 波動率 = (double)numericUpDownVolatility.Value;
                int 天數 = (int)numericUpDownDays.Value;
                DateTime 開始日期 = DateTime.Today.AddDays(-天數);

                // 根據選擇的模型生成數據
                當前數據 = comboBoxModel.SelectedIndex switch // 幾何布朗運動
                {
                    0 => 數據生成器.生成幾何布朗運動(初始價格, 漂移率, 波動率, 天數, 開始日期),
                    _ => 數據生成器.生成默頓跳躍擴散(初始價格, 漂移率, 波動率, 5.0, -0.02, 0.1, 天數, 開始日期),
                };

                // 更新顯示
                更新圖表();
                更新數據網格();
                更新統計資訊();

                MessageBox.Show($"成功生成 {天數} 天的金融數據！", "數據生成完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成數據時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據再進行分析！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (checkBoxMovingAverage.Checked)
                {
                    int 移動平均週期 = (int)numericUpDownMA.Value;
                    移動平均值 = 金融分析器.計算移動平均(當前數據, 移動平均週期);
                }

                更新圖表();
                更新統計資訊();
                MessageBox.Show("技術分析完成！", "分析完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"分析時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddNoise_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any() || 數據生成器 == null)
            {
                MessageBox.Show("請先生成數據再添加雜訊！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                double 雜訊水平 = (double)numericUpDownNoiseLevel.Value;
                數據生成器.添加白雜訊(當前數據, 雜訊水平);

                // 重新計算收益率
                for (int i = 1; i < 當前數據.Count; i++)
                {
                    當前數據[i].報酬率 = (當前數據[i].收盤價 - 當前數據[i-1].收盤價) / 當前數據[i-1].收盤價;
                }

                // 更新顯示
                更新圖表();
                更新數據網格();
                更新統計資訊();

                MessageBox.Show($"成功添加雜訊水平 {雜訊水平:P1}！", "雜訊添加完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加雜訊時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 更新圖表()
        {
            if (當前數據 == null || !當前數據.Any()) return;
            pictureBoxChart.Invalidate(); // 觸發重繪
        }

        private void PictureBoxChart_Paint(object sender, PaintEventArgs e)
        {
            if (當前數據 == null || !當前數據.Any()) return;

            Graphics 圖形 = e.Graphics;
            圖形.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 圖表區域
            Rectangle 圖表區域 = new Rectangle(60, 30, pictureBoxChart.Width - 80, pictureBoxChart.Height - 60);
            
            // 背景
            圖形.FillRectangle(Brushes.White, 圖表區域);
            圖形.DrawRectangle(Pens.Black, 圖表區域);

            // 計算數據範圍
            double 最低價格 = 當前數據.Min(d => d.收盤價);
            double 最高價格 = 當前數據.Max(d => d.收盤價);
            double 價格範圍 = 最高價格 - 最低價格;
            if (價格範圍 == 0) 價格範圍 = 1;

            // 繪製網格線
            using (Pen 網格筆 = new Pen(Color.LightGray, 1))
            {
                // 水平網格線
                for (int i = 0; i <= 5; i++)
                {
                    int y = 圖表區域.Top + (圖表區域.Height * i / 5);
                    圖形.DrawLine(網格筆, 圖表區域.Left, y, 圖表區域.Right, y);

                    // Y軸標籤
                    double 價格 = 最高價格 - (價格範圍 * i / 5);
                    圖形.DrawString(價格.ToString("F1"), SystemFonts.DefaultFont, Brushes.Black, 
                        圖表區域.Left - 50, y - 7);
                }

                // 垂直網格線
                for (int i = 0; i <= 5; i++)
                {
                    int x = 圖表區域.Left + (圖表區域.Width * i / 5);
                    圖形.DrawLine(網格筆, x, 圖表區域.Top, x, 圖表區域.Bottom);
                }
            }

            // 繪製價格線
            if (當前數據.Count > 1)
            {
                List<PointF> 價格點 = new List<PointF>();
                for (int i = 0; i < 當前數據.Count; i++)
                {
                    float x = 圖表區域.Left + (float)(圖表區域.Width * i / (當前數據.Count - 1));
                    float y = 圖表區域.Bottom - (float)(圖表區域.Height * (當前數據[i].收盤價 - 最低價格) / 價格範圍);
                    價格點.Add(new PointF(x, y));
                }

                if (價格點.Count > 1)
                {
                    using (Pen 價格筆 = new Pen(Color.Blue, 2))
                    {
                        圖形.DrawLines(價格筆, 價格點.ToArray());
                    }
                }
            }

            // 繪製移動平均線
            if (移動平均值 != null && checkBoxMovingAverage.Checked)
            {
                List<PointF> 移動平均點 = new List<PointF>();
                for (int i = 0; i < 當前數據.Count; i++)
                {
                    if (!double.IsNaN(移動平均值[i]))
                    {
                        float x = 圖表區域.Left + (float)(圖表區域.Width * i / (當前數據.Count - 1));
                        float y = 圖表區域.Bottom - (float)(圖表區域.Height * (移動平均值[i] - 最低價格) / 價格範圍);
                        移動平均點.Add(new PointF(x, y));
                    }
                }

                if (移動平均點.Count > 1)
                {
                    using (Pen 移動平均筆 = new Pen(Color.Red, 2))
                    {
                        移動平均筆.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        圖形.DrawLines(移動平均筆, 移動平均點.ToArray());
                    }
                }
            }

            // 繪製圖例
            int 圖例Y = 10;
            圖形.FillRectangle(Brushes.Blue, 10, 圖例Y, 15, 3);
            圖形.DrawString("收盤價", SystemFonts.DefaultFont, Brushes.Black, 30, 圖例Y - 2);

            if (移動平均值 != null && checkBoxMovingAverage.Checked)
            {
                using (Pen 圖例筆 = new Pen(Color.Red, 3))
                {
                    圖例筆.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    圖形.DrawLine(圖例筆, 100, 圖例Y + 1, 115, 圖例Y + 1);
                }
                圖形.DrawString("移動平均", SystemFonts.DefaultFont, Brushes.Black, 120, 圖例Y - 2);
            }

            // 軸標籤
            圖形.DrawString("價格", SystemFonts.DefaultFont, Brushes.Black, 10, 圖表區域.Top + 圖表區域.Height / 2);
            圖形.DrawString("時間", SystemFonts.DefaultFont, Brushes.Black, 圖表區域.Left + 圖表區域.Width / 2, 圖表區域.Bottom + 20);
        }

        private void 更新數據網格()
        {
            if (當前數據 == null || !當前數據.Any()) return;

            dataGridView1.DataSource = 當前數據.TakeLast(50).ToList(); // 只顯示最後50筆數據
        }

        private void 更新統計資訊()
        {
            if (當前數據 == null || !當前數據.Any()) return;

            try
            {
                // 計算統計指標
                double 當前價格 = 當前數據.Last().收盤價;
                double 初始價格 = 當前數據.First().收盤價;
                double 總報酬率 = (當前價格 - 初始價格) / 初始價格;
                
                double 波動率 = 金融分析器.計算波動率(當前數據);
                double 夏普比率 = 金融分析器.計算夏普比率(當前數據);
                
                double 最高價格 = 當前數據.Max(d => d.收盤價);
                double 最低價格 = 當前數據.Min(d => d.收盤價);
                double 平均成交量 = 當前數據.Average(d => d.成交量);

                // 計算最大回撤
                double 最大回撤 = 計算最大回撤(當前數據);

                // 更新統計標籤
                StringBuilder 統計資訊 = new StringBuilder();
                統計資訊.AppendLine("=== 統計資訊 ===");
                統計資訊.AppendLine($"數據筆數: {當前數據.Count:N0}");
                統計資訊.AppendLine($"期間: {當前數據.First().日期:yyyy-MM-dd} 至 {當前數據.Last().日期:yyyy-MM-dd}");
                統計資訊.AppendLine();
                統計資訊.AppendLine("=== 價格統計 ===");
                統計資訊.AppendLine($"當前價格: {當前價格:F2}");
                統計資訊.AppendLine($"初始價格: {初始價格:F2}");
                統計資訊.AppendLine($"總報酬率: {總報酬率:P2}");
                統計資訊.AppendLine($"最高價格: {最高價格:F2}");
                統計資訊.AppendLine($"最低價格: {最低價格:F2}");
                統計資訊.AppendLine();
                統計資訊.AppendLine("=== 風險指標 ===");
                統計資訊.AppendLine($"年化波動率: {波動率:P2}");
                統計資訊.AppendLine($"夏普比率: {夏普比率:F3}");
                統計資訊.AppendLine($"最大回撤: {最大回撤:P2}");
                統計資訊.AppendLine();
                統計資訊.AppendLine("=== 交易統計 ===");
                統計資訊.AppendLine($"平均成交量: {平均成交量:N0}");
                
                // 計算正負報酬率天數
                var 上漲天數 = 當前數據.Where(d => d.報酬率 > 0).Count();
                var 下跌天數 = 當前數據.Where(d => d.報酬率 < 0).Count();
                統計資訊.AppendLine($"上漲天數: {上漲天數}");
                統計資訊.AppendLine($"下跌天數: {下跌天數}");
                if (上漲天數 + 下跌天數 > 0)
                    統計資訊.AppendLine($"勝率: {(double)上漲天數 / (上漲天數 + 下跌天數):P1}");

                labelStats.Text = 統計資訊.ToString();
            }
            catch (Exception ex)
            {
                labelStats.Text = $"統計計算錯誤: {ex.Message}";
            }
        }

        private double 計算最大回撤(List<金融數據> 數據)
        {
            double 最大回撤 = 0;
            double 峰值 = 數據.First().收盤價;

            foreach (var 項目 in 數據)
            {
                if (項目.收盤價 > 峰值)
                    峰值 = 項目.收盤價;

                double 回撤 = (峰值 - 項目.收盤價) / 峰值;
                if (回撤 > 最大回撤)
                    最大回撤 = 回撤;
            }

            return 最大回撤;
        }

        // 新增按鈕事件處理器
        private void Button計算技術指標_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 計算RSI
                RSI值 = 金融分析器.計算RSI(當前數據, 14);
                
                // 計算MACD
                MACD數據 = 金融分析器.計算MACD(當前數據, 12, 26, 9);
                
                // 計算布林通道
                布林通道數據 = 金融分析器.計算布林通道(當前數據, 20, 2.0);

                // 更新圖表和統計資訊
                更新圖表();
                更新進階統計資訊();
                
                MessageBox.Show("技術指標計算完成！", "計算完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"計算技術指標時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button執行回測_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 使用簡單移動平均交叉策略
                var 策略 = new 移動平均交叉策略(5, 20);
                最新回測結果 = 金融分析器.執行簡單回測(當前數據, 策略, 100000);

                // 顯示回測結果
                ShowBacktestResults(最新回測結果);
                
                MessageBox.Show("回測完成！請查看結果視窗。", "回測完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"執行回測時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button匯出數據_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var 儲存對話框 = new SaveFileDialog())
            {
                儲存對話框.Filter = "CSV檔案 (*.csv)|*.csv|JSON檔案 (*.json)|*.json";
                儲存對話框.DefaultExt = "csv";
                儲存對話框.FileName = $"金融數據_{DateTime.Now:yyyyMMdd}";

                if (儲存對話框.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string 檔案路徑 = 儲存對話框.FileName;
                        
                        if (檔案路徑.EndsWith(".csv"))
                        {
                            數據匯出器.匯出CSV(當前數據, 檔案路徑);
                        }
                        else if (檔案路徑.EndsWith(".json"))
                        {
                            數據匯出器.匯出JSON(當前數據, 檔案路徑);
                        }

                        MessageBox.Show($"數據已成功匯出至：{檔案路徑}", "匯出完成", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"匯出數據時發生錯誤：{ex.Message}", "錯誤", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Button匯出技術指標_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var 儲存對話框 = new SaveFileDialog())
            {
                儲存對話框.Filter = "CSV檔案 (*.csv)|*.csv";
                儲存對話框.DefaultExt = "csv";
                儲存對話框.FileName = $"技術指標分析_{DateTime.Now:yyyyMMdd}";

                if (儲存對話框.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        數據匯出器.匯出技術指標CSV(當前數據, 移動平均值, RSI值, MACD數據, 布林通道數據, 儲存對話框.FileName);
                        
                        MessageBox.Show($"技術指標已成功匯出至：{儲存對話框.FileName}", "匯出完成", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"匯出技術指標時發生錯誤：{ex.Message}", "錯誤", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Button產生統計報告_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var 儲存對話框 = new SaveFileDialog())
            {
                儲存對話框.Filter = "CSV檔案 (*.csv)|*.csv";
                儲存對話框.DefaultExt = "csv";
                儲存對話框.FileName = $"統計報告_{DateTime.Now:yyyyMMdd}";

                if (儲存對話框.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        數據匯出器.匯出統計報告(當前數據, 儲存對話框.FileName);
                        
                        MessageBox.Show($"統計報告已成功匯出至：{儲存對話框.FileName}", "匯出完成", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"產生統計報告時發生錯誤：{ex.Message}", "錯誤", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ShowBacktestResults(回測結果 結果)
        {
            var 回測視窗 = new Form
            {
                Text = "回測結果",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            var 結果文字 = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("新細明體", 9)
            };

            var 結果內容 = new StringBuilder();
            結果內容.AppendLine("=== 回測結果摘要 ===");
            結果內容.AppendLine($"初始資金: {結果.初始資金:C0}");
            結果內容.AppendLine($"最終資產: {結果.最終資產:C0}");
            結果內容.AppendLine($"總報酬率: {結果.總報酬率:P2}");
            結果內容.AppendLine($"交易次數: {結果.交易次數}");
            結果內容.AppendLine($"總手續費: {結果.總手續費:C0}");
            結果內容.AppendLine();
            
            if (結果.交易紀錄.Any())
            {
                結果內容.AppendLine("=== 最近10筆交易 ===");
                var 最近交易 = 結果.交易紀錄.TakeLast(10);
                foreach (var 交易 in 最近交易)
                {
                    結果內容.AppendLine($"{交易.日期:yyyy-MM-dd} {交易.類型} " +
                                        $"價格:{交易.價格:F2} 數量:{交易.數量:F0} " +
                                        $"手續費:{交易.手續費:F2}");
                }
            }

            結果文字.Text = 結果內容.ToString();
            回測視窗.Controls.Add(結果文字);
            回測視窗.ShowDialog();
        }

        private void 更新進階統計資訊()
        {
            if (當前數據 == null || !當前數據.Any()) return;

            try
            {
                // 原有統計資訊
                double 當前價格 = 當前數據.Last().收盤價;
                double 初始價格 = 當前數據.First().收盤價;
                double 總報酬率 = (當前價格 - 初始價格) / 初始價格;
                
                double 波動率 = 金融分析器.計算波動率(當前數據);
                double 夏普比率 = 金融分析器.計算夏普比率(當前數據);
                double 最大回撤 = 計算最大回撤(當前數據);

                // 新增風險指標
                double VaR95 = 金融分析器.計算VaR(當前數據, 0.95);
                double CVaR95 = 金融分析器.計算CVaR(當前數據, 0.95);
                int 最大回撤期間 = 金融分析器.計算最大回撤期間(當前數據);

                // 技術指標統計
                double 當前RSI = RSI值?.LastOrDefault(x => !double.IsNaN(x)) ?? double.NaN;
                double 當前MACD = MACD數據?.MACD.LastOrDefault(x => !double.IsNaN(x)) ?? double.NaN;

                StringBuilder 統計資訊 = new StringBuilder();
                統計資訊.AppendLine("=== 基本統計 ===");
                統計資訊.AppendLine($"數據筆數: {當前數據.Count:N0}");
                統計資訊.AppendLine($"期間: {當前數據.First().日期:yyyy-MM-dd} 至 {當前數據.Last().日期:yyyy-MM-dd}");
                統計資訊.AppendLine($"當前價格: {當前價格:F2}");
                統計資訊.AppendLine($"總報酬率: {總報酬率:P2}");
                統計資訊.AppendLine();
                
                統計資訊.AppendLine("=== 風險指標 ===");
                統計資訊.AppendLine($"年化波動率: {波動率:P2}");
                統計資訊.AppendLine($"夏普比率: {夏普比率:F3}");
                統計資訊.AppendLine($"最大回撤: {最大回撤:P2}");
                統計資訊.AppendLine($"VaR(95%): {VaR95:P2}");
                統計資訊.AppendLine($"CVaR(95%): {CVaR95:P2}");
                統計資訊.AppendLine($"最大回撤期間: {最大回撤期間} 天");
                統計資訊.AppendLine();
                
                統計資訊.AppendLine("=== 技術指標 ===");
                if (!double.IsNaN(當前RSI))
                    統計資訊.AppendLine($"當前RSI: {當前RSI:F2}");
                if (!double.IsNaN(當前MACD))
                    統計資訊.AppendLine($"當前MACD: {當前MACD:F4}");
                
                if (布林通道數據.HasValue)
                {
                    var 布林上軌 = 布林通道數據.Value.上軌.LastOrDefault(x => !double.IsNaN(x));
                    var 布林下軌 = 布林通道數據.Value.下軌.LastOrDefault(x => !double.IsNaN(x));
                    if (!double.IsNaN(布林上軌) && !double.IsNaN(布林下軌))
                    {
                        統計資訊.AppendLine($"布林上軌: {布林上軌:F2}");
                        統計資訊.AppendLine($"布林下軌: {布林下軌:F2}");
                        double 布林位置 = (當前價格 - 布林下軌) / (布林上軌 - 布林下軌);
                        統計資訊.AppendLine($"布林位置: {布林位置:P1}");
                    }
                }

                labelStats.Text = 統計資訊.ToString();
            }
            catch (Exception ex)
            {
                labelStats.Text = $"統計計算錯誤: {ex.Message}";
            }
        }
    }
}
