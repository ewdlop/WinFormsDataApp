using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private List<FinancialData>? currentData;
        private StochasticDataGenerator? dataGenerator;
        private List<double>? movingAverages;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 初始化數據生成器
            dataGenerator = new StochasticDataGenerator();
            
            // 設置默認值
            comboBoxModel.SelectedIndex = 0; // 默認選擇幾何布朗運動
            
            // 初始化數據網格
            SetupDataGrid();
        }

        private void SetupDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // 添加欄位
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Date", 
                HeaderText = "日期", 
                DataPropertyName = "Date",
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Close", 
                HeaderText = "收盤價", 
                DataPropertyName = "Close",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" },
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Returns", 
                HeaderText = "報酬率", 
                DataPropertyName = "Returns",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "P4" },
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Volume", 
                HeaderText = "成交量", 
                DataPropertyName = "Volume",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" },
                Width = 100
            });
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGenerator == null) return;

                double initialPrice = (double)numericUpDownInitialPrice.Value;
                double drift = (double)numericUpDownDrift.Value;
                double volatility = (double)numericUpDownVolatility.Value;
                int days = (int)numericUpDownDays.Value;
                DateTime startDate = DateTime.Today.AddDays(-days);

                // 根據選擇的模型生成數據
                if (comboBoxModel.SelectedIndex == 0) // 幾何布朗運動
                {
                    currentData = dataGenerator.GenerateGeometricBrownianMotion(
                        initialPrice, drift, volatility, days, startDate);
                }
                else // Merton跳躍擴散
                {
                    currentData = dataGenerator.GenerateMertonJumpDiffusion(
                        initialPrice, drift, volatility, 5.0, -0.02, 0.1, days, startDate);
                }

                // 更新顯示
                UpdateChart();
                UpdateDataGrid();
                UpdateStatistics();

                MessageBox.Show($"成功生成 {days} 天的金融數據！", "數據生成完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成數據時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            if (currentData == null || !currentData.Any())
            {
                MessageBox.Show("請先生成數據再進行分析！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (checkBoxMovingAverage.Checked)
                {
                    int maPeriod = (int)numericUpDownMA.Value;
                    movingAverages = FinancialAnalyzer.CalculateMovingAverage(currentData, maPeriod);
                }

                UpdateChart();
                UpdateStatistics();
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
            if (currentData == null || !currentData.Any() || dataGenerator == null)
            {
                MessageBox.Show("請先生成數據再添加雜訊！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                double noiseLevel = (double)numericUpDownNoiseLevel.Value;
                dataGenerator.AddWhiteNoise(currentData, noiseLevel);

                // 重新計算收益率
                for (int i = 1; i < currentData.Count; i++)
                {
                    currentData[i].Returns = (currentData[i].Close - currentData[i-1].Close) / currentData[i-1].Close;
                }

                // 更新顯示
                UpdateChart();
                UpdateDataGrid();
                UpdateStatistics();

                MessageBox.Show($"成功添加雜訊水平 {noiseLevel:P1}！", "雜訊添加完成", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加雜訊時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChart()
        {
            if (currentData == null || !currentData.Any()) return;
            pictureBoxChart.Invalidate(); // 觸發重繪
        }

        private void pictureBoxChart_Paint(object sender, PaintEventArgs e)
        {
            if (currentData == null || !currentData.Any()) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 圖表區域
            Rectangle chartArea = new Rectangle(60, 30, pictureBoxChart.Width - 80, pictureBoxChart.Height - 60);
            
            // 背景
            g.FillRectangle(Brushes.White, chartArea);
            g.DrawRectangle(Pens.Black, chartArea);

            // 計算數據範圍
            double minPrice = currentData.Min(d => d.Close);
            double maxPrice = currentData.Max(d => d.Close);
            double priceRange = maxPrice - minPrice;
            if (priceRange == 0) priceRange = 1;

            // 繪製網格線
            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                // 水平網格線
                for (int i = 0; i <= 5; i++)
                {
                    int y = chartArea.Top + (chartArea.Height * i / 5);
                    g.DrawLine(gridPen, chartArea.Left, y, chartArea.Right, y);

                    // Y軸標籤
                    double price = maxPrice - (priceRange * i / 5);
                    g.DrawString(price.ToString("F1"), SystemFonts.DefaultFont, Brushes.Black, 
                        chartArea.Left - 50, y - 7);
                }

                // 垂直網格線
                for (int i = 0; i <= 5; i++)
                {
                    int x = chartArea.Left + (chartArea.Width * i / 5);
                    g.DrawLine(gridPen, x, chartArea.Top, x, chartArea.Bottom);
                }
            }

            // 繪製價格線
            if (currentData.Count > 1)
            {
                List<PointF> pricePoints = new List<PointF>();
                for (int i = 0; i < currentData.Count; i++)
                {
                    float x = chartArea.Left + (float)(chartArea.Width * i / (currentData.Count - 1));
                    float y = chartArea.Bottom - (float)(chartArea.Height * (currentData[i].Close - minPrice) / priceRange);
                    pricePoints.Add(new PointF(x, y));
                }

                if (pricePoints.Count > 1)
                {
                    using (Pen pricePen = new Pen(Color.Blue, 2))
                    {
                        g.DrawLines(pricePen, pricePoints.ToArray());
                    }
                }
            }

            // 繪製移動平均線
            if (movingAverages != null && checkBoxMovingAverage.Checked)
            {
                List<PointF> maPoints = new List<PointF>();
                for (int i = 0; i < currentData.Count; i++)
                {
                    if (!double.IsNaN(movingAverages[i]))
                    {
                        float x = chartArea.Left + (float)(chartArea.Width * i / (currentData.Count - 1));
                        float y = chartArea.Bottom - (float)(chartArea.Height * (movingAverages[i] - minPrice) / priceRange);
                        maPoints.Add(new PointF(x, y));
                    }
                }

                if (maPoints.Count > 1)
                {
                    using (Pen maPen = new Pen(Color.Red, 2))
                    {
                        maPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawLines(maPen, maPoints.ToArray());
                    }
                }
            }

            // 繪製圖例
            int legendY = 10;
            g.FillRectangle(Brushes.Blue, 10, legendY, 15, 3);
            g.DrawString("收盤價", SystemFonts.DefaultFont, Brushes.Black, 30, legendY - 2);

            if (movingAverages != null && checkBoxMovingAverage.Checked)
            {
                using (Pen legendPen = new Pen(Color.Red, 3))
                {
                    legendPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(legendPen, 100, legendY + 1, 115, legendY + 1);
                }
                g.DrawString("移動平均", SystemFonts.DefaultFont, Brushes.Black, 120, legendY - 2);
            }

            // 軸標籤
            g.DrawString("價格", SystemFonts.DefaultFont, Brushes.Black, 10, chartArea.Top + chartArea.Height / 2);
            g.DrawString("時間", SystemFonts.DefaultFont, Brushes.Black, chartArea.Left + chartArea.Width / 2, chartArea.Bottom + 20);
        }

        private void UpdateDataGrid()
        {
            if (currentData == null || !currentData.Any()) return;

            dataGridView1.DataSource = currentData.TakeLast(50).ToList(); // 只顯示最後50筆數據
        }

        private void UpdateStatistics()
        {
            if (currentData == null || !currentData.Any()) return;

            try
            {
                // 計算統計指標
                double currentPrice = currentData.Last().Close;
                double initialPrice = currentData.First().Close;
                double totalReturn = (currentPrice - initialPrice) / initialPrice;
                
                double volatility = FinancialAnalyzer.CalculateVolatility(currentData);
                double sharpeRatio = FinancialAnalyzer.CalculateSharpeRatio(currentData);
                
                double maxPrice = currentData.Max(d => d.Close);
                double minPrice = currentData.Min(d => d.Close);
                double avgVolume = currentData.Average(d => d.Volume);

                // 計算最大回撤
                double maxDrawdown = CalculateMaxDrawdown(currentData);

                // 更新統計標籤
                StringBuilder stats = new StringBuilder();
                stats.AppendLine("=== 統計資訊 ===");
                stats.AppendLine($"數據筆數: {currentData.Count:N0}");
                stats.AppendLine($"期間: {currentData.First().Date:yyyy-MM-dd} 至 {currentData.Last().Date:yyyy-MM-dd}");
                stats.AppendLine();
                stats.AppendLine("=== 價格統計 ===");
                stats.AppendLine($"當前價格: {currentPrice:F2}");
                stats.AppendLine($"初始價格: {initialPrice:F2}");
                stats.AppendLine($"總報酬率: {totalReturn:P2}");
                stats.AppendLine($"最高價格: {maxPrice:F2}");
                stats.AppendLine($"最低價格: {minPrice:F2}");
                stats.AppendLine();
                stats.AppendLine("=== 風險指標 ===");
                stats.AppendLine($"年化波動率: {volatility:P2}");
                stats.AppendLine($"夏普比率: {sharpeRatio:F3}");
                stats.AppendLine($"最大回撤: {maxDrawdown:P2}");
                stats.AppendLine();
                stats.AppendLine("=== 交易統計 ===");
                stats.AppendLine($"平均成交量: {avgVolume:N0}");
                
                // 計算正負報酬率天數
                var positiveReturns = currentData.Where(d => d.Returns > 0).Count();
                var negativeReturns = currentData.Where(d => d.Returns < 0).Count();
                stats.AppendLine($"上漲天數: {positiveReturns}");
                stats.AppendLine($"下跌天數: {negativeReturns}");
                if (positiveReturns + negativeReturns > 0)
                    stats.AppendLine($"勝率: {(double)positiveReturns / (positiveReturns + negativeReturns):P1}");

                labelStats.Text = stats.ToString();
            }
            catch (Exception ex)
            {
                labelStats.Text = $"統計計算錯誤: {ex.Message}";
            }
        }

        private double CalculateMaxDrawdown(List<FinancialData> data)
        {
            double maxDrawdown = 0;
            double peak = data.First().Close;

            foreach (var item in data)
            {
                if (item.Close > peak)
                    peak = item.Close;

                double drawdown = (peak - item.Close) / peak;
                if (drawdown > maxDrawdown)
                    maxDrawdown = drawdown;
            }

            return maxDrawdown;
        }
    }
}
