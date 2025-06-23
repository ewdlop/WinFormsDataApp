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
        private List<金融數據>? 當前數據;
        private 隨機數據生成器? 數據生成器;
        private List<double>? 移動平均值;

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

        private void buttonGenerate_Click(object sender, EventArgs e)
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
                if (comboBoxModel.SelectedIndex == 0) // 幾何布朗運動
                {
                    當前數據 = 數據生成器.生成幾何布朗運動(
                        初始價格, 漂移率, 波動率, 天數, 開始日期);
                }
                else // Merton跳躍擴散
                {
                    當前數據 = 數據生成器.生成默頓跳躍擴散(
                        初始價格, 漂移率, 波動率, 5.0, -0.02, 0.1, 天數, 開始日期);
                }

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

        private void buttonAnalyze_Click(object sender, EventArgs e)
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

        private void pictureBoxChart_Paint(object sender, PaintEventArgs e)
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
    }
}
