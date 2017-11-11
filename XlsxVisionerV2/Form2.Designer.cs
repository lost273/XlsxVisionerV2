namespace XlsxVisionerV2 {
    partial class Form2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.printChartButton = new System.Windows.Forms.Button();
            this.chartPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.chartPrintDialog = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).BeginInit();
            this.SuspendLayout();
            // 
            // dataChart
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea1);
            this.dataChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataChart.Location = new System.Drawing.Point(0, 0);
            this.dataChart.Name = "dataChart";
            this.dataChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.MarkerSize = 10;
            series1.Name = "Series1";
            this.dataChart.Series.Add(series1);
            this.dataChart.Size = new System.Drawing.Size(779, 529);
            this.dataChart.TabIndex = 0;
            this.dataChart.Text = "chart1";
            // 
            // printChartButton
            // 
            this.printChartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printChartButton.Location = new System.Drawing.Point(335, 535);
            this.printChartButton.Name = "printChartButton";
            this.printChartButton.Size = new System.Drawing.Size(75, 45);
            this.printChartButton.TabIndex = 1;
            this.printChartButton.Text = "Print";
            this.printChartButton.UseVisualStyleBackColor = true;
            this.printChartButton.Click += new System.EventHandler(this.PrintChartButton_Click);
            // 
            // chartPrintDocument
            // 
            this.chartPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.chartPrintDocument_BeginPrint);
            // 
            // chartPrintDialog
            // 
            this.chartPrintDialog.UseEXDialog = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 592);
            this.Controls.Add(this.printChartButton);
            this.Controls.Add(this.dataChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Diagram";
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.Button printChartButton;
        private System.Drawing.Printing.PrintDocument chartPrintDocument;
        private System.Windows.Forms.PrintDialog chartPrintDialog;
    }
}