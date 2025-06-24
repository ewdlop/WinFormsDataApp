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
        
        // 滑鼠懸停相關
        private ToolTip 圖表提示框;
        private Point 上次滑鼠位置 = Point.Empty;
        private Point 上次ToolTip位置 = Point.Empty;
        private Point 十字線位置 = Point.Empty;
        private bool 顯示十字線 = false;

        public Form1()
        {
            InitializeComponent();
            初始化();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 確保表單以最大化狀態載入
            //this.WindowState = FormWindowState.Maximized;
            
            // 調整控件位置以適應全屏
            //AdjustControlsForFullScreen();
        }

        //private void AdjustControlsForFullScreen()
        //{
        //    // 重新計算控件位置以適應螢幕大小
        //    int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        //    int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            
        //    // 調整圖表大小
        //    pictureBoxChart.Width = screenWidth - 800; // 為右側統計區域留更多空間
        //    pictureBoxChart.Height = screenHeight - 650; // 為底部TableLayoutPanel留空間
            
        //    // 調整統計標籤位置
        //    labelStats.Left = pictureBoxChart.Right + 20;
        //    labelStats.Width = screenWidth - labelStats.Left - 20;
        //    labelStats.Height = (screenHeight - 650) / 2 - 10;
            
        //    // 調整數據網格位置
        //    dataGridView1.Left = labelStats.Left;
        //    dataGridView1.Top = labelStats.Bottom + 10;
        //    dataGridView1.Width = labelStats.Width;
        //    dataGridView1.Height = labelStats.Height;
            
        //    // 調整TableLayoutPanel位置和大小
        //    panelControls.Top = pictureBoxChart.Bottom + 20;
        //    panelControls.Width = pictureBoxChart.Width;
        //    panelControls.Height = 610; // 固定高度以容納完整的控件
        //}

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 當窗體大小改變時重新調整控件
            if (this.WindowState == FormWindowState.Maximized)
            {
                //AdjustControlsForFullScreen();
            }
            
            // 重繪圖表以適應新尺寸
            更新圖表();
        }

        private void 初始化()
        {
            // 初始化數據生成器
            數據生成器 = new 隨機數據生成器();
            
            // 初始化圖表提示框
            圖表提示框 = new ToolTip();
            圖表提示框.ShowAlways = true;
            圖表提示框.AutoPopDelay = 10000;  // 延長顯示時間
            圖表提示框.InitialDelay = 100;    // 減少初始延遲
            圖表提示框.ReshowDelay = 50;      // 減少重新顯示延遲
            圖表提示框.IsBalloon = false;     // 使用矩形樣式
            圖表提示框.UseAnimation = false;  // 關閉動畫以提高性能
            圖表提示框.UseFading = false;     // 關閉淡入淡出效果
            
            // 設置默認值
            comboBoxModel.SelectedIndex = 0; // 默認選擇幾何布朗運動
            
            // 初始化數據網格
            設置數據網格();
            
            // 確保TableLayoutPanel滾動功能正常
            設置滾動面板();
        }

        private void 設置滾動面板()
        {
            // 確保TableLayoutPanel可以滾動
            panelControls.AutoScroll = true;
            panelControls.AutoScrollMinSize = new Size(panelControls.Width, 600);
            
            // 設定滾動條屬性
            panelControls.HorizontalScroll.Enabled = true;
            panelControls.VerticalScroll.Enabled = true;
            panelControls.HorizontalScroll.Visible = true;
            panelControls.VerticalScroll.Visible = true;
        }

        private void 重置圖表數據()
        {
            // 清除所有數據
            當前數據 = null;
            移動平均值 = null;
            RSI值 = null;
            MACD數據 = null;
            布林通道數據 = null;
            最新回測結果 = null;
            
            // 清除數據網格
            dataGridView1.DataSource = null;
            
            // 清除統計資訊
            labelStats.Text = "請先生成數據";
            
            // 重繪圖表
            pictureBoxChart.Invalidate();
        }

        private void 重置技術指標數據()
        {
            // 只清除技術指標相關數據，保留基礎價格數據
            移動平均值 = null;
            RSI值 = null;
            MACD數據 = null;
            布林通道數據 = null;
            最新回測結果 = null;
            
            // 重繪圖表
            pictureBoxChart.Invalidate();
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

                // 重置圖表數據
                重置圖表數據();

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
                // 重置技術指標數據（保留基礎數據）
                重置技術指標數據();

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
            
            // 如果有技術指標，擴展範圍
            if (布林通道數據.HasValue)
            {
                var 上軌最高 = 布林通道數據.Value.上軌.Where(x => !double.IsNaN(x)).DefaultIfEmpty(最高價格).Max();
                var 下軌最低 = 布林通道數據.Value.下軌.Where(x => !double.IsNaN(x)).DefaultIfEmpty(最低價格).Min();
                最高價格 = Math.Max(最高價格, 上軌最高);
                最低價格 = Math.Min(最低價格, 下軌最低);
            }
            
            double 價格範圍 = 最高價格 - 最低價格;
            if (價格範圍 == 0) 價格範圍 = 1;

            // 繪製網格線
            using (Pen 網格筆 = new Pen(Color.LightGray, 1))
            {
                for (int i = 1; i < 10; i++)
                {
                    int y = 圖表區域.Top + (圖表區域.Height * i / 10);
                    圖形.DrawLine(網格筆, 圖表區域.Left, y, 圖表區域.Right, y);
                }
                
                for (int i = 1; i < 10; i++)
                {
                    int x = 圖表區域.Left + (圖表區域.Width * i / 10);
                    圖形.DrawLine(網格筆, x, 圖表區域.Top, x, 圖表區域.Bottom);
                }
            }

            // 繪製價格軸標籤
            using (Font 字體 = new Font("Microsoft JhengHei", 8))
            using (Brush 文字筆刷 = new SolidBrush(Color.Black))
            {
                for (int i = 0; i <= 5; i++)
                {
                    double 價格 = 最低價格 + (價格範圍 * i / 5);
                    int y = 圖表區域.Bottom - (圖表區域.Height * i / 5);
                    圖形.DrawString(價格.ToString("F1"), 字體, 文字筆刷, 10, y - 6);
                }
            }

            // 準備繪製點陣列
            Point[] 價格點陣列 = new Point[當前數據.Count];
            for (int i = 0; i < 當前數據.Count; i++)
            {
                int x = 圖表區域.Left + (圖表區域.Width * i / (當前數據.Count - 1));
                int y = 圖表區域.Bottom - (int)((當前數據[i].收盤價 - 最低價格) / 價格範圍 * 圖表區域.Height);
                價格點陣列[i] = new Point(x, y);
            }

            // 繪製布林通道（如果有）
            if (布林通道數據.HasValue)
            {
                繪製布林通道(圖形, 圖表區域, 最低價格, 價格範圍);
            }

            // 繪製主要價格線
            if (價格點陣列.Length > 1)
            {
                圖形.DrawLines(new Pen(Color.Blue, 2), 價格點陣列);
            }

            // 繪製移動平均線（如果有）
            if (移動平均值 != null && 移動平均值.Any())
            {
                繪製移動平均線(圖形, 圖表區域, 最低價格, 價格範圍);
            }

            // 繪製EMA線（如果有）
            if (RSI值 != null && RSI值.Any() && checkBoxEMA?.Checked == true)
            {
                // 這裡應該有EMA數據，暫時用移動平均代替演示
                繪製EMA線(圖形, 圖表區域, 最低價格, 價格範圍);
            }

            // 繪製交易信號點（如果有回測結果）
            if (最新回測結果?.交易紀錄 != null)
            {
                繪製交易信號(圖形, 圖表區域, 最低價格, 價格範圍);
            }
            
            // 繪製十字線（最後繪製，確保在最上層）
            if (顯示十字線)
            {
                繪製十字線(圖形, 圖表區域);
            }
        }

        // 繪製布林通道
        private void 繪製布林通道(Graphics 圖形, Rectangle 圖表區域, double 最低價格, double 價格範圍)
        {
            if (!布林通道數據.HasValue) return;

            var (上軌, 中軌, 下軌) = 布林通道數據.Value;
            
            using (Pen 布林筆 = new Pen(Color.Purple, 1))
            {
                // 繪製上軌
                var 上軌點 = new List<Point>();
                var 下軌點 = new List<Point>();
                var 中軌點 = new List<Point>();

                for (int i = 0; i < 當前數據.Count; i++)
                {
                    int x = 圖表區域.Left + (圖表區域.Width * i / (當前數據.Count - 1));
                    
                    if (i < 上軌.Count && !double.IsNaN(上軌[i]))
                    {
                        int y上 = 圖表區域.Bottom - (int)((上軌[i] - 最低價格) / 價格範圍 * 圖表區域.Height);
                        上軌點.Add(new Point(x, y上));
                    }
                    
                    if (i < 下軌.Count && !double.IsNaN(下軌[i]))
                    {
                        int y下 = 圖表區域.Bottom - (int)((下軌[i] - 最低價格) / 價格範圍 * 圖表區域.Height);
                        下軌點.Add(new Point(x, y下));
                    }
                    
                    if (i < 中軌.Count && !double.IsNaN(中軌[i]))
                    {
                        int y中 = 圖表區域.Bottom - (int)((中軌[i] - 最低價格) / 價格範圍 * 圖表區域.Height);
                        中軌點.Add(new Point(x, y中));
                    }
                }

                // 繪製線條
                if (上軌點.Count > 1) 圖形.DrawLines(布林筆, 上軌點.ToArray());
                if (下軌點.Count > 1) 圖形.DrawLines(布林筆, 下軌點.ToArray());
                if (中軌點.Count > 1) 圖形.DrawLines(new Pen(Color.Orange, 1), 中軌點.ToArray());
            }
        }

        // 繪製移動平均線
        private void 繪製移動平均線(Graphics 圖形, Rectangle 圖表區域, double 最低價格, double 價格範圍)
        {
            var MA點陣列 = new List<Point>();
            
            for (int i = 0; i < 移動平均值.Count; i++)
            {
                if (!double.IsNaN(移動平均值[i]))
                {
                    int x = 圖表區域.Left + (圖表區域.Width * i / (當前數據.Count - 1));
                    int y = 圖表區域.Bottom - (int)((移動平均值[i] - 最低價格) / 價格範圍 * 圖表區域.Height);
                    MA點陣列.Add(new Point(x, y));
                }
            }

            if (MA點陣列.Count > 1)
            {
                圖形.DrawLines(new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 
                              MA點陣列.ToArray());
            }
        }

        // 繪製EMA線
        private void 繪製EMA線(Graphics 圖形, Rectangle 圖表區域, double 最低價格, double 價格範圍)
        {
            // 簡化版本，實際應該計算EMA
            if (移動平均值 != null)
            {
                繪製移動平均線(圖形, 圖表區域, 最低價格, 價格範圍);
            }
        }

        // 繪製交易信號
        private void 繪製交易信號(Graphics 圖形, Rectangle 圖表區域, double 最低價格, double 價格範圍)
        {
            foreach (var 交易 in 最新回測結果.交易紀錄)
            {
                // 找到對應的數據點
                var 數據點 = 當前數據.FirstOrDefault(d => d.日期.Date == 交易.日期.Date);
                if (數據點 != null)
                {
                    int 索引 = 當前數據.IndexOf(數據點);
                    int x = 圖表區域.Left + (圖表區域.Width * 索引 / (當前數據.Count - 1));
                    int y = 圖表區域.Bottom - (int)((交易.價格 - 最低價格) / 價格範圍 * 圖表區域.Height);

                    // 繪製交易信號
                    if (交易.類型 == 交易信號.買入)
                    {
                        圖形.FillEllipse(Brushes.Green, x - 5, y - 5, 10, 10);
                        圖形.DrawString("買", new Font("Microsoft JhengHei", 8), Brushes.Green, x - 8, y - 20);
                    }
                    else if (交易.類型 == 交易信號.賣出)
                    {
                        圖形.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                        圖形.DrawString("賣", new Font("Microsoft JhengHei", 8), Brushes.Red, x - 8, y - 20);
                    }
                }
            }
        }

        // 繪製十字線
        private void 繪製十字線(Graphics 圖形, Rectangle 圖表區域)
        {
            // 檢查十字線位置是否在圖表區域內
            if (!圖表區域.Contains(十字線位置))
                return;

            // 使用半透明的十字線，不會太突兀
            using (Pen 十字線筆 = new Pen(Color.FromArgb(150, Color.DarkGray), 1))
            {
                十字線筆.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                
                // 繪製垂直線
                圖形.DrawLine(十字線筆, 十字線位置.X, 圖表區域.Top, 十字線位置.X, 圖表區域.Bottom);
                
                // 繪製水平線
                圖形.DrawLine(十字線筆, 圖表區域.Left, 十字線位置.Y, 圖表區域.Right, 十字線位置.Y);
            }

            // 繪製十字線交叉點的小圓圈，使用更醒目的顏色
            using (Brush 圓圈筆刷 = new SolidBrush(Color.FromArgb(180, Color.Blue)))
            using (Pen 圓圈邊框 = new Pen(Color.White, 1))
            {
                圖形.FillEllipse(圓圈筆刷, 十字線位置.X - 4, 十字線位置.Y - 4, 8, 8);
                圖形.DrawEllipse(圓圈邊框, 十字線位置.X - 4, 十字線位置.Y - 4, 8, 8);
            }
            
            // 繪製邊框上的座標標籤
            if (當前數據 != null && 當前數據.Any())
            {
                繪製十字線座標標籤(圖形, 圖表區域);
            }
        }

        // 繪製十字線座標標籤
        private void 繪製十字線座標標籤(Graphics 圖形, Rectangle 圖表區域)
        {
            try
            {
                // 計算價格範圍
                double 最低價格 = 當前數據.Min(d => d.收盤價);
                double 最高價格 = 當前數據.Max(d => d.收盤價);
                
                // 如果有技術指標，擴展範圍
                if (布林通道數據.HasValue)
                {
                    var 上軌最高 = 布林通道數據.Value.上軌.Where(x => !double.IsNaN(x)).DefaultIfEmpty(最高價格).Max();
                    var 下軌最低 = 布林通道數據.Value.下軌.Where(x => !double.IsNaN(x)).DefaultIfEmpty(最低價格).Min();
                    最高價格 = Math.Max(最高價格, 上軌最高);
                    最低價格 = Math.Min(最低價格, 下軌最低);
                }
                
                double 價格範圍 = 最高價格 - 最低價格;
                if (價格範圍 == 0) 價格範圍 = 1;

                // 計算當前滑鼠位置對應的價格
                double 當前價格 = 最高價格 - ((十字線位置.Y - 圖表區域.Top) / (double)圖表區域.Height) * 價格範圍;
                
                // 計算當前滑鼠位置對應的數據索引和日期
                int 數據索引 = (int)Math.Round((double)(十字線位置.X - 圖表區域.Left) / 圖表區域.Width * (當前數據.Count - 1));
                數據索引 = Math.Max(0, Math.Min(數據索引, 當前數據.Count - 1));
                DateTime 當前日期 = 當前數據[數據索引].日期;

                using (Font 標籤字體 = new Font("Microsoft JhengHei", 8))
                using (Brush 背景筆刷 = new SolidBrush(Color.FromArgb(200, Color.Yellow)))
                using (Brush 文字筆刷 = new SolidBrush(Color.Black))
                using (Pen 邊框筆 = new Pen(Color.Gray))
                {
                    // Y軸價格標籤
                    string 價格文字 = 當前價格.ToString("F2");
                    SizeF 價格尺寸 = 圖形.MeasureString(價格文字, 標籤字體);
                    Rectangle 價格矩形 = new Rectangle(
                        圖表區域.Left + 2,
                        十字線位置.Y - (int)(價格尺寸.Height / 2),
                        (int)價格尺寸.Width + 4,
                        (int)價格尺寸.Height + 2
                    );
                    
                    圖形.FillRectangle(背景筆刷, 價格矩形);
                    圖形.DrawRectangle(邊框筆, 價格矩形);
                    圖形.DrawString(價格文字, 標籤字體, 文字筆刷, 價格矩形.X + 2, 價格矩形.Y + 1);

                    // X軸日期標籤
                    string 日期文字 = 當前日期.ToString("MM-dd");
                    SizeF 日期尺寸 = 圖形.MeasureString(日期文字, 標籤字體);
                    Rectangle 日期矩形 = new Rectangle(
                        十字線位置.X - (int)(日期尺寸.Width / 2),
                        圖表區域.Bottom + 2,
                        (int)日期尺寸.Width + 4,
                        (int)日期尺寸.Height + 2
                    );
                    
                    圖形.FillRectangle(背景筆刷, 日期矩形);
                    圖形.DrawRectangle(邊框筆, 日期矩形);
                    圖形.DrawString(日期文字, 標籤字體, 文字筆刷, 日期矩形.X + 2, 日期矩形.Y + 1);
                }
            }
            catch
            {
                // 如果發生錯誤，忽略座標標籤的繪製
            }
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

        // 新增的JSON匯出事件處理器
        private void ButtonExportJSON_Click(object sender, EventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                MessageBox.Show("請先生成數據再進行匯出！", "警告", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "JSON files (*.json)|*.json";
                    saveDialog.DefaultExt = "json";
                    saveDialog.FileName = $"金融數據_{DateTime.Now:yyyyMMdd_HHmmss}.json";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        數據匯出器.匯出JSON(當前數據, saveDialog.FileName);
                        MessageBox.Show($"JSON數據已成功匯出至：{saveDialog.FileName}", "匯出完成", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"匯出JSON時發生錯誤：{ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 滑鼠懸停事件處理器
        private void PictureBoxChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (當前數據 == null || !當前數據.Any())
            {
                圖表提示框.Hide(pictureBoxChart);
                顯示十字線 = false;
                pictureBoxChart.Invalidate();
                return;
            }

            // 計算圖表區域
            Rectangle 圖表區域 = new Rectangle(60, 30, pictureBoxChart.Width - 80, pictureBoxChart.Height - 60);
            
            // 檢查是否在圖表區域內
            if (圖表區域.Contains(e.Location))
            {
                // 更新十字線位置
                十字線位置 = e.Location;
                顯示十字線 = true;
                
                // 觸發重繪（只在位置變化較大時）
                if (Math.Abs(e.X - 上次滑鼠位置.X) > 2 || Math.Abs(e.Y - 上次滑鼠位置.Y) > 2)
                {
                    pictureBoxChart.Invalidate();
                    上次滑鼠位置 = e.Location;
                }
            }
            else
            {
                // 滑鼠在圖表區域外，隱藏十字線
                if (顯示十字線)
                {
                    顯示十字線 = false;
                    pictureBoxChart.Invalidate();
                }
                圖表提示框.Hide(pictureBoxChart);
                return;
            }

            try
            {
                // 計算數據索引
                int 數據索引 = (int)Math.Round((double)(e.X - 圖表區域.Left) / 圖表區域.Width * (當前數據.Count - 1));
                數據索引 = Math.Max(0, Math.Min(數據索引, 當前數據.Count - 1));

                // 獲取對應的數據點
                金融數據? 數據點 = 當前數據[數據索引];
                
                // 建立提示文字
                StringBuilder 提示文字 = new StringBuilder();
                提示文字.AppendLine($"日期: {數據點.日期:yyyy-MM-dd}");
                提示文字.AppendLine($"收盤價: {數據點.收盤價:F2}");
                提示文字.AppendLine($"開盤價: {數據點.開盤價:F2}");
                提示文字.AppendLine($"最高價: {數據點.最高價:F2}");
                提示文字.AppendLine($"最低價: {數據點.最低價:F2}");
                提示文字.AppendLine($"報酬率: {數據點.報酬率:P2}");
                提示文字.AppendLine($"成交量: {數據點.成交量:N0}");


                // 添加移動平均資訊
                if (移動平均值 != null && 數據索引 < 移動平均值.Count && !double.IsNaN(移動平均值[數據索引]))
                {
                    提示文字.AppendLine($"移動平均: {移動平均值[數據索引]:F2}");
                }

                // 添加技術指標資訊
                if (RSI值 != null && 數據索引 < RSI值.Count && !double.IsNaN(RSI值[數據索引]))
                {
                    提示文字.AppendLine($"RSI: {RSI值[數據索引]:F2}");
                }

                if (MACD數據.HasValue && 數據索引 < MACD數據.Value.MACD.Count && !double.IsNaN(MACD數據.Value.MACD[數據索引]))
                {
                    提示文字.AppendLine($"MACD: {MACD數據.Value.MACD[數據索引]:F4}");
                    if (數據索引 < MACD數據.Value.信號線.Count && !double.IsNaN(MACD數據.Value.信號線[數據索引]))
                    {
                        提示文字.AppendLine($"信號線: {MACD數據.Value.信號線[數據索引]:F4}");
                    }
                }

                if (布林通道數據.HasValue && 數據索引 < 布林通道數據.Value.上軌.Count)
                {
                    var 上軌值 = 布林通道數據.Value.上軌[數據索引];
                    var 下軌值 = 布林通道數據.Value.下軌[數據索引];
                    if (!double.IsNaN(上軌值) && !double.IsNaN(下軌值))
                    {
                        提示文字.AppendLine($"布林上軌: {上軌值:F2}");
                        提示文字.AppendLine($"布林下軌: {下軌值:F2}");
                    }
                }

                // 在滑鼠位置變化較大時或首次進入時更新ToolTip
                if (上次ToolTip位置.IsEmpty || Math.Abs(e.X - 上次ToolTip位置.X) > 5 || Math.Abs(e.Y - 上次ToolTip位置.Y) > 5)
                {
                    // 計算ToolTip位置，避免與十字線重疊
                    int toolTipX = e.X + 15;
                    int toolTipY = e.Y - 10;
                    
                    // 如果ToolTip會超出右邊界，顯示在左側
                    if (toolTipX + 200 > pictureBoxChart.Width)
                    {
                        toolTipX = e.X - 220;
                    }
                    
                    // 如果ToolTip會超出上邊界，顯示在下方
                    if (toolTipY < 0)
                    {
                        toolTipY = e.Y + 20;
                    }
                    
                    // 顯示提示框
                    圖表提示框.Show(提示文字.ToString().TrimEnd(), pictureBoxChart, toolTipX, toolTipY);
                    上次ToolTip位置 = e.Location;
                }
            }
            catch (Exception ex)
            {
                // 如果發生錯誤，隱藏提示框
                圖表提示框.Hide(pictureBoxChart);
            }
        }

        private void PictureBoxChart_MouseLeave(object sender, EventArgs e)
        {
            // 滑鼠離開圖表區域時隱藏提示框和十字線
            圖表提示框.Hide(pictureBoxChart);
            顯示十字線 = false;
            上次滑鼠位置 = Point.Empty;
            上次ToolTip位置 = Point.Empty;
            十字線位置 = Point.Empty;
            pictureBoxChart.Invalidate();
        }
    }
}
