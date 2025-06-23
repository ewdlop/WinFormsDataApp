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
            this.pictureBoxChart = new System.Windows.Forms.PictureBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.groupBoxGenerator = new System.Windows.Forms.GroupBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVolatility = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDrift = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInitialPrice = new System.Windows.Forms.NumericUpDown();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxAnalysis = new System.Windows.Forms.GroupBox();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.numericUpDownMA = new System.Windows.Forms.NumericUpDown();
            this.checkBoxMovingAverage = new System.Windows.Forms.CheckBox();
            this.groupBoxNoise = new System.Windows.Forms.GroupBox();
            this.buttonAddNoise = new System.Windows.Forms.Button();
            this.numericUpDownNoiseLevel = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelStats = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChart)).BeginInit();
            this.panelControls.SuspendLayout();
            this.groupBoxGenerator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolatility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDrift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialPrice)).BeginInit();
            this.groupBoxAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMA)).BeginInit();
            this.groupBoxNoise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxChart
            // 
            this.pictureBoxChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxChart.BackColor = System.Drawing.Color.White;
            this.pictureBoxChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxChart.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxChart.Name = "pictureBoxChart";
            this.pictureBoxChart.Size = new System.Drawing.Size(800, 400);
            this.pictureBoxChart.TabIndex = 0;
            this.pictureBoxChart.TabStop = false;
            this.pictureBoxChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxChart_Paint);
            // 
            // panelControls
            // 
            this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControls.Controls.Add(this.groupBoxNoise);
            this.panelControls.Controls.Add(this.groupBoxAnalysis);
            this.panelControls.Controls.Add(this.groupBoxGenerator);
            this.panelControls.Location = new System.Drawing.Point(12, 418);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(800, 200);
            this.panelControls.TabIndex = 1;
            // 
            // groupBoxGenerator
            // 
            this.groupBoxGenerator.Controls.Add(this.buttonGenerate);
            this.groupBoxGenerator.Controls.Add(this.numericUpDownDays);
            this.groupBoxGenerator.Controls.Add(this.numericUpDownVolatility);
            this.groupBoxGenerator.Controls.Add(this.numericUpDownDrift);
            this.groupBoxGenerator.Controls.Add(this.numericUpDownInitialPrice);
            this.groupBoxGenerator.Controls.Add(this.comboBoxModel);
            this.groupBoxGenerator.Controls.Add(this.label5);
            this.groupBoxGenerator.Controls.Add(this.label4);
            this.groupBoxGenerator.Controls.Add(this.label3);
            this.groupBoxGenerator.Controls.Add(this.label2);
            this.groupBoxGenerator.Controls.Add(this.label1);
            this.groupBoxGenerator.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGenerator.Name = "groupBoxGenerator";
            this.groupBoxGenerator.Size = new System.Drawing.Size(300, 190);
            this.groupBoxGenerator.TabIndex = 0;
            this.groupBoxGenerator.TabStop = false;
            this.groupBoxGenerator.Text = "隨機過程生成器";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(200, 150);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(90, 30);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "生成數據";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // numericUpDownDays
            // 
            this.numericUpDownDays.Location = new System.Drawing.Point(120, 120);
            this.numericUpDownDays.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownDays.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.Size = new System.Drawing.Size(170, 23);
            this.numericUpDownDays.TabIndex = 9;
            this.numericUpDownDays.Value = new decimal(new int[] {
            252,
            0,
            0,
            0});
            // 
            // numericUpDownVolatility
            // 
            this.numericUpDownVolatility.DecimalPlaces = 3;
            this.numericUpDownVolatility.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownVolatility.Location = new System.Drawing.Point(120, 90);
            this.numericUpDownVolatility.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownVolatility.Name = "numericUpDownVolatility";
            this.numericUpDownVolatility.Size = new System.Drawing.Size(170, 23);
            this.numericUpDownVolatility.TabIndex = 8;
            this.numericUpDownVolatility.Value = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            // 
            // numericUpDownDrift
            // 
            this.numericUpDownDrift.DecimalPlaces = 3;
            this.numericUpDownDrift.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownDrift.Location = new System.Drawing.Point(120, 60);
            this.numericUpDownDrift.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.numericUpDownDrift.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147352576});
            this.numericUpDownDrift.Name = "numericUpDownDrift";
            this.numericUpDownDrift.Size = new System.Drawing.Size(170, 23);
            this.numericUpDownDrift.TabIndex = 7;
            this.numericUpDownDrift.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // numericUpDownInitialPrice
            // 
            this.numericUpDownInitialPrice.Location = new System.Drawing.Point(120, 30);
            this.numericUpDownInitialPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownInitialPrice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInitialPrice.Name = "numericUpDownInitialPrice";
            this.numericUpDownInitialPrice.Size = new System.Drawing.Size(170, 23);
            this.numericUpDownInitialPrice.TabIndex = 6;
            this.numericUpDownInitialPrice.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Items.AddRange(new object[] {
            "幾何布朗運動",
            "Merton跳躍擴散"});
            this.comboBoxModel.Location = new System.Drawing.Point(6, 150);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(170, 23);
            this.comboBoxModel.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "天數 (Days):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "波動率 (Volatility):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "漂移率 (Drift):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "初始價格 (Price):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "模型 (Model):";
            // 
            // groupBoxAnalysis
            // 
            this.groupBoxAnalysis.Controls.Add(this.buttonAnalyze);
            this.groupBoxAnalysis.Controls.Add(this.numericUpDownMA);
            this.groupBoxAnalysis.Controls.Add(this.checkBoxMovingAverage);
            this.groupBoxAnalysis.Location = new System.Drawing.Point(309, 3);
            this.groupBoxAnalysis.Name = "groupBoxAnalysis";
            this.groupBoxAnalysis.Size = new System.Drawing.Size(200, 190);
            this.groupBoxAnalysis.TabIndex = 1;
            this.groupBoxAnalysis.TabStop = false;
            this.groupBoxAnalysis.Text = "技術分析";
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(50, 150);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(100, 30);
            this.buttonAnalyze.TabIndex = 2;
            this.buttonAnalyze.Text = "執行分析";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.ButtonAnalyze_Click);
            // 
            // numericUpDownMA
            // 
            this.numericUpDownMA.Location = new System.Drawing.Point(6, 50);
            this.numericUpDownMA.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMA.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMA.Name = "numericUpDownMA";
            this.numericUpDownMA.Size = new System.Drawing.Size(180, 23);
            this.numericUpDownMA.TabIndex = 1;
            this.numericUpDownMA.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkBoxMovingAverage
            // 
            this.checkBoxMovingAverage.AutoSize = true;
            this.checkBoxMovingAverage.Checked = true;
            this.checkBoxMovingAverage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMovingAverage.Location = new System.Drawing.Point(6, 22);
            this.checkBoxMovingAverage.Name = "checkBoxMovingAverage";
            this.checkBoxMovingAverage.Size = new System.Drawing.Size(134, 19);
            this.checkBoxMovingAverage.TabIndex = 0;
            this.checkBoxMovingAverage.Text = "顯示移動平均線";
            this.checkBoxMovingAverage.UseVisualStyleBackColor = true;
            // 
            // groupBoxNoise
            // 
            this.groupBoxNoise.Controls.Add(this.buttonAddNoise);
            this.groupBoxNoise.Controls.Add(this.numericUpDownNoiseLevel);
            this.groupBoxNoise.Controls.Add(this.label6);
            this.groupBoxNoise.Location = new System.Drawing.Point(515, 3);
            this.groupBoxNoise.Name = "groupBoxNoise";
            this.groupBoxNoise.Size = new System.Drawing.Size(200, 190);
            this.groupBoxNoise.TabIndex = 2;
            this.groupBoxNoise.TabStop = false;
            this.groupBoxNoise.Text = "雜訊生成";
            // 
            // buttonAddNoise
            // 
            this.buttonAddNoise.Location = new System.Drawing.Point(50, 150);
            this.buttonAddNoise.Name = "buttonAddNoise";
            this.buttonAddNoise.Size = new System.Drawing.Size(100, 30);
            this.buttonAddNoise.TabIndex = 2;
            this.buttonAddNoise.Text = "添加雜訊";
            this.buttonAddNoise.UseVisualStyleBackColor = true;
            this.buttonAddNoise.Click += new System.EventHandler(this.buttonAddNoise_Click);
            // 
            // numericUpDownNoiseLevel
            // 
            this.numericUpDownNoiseLevel.DecimalPlaces = 3;
            this.numericUpDownNoiseLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownNoiseLevel.Location = new System.Drawing.Point(6, 50);
            this.numericUpDownNoiseLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.numericUpDownNoiseLevel.Name = "numericUpDownNoiseLevel";
            this.numericUpDownNoiseLevel.Size = new System.Drawing.Size(180, 23);
            this.numericUpDownNoiseLevel.TabIndex = 1;
            this.numericUpDownNoiseLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "雜訊水平 (0-0.1):";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(830, 418);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(342, 200);
            this.dataGridView1.TabIndex = 2;
            // 
            // labelStats
            // 
            this.labelStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStats.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStats.Location = new System.Drawing.Point(830, 12);
            this.labelStats.Name = "labelStats";
            this.labelStats.Size = new System.Drawing.Size(342, 400);
            this.labelStats.TabIndex = 3;
            this.labelStats.Text = "統計資訊將在此顯示";
            this.labelStats.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 630);
            this.Controls.Add(this.labelStats);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.pictureBoxChart);
            this.MinimumSize = new System.Drawing.Size(1200, 669);
            this.Name = "Form1";
            this.Text = "金融數據分析工具 - 隨機過程生成器";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChart)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.groupBoxGenerator.ResumeLayout(false);
            this.groupBoxGenerator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolatility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDrift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialPrice)).EndInit();
            this.groupBoxAnalysis.ResumeLayout(false);
            this.groupBoxAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMA)).EndInit();
            this.groupBoxNoise.ResumeLayout(false);
            this.groupBoxNoise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxChart;
        private Panel panelControls;
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
        private DataGridView dataGridView1;
        private Label labelStats;
    }
}
