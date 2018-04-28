using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]
namespace Hasher
{
    public partial class HasherForm : Form
    {
        //TODO: tabbing
        //TODO: layout
        //TODO: add backgroundworker for file count
        //TODO: look at when the table should be cleared
        //TODO: performance improvements
        Dictionary<string, string> hashResults = new Dictionary<string, string>();
        
        public HasherForm()
        {
            InitializeComponent();
        }

        private void ComboBoxHash_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the state of the key text box based on whether a keyed hash has been selected
            using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create(comboBoxHash.Text))
            {
                //System.Security.Cryptography.KeyedHashAlgorithm keyedHash = hash as System.Security.Cryptography.KeyedHashAlgorithm;
                //if (keyedHash != null)  //TODO: test performance
                textBoxHashKey.Enabled = (hash.GetType().IsSubclassOf(typeof(System.Security.Cryptography.KeyedHashAlgorithm)));
            }

            buttonCalculate.Enabled = CheckCalculateButtonValidity();

            dataGridViewHash.Rows.Clear();
        }

        private void TextBoxHashKey_TextChanged(object sender, EventArgs e)
        {
            // when entering a key, validate the form
            buttonCalculate.Enabled = CheckCalculateButtonValidity();

            dataGridViewHash.Rows.Clear();
        }
        
        private void DirectoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // set the state of the recursive option based on whether directory hashing has been selected
            RecursiveCheckBox.Enabled = DirectoryRadioButton.Checked;

            dataGridViewHash.Rows.Clear();
        }

        private void RecursiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // when changing the search type, validate the form and set the number of files
            buttonCalculate.Enabled = CheckCalculateButtonValidity(true);

            dataGridViewHash.Rows.Clear();
        }

        private void ButtonSelectFileOrDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // show the related chooser dialog based on the radio button selection
            if (FileRadioButton.Checked)
            {
                result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // may return multiple files so display with appropriate separator
                    textBoxFileOrDirectoryToHash.Text = string.Join(";", fileDialog.FileNames);
                }
            }
            else if (DirectoryRadioButton.Checked)
            {
                result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxFileOrDirectoryToHash.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void TextBoxFileOrDirectoryToHash_DragEnter(object sender, DragEventArgs e)
        {
            // only allow drag and drop when using a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TextBoxFileOrDirectoryToHash_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // may return multiple files so display with appropriate separator
            textBoxFileOrDirectoryToHash.Text = string.Join(";", files);
        }

        private void TextBoxFileOrDirectoryToHash_TextChanged(object sender, EventArgs e)
        {
            // when entering a file or directory, validate the form and set the number of files
            buttonCalculate.Enabled = CheckCalculateButtonValidity(true);

            dataGridViewHash.Rows.Clear();
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            // should only be possible to click when valid but double check before starting
            if (CheckCalculateButtonValidity())
            {
                dataGridViewHash.Rows.Clear();

                System.IO.SearchOption searchOption;
                if (RecursiveCheckBox.Checked)
                {
                    searchOption = System.IO.SearchOption.AllDirectories;
                }
                else
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                }

                if (hashBackgroundWorker.IsBusy)
                {
                    hashBackgroundWorker.CancelAsync();
                }
                else
                {
                    List<object> arguments = new List<object>
                    {
                        textBoxFileOrDirectoryToHash.Text,
                        comboBoxHash.Text,
                        textBoxHashKey.Text,
                        searchOption
                    };

                    hashBackgroundWorker.RunWorkerAsync(arguments);
                }
            }
        }

        private void HashBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;

            List<string> filesToHash = new List<string>();
            
            string[] list = ((string)arguments[0]).Split(';');
            foreach (string fileOrDirectory in list)
            {
                if (System.IO.Directory.Exists(fileOrDirectory))
                {
                    filesToHash.AddRange(System.IO.Directory.GetFiles(fileOrDirectory.Trim(), "*", (System.IO.SearchOption)arguments[3]));
                }
                else if (System.IO.File.Exists(fileOrDirectory))
                {
                    filesToHash.Add(fileOrDirectory.Trim());
                }
            }

            // report the number of files to support the progress bar limits
            hashBackgroundWorker.ReportProgress(0, filesToHash.Count);

            if (filesToHash.Count > 0)
            {
                using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create((string)arguments[1]))
                {
                    // if using a keyed hash, set the key before computing the hash
                    if (hash.GetType().IsSubclassOf(typeof(System.Security.Cryptography.KeyedHashAlgorithm)))
                    {
                        byte[] key = System.Text.Encoding.ASCII.GetBytes((string)arguments[2]);
                        ((System.Security.Cryptography.KeyedHashAlgorithm)hash).Key = key;
                    }


                    filesToHash.ForEach(file =>
                    {
                        HashFile(file, hash);
                        hashBackgroundWorker.ReportProgress(0);
                    });
                }
            }
        }

        private void HashBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                progressBarHash.Value = 0;
                progressBarHash.Maximum = (int)e.UserState;
            }
            else
            {
                progressBarHash.PerformStep();
            }
        }

        private void HashBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SuspendLayout();

            dataGridViewHash.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewHash.RowHeadersVisible = false;
            dataGridViewHash.AutoSize = false;
            dataGridViewHash.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridViewHash.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dataGridViewHash.Rows.Clear();

            foreach (KeyValuePair<string, string> result in hashResults)
            {
                dataGridViewHash.Rows.Add(result.Key, result.Value);
            }

            //dataGridViewHash.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            //dataGridViewHash.RowHeadersVisible = true;

            ResumeLayout();

            hashResults.Clear();
        }

        private void HashFile(string file, System.Security.Cryptography.HashAlgorithm hash)
        {
            using (System.IO.FileStream stream = System.IO.File.OpenRead(file)) //TODO: needs protection
            {
                byte[] hashOutput = hash.ComputeHash(stream);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashOutput)
                {
                    sb.Append(b.ToString("X2", System.Globalization.CultureInfo.CurrentCulture));
                }
                
                hashResults.Add(file, sb.ToString());
                //dataGridViewHash.Rows.Add(file, sb.ToString());
            }
        }

        private bool CheckCalculateButtonValidity(bool IncludeFileCount = false)
        {
            return IsFileOrDirectoryValid(IncludeFileCount) && IsHashValid();
        }

        private bool IsHashValid()
        {
            // first, validate the actual hash chosen
            bool valid = comboBoxHash.Items.Contains(comboBoxHash.Text);

            // second, for keyed hash algorithms only, validate the key
            using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create(comboBoxHash.Text))
            {
                if (hash != null)
                {
                    if (hash.GetType().IsSubclassOf(typeof(System.Security.Cryptography.KeyedHashAlgorithm)))
                    {
                        byte[] key = System.Text.Encoding.ASCII.GetBytes(textBoxHashKey.Text);

                        // MACTripleDES is a special case that only accepts a key that is 16 or 20 bytes long
                        // and must not be "weak" as determined by the library
                        if (hash is System.Security.Cryptography.MACTripleDES)
                        {
                            valid = (key.Length == 16 || key.Length == 24) && !System.Security.Cryptography.TripleDES.IsWeakKey(key);

                            //Array.Resize(ref key, 24);
                            //System.Security.Cryptography.TripleDESCng temp = new System.Security.Cryptography.TripleDESCng();
                            //temp.GenerateKey();
                            //key = temp.Key;
                        }
                        // all other keyed hashes only require non empty keys
                        else
                        {
                            valid = string.IsNullOrEmpty(textBoxHashKey.Text);
                        }
                    }
                }
                else
                {
                    valid = false;
                }
            }

            return valid;
        }

        private bool IsFileOrDirectoryValid(bool IncludeFileCount = false)
        {
            bool valid = false;

            System.IO.SearchOption searchOption;
            if (RecursiveCheckBox.Checked)
            {
                searchOption = System.IO.SearchOption.AllDirectories;
            }
            else
            {
                searchOption = System.IO.SearchOption.TopDirectoryOnly;
            }

            string[] list = textBoxFileOrDirectoryToHash.Text.Split(';');

            if (!IncludeFileCount)
            {
                foreach (string fileOrDirectory in list)
                {
                    if (System.IO.File.Exists(fileOrDirectory) || System.IO.Directory.Exists(fileOrDirectory))
                    {
                        // return true if there is at least one valid entry
                        valid = true;
                        break;
                    }
                }
            }
            else
            {
                int fileCount = 0;
                foreach (string fileOrDirectory in list)
                {
                    if (System.IO.File.Exists(fileOrDirectory))
                    {
                        fileCount++;
                    }
                    else if (System.IO.Directory.Exists(fileOrDirectory))
                    {
                        fileCount = fileCount + System.IO.Directory.GetFiles(fileOrDirectory, "*", searchOption).Count();
                    }
                }

                // return true if there is at least one valid entry
                valid = fileCount > 0;

                labelFilesCount.Text = "Number of file(s) to hash: " + fileCount.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }

            return valid;
        }
    }
}
