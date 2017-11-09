using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace XlsxVisionerV2 {
    public partial class Form2 : Form {
        public Form2 () {
            InitializeComponent();
            DrawDiagram(DiagramData.xValues, DiagramData.yValues);
        }
        public void DrawDiagram (List<string> x, List<decimal> y) {
            // config the diagram
            dataChart.BackColor = Color.Gray;
            dataChart.BackSecondaryColor = Color.WhiteSmoke;
            dataChart.BackGradientStyle = GradientStyle.DiagonalRight;
            dataChart.BorderlineDashStyle = ChartDashStyle.Solid;
            dataChart.BorderlineColor = Color.Gray;
            dataChart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // config an area of the diagram
            dataChart.ChartAreas[0].BackColor = Color.Wheat;

            // add and format the title
            dataChart.Titles.Add("Data");
            dataChart.Titles[0].Font = new Font("Courier New", 10);

            dataChart.Series.Add(new Series("ColumnSeries") { ChartType = SeriesChartType.Pie });
            dataChart.Series["ColumnSeries"].Points.DataBindXY(x, y);
            dataChart.Series["ColumnSeries"].IsValueShownAsLabel = true;
            dataChart.ChartAreas[0].Area3DStyle.Enable3D = true;

        }
    }
}
