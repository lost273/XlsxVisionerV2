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
        DataTable dataTableOriginal = new DataTable();
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
            // reset dataTable and dataGridView
            dataTableOriginal.Clear();

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr)) {
                using (OleDbCommand cmd = new OleDbCommand()) {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter()) {
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
            DataRow rows = dataTableSelect.NewRow();
            int numberOfCells = dataGridViewOriginal.SelectedCells.Count;

            for (int i = 0; i < numberOfCells; i++) {
                // extend a number of columns
                if (i >= dataTableSelect.Columns.Count) {
                    dataTableSelect.Columns.Add((i + 1).ToString());
                }
                // make rows from rotate value
                rows[i] = dataGridViewOriginal.SelectedCells[numberOfCells - i - 1].FormattedValue.ToString();
            }
            //determine accordance
            if (!AccordanceWithPattern(numberOfCells, rows)) {
                MessageBox.Show("Selected data not have according with pattern!") ;
                return;
            }
            // clear selected cells
            for (int i = 0; i < numberOfCells; i++) {
                dataGridViewOriginal.SelectedCells[i].Value = string.Empty;
            }
            //determine the vector selected by the user [vertical][horizontal]

            //data collection according to vector and method of summing

            dataTableSelect.Rows.Add(rows);
            dataGridViewSelect.DataSource = dataTableSelect;
            // remove empty rows
            for (int i = dataGridViewOriginal.Rows.Count-1; i > -1; --i) {
                DataGridViewRow row = dataGridViewOriginal.Rows[i];
                bool isRowEmpty = true;
                for (int col = 0; col < row.Cells.Count; col++) {
                    if ((row.Cells[col].Value != null) && (row.Cells[col].Value.ToString().Length > 0)) {
                        isRowEmpty = false;
                    }
                }
                if (isRowEmpty) dataGridViewOriginal.Rows.RemoveAt(i);
            }
        //    private void clearGrid (DataGridView view) {
        //    for (int row = 0; row < view.Rows.Count; ++row) {
        //        bool isEmpty = true;
        //        for (int col = 0; col < view.Columns.Count; ++col) {
        //            object value = view.Rows[row].Cells[col].Value;
        //            if (value != null && value.ToString().Length > 0) {
        //                isEmpty = false;
        //                break;
        //            }
        //        }
        //        if (isEmpty) {
        //            // deincrement (after the call) since we are removing the row
        //            view.Rows.RemoveAt(row--);
        //        }
        //    }
        //}


        //DataGridViewRow row = (DataGridViewRow)yourDataGridView.Rows[0].Clone();
        //row.Cells[0].Value = "XYZ";
        //row.Cells[1].Value = 50.2;
        //yourDataGridView.Rows.Add(row);
        //    if (DataGridView1.SelectedCells[counter].FormattedValueType ==
        //Type.GetType("System.String"))
        //    value = DataGridView1.SelectedCells[counter]
        //        .FormattedValue.ToString();

        }
        // two cells pattern = [string - comparable_field][total-sumrable_field]
        // four cells pattern = [string - comparable_field][quantity-sumrable_field][cost-sumrable_field][quantity + cost] 
        public bool AccordanceWithPattern (int length, DataRow rows) {
            decimal result;
            switch (length) {
                case 2:
                    if (!Decimal.TryParse(rows[0].ToString(), out result))
                        if (Decimal.TryParse(rows[1].ToString(), out result))
                            return true;
                    break;
                case 4:
                    if (!Decimal.TryParse(rows[0].ToString(), out result))
                        if (Decimal.TryParse(rows[1].ToString(), out result))
                            if (Decimal.TryParse(rows[2].ToString(), out result))
                                if (Decimal.TryParse(rows[3].ToString(), out result))
                                    return true;
                    break;
            }
            return false;
        }
    }
}
