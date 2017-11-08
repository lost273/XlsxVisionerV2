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
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'";
        DataTable dataTableSelect = new DataTable();
        public Form1 () {
            InitializeComponent();
            dataGridViewOriginal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewSelect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void OpenButton_Click (object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk (object sender, CancelEventArgs e) {
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
        private void SelectButton_Click (object sender, EventArgs e) {
            DataRow row = dataTableSelect.NewRow();
            CollectProgressBar.Value = 0;
            int numberOfCells = dataGridViewOriginal.SelectedCells.Count;

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
            for (int index = 0; index < numberOfCells; index++) {
                dataGridViewOriginal.SelectedCells[index].Value = DBNull.Value;
            }
            //determine the vector selected by the user [vertical][horizontal]
            if (dataGridViewOriginal.SelectedCells[0].ColumnIndex == dataGridViewOriginal.SelectedCells[1].ColumnIndex) {
                //[vertical]
                CollectVerticalData(dataGridViewOriginal, row, numberOfCells);
                dataTableSelect.Rows.Add(row);
                //collection all data
                if (collectCheckBox.Checked == true) {
                    CollectAllVerticalData(dataGridViewOriginal, numberOfCells);
                }  
            }
            else {
                //[horizontal]
                CollectHorizontalData(dataGridViewOriginal, row, numberOfCells);
                dataTableSelect.Rows.Add(row);
                //collection all data
                if (collectCheckBox.Checked == true) {
                    CollectAllHorizontalData(dataGridViewOriginal, numberOfCells);
                }  
            }
            // remove empty rows
            RemoveEmptyRows(dataGridViewOriginal);
            dataGridViewSelect.DataSource = dataTableSelect;
        }
        // two cells pattern = [string - comparable_field][total-sumrable_field]
        // four cells pattern = [string - comparable_field][quantity-sumrable_field][cost][quantity * cost] 
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
                    if (IsDataNotEmpty(value)) {
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
        //check the data to null or epmty
        private bool IsDataNotEmpty(object data) {
            if ((data != null) && (data.ToString().Length > 0)) {
                return true;
            }
            return false;
        }
        //clear the data
        private void ClearValueFromDataGrid(DataGridView view, int cellsCount, int row, int col, string vector) {
            if (vector == "horizontal") {
                while (cellsCount > 0) {
                    view.Rows[row].Cells[col + cellsCount - 1].Value = DBNull.Value;
                    cellsCount--;
                }
            }
            else {
                while (cellsCount > 0) {
                    view.Rows[row + cellsCount - 1].Cells[col].Value = DBNull.Value;
                    cellsCount--;
                }
            }
        }
        //data collection according to vector whom will be chose the user
        private void CollectVerticalData (DataGridView view, DataRow completeRow, int cellsCount) {
            decimal result;
            //to not check unnecessary rows
            int rowsForCheck = view.Rows.Count - cellsCount + 1;
            decimal cellOne = 0;
            decimal cellTwo = 0;

            for (int col = 0; col < view.Columns.Count; ++col) {
                for (int row = 0; row < rowsForCheck; ++row) {
                    object value = view.Rows[row].Cells[col].Value;
                    if ((IsDataNotEmpty(value)) && (!Decimal.TryParse(value.ToString(), out result)) && 
                        (value.ToString() == completeRow[0].ToString()) && (Decimal.TryParse(view.Rows[row + 1].Cells[col].Value.ToString(), out cellOne))) {
                        if (cellsCount == 2) {
                            //[total-sumrable_field]
                            completeRow[1] = Convert.ToDecimal(completeRow[1]) + cellOne;
                            //clear
                            ClearValueFromDataGrid(view, cellsCount, row, col, "vertical");
                        }
                        if ((cellsCount == 4) && (Decimal.TryParse(view.Rows[row + 2].Cells[col].Value.ToString(), out cellTwo)) &&
                            (cellTwo == Convert.ToDecimal(completeRow[2]))) {
                            //[quantity-sumrable_field]
                            completeRow[1] = Convert.ToDecimal(completeRow[1]) + cellOne;
                            //[quantity * cost]
                            completeRow[3] = Convert.ToDecimal(completeRow[1]) * Convert.ToDecimal(completeRow[2]);
                            //clear
                            ClearValueFromDataGrid(view, cellsCount, row, col, "vertical");
                        }
                    }
                }
            }
        }
        //data collection according to vector whom will be chose the user
        private void CollectHorizontalData (DataGridView view, DataRow completeRow, int cellsCount) {
            decimal result;
            //to not check unnecessary columns
            int columnsForCheck = view.Columns.Count - cellsCount + 1;
            decimal cellOne = 0;
            decimal cellTwo = 0;

            for (int row = 0; row < view.Rows.Count; ++row) {
                for (int col = 0; col < columnsForCheck; ++col) {
                    object value = view.Rows[row].Cells[col].Value;
                    if ((IsDataNotEmpty(value)) && (!Decimal.TryParse(value.ToString(), out result)) && 
                        (value.ToString() == completeRow[0].ToString()) && (Decimal.TryParse(view.Rows[row].Cells[col + 1].Value.ToString(), out cellOne))) {
                        if (cellsCount == 2) {
                            //[total-sumrable_field]
                            completeRow[1] = Convert.ToDecimal(completeRow[1]) + cellOne;
                            //clear
                            ClearValueFromDataGrid(view, cellsCount, row, col, "horizontal");
                        }
                        if ((cellsCount == 4) && (Decimal.TryParse(view.Rows[row].Cells[col + 2].Value.ToString(), out cellTwo)) &&
                            (cellTwo  == Convert.ToDecimal(completeRow[2]))) {
                            //[quantity-sumrable_field]
                            completeRow[1] = Convert.ToDecimal(completeRow[1]) + cellOne;
                            //[quantity * cost]
                            completeRow[3] = Convert.ToDecimal(completeRow[1]) * Convert.ToDecimal(completeRow[2]);
                            //clear
                            ClearValueFromDataGrid(view, cellsCount, row, col, "horizontal");
                        }
                    }
                }
            }
        }
        //all data collection according to vector
        private void CollectAllVerticalData(DataGridView view, int cellsCount) {
            decimal result;
            DataRow checkRow = dataTableSelect.NewRow();
            CollectProgressBar.Maximum = view.Columns.Count;
            for (int col = 0; col < view.Columns.Count; ++col, ++CollectProgressBar.Value) {
                //to not check unnecessary rows
                for (int row = 0; row < (view.Rows.Count - cellsCount + 1); ++row) {
                    //make row
                    for (int rowIndex = 0; rowIndex < cellsCount; ++rowIndex) {
                        checkRow[rowIndex] = view.Rows[row + rowIndex].Cells[col].Value;
                    }
                    //check on if current pattern not contain in other pattern
                    bool notContainOtherData = false;
                    if ((view.Rows.Count > row + cellsCount) && (IsDataNotEmpty(view.Rows[row + cellsCount].Cells[col].Value))) {
                        if (!Decimal.TryParse(view.Rows[row + cellsCount].Cells[col].Value.ToString(), out result)) {
                            notContainOtherData = true;
                        }
                    }
                    else {
                        notContainOtherData = true;
                    }
                    //determine accordance
                    if ((AccordanceWithPattern(cellsCount, checkRow)) && (IsDataNotEmpty(checkRow[0])) && (notContainOtherData)) {
                        //clear
                        ClearValueFromDataGrid(view, cellsCount, row, col, "vertical");
                        CollectVerticalData(view, checkRow, cellsCount);
                        DataRow checkRowClone = dataTableSelect.NewRow();
                        checkRowClone.ItemArray = (object[])checkRow.ItemArray.Clone();
                        dataTableSelect.Rows.Add(checkRowClone);
                    }
                }
            }

        }
        //all data collection according to vector
        private void CollectAllHorizontalData(DataGridView view, int cellsCount) {
            decimal result;
            DataRow checkRow = dataTableSelect.NewRow();
            CollectProgressBar.Maximum = view.Rows.Count;
            for (int row = 0; row < view.Rows.Count; ++row, ++CollectProgressBar.Value) {
                //to not check unnecessary columns
                for (int col = 0; col < (view.Columns.Count - cellsCount + 1); ++col) {
                    //make row
                    for (int colIndex = 0; colIndex < cellsCount; ++colIndex) {
                        checkRow[colIndex] = view.Rows[row].Cells[col + colIndex].Value;
                    }
                    //check on if current pattern not contain in other pattern
                    bool notContainOtherData = false;
                    if ((view.Columns.Count > col + cellsCount) && (IsDataNotEmpty(view.Rows[row].Cells[col + cellsCount].Value))) {
                        if (!Decimal.TryParse(view.Rows[row].Cells[col + cellsCount].Value.ToString(), out result)) {
                            notContainOtherData = true;
                        }
                    }
                    else {
                        notContainOtherData = true;
                    }
                    //determine accordance
                    if ((AccordanceWithPattern(cellsCount, checkRow)) && (IsDataNotEmpty(checkRow[0])) && (notContainOtherData)) {
                        //clear
                        ClearValueFromDataGrid(view, cellsCount, row, col, "horizontal");
                        CollectHorizontalData(view, checkRow, cellsCount);
                        DataRow checkRowClone = dataTableSelect.NewRow();
                        checkRowClone.ItemArray = (object[])checkRow.ItemArray.Clone();
                        dataTableSelect.Rows.Add(checkRowClone);
                    }
                }
            }
        }

        private void RenameButton_Click(object sender, EventArgs e) {
            try {
                dataGridViewSelect.Columns[oldnameTextBox.Text].HeaderText = newnameTextBox.Text;
            }
            catch (Exception) {

                MessageBox.Show("Wrong № of the column!"); ;
            }
        }

        private void PrintButton_Click (object sender, EventArgs e) {
            tablePrintDialog.ShowDialog();
            tablePrintPreviewDialog.ShowDialog();
        }

        private void tablePrintDocument_PrintPage (object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            int z = 0;
            int rowCounter = 0;
            

            int width = 500 / (dataGridViewSelect.Columns.Count - 1); // ширина ячейки
            int realwidth = 100; // общая ширина
            int height = 15; // высота строки

            int realheight = 60; // общая высота


            // Рисуем название файла
            if (rowCounter == 0) {
                e.Graphics.FillRectangle(Brushes.ForestGreen, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(openFileDialog1.SafeFileName, dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);
                
                realheight = realheight + height;
            }

            // Рисуем названия колонок
            for (z = 0; z < dataGridViewSelect.Columns.Count; z++) {
                e.Graphics.FillRectangle(Brushes.RoyalBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);

                e.Graphics.DrawString(dataGridViewSelect.Columns[z].HeaderText, dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);

                realwidth = realwidth + width;
            }
            realheight = realheight + height;

            // Рисуем остальную таблицу
            while (rowCounter < dataGridViewSelect.Rows.Count) {
                realwidth = 100;

                if (dataGridViewSelect.Rows[rowCounter].Cells[0].Value == null) {
                    dataGridViewSelect.Rows[rowCounter].Cells[0].Value = "";
                }
                e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(dataGridViewSelect.Rows[rowCounter].Cells[0].Value.ToString(), dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);
                realwidth = realwidth + width;

                if (dataGridViewSelect.Rows[rowCounter].Cells[1].Value == null) {
                    dataGridViewSelect.Rows[rowCounter].Cells[1].Value = "";
                }
                e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(dataGridViewSelect.Rows[rowCounter].Cells[1].Value.ToString(), dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);
                realwidth = realwidth + width;

                if (dataGridViewSelect.Rows[rowCounter].Cells[2].Value == null) {
                    dataGridViewSelect.Rows[rowCounter].Cells[2].Value = "";
                }
                e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(dataGridViewSelect.Rows[rowCounter].Cells[2].Value.ToString(), dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);
                realwidth = realwidth + width;

                if (dataGridViewSelect.Rows[rowCounter].Cells[3].Value == null) {
                    dataGridViewSelect.Rows[rowCounter].Cells[3].Value = "";
                }
                e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(dataGridViewSelect.Rows[rowCounter].Cells[3].Value.ToString(), dataGridViewSelect.Font, Brushes.Black, realwidth, realheight);
                realwidth = realwidth + width;

                ++rowCounter;
                realheight = realheight + height;

                //если 1000 пикселей уже напечатано - переводим на новый лист
                if (realheight >= 1000) { e.HasMorePages = true; break; }
                // если 1000 пикселей не напечатано, а таблица закончилась
                if ((realheight < 1000) && (rowCounter >= dataGridViewSelect.Rows.Count)) { e.HasMorePages = false; rowCounter = 0; break; }

            }
        }
    }
}
