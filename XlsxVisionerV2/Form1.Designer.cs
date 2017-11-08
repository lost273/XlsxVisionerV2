namespace XlsxVisionerV2 {
    partial class Form1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridViewSelect = new System.Windows.Forms.DataGridView();
            this.openButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.dataGridViewOriginal = new System.Windows.Forms.DataGridView();
            this.collectCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CollectProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newnameTextBox = new System.Windows.Forms.TextBox();
            this.oldnameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.renameButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.diagramButton = new System.Windows.Forms.Button();
            this.tablePrintDialog = new System.Windows.Forms.PrintDialog();
            this.tablePrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.tablePrintDocument = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOriginal)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
            // 
            // dataGridViewSelect
            // 
            this.dataGridViewSelect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridViewSelect.Location = new System.Drawing.Point(620, 0);
            this.dataGridViewSelect.Name = "dataGridViewSelect";
            this.dataGridViewSelect.Size = new System.Drawing.Size(489, 444);
            this.dataGridViewSelect.TabIndex = 2;
            // 
            // openButton
            // 
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openButton.Location = new System.Drawing.Point(521, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectButton.Location = new System.Drawing.Point(6, 48);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(95, 57);
            this.selectButton.TabIndex = 3;
            this.selectButton.Text = "Collect\r\n>>";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // dataGridViewOriginal
            // 
            this.dataGridViewOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewOriginal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOriginal.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewOriginal.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewOriginal.Name = "dataGridViewOriginal";
            this.dataGridViewOriginal.Size = new System.Drawing.Size(500, 444);
            this.dataGridViewOriginal.TabIndex = 1;
            // 
            // collectCheckBox
            // 
            this.collectCheckBox.AutoSize = true;
            this.collectCheckBox.Location = new System.Drawing.Point(6, 19);
            this.collectCheckBox.Name = "collectCheckBox";
            this.collectCheckBox.Size = new System.Drawing.Size(95, 17);
            this.collectCheckBox.TabIndex = 4;
            this.collectCheckBox.Text = "Collect all data";
            this.collectCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectButton);
            this.groupBox1.Controls.Add(this.collectCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(506, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collection";
            // 
            // CollectProgressBar
            // 
            this.CollectProgressBar.Location = new System.Drawing.Point(512, 186);
            this.CollectProgressBar.Name = "CollectProgressBar";
            this.CollectProgressBar.Size = new System.Drawing.Size(95, 23);
            this.CollectProgressBar.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Collect progress";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newnameTextBox);
            this.groupBox2.Controls.Add(this.oldnameTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.renameButton);
            this.groupBox2.Location = new System.Drawing.Point(506, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(108, 132);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rename column";
            // 
            // newnameTextBox
            // 
            this.newnameTextBox.Location = new System.Drawing.Point(6, 77);
            this.newnameTextBox.Name = "newnameTextBox";
            this.newnameTextBox.Size = new System.Drawing.Size(95, 20);
            this.newnameTextBox.TabIndex = 2;
            // 
            // oldnameTextBox
            // 
            this.oldnameTextBox.Location = new System.Drawing.Point(6, 32);
            this.oldnameTextBox.Name = "oldnameTextBox";
            this.oldnameTextBox.Size = new System.Drawing.Size(95, 20);
            this.oldnameTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "№ of column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Name";
            // 
            // renameButton
            // 
            this.renameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renameButton.Location = new System.Drawing.Point(15, 103);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(75, 23);
            this.renameButton.TabIndex = 0;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // printButton
            // 
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Location = new System.Drawing.Point(521, 364);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 23);
            this.printButton.TabIndex = 0;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // diagramButton
            // 
            this.diagramButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.diagramButton.Location = new System.Drawing.Point(521, 393);
            this.diagramButton.Name = "diagramButton";
            this.diagramButton.Size = new System.Drawing.Size(75, 23);
            this.diagramButton.TabIndex = 0;
            this.diagramButton.Text = "Diagram";
            this.diagramButton.UseVisualStyleBackColor = true;
            this.diagramButton.Click += new System.EventHandler(this.DiagramButton_Click);
            // 
            // tablePrintDialog
            // 
            this.tablePrintDialog.UseEXDialog = true;
            // 
            // tablePrintPreviewDialog
            // 
            this.tablePrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.tablePrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.tablePrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.tablePrintPreviewDialog.Enabled = true;
            this.tablePrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("tablePrintPreviewDialog.Icon")));
            this.tablePrintPreviewDialog.Name = "tablePrintPreviewDialog";
            this.tablePrintPreviewDialog.Visible = false;
            // 
            // tablePrintDocument
            // 
            this.tablePrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.tablePrintDocument_BeginPrint);
            this.tablePrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.tablePrintDocument_PrintPage);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 444);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CollectProgressBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewOriginal);
            this.Controls.Add(this.diagramButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.dataGridViewSelect);
            this.MinimumSize = new System.Drawing.Size(1125, 39);
            this.Name = "Form1";
            this.Text = "XlsxVisionerV2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOriginal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridViewSelect;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.DataGridView dataGridViewOriginal;
        private System.Windows.Forms.CheckBox collectCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar CollectProgressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.TextBox oldnameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newnameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button diagramButton;
        private System.Windows.Forms.PrintDialog tablePrintDialog;
        private System.Windows.Forms.PrintPreviewDialog tablePrintPreviewDialog;
        private System.Drawing.Printing.PrintDocument tablePrintDocument;
    }
}

