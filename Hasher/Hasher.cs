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
        //TODO: layout
        //TODO: look at when the table should be cleared
        //TODO: performance improvements
        //TODO: tabbing
        Dictionary<string, string> hashResults = new Dictionary<string, string>();

        public HasherForm()
        {
            InitializeComponent();
        }

        private void ComboBoxHash_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable controls as required
            textBoxHashKey.Enabled = IsHashAlgorithm(comboBoxHash.Text);
            buttonCalculate.Enabled = CheckCalculateButtonValidity(false);

            // clear the table when hash changed
            //dataGridViewHash.Rows.Clear();
        }

        private void TextBoxHashKey_TextChanged(object sender, EventArgs e)
        {
            // when entering a key, validate the form
            buttonCalculate.Enabled = CheckCalculateButtonValidity(false);

            // clear the table when hash key changed
            dataGridViewHash.Rows.Clear();
        }

        private void DirectoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // set the state of the recursive option based on whether directory hashing has been selected
            checkBoxRecursive.Enabled = radioButtonDirectory.Checked;

            // clear the table when dialog type changed
            //dataGridViewHash.Rows.Clear();
        }

        private void RecursiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // when changing the search type, validate the form and set the number of files
            buttonCalculate.Enabled = CheckCalculateButtonValidity(true);
            
            // clear the table when recursive setting changed
            dataGridViewHash.Rows.Clear();
        }

        private void ButtonSelectFileOrDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // show the related chooser dialog based on the radio button selection
            if (radioButtonFile.Checked)
            {
                result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // may return multiple files so display with appropriate separator
                    textBoxFileOrDirectoryToHash.Text = string.Join(";", fileDialog.FileNames);
                }
            }
            else if (radioButtonDirectory.Checked)
            {
                result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // only able to return a single directory path
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
                // make sure the background worker is ready before starting
                while (hashBackgroundWorker.IsBusy)
                {
                    hashBackgroundWorker.CancelAsync();
                    Application.DoEvents();
                }

                // enable cancel and disable all other controls
                buttonCancel.Enabled = true;
                buttonCalculate.Enabled = false;
                comboBoxHash.Enabled = false;
                textBoxHashKey.Enabled = false;
                textBoxFileOrDirectoryToHash.Enabled = false;
                radioButtonFile.Enabled = false;
                radioButtonDirectory.Enabled = false;
                checkBoxRecursive.Enabled = false;
                dataGridViewHash.Enabled = false;
                buttonSelectFile.Enabled = false;
                
                System.IO.SearchOption searchOption;
                if (checkBoxRecursive.Checked)
                {
                    searchOption = System.IO.SearchOption.AllDirectories;
                }
                else
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                }

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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            hashBackgroundWorker.CancelAsync();

            //buttonCancel.Enabled = false;
        }

        private void HashBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            List<object> arguments = e.Argument as List<object>;

            List<string> filesToHash = new List<string>();

            // retrieve all files to hash from user input
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
            bw.ReportProgress(0, filesToHash.Count);

            using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.HashAlgorithm.Create((string)arguments[1]))
            {
                // if using a keyed hash, set the key before computing the hash
                if (hash.GetType().IsSubclassOf(typeof(System.Security.Cryptography.KeyedHashAlgorithm)))
                {
                    byte[] key = System.Text.Encoding.ASCII.GetBytes((string)arguments[2]);
                    ((System.Security.Cryptography.KeyedHashAlgorithm)hash).Key = key;
                }

                foreach (string file in filesToHash)
                {
                    if (bw.CancellationPending)
                    {
                        hashResults.Add(file, "Cancelled");
                        e.Cancel = true;
                        break;
                    }

                    hashResults.Add(file, HashFile(file, hash));
                    bw.ReportProgress(0);
                }
            }
        }

        private void HashBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // userstate will be set to the number of files to hash
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
            if (e.Cancelled || e.Error != null)
            {
                progressBarHash.Value = 0;
            }

            //PERFORMANCE TESTING
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            dataGridViewHash.SuspendLayout();
            
            // testing with 20000 elements and 

            // nothing disabled - 60 seconds 
            // 5 second refresh with all disabled

            //dataGridViewHash.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // 58 seconds
            dataGridViewHash.RowHeadersVisible = false;
            //dataGridViewHash.AutoSize = false;
            //dataGridViewHash.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //dataGridViewHash.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dataGridViewHash.Rows.Clear();

            dataGridViewHash.Columns[1].HeaderCell.Value = "Hash (" + comboBoxHash.Text + ")";

            foreach (KeyValuePair<string, string> result in hashResults)
            {
                dataGridViewHash.Rows.Add(result.Key, result.Value);
            }

            //dataGridViewHash.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            //dataGridViewHash.RowHeadersVisible = true;

            dataGridViewHash.ResumeLayout();

            hashResults.Clear();

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

            //now restore the controls
            buttonCancel.Enabled = false;
            buttonCalculate.Enabled = true;
            comboBoxHash.Enabled = true;
            textBoxHashKey.Enabled = IsHashAlgorithm(comboBoxHash.Text);
            textBoxFileOrDirectoryToHash.Enabled = true;
            radioButtonFile.Enabled = true;
            radioButtonDirectory.Enabled = true;
            checkBoxRecursive.Enabled = radioButtonDirectory.Checked;
            dataGridViewHash.Enabled = true;
            buttonSelectFile.Enabled = true;
        }

        private string HashFile(string file, System.Security.Cryptography.HashAlgorithm hash)
        {
            using (System.IO.FileStream stream = System.IO.File.OpenRead(file)) //TODO: needs protection
            {
                byte[] hashOutput = hash.ComputeHash(stream);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashOutput)
                {
                    sb.Append(b.ToString("X2", System.Globalization.CultureInfo.CurrentCulture));
                }

                return sb.ToString();
            }
        }

        private bool CheckCalculateButtonValidity(bool IncludeFileCount)
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
                        }
                        // all other keyed hashes only require non empty keys
                        else
                        {
                            valid = !string.IsNullOrEmpty(textBoxHashKey.Text);
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

            string[] list = textBoxFileOrDirectoryToHash.Text.Split(';');

            foreach (string fileOrDirectory in list)
            {
                if (System.IO.File.Exists(fileOrDirectory) || System.IO.Directory.Exists(fileOrDirectory))
                {
                    // return true if there is at least one valid entry
                    valid = true;
                    break;
                }
            }

            if (IncludeFileCount)
            {
                labelFilesCount.Text = "Number of file(s) to hash: ...";

                while (countBackgroundWorker.IsBusy)
                {
                    countBackgroundWorker.CancelAsync();
                    Application.DoEvents();
                }

                System.IO.SearchOption searchOption;
                if (checkBoxRecursive.Checked)
                {
                    searchOption = System.IO.SearchOption.AllDirectories;
                }
                else
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                }

                List<object> arguments = new List<object>
                {
                    list,
                    searchOption
                };

                countBackgroundWorker.RunWorkerAsync(arguments);
            }

            return valid;
        }

        private void CountBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            List<object> arguments = e.Argument as List<object>;

            int fileCount = 0;
            foreach (string fileOrDirectory in (string[])arguments[0])
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                if (System.IO.File.Exists(fileOrDirectory))
                {
                    fileCount++;
                }
                else if (System.IO.Directory.Exists(fileOrDirectory))
                {
                    fileCount = fileCount + System.IO.Directory.GetFiles(fileOrDirectory, "*", (System.IO.SearchOption)arguments[1]).Count(); //TODO: needs protection
                }
            }

            e.Result = fileCount;
        }

        private void CountBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                labelFilesCount.Text = "Number of file(s) to hash: Error";
            }
            else if (e.Cancelled)
            {
                labelFilesCount.Text = "Number of file(s) to hash: ...";
            }
            else
            {
                labelFilesCount.Text = "Number of file(s) to hash: " + ((int)e.Result).ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        //TODO: probably use elsewhere but careful of the using
        private bool IsHashAlgorithm(string hash)
        {
            bool isHashAlgorithm = false;

            // set the state of the key text box based on whether a keyed hash has been selected
            using (System.Security.Cryptography.HashAlgorithm hashAlgorithm = System.Security.Cryptography.HashAlgorithm.Create(hash))
            {
                //System.Security.Cryptography.KeyedHashAlgorithm keyedHash = hash as System.Security.Cryptography.KeyedHashAlgorithm;
                //if (keyedHash != null)  //TODO: test performance
                isHashAlgorithm = (hashAlgorithm.GetType().IsSubclassOf(typeof(System.Security.Cryptography.KeyedHashAlgorithm)));
            }

            return isHashAlgorithm;
        }
    }
}
