using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XlsxVisionerV2 {
    public partial class Form1 : Form {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        DataTable dataTableSelect = new DataTable();
        public Form1 () {
            InitializeComponent();
            dataGridViewOriginal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewSelect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void openButton_Click (object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk (object sender, CancelEventArgs e) {
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string header = "NO";//rbHeaderYes.Checked ? "YES" : "NO";
            string conStr;
            List<string> sheetNames = new List<string>();

            conStr = string.Empty;
            switch (extension) {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            // get the names of the sheets.
            using (OleDbConnection con = new OleDbConnection(conStr)) {
                using (OleDbCommand cmd = new OleDbCommand()) {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    // fill the list
                    for (int i = 0; i < dtExcelSchema.Rows.Count; i++) {
                        sheetNames.Add(dtExcelSchema.Rows[i]["TABLE_NAME"].ToString());
                    }
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr)) {
                using (OleDbCommand cmd = new OleDbCommand()) {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter()) {
                        DataTable dataTableOriginal = new DataTable();
                        con.Open();
                        foreach (string Name in sheetNames) {
                            cmd.CommandText = "SELECT * From [" + Name + "]";
                            cmd.Connection = con;
                            oda.SelectCommand = cmd;
                            oda.Fill(dataTableOriginal);
                        }
                        con.Close();
                        dataGridViewOriginal.DataSource = dataTableOriginal;
                    }
                }
            }
        }
        // move selected cells from 'original' to 'select' DataTable
        private void selectButton_Click (object sender, EventArgs e) {
            DataRow row = dataTableSelect.NewRow();
            int numberOfCells = dataGridViewOriginal.SelectedCells.Count;
            byte[,] vector = {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };

            for (int i = 0; i < numberOfCells; i++) {
                // extend a number of columns
                if (i >= dataTableSelect.Columns.Count) {
                    dataTableSelect.Columns.Add((i + 1).ToString());
                }
                // make rows from rotate value
                row[i] = dataGridViewOriginal.SelectedCells[numberOfCells - i - 1].FormattedValue;//.ToString();
            }
            //determine accordance
            if (!AccordanceWithPattern(numberOfCells, row)) {
                MessageBox.Show("Selected data not have according with pattern!") ;
                return;
            }
            // clear selected cells
            dataGridViewOriginal.SelectedCells[0].Value = DBNull.Value;
            dataGridViewOriginal.SelectedCells[1].Value = DBNull.Value;
            if (numberOfCells == 4) {
                dataGridViewOriginal.SelectedCells[2].Value = DBNull.Value;
                dataGridViewOriginal.SelectedCells[3].Value = DBNull.Value;
            }
            //determine the vector selected by the user [vertical][horizontal]
            if (dataGridViewOriginal.SelectedCells[0].ColumnIndex == dataGridViewOriginal.SelectedCells[1].ColumnIndex) {
                //[vertical]
                CollectVerticalData(dataGridViewOriginal, row, numberOfCells);
            }
            else {
                //[horizontal]
                CollectHorizontalData(dataGridViewOriginal, row, numberOfCells);
            }
           
            dataTableSelect.Rows.Add(row);
            dataGridViewSelect.DataSource = dataTableSelect;
            // remove empty rows
            RemoveEmptyRows(dataGridViewOriginal);
        }
        // two cells pattern = [string - comparable_field][total-sumrable_field]
        // four cells pattern = [string - comparable_field][quantity-sumrable_field][cost-sumrable_field][quantity + cost] 
        private bool AccordanceWithPattern (int length, DataRow row) {
            decimal result;
            switch (length) {
                case 2:
                    if (!Decimal.TryParse(row[0].ToString(), out result))
                        if (Decimal.TryParse(row[1].ToString(), out result))
                            return true;
                    break;
                case 4:
                    if (!Decimal.TryParse(row[0].ToString(), out result))
                        if (Decimal.TryParse(row[1].ToString(), out result))
                            if (Decimal.TryParse(row[2].ToString(), out result))
                                if (Decimal.TryParse(row[3].ToString(), out result))
                                    return true;
                    break;
            }
            return false;
        }
        // remove empty rows in DataGridView
        private void RemoveEmptyRows (DataGridView view) {
            for (int row = 0; row < view.Rows.Count; ++row) {
                bool isEmpty = true;
                for (int col = 0; col < view.Columns.Count; ++col) {
                    object value = view.Rows[row].Cells[col].Value;
                    if ((value != null) && (value.ToString().Length > 0)) {
                        isEmpty = false;
                        break;
                    }
                }
                if ((isEmpty) && (!view.Rows[row].IsNewRow)) {
                    // deincrement (after the call) since we are removing the row
                    view.Rows.RemoveAt(row--);
                }
            }
        }
        //data collection according to vector whom will be chose the user
        private void CollectVerticalData (DataGridView view, DataRow completeRow, int cellsCount) {
            decimal result;
            for (int row = 0; row < view.Rows.Count; ++row) {
                for (int col = 0; col < view.Columns.Count; ++col) {
                    object value = view.Rows[row].Cells[col].Value;
                    if ((value != null) && (value.ToString().Length > 0)) {
                        if ((!Decimal.TryParse(value.ToString(), out result)) && (value.ToString() == completeRow[0].ToString())) {
                            completeRow[1] = view.Rows[row].Cells[col];
                        }
                    }
                }
            }
        }
        //data collection according to vector whom will be chose the user
        private void CollectHorizontalData (DataGridView view, DataRow completeRow, int cellsCount) {

        }
    }
}
