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
            Color[] barColors = new Color[8]{
                Color.Purple,
                Color.SteelBlue,
                Color.Aqua,
                Color.Yellow,
                Color.Navy,
                Color.Green,
                Color.Blue,
                Color.Red
            };

            // config the diagram
            dataChart.BackColor = Color.Gray;
            dataChart.BackSecondaryColor = Color.WhiteSmoke;
            dataChart.BackGradientStyle = GradientStyle.DiagonalRight;
            dataChart.BorderlineDashStyle = ChartDashStyle.Solid;
            dataChart.BorderlineColor = Color.Gray;
            dataChart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            dataChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // config an area of the diagram
            dataChart.ChartAreas[0].BackColor = Color.WhiteSmoke;
            dataChart.ChartAreas[0].AxisX.Interval = 1;

            // add and format the title
            dataChart.Titles.Add("Data");
            dataChart.Titles[0].Font = new Font("Courier New", 10);

            dataChart.Series.Add(new Series("ColumnSeries") { ChartType = SeriesChartType.Bar });
            dataChart.Series["ColumnSeries"].Points.DataBindXY(x, y);
            dataChart.Series["ColumnSeries"].IsValueShownAsLabel = true;

            Random rand = new Random();
            foreach (DataPoint item in dataChart.Series["ColumnSeries"].Points) {
                item.Color = barColors[rand.Next(8)];
            }
            //dataChart.ChartAreas[0].Area3DStyle.Enable3D = true;

        }
    }
}
