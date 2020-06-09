namespace HyperHeuristicAnyTimeAlgorithm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label3 = new System.Windows.Forms.Label();
            this.nupd_times = new System.Windows.Forms.NumericUpDown();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_set = new System.Windows.Forms.Button();
            this.nupd_pointCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chrt_distance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_anyTime = new System.Windows.Forms.Button();
            this.btn_resume = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupd_times)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupd_pointCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrt_distance)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "X Times";
            // 
            // nupd_times
            // 
            this.nupd_times.Enabled = false;
            this.nupd_times.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupd_times.Location = new System.Drawing.Point(106, 223);
            this.nupd_times.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nupd_times.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupd_times.Name = "nupd_times";
            this.nupd_times.Size = new System.Drawing.Size(123, 22);
            this.nupd_times.TabIndex = 0;
            this.nupd_times.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btn_run
            // 
            this.btn_run.Enabled = false;
            this.btn_run.Location = new System.Drawing.Point(134, 260);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(65, 50);
            this.btn_run.TabIndex = 4;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_set
            // 
            this.btn_set.Location = new System.Drawing.Point(132, 124);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(65, 49);
            this.btn_set.TabIndex = 3;
            this.btn_set.Text = "Set";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // nupd_pointCount
            // 
            this.nupd_pointCount.Location = new System.Drawing.Point(134, 85);
            this.nupd_pointCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nupd_pointCount.Name = "nupd_pointCount";
            this.nupd_pointCount.Size = new System.Drawing.Size(63, 22);
            this.nupd_pointCount.TabIndex = 0;
            this.nupd_pointCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivery Location Count";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Goldenrod;
            this.splitContainer1.Panel1.Controls.Add(this.btn_resume);
            this.splitContainer1.Panel1.Controls.Add(this.chrt_distance);
            this.splitContainer1.Panel1.Controls.Add(this.btn_anyTime);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.nupd_times);
            this.splitContainer1.Panel1.Controls.Add(this.btn_run);
            this.splitContainer1.Panel1.Controls.Add(this.btn_set);
            this.splitContainer1.Panel1.Controls.Add(this.nupd_pointCount);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.DarkGray;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1246, 770);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // chrt_distance
            // 
            chartArea2.Name = "ChartArea1";
            this.chrt_distance.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chrt_distance.Legends.Add(legend2);
            this.chrt_distance.Location = new System.Drawing.Point(9, 455);
            this.chrt_distance.Name = "chrt_distance";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Distance";
            this.chrt_distance.Series.Add(series2);
            this.chrt_distance.Size = new System.Drawing.Size(384, 307);
            this.chrt_distance.TabIndex = 0;
            this.chrt_distance.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Distance";
            this.chrt_distance.Titles.Add(title2);
            // 
            // btn_anyTime
            // 
            this.btn_anyTime.Enabled = false;
            this.btn_anyTime.Location = new System.Drawing.Point(116, 385);
            this.btn_anyTime.Name = "btn_anyTime";
            this.btn_anyTime.Size = new System.Drawing.Size(101, 46);
            this.btn_anyTime.TabIndex = 6;
            this.btn_anyTime.Text = "Any Time";
            this.btn_anyTime.UseVisualStyleBackColor = true;
            this.btn_anyTime.Click += new System.EventHandler(this.btn_anyTime_Click);
            // 
            // btn_resume
            // 
            this.btn_resume.Enabled = false;
            this.btn_resume.Location = new System.Drawing.Point(116, 323);
            this.btn_resume.Name = "btn_resume";
            this.btn_resume.Size = new System.Drawing.Size(101, 50);
            this.btn_resume.TabIndex = 7;
            this.btn_resume.Text = "Pause";
            this.btn_resume.UseVisualStyleBackColor = true;
            this.btn_resume.Click += new System.EventHandler(this.btn_resume_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 770);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nupd_times)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupd_pointCount)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chrt_distance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupd_times;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.NumericUpDown nupd_pointCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_anyTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrt_distance;
        private System.Windows.Forms.Button btn_resume;
    }
}

