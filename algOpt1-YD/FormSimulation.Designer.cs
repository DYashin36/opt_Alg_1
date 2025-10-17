
namespace algOpt1_YD
{
    partial class FormSimulation
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
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStep = new System.Windows.Forms.Button();
            this.simBox = new System.Windows.Forms.CheckedListBox();
            this.txtSteps = new System.Windows.Forms.TextBox();
            this.btnNSteps = new System.Windows.Forms.Button();
            this.vertexValueGrid = new System.Windows.Forms.DataGridView();
            this.btnWeightsSet = new System.Windows.Forms.Button();
            this.cleanBtn = new System.Windows.Forms.Button();
            this.readyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertexValueGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(855, 410);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(165, 553);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(112, 47);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "Сделать Шаг";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // simBox
            // 
            this.simBox.FormattingEnabled = true;
            this.simBox.Location = new System.Drawing.Point(12, 454);
            this.simBox.Name = "simBox";
            this.simBox.Size = new System.Drawing.Size(131, 174);
            this.simBox.TabIndex = 2;
            this.simBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.simBox_ItemCheck);
            // 
            // txtSteps
            // 
            this.txtSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSteps.Location = new System.Drawing.Point(165, 454);
            this.txtSteps.Name = "txtSteps";
            this.txtSteps.Size = new System.Drawing.Size(112, 27);
            this.txtSteps.TabIndex = 3;
            // 
            // btnNSteps
            // 
            this.btnNSteps.Location = new System.Drawing.Point(165, 500);
            this.btnNSteps.Name = "btnNSteps";
            this.btnNSteps.Size = new System.Drawing.Size(112, 38);
            this.btnNSteps.TabIndex = 4;
            this.btnNSteps.Text = "n шагов";
            this.btnNSteps.UseVisualStyleBackColor = true;
            this.btnNSteps.Click += new System.EventHandler(this.button1_Click);
            // 
            // vertexValueGrid
            // 
            this.vertexValueGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vertexValueGrid.Location = new System.Drawing.Point(887, 12);
            this.vertexValueGrid.Name = "vertexValueGrid";
            this.vertexValueGrid.RowHeadersWidth = 51;
            this.vertexValueGrid.RowTemplate.Height = 24;
            this.vertexValueGrid.Size = new System.Drawing.Size(313, 410);
            this.vertexValueGrid.TabIndex = 5;
            this.vertexValueGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.vertexValueGrid_CellContentClick);
            // 
            // btnWeightsSet
            // 
            this.btnWeightsSet.Location = new System.Drawing.Point(817, 454);
            this.btnWeightsSet.Name = "btnWeightsSet";
            this.btnWeightsSet.Size = new System.Drawing.Size(50, 38);
            this.btnWeightsSet.TabIndex = 6;
            this.btnWeightsSet.Text = ">>";
            this.btnWeightsSet.UseVisualStyleBackColor = true;
            this.btnWeightsSet.Click += new System.EventHandler(this.btnWeightsSet_Click);
            // 
            // cleanBtn
            // 
            this.cleanBtn.Location = new System.Drawing.Point(296, 553);
            this.cleanBtn.Name = "cleanBtn";
            this.cleanBtn.Size = new System.Drawing.Size(99, 38);
            this.cleanBtn.TabIndex = 8;
            this.cleanBtn.Text = "Очистить";
            this.cleanBtn.UseVisualStyleBackColor = true;
            this.cleanBtn.Visible = false;
            this.cleanBtn.Click += new System.EventHandler(this.cleanBtn_Click);
            // 
            // readyBtn
            // 
            this.readyBtn.Location = new System.Drawing.Point(296, 500);
            this.readyBtn.Name = "readyBtn";
            this.readyBtn.Size = new System.Drawing.Size(99, 38);
            this.readyBtn.TabIndex = 9;
            this.readyBtn.Text = "Применить";
            this.readyBtn.UseVisualStyleBackColor = true;
            this.readyBtn.Click += new System.EventHandler(this.readyBtn_Click);
            // 
            // FormSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 644);
            this.Controls.Add(this.readyBtn);
            this.Controls.Add(this.cleanBtn);
            this.Controls.Add(this.btnWeightsSet);
            this.Controls.Add(this.vertexValueGrid);
            this.Controls.Add(this.btnNSteps);
            this.Controls.Add(this.txtSteps);
            this.Controls.Add(this.simBox);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.chart1);
            this.Name = "FormSimulation";
            this.Text = "FormSimulation";
            this.Load += new System.EventHandler(this.FormSimulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertexValueGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.CheckedListBox simBox;
        private System.Windows.Forms.TextBox txtSteps;
        private System.Windows.Forms.Button btnNSteps;
        private System.Windows.Forms.DataGridView vertexValueGrid;
        private System.Windows.Forms.Button btnWeightsSet;
        private System.Windows.Forms.Button cleanBtn;
        private System.Windows.Forms.Button readyBtn;
    }
}