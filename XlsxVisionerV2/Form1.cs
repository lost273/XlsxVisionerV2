﻿using System;
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
        public Form1 () {
            InitializeComponent();
        }

        private void OpenButton_Click (object sender, EventArgs e) {
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
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
    }
}
