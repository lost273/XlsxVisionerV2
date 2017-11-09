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
        public void DrawDiagram (string[] x, decimal[] y) {
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
            dataChart.Titles.Add("Структура продаж");
            dataChart.Titles[0].Font = new Font("Courier New", 10);

            dataChart.Series.Add(new Series("ColumnSeries") { ChartType = SeriesChartType.Pie });
            //формируем массив значений для графика
            string[] xValues = { "Заправка лазерных - ", "Заправка струйных - ", "Ремонт картриджей - ", "Ремонт принтера - ", "Чернила - ", "Печать - ", "Товар - " };
            double[] yValues = { 0, 0, 0, 0, 0, 0, 0 };

            for (int Rnum = 0; Rnum < dt.Rows.Count; Rnum++) {
                if (dt.Rows[Rnum][0].ToString() == "заправка лазерного") { yValues[0] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else if (dt.Rows[Rnum][0].ToString() == "заправка струйного") { yValues[1] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else if (dt.Rows[Rnum][0].ToString() == "ремонт картриджа") { yValues[2] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else if (dt.Rows[Rnum][0].ToString() == "ремонт принтера") { yValues[3] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else if (dt.Rows[Rnum][0].ToString() == "чернила") { yValues[4] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else if (dt.Rows[Rnum][0].ToString() == "печать") { yValues[5] += Convert.ToDouble(dt.Rows[Rnum][3]); }
                else { yValues[6] += Convert.ToDouble(dt.Rows[Rnum][3]); }
            }

            xValues[0] = xValues[0] + Math.Round((yValues[0] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[1] = xValues[1] + Math.Round((yValues[1] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[2] = xValues[2] + Math.Round((yValues[2] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[3] = xValues[3] + Math.Round((yValues[3] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[4] = xValues[4] + Math.Round((yValues[4] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[5] = xValues[5] + Math.Round((yValues[5] / totalInfo[0] * 100), 2).ToString() + " %";
            xValues[6] = xValues[6] + Math.Round((yValues[6] / totalInfo[0] * 100), 2).ToString() + " %";

            Chart1.Series["ColumnSeries"].Points.DataBindXY(xValues, yValues);
            Chart1.Series["ColumnSeries"].IsValueShownAsLabel = true;

            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }
    }
}
