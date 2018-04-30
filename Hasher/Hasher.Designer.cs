namespace Hasher
{
    partial class HasherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HasherForm));
            this.comboBoxHash = new System.Windows.Forms.ComboBox();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBoxFileOrDirectoryToHash = new System.Windows.Forms.TextBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.radioButtonFile = new System.Windows.Forms.RadioButton();
            this.radioButtonDirectory = new System.Windows.Forms.RadioButton();
            this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
            this.dataGridViewHash = new System.Windows.Forms.DataGridView();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHashKey = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.progressBarHash = new System.Windows.Forms.ProgressBar();
            this.labelFilesCount = new System.Windows.Forms.Label();
            this.hashBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.countBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHash)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHash.FormattingEnabled = true;
            this.comboBoxHash.Items.AddRange(new object[] {
            "MD5",
            "SHA1",
            "SHA256",
            "SHA384",
            "SHA512",
            "RIPEMD160",
            "MACTripleDES",
            "HMACMD5",
            "HMACSHA1",
            "HMACSHA256",
            "HMACSHA384",
            "HMACSHA512",
            "HMACRIPEMD160"});
            this.comboBoxHash.Location = new System.Drawing.Point(115, 6);
            this.comboBoxHash.Name = "comboBoxHash";
            this.comboBoxHash.Size = new System.Drawing.Size(1043, 21);
            this.comboBoxHash.TabIndex = 1;
            this.comboBoxHash.SelectedIndexChanged += new System.EventHandler(this.ComboBoxHash_SelectedIndexChanged);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog";
            this.fileDialog.Multiselect = true;
            // 
            // textBoxFileOrDirectoryToHash
            // 
            this.textBoxFileOrDirectoryToHash.AllowDrop = true;
            this.textBoxFileOrDirectoryToHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileOrDirectoryToHash.Location = new System.Drawing.Point(115, 59);
            this.textBoxFileOrDirectoryToHash.Name = "textBoxFileOrDirectoryToHash";
            this.textBoxFileOrDirectoryToHash.Size = new System.Drawing.Size(812, 20);
            this.textBoxFileOrDirectoryToHash.TabIndex = 5;
            this.textBoxFileOrDirectoryToHash.TextChanged += new System.EventHandler(this.TextBoxFileOrDirectoryToHash_TextChanged);
            this.textBoxFileOrDirectoryToHash.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxFileOrDirectoryToHash_DragDrop);
            this.textBoxFileOrDirectoryToHash.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBoxFileOrDirectoryToHash_DragEnter);
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSelectFile.Location = new System.Drawing.Point(1133, 59);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(25, 20);
            this.buttonSelectFile.TabIndex = 9;
            this.buttonSelectFile.Text = "...";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.ButtonSelectFileOrDirectory_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Element(s) to hash:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hash Algorithm:";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // radioButtonFile
            // 
            this.radioButtonFile.AutoSize = true;
            this.radioButtonFile.Checked = true;
            this.radioButtonFile.Location = new System.Drawing.Point(933, 60);
            this.radioButtonFile.Name = "radioButtonFile";
            this.radioButtonFile.Size = new System.Drawing.Size(41, 17);
            this.radioButtonFile.TabIndex = 6;
            this.radioButtonFile.TabStop = true;
            this.radioButtonFile.Text = "File";
            this.radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonDirectory
            // 
            this.radioButtonDirectory.AutoSize = true;
            this.radioButtonDirectory.Location = new System.Drawing.Point(980, 60);
            this.radioButtonDirectory.Name = "radioButtonDirectory";
            this.radioButtonDirectory.Size = new System.Drawing.Size(67, 17);
            this.radioButtonDirectory.TabIndex = 7;
            this.radioButtonDirectory.Text = "Directory";
            this.radioButtonDirectory.UseVisualStyleBackColor = true;
            this.radioButtonDirectory.CheckedChanged += new System.EventHandler(this.DirectoryRadioButton_CheckedChanged);
            // 
            // checkBoxRecursive
            // 
            this.checkBoxRecursive.AutoSize = true;
            this.checkBoxRecursive.Enabled = false;
            this.checkBoxRecursive.Location = new System.Drawing.Point(1053, 61);
            this.checkBoxRecursive.Name = "checkBoxRecursive";
            this.checkBoxRecursive.Size = new System.Drawing.Size(74, 17);
            this.checkBoxRecursive.TabIndex = 8;
            this.checkBoxRecursive.Text = "Recursive";
            this.checkBoxRecursive.UseVisualStyleBackColor = true;
            this.checkBoxRecursive.CheckedChanged += new System.EventHandler(this.RecursiveCheckBox_CheckedChanged);
            // 
            // dataGridViewHash
            // 
            this.dataGridViewHash.AllowUserToAddRows = false;
            this.dataGridViewHash.AllowUserToDeleteRows = false;
            this.dataGridViewHash.AllowUserToResizeRows = false;
            this.dataGridViewHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHash.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewHash.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewHash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewHash.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File,
            this.Hash});
            this.dataGridViewHash.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewHash.Location = new System.Drawing.Point(12, 85);
            this.dataGridViewHash.Name = "dataGridViewHash";
            this.dataGridViewHash.ReadOnly = true;
            this.dataGridViewHash.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewHash.RowHeadersVisible = false;
            this.dataGridViewHash.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewHash.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewHash.ShowCellErrors = false;
            this.dataGridViewHash.ShowCellToolTips = false;
            this.dataGridViewHash.ShowEditingIcon = false;
            this.dataGridViewHash.ShowRowErrors = false;
            this.dataGridViewHash.Size = new System.Drawing.Size(1146, 375);
            this.dataGridViewHash.TabIndex = 10;
            // 
            // File
            // 
            this.File.HeaderText = "File";
            this.File.Name = "File";
            this.File.ReadOnly = true;
            this.File.Width = 572;
            // 
            // Hash
            // 
            this.Hash.HeaderText = "Hash";
            this.Hash.Name = "Hash";
            this.Hash.ReadOnly = true;
            this.Hash.Width = 571;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hash Key:";
            // 
            // textBoxHashKey
            // 
            this.textBoxHashKey.AllowDrop = true;
            this.textBoxHashKey.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxHashKey.Enabled = false;
            this.textBoxHashKey.Location = new System.Drawing.Point(115, 33);
            this.textBoxHashKey.Name = "textBoxHashKey";
            this.textBoxHashKey.Size = new System.Drawing.Size(1043, 20);
            this.textBoxHashKey.TabIndex = 3;
            this.textBoxHashKey.Text = "Example Key";
            this.textBoxHashKey.TextChanged += new System.EventHandler(this.TextBoxHashKey_TextChanged);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalculate.Enabled = false;
            this.buttonCalculate.Location = new System.Drawing.Point(1002, 466);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculate.TabIndex = 13;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // progressBarHash
            // 
            this.progressBarHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarHash.Location = new System.Drawing.Point(15, 466);
            this.progressBarHash.Name = "progressBarHash";
            this.progressBarHash.Size = new System.Drawing.Size(787, 23);
            this.progressBarHash.Step = 1;
            this.progressBarHash.TabIndex = 11;
            // 
            // labelFilesCount
            // 
            this.labelFilesCount.AutoSize = true;
            this.labelFilesCount.Location = new System.Drawing.Point(808, 471);
            this.labelFilesCount.Name = "labelFilesCount";
            this.labelFilesCount.Size = new System.Drawing.Size(124, 13);
            this.labelFilesCount.TabIndex = 12;
            this.labelFilesCount.Text = "Number of file(s) to hash:";
            // 
            // hashBackgroundWorker
            // 
            this.hashBackgroundWorker.WorkerReportsProgress = true;
            this.hashBackgroundWorker.WorkerSupportsCancellation = true;
            this.hashBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HashBackgroundWorker_DoWork);
            this.hashBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.HashBackgroundWorker_ProgressChanged);
            this.hashBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HashBackgroundWorker_RunWorkerCompleted);
            // 
            // countBackgroundWorker
            // 
            this.countBackgroundWorker.WorkerSupportsCancellation = true;
            this.countBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CountBackgroundWorker_DoWork);
            this.countBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CountBackgroundWorker_RunWorkerCompleted);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(1083, 466);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // HasherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 501);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelFilesCount);
            this.Controls.Add(this.progressBarHash);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.textBoxHashKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewHash);
            this.Controls.Add(this.checkBoxRecursive);
            this.Controls.Add(this.radioButtonDirectory);
            this.Controls.Add(this.radioButtonFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileOrDirectoryToHash);
            this.Controls.Add(this.buttonSelectFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxHash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HasherForm";
            this.Text = "Hasher";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHash;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.TextBox textBoxFileOrDirectoryToHash;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RadioButton radioButtonFile;
        private System.Windows.Forms.RadioButton radioButtonDirectory;
        private System.Windows.Forms.CheckBox checkBoxRecursive;
        private System.Windows.Forms.DataGridView dataGridViewHash;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHashKey;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.ProgressBar progressBarHash;
        private System.Windows.Forms.Label labelFilesCount;
        private System.ComponentModel.BackgroundWorker hashBackgroundWorker;
        private System.ComponentModel.BackgroundWorker countBackgroundWorker;
        private System.Windows.Forms.Button buttonCancel;
    }
}

