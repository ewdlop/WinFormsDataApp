namespace WinFormsApp3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                圖表提示框?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxChart = new PictureBox();
            panelControls = new TableLayoutPanel();
            groupBoxGenerator = new GroupBox();
            buttonGenerate = new Button();
            numericUpDownDays = new NumericUpDown();
            numericUpDownVolatility = new NumericUpDown();
            numericUpDownDrift = new NumericUpDown();
            numericUpDownInitialPrice = new NumericUpDown();
            comboBoxModel = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBoxAnalysis = new GroupBox();
            buttonAnalyze = new Button();
            numericUpDownMA = new NumericUpDown();
            checkBoxMovingAverage = new CheckBox();
            groupBoxNoise = new GroupBox();
            buttonAddNoise = new Button();
            numericUpDownNoiseLevel = new NumericUpDown();
            label6 = new Label();
            groupBoxTechnical = new GroupBox();
            checkBoxRSI = new CheckBox();
            checkBoxMACD = new CheckBox();
            checkBoxBollinger = new CheckBox();
            checkBoxEMA = new CheckBox();
            numericUpDownEMA = new NumericUpDown();
            buttonCalculateTechnical = new Button();
            labelEMA = new Label();
            groupBoxBacktest = new GroupBox();
            numericUpDownShortMA = new NumericUpDown();
            numericUpDownLongMA = new NumericUpDown();
            numericUpDownInitialCapital = new NumericUpDown();
            buttonRunBacktest = new Button();
            labelShortMA = new Label();
            labelLongMA = new Label();
            labelInitialCapital = new Label();
            groupBoxExport = new GroupBox();
            buttonExportData = new Button();
            buttonExportTechnical = new Button();
            buttonExportStats = new Button();
            buttonExportJSON = new Button();
            dataGridView1 = new DataGridView();
            labelStats = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxChart).BeginInit();
            panelControls.SuspendLayout();
            groupBoxGenerator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVolatility).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDrift).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInitialPrice).BeginInit();
            groupBoxAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMA).BeginInit();
            groupBoxNoise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNoiseLevel).BeginInit();
            groupBoxTechnical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEMA).BeginInit();
            groupBoxBacktest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShortMA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLongMA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInitialCapital).BeginInit();
            groupBoxExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxChart
            // 
            pictureBoxChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxChart.BackColor = Color.White;
            pictureBoxChart.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxChart.Location = new Point(17, 20);
            pictureBoxChart.Margin = new Padding(4, 5, 4, 5);
            pictureBoxChart.Name = "pictureBoxChart";
            pictureBoxChart.Size = new Size(1534, 965);
            pictureBoxChart.TabIndex = 0;
            pictureBoxChart.TabStop = false;
            pictureBoxChart.Paint += PictureBoxChart_Paint;
            pictureBoxChart.MouseLeave += PictureBoxChart_MouseLeave;
            pictureBoxChart.MouseMove += PictureBoxChart_MouseMove;
            // 
            // panelControls
            // 
            panelControls.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelControls.AutoScroll = true;
            panelControls.AutoScrollMinSize = new Size(1100, 700);
            panelControls.BackColor = Color.LightGray;
            panelControls.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panelControls.ColumnCount = 3;
            panelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.1681557F));
            panelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.37925F));
            panelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            panelControls.Controls.Add(groupBoxGenerator, 0, 0);
            panelControls.Controls.Add(groupBoxAnalysis, 1, 0);
            panelControls.Controls.Add(groupBoxNoise, 2, 0);
            panelControls.Controls.Add(groupBoxTechnical, 0, 1);
            panelControls.Controls.Add(groupBoxBacktest, 1, 1);
            panelControls.Controls.Add(groupBoxExport, 2, 1);
            panelControls.Location = new Point(17, 995);
            panelControls.Margin = new Padding(4, 5, 4, 5);
            panelControls.Name = "panelControls";
            panelControls.RowCount = 2;
            panelControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 376F));
            panelControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            panelControls.Size = new Size(1534, 504);
            panelControls.TabIndex = 1;
            // 
            // groupBoxGenerator
            // 
            groupBoxGenerator.Controls.Add(buttonGenerate);
            groupBoxGenerator.Controls.Add(numericUpDownDays);
            groupBoxGenerator.Controls.Add(numericUpDownVolatility);
            groupBoxGenerator.Controls.Add(numericUpDownDrift);
            groupBoxGenerator.Controls.Add(numericUpDownInitialPrice);
            groupBoxGenerator.Controls.Add(comboBoxModel);
            groupBoxGenerator.Controls.Add(label5);
            groupBoxGenerator.Controls.Add(label4);
            groupBoxGenerator.Controls.Add(label3);
            groupBoxGenerator.Controls.Add(label2);
            groupBoxGenerator.Controls.Add(label1);
            groupBoxGenerator.Dock = DockStyle.Fill;
            groupBoxGenerator.Location = new Point(5, 6);
            groupBoxGenerator.Margin = new Padding(4, 5, 4, 5);
            groupBoxGenerator.Name = "groupBoxGenerator";
            groupBoxGenerator.Padding = new Padding(4, 5, 4, 5);
            groupBoxGenerator.Size = new Size(512, 366);
            groupBoxGenerator.TabIndex = 0;
            groupBoxGenerator.TabStop = false;
            groupBoxGenerator.Text = "隨機過程生成器";
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(8, 324);
            buttonGenerate.Margin = new Padding(4, 5, 4, 5);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(150, 35);
            buttonGenerate.TabIndex = 10;
            buttonGenerate.Text = "生成數據";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += ButtonGenerate_Click;
            // 
            // numericUpDownDays
            // 
            numericUpDownDays.Location = new Point(171, 200);
            numericUpDownDays.Margin = new Padding(4, 5, 4, 5);
            numericUpDownDays.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownDays.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownDays.Name = "numericUpDownDays";
            numericUpDownDays.Size = new Size(243, 31);
            numericUpDownDays.TabIndex = 9;
            numericUpDownDays.Value = new decimal(new int[] { 252, 0, 0, 0 });
            // 
            // numericUpDownVolatility
            // 
            numericUpDownVolatility.DecimalPlaces = 3;
            numericUpDownVolatility.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownVolatility.Location = new Point(171, 150);
            numericUpDownVolatility.Margin = new Padding(4, 5, 4, 5);
            numericUpDownVolatility.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownVolatility.Name = "numericUpDownVolatility";
            numericUpDownVolatility.Size = new Size(243, 31);
            numericUpDownVolatility.TabIndex = 8;
            numericUpDownVolatility.Value = new decimal(new int[] { 20, 0, 0, 131072 });
            // 
            // numericUpDownDrift
            // 
            numericUpDownDrift.DecimalPlaces = 3;
            numericUpDownDrift.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownDrift.Location = new Point(171, 100);
            numericUpDownDrift.Margin = new Padding(4, 5, 4, 5);
            numericUpDownDrift.Maximum = new decimal(new int[] { 50, 0, 0, 131072 });
            numericUpDownDrift.Minimum = new decimal(new int[] { 50, 0, 0, -2147352576 });
            numericUpDownDrift.Name = "numericUpDownDrift";
            numericUpDownDrift.Size = new Size(243, 31);
            numericUpDownDrift.TabIndex = 7;
            numericUpDownDrift.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // numericUpDownInitialPrice
            // 
            numericUpDownInitialPrice.Location = new Point(171, 50);
            numericUpDownInitialPrice.Margin = new Padding(4, 5, 4, 5);
            numericUpDownInitialPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownInitialPrice.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownInitialPrice.Name = "numericUpDownInitialPrice";
            numericUpDownInitialPrice.Size = new Size(243, 31);
            numericUpDownInitialPrice.TabIndex = 6;
            numericUpDownInitialPrice.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // comboBoxModel
            // 
            comboBoxModel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxModel.FormattingEnabled = true;
            comboBoxModel.Items.AddRange(new object[] { "幾何布朗運動", "Merton跳躍擴散" });
            comboBoxModel.Location = new Point(9, 290);
            comboBoxModel.Margin = new Padding(4, 5, 4, 5);
            comboBoxModel.Name = "comboBoxModel";
            comboBoxModel.Size = new Size(241, 33);
            comboBoxModel.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 203);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(106, 25);
            label5.TabIndex = 4;
            label5.Text = "天數 (Days):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 153);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(152, 25);
            label4.TabIndex = 3;
            label4.Text = "波動率 (Volatility):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 103);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(120, 25);
            label3.TabIndex = 2;
            label3.Text = "漂移率 (Drift):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 53);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(140, 25);
            label2.TabIndex = 1;
            label2.Text = "初始價格 (Price):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 260);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(118, 25);
            label1.TabIndex = 0;
            label1.Text = "模型 (Model):";
            // 
            // groupBoxAnalysis
            // 
            groupBoxAnalysis.Controls.Add(buttonAnalyze);
            groupBoxAnalysis.Controls.Add(numericUpDownMA);
            groupBoxAnalysis.Controls.Add(checkBoxMovingAverage);
            groupBoxAnalysis.Dock = DockStyle.Fill;
            groupBoxAnalysis.Location = new Point(526, 6);
            groupBoxAnalysis.Margin = new Padding(4, 5, 4, 5);
            groupBoxAnalysis.Name = "groupBoxAnalysis";
            groupBoxAnalysis.Padding = new Padding(4, 5, 4, 5);
            groupBoxAnalysis.Size = new Size(485, 366);
            groupBoxAnalysis.TabIndex = 1;
            groupBoxAnalysis.TabStop = false;
            groupBoxAnalysis.Text = "技術分析";
            // 
            // buttonAnalyze
            // 
            buttonAnalyze.Location = new Point(71, 120);
            buttonAnalyze.Margin = new Padding(4, 5, 4, 5);
            buttonAnalyze.Name = "buttonAnalyze";
            buttonAnalyze.Size = new Size(143, 30);
            buttonAnalyze.TabIndex = 2;
            buttonAnalyze.Text = "執行分析";
            buttonAnalyze.UseVisualStyleBackColor = true;
            buttonAnalyze.Click += ButtonAnalyze_Click;
            // 
            // numericUpDownMA
            // 
            numericUpDownMA.Location = new Point(9, 83);
            numericUpDownMA.Margin = new Padding(4, 5, 4, 5);
            numericUpDownMA.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownMA.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownMA.Name = "numericUpDownMA";
            numericUpDownMA.Size = new Size(257, 31);
            numericUpDownMA.TabIndex = 1;
            numericUpDownMA.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // checkBoxMovingAverage
            // 
            checkBoxMovingAverage.AutoSize = true;
            checkBoxMovingAverage.Checked = true;
            checkBoxMovingAverage.CheckState = CheckState.Checked;
            checkBoxMovingAverage.Location = new Point(9, 37);
            checkBoxMovingAverage.Margin = new Padding(4, 5, 4, 5);
            checkBoxMovingAverage.Name = "checkBoxMovingAverage";
            checkBoxMovingAverage.Size = new Size(164, 29);
            checkBoxMovingAverage.TabIndex = 0;
            checkBoxMovingAverage.Text = "顯示移動平均線";
            checkBoxMovingAverage.UseVisualStyleBackColor = true;
            // 
            // groupBoxNoise
            // 
            groupBoxNoise.Controls.Add(buttonAddNoise);
            groupBoxNoise.Controls.Add(numericUpDownNoiseLevel);
            groupBoxNoise.Controls.Add(label6);
            groupBoxNoise.Dock = DockStyle.Fill;
            groupBoxNoise.Location = new Point(1020, 6);
            groupBoxNoise.Margin = new Padding(4, 5, 4, 5);
            groupBoxNoise.Name = "groupBoxNoise";
            groupBoxNoise.Padding = new Padding(4, 5, 4, 5);
            groupBoxNoise.Size = new Size(502, 366);
            groupBoxNoise.TabIndex = 2;
            groupBoxNoise.TabStop = false;
            groupBoxNoise.Text = "雜訊生成";
            // 
            // buttonAddNoise
            // 
            buttonAddNoise.Location = new Point(71, 120);
            buttonAddNoise.Margin = new Padding(4, 5, 4, 5);
            buttonAddNoise.Name = "buttonAddNoise";
            buttonAddNoise.Size = new Size(143, 30);
            buttonAddNoise.TabIndex = 2;
            buttonAddNoise.Text = "添加雜訊";
            buttonAddNoise.UseVisualStyleBackColor = true;
            buttonAddNoise.Click += buttonAddNoise_Click;
            // 
            // numericUpDownNoiseLevel
            // 
            numericUpDownNoiseLevel.DecimalPlaces = 3;
            numericUpDownNoiseLevel.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownNoiseLevel.Location = new Point(9, 83);
            numericUpDownNoiseLevel.Margin = new Padding(4, 5, 4, 5);
            numericUpDownNoiseLevel.Maximum = new decimal(new int[] { 10, 0, 0, 131072 });
            numericUpDownNoiseLevel.Name = "numericUpDownNoiseLevel";
            numericUpDownNoiseLevel.Size = new Size(257, 31);
            numericUpDownNoiseLevel.TabIndex = 1;
            numericUpDownNoiseLevel.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 37);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(144, 25);
            label6.TabIndex = 0;
            label6.Text = "雜訊水平 (0-0.1):";
            // 
            // groupBoxTechnical
            // 
            groupBoxTechnical.Controls.Add(checkBoxRSI);
            groupBoxTechnical.Controls.Add(checkBoxMACD);
            groupBoxTechnical.Controls.Add(checkBoxBollinger);
            groupBoxTechnical.Controls.Add(checkBoxEMA);
            groupBoxTechnical.Controls.Add(numericUpDownEMA);
            groupBoxTechnical.Controls.Add(buttonCalculateTechnical);
            groupBoxTechnical.Controls.Add(labelEMA);
            groupBoxTechnical.Dock = DockStyle.Fill;
            groupBoxTechnical.Location = new Point(5, 383);
            groupBoxTechnical.Margin = new Padding(4, 5, 4, 5);
            groupBoxTechnical.Name = "groupBoxTechnical";
            groupBoxTechnical.Padding = new Padding(4, 5, 4, 5);
            groupBoxTechnical.Size = new Size(512, 311);
            groupBoxTechnical.TabIndex = 3;
            groupBoxTechnical.TabStop = false;
            groupBoxTechnical.Text = "技術指標";
            // 
            // checkBoxRSI
            // 
            checkBoxRSI.AutoSize = true;
            checkBoxRSI.Location = new Point(9, 190);
            checkBoxRSI.Margin = new Padding(4, 5, 4, 5);
            checkBoxRSI.Name = "checkBoxRSI";
            checkBoxRSI.Size = new Size(64, 29);
            checkBoxRSI.TabIndex = 6;
            checkBoxRSI.Text = "RSI";
            checkBoxRSI.UseVisualStyleBackColor = true;
            // 
            // checkBoxMACD
            // 
            checkBoxMACD.AutoSize = true;
            checkBoxMACD.Location = new Point(9, 160);
            checkBoxMACD.Margin = new Padding(4, 5, 4, 5);
            checkBoxMACD.Name = "checkBoxMACD";
            checkBoxMACD.Size = new Size(90, 29);
            checkBoxMACD.TabIndex = 5;
            checkBoxMACD.Text = "MACD";
            checkBoxMACD.UseVisualStyleBackColor = true;
            // 
            // checkBoxBollinger
            // 
            checkBoxBollinger.AutoSize = true;
            checkBoxBollinger.Location = new Point(9, 130);
            checkBoxBollinger.Margin = new Padding(4, 5, 4, 5);
            checkBoxBollinger.Name = "checkBoxBollinger";
            checkBoxBollinger.Size = new Size(107, 29);
            checkBoxBollinger.TabIndex = 4;
            checkBoxBollinger.Text = "Bollinger";
            checkBoxBollinger.UseVisualStyleBackColor = true;
            // 
            // checkBoxEMA
            // 
            checkBoxEMA.AutoSize = true;
            checkBoxEMA.Location = new Point(9, 100);
            checkBoxEMA.Margin = new Padding(4, 5, 4, 5);
            checkBoxEMA.Name = "checkBoxEMA";
            checkBoxEMA.Size = new Size(75, 29);
            checkBoxEMA.TabIndex = 3;
            checkBoxEMA.Text = "EMA";
            checkBoxEMA.UseVisualStyleBackColor = true;
            // 
            // numericUpDownEMA
            // 
            numericUpDownEMA.Location = new Point(9, 37);
            numericUpDownEMA.Margin = new Padding(4, 5, 4, 5);
            numericUpDownEMA.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownEMA.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownEMA.Name = "numericUpDownEMA";
            numericUpDownEMA.Size = new Size(257, 31);
            numericUpDownEMA.TabIndex = 2;
            numericUpDownEMA.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // buttonCalculateTechnical
            // 
            buttonCalculateTechnical.Location = new Point(280, 160);
            buttonCalculateTechnical.Margin = new Padding(4, 5, 4, 5);
            buttonCalculateTechnical.Name = "buttonCalculateTechnical";
            buttonCalculateTechnical.Size = new Size(107, 30);
            buttonCalculateTechnical.TabIndex = 7;
            buttonCalculateTechnical.Text = "計算指標";
            buttonCalculateTechnical.UseVisualStyleBackColor = true;
            buttonCalculateTechnical.Click += Button計算技術指標_Click;
            // 
            // labelEMA
            // 
            labelEMA.AutoSize = true;
            labelEMA.Location = new Point(9, 80);
            labelEMA.Margin = new Padding(4, 0, 4, 0);
            labelEMA.Name = "labelEMA";
            labelEMA.Size = new Size(156, 25);
            labelEMA.TabIndex = 0;
            labelEMA.Text = "EMA 天數 (5-200):";
            // 
            // groupBoxBacktest
            // 
            groupBoxBacktest.Controls.Add(numericUpDownShortMA);
            groupBoxBacktest.Controls.Add(numericUpDownLongMA);
            groupBoxBacktest.Controls.Add(numericUpDownInitialCapital);
            groupBoxBacktest.Controls.Add(buttonRunBacktest);
            groupBoxBacktest.Controls.Add(labelShortMA);
            groupBoxBacktest.Controls.Add(labelLongMA);
            groupBoxBacktest.Controls.Add(labelInitialCapital);
            groupBoxBacktest.Dock = DockStyle.Fill;
            groupBoxBacktest.Location = new Point(526, 383);
            groupBoxBacktest.Margin = new Padding(4, 5, 4, 5);
            groupBoxBacktest.Name = "groupBoxBacktest";
            groupBoxBacktest.Padding = new Padding(4, 5, 4, 5);
            groupBoxBacktest.Size = new Size(485, 311);
            groupBoxBacktest.TabIndex = 4;
            groupBoxBacktest.TabStop = false;
            groupBoxBacktest.Text = "回測";
            // 
            // numericUpDownShortMA
            // 
            numericUpDownShortMA.Location = new Point(9, 37);
            numericUpDownShortMA.Margin = new Padding(4, 5, 4, 5);
            numericUpDownShortMA.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownShortMA.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownShortMA.Name = "numericUpDownShortMA";
            numericUpDownShortMA.Size = new Size(257, 31);
            numericUpDownShortMA.TabIndex = 0;
            numericUpDownShortMA.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // numericUpDownLongMA
            // 
            numericUpDownLongMA.Location = new Point(9, 83);
            numericUpDownLongMA.Margin = new Padding(4, 5, 4, 5);
            numericUpDownLongMA.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownLongMA.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownLongMA.Name = "numericUpDownLongMA";
            numericUpDownLongMA.Size = new Size(257, 31);
            numericUpDownLongMA.TabIndex = 1;
            numericUpDownLongMA.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // numericUpDownInitialCapital
            // 
            numericUpDownInitialCapital.Location = new Point(9, 130);
            numericUpDownInitialCapital.Margin = new Padding(4, 5, 4, 5);
            numericUpDownInitialCapital.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownInitialCapital.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownInitialCapital.Name = "numericUpDownInitialCapital";
            numericUpDownInitialCapital.Size = new Size(257, 31);
            numericUpDownInitialCapital.TabIndex = 2;
            numericUpDownInitialCapital.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // buttonRunBacktest
            // 
            buttonRunBacktest.Location = new Point(9, 170);
            buttonRunBacktest.Margin = new Padding(4, 5, 4, 5);
            buttonRunBacktest.Name = "buttonRunBacktest";
            buttonRunBacktest.Size = new Size(143, 30);
            buttonRunBacktest.TabIndex = 6;
            buttonRunBacktest.Text = "執行回測";
            buttonRunBacktest.UseVisualStyleBackColor = true;
            buttonRunBacktest.Click += Button執行回測_Click;
            // 
            // labelShortMA
            // 
            labelShortMA.AutoSize = true;
            labelShortMA.Location = new Point(9, 37);
            labelShortMA.Margin = new Padding(4, 0, 4, 0);
            labelShortMA.Name = "labelShortMA";
            labelShortMA.Size = new Size(178, 25);
            labelShortMA.TabIndex = 0;
            labelShortMA.Text = "短期移動平均線天數:";
            // 
            // labelLongMA
            // 
            labelLongMA.AutoSize = true;
            labelLongMA.Location = new Point(9, 83);
            labelLongMA.Margin = new Padding(4, 0, 4, 0);
            labelLongMA.Name = "labelLongMA";
            labelLongMA.Size = new Size(178, 25);
            labelLongMA.TabIndex = 1;
            labelLongMA.Text = "長期移動平均線天數:";
            // 
            // labelInitialCapital
            // 
            labelInitialCapital.AutoSize = true;
            labelInitialCapital.Location = new Point(9, 130);
            labelInitialCapital.Margin = new Padding(4, 0, 4, 0);
            labelInitialCapital.Name = "labelInitialCapital";
            labelInitialCapital.Size = new Size(88, 25);
            labelInitialCapital.TabIndex = 2;
            labelInitialCapital.Text = "初始資本:";
            // 
            // groupBoxExport
            // 
            groupBoxExport.Controls.Add(buttonExportData);
            groupBoxExport.Controls.Add(buttonExportTechnical);
            groupBoxExport.Controls.Add(buttonExportStats);
            groupBoxExport.Controls.Add(buttonExportJSON);
            groupBoxExport.Dock = DockStyle.Fill;
            groupBoxExport.Location = new Point(1020, 383);
            groupBoxExport.Margin = new Padding(4, 5, 4, 5);
            groupBoxExport.Name = "groupBoxExport";
            groupBoxExport.Padding = new Padding(4, 5, 4, 5);
            groupBoxExport.Size = new Size(502, 311);
            groupBoxExport.TabIndex = 5;
            groupBoxExport.TabStop = false;
            groupBoxExport.Text = "匯出";
            // 
            // buttonExportData
            // 
            buttonExportData.Location = new Point(9, 37);
            buttonExportData.Margin = new Padding(4, 5, 4, 5);
            buttonExportData.Name = "buttonExportData";
            buttonExportData.Size = new Size(186, 50);
            buttonExportData.TabIndex = 0;
            buttonExportData.Text = "匯出數據";
            buttonExportData.UseVisualStyleBackColor = true;
            buttonExportData.Click += Button匯出數據_Click;
            // 
            // buttonExportTechnical
            // 
            buttonExportTechnical.Location = new Point(9, 97);
            buttonExportTechnical.Margin = new Padding(4, 5, 4, 5);
            buttonExportTechnical.Name = "buttonExportTechnical";
            buttonExportTechnical.Size = new Size(186, 50);
            buttonExportTechnical.TabIndex = 1;
            buttonExportTechnical.Text = "匯出技術指標";
            buttonExportTechnical.UseVisualStyleBackColor = true;
            buttonExportTechnical.Click += Button匯出技術指標_Click;
            // 
            // buttonExportStats
            // 
            buttonExportStats.Location = new Point(9, 157);
            buttonExportStats.Margin = new Padding(4, 5, 4, 5);
            buttonExportStats.Name = "buttonExportStats";
            buttonExportStats.Size = new Size(186, 50);
            buttonExportStats.TabIndex = 2;
            buttonExportStats.Text = "匯出統計報告";
            buttonExportStats.UseVisualStyleBackColor = true;
            buttonExportStats.Click += Button產生統計報告_Click;
            // 
            // buttonExportJSON
            // 
            buttonExportJSON.Location = new Point(9, 217);
            buttonExportJSON.Margin = new Padding(4, 5, 4, 5);
            buttonExportJSON.Name = "buttonExportJSON";
            buttonExportJSON.Size = new Size(186, 50);
            buttonExportJSON.TabIndex = 3;
            buttonExportJSON.Text = "匯出JSON";
            buttonExportJSON.UseVisualStyleBackColor = true;
            buttonExportJSON.Click += ButtonExportJSON_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1559, 999);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(714, 500);
            dataGridView1.TabIndex = 2;
            // 
            // labelStats
            // 
            labelStats.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelStats.Font = new Font("Microsoft JhengHei", 9F);
            labelStats.Location = new Point(1559, 20);
            labelStats.Margin = new Padding(4, 0, 4, 0);
            labelStats.Name = "labelStats";
            labelStats.Size = new Size(714, 800);
            labelStats.TabIndex = 3;
            labelStats.Text = "統計資訊將在此顯示";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2286, 1500);
            Controls.Add(labelStats);
            Controls.Add(dataGridView1);
            Controls.Add(panelControls);
            Controls.Add(pictureBoxChart);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1991, 1296);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "金融數據分析工具 - 專業版本 v2.1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBoxChart).EndInit();
            panelControls.ResumeLayout(false);
            groupBoxGenerator.ResumeLayout(false);
            groupBoxGenerator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVolatility).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDrift).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInitialPrice).EndInit();
            groupBoxAnalysis.ResumeLayout(false);
            groupBoxAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMA).EndInit();
            groupBoxNoise.ResumeLayout(false);
            groupBoxNoise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNoiseLevel).EndInit();
            groupBoxTechnical.ResumeLayout(false);
            groupBoxTechnical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEMA).EndInit();
            groupBoxBacktest.ResumeLayout(false);
            groupBoxBacktest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShortMA).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLongMA).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownInitialCapital).EndInit();
            groupBoxExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxChart;
        private TableLayoutPanel panelControls;
        private GroupBox groupBoxGenerator;
        private Button buttonGenerate;
        private NumericUpDown numericUpDownDays;
        private NumericUpDown numericUpDownVolatility;
        private NumericUpDown numericUpDownDrift;
        private NumericUpDown numericUpDownInitialPrice;
        private ComboBox comboBoxModel;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBoxAnalysis;
        private Button buttonAnalyze;
        private NumericUpDown numericUpDownMA;
        private CheckBox checkBoxMovingAverage;
        private GroupBox groupBoxNoise;
        private Button buttonAddNoise;
        private NumericUpDown numericUpDownNoiseLevel;
        private Label label6;
        private GroupBox groupBoxTechnical;
        private CheckBox checkBoxRSI;
        private CheckBox checkBoxMACD;
        private CheckBox checkBoxBollinger;
        private CheckBox checkBoxEMA;
        private NumericUpDown numericUpDownEMA;
        private Button buttonCalculateTechnical;
        private Label labelEMA;
        private GroupBox groupBoxBacktest;
        private NumericUpDown numericUpDownShortMA;
        private NumericUpDown numericUpDownLongMA;
        private NumericUpDown numericUpDownInitialCapital;
        private Button buttonRunBacktest;
        private Label labelShortMA;
        private Label labelLongMA;
        private Label labelInitialCapital;
        private GroupBox groupBoxExport;
        private Button buttonExportData;
        private Button buttonExportTechnical;
        private Button buttonExportStats;
        private Button buttonExportJSON;
        private DataGridView dataGridView1;
        private Label labelStats;
    }
}
