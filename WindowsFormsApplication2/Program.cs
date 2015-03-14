using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


//Modified example code from: http://support.microsoft.com/en-us/kb/303974
namespace RecursiveSearchCS
{
    /// <summary>
    /// Summary description for Form1
    /// </summary>
    public class DuplicationFinderForm : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label lblDirectory;
        internal System.Windows.Forms.ListBox lstFilesFound;
        internal System.Windows.Forms.ComboBox cboDirectory;
        /// <summary>
        /// Required designer variable
        /// </summary>
        private System.ComponentModel.Container components = null;
        private Button ProcessBtn;
        private ProgressBar progressBar1;
        ArrayList fileList = new ArrayList();
        private Label processingLabel;
        private Label sizeLabel;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button Browse;
        private Button deleteBtn;
        long totalSize;
        BinaryTree bst = new BinaryTree();


        public DuplicationFinderForm()
        {
            // 
            // Required for Windows Form Designer support
            // 
            InitializeComponent();

            // 
            // TODO: Add any constructor code after InitializeComponent call.
            // 
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support: do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.lstFilesFound = new System.Windows.Forms.ListBox();
            this.cboDirectory = new System.Windows.Forms.ComboBox();
            this.ProcessBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.processingLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Browse = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 268);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblDirectory
            // 
            this.lblDirectory.Location = new System.Drawing.Point(13, 244);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(55, 23);
            this.lblDirectory.TabIndex = 3;
            this.lblDirectory.Text = "Look In:";
            // 
            // lstFilesFound
            // 
            this.lstFilesFound.HorizontalScrollbar = true;
            this.lstFilesFound.Location = new System.Drawing.Point(12, 12);
            this.lstFilesFound.Name = "lstFilesFound";
            this.lstFilesFound.Size = new System.Drawing.Size(679, 225);
            this.lstFilesFound.TabIndex = 1;
            // 
            // cboDirectory
            // 
            this.cboDirectory.DropDownWidth = 112;
            this.cboDirectory.Location = new System.Drawing.Point(74, 243);
            this.cboDirectory.Name = "cboDirectory";
            this.cboDirectory.Size = new System.Drawing.Size(421, 21);
            this.cboDirectory.TabIndex = 2;
            this.cboDirectory.Text = "ComboBox1";
            // 
            // ProcessBtn
            // 
            this.ProcessBtn.Enabled = false;
            this.ProcessBtn.Location = new System.Drawing.Point(93, 268);
            this.ProcessBtn.Name = "ProcessBtn";
            this.ProcessBtn.Size = new System.Drawing.Size(75, 23);
            this.ProcessBtn.TabIndex = 6;
            this.ProcessBtn.Text = "Process";
            this.ProcessBtn.UseVisualStyleBackColor = true;
            this.ProcessBtn.Click += new System.EventHandler(this.ProcessBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(174, 268);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(517, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 7;
            // 
            // processingLabel
            // 
            this.processingLabel.AutoSize = true;
            this.processingLabel.Location = new System.Drawing.Point(12, 298);
            this.processingLabel.Name = "processingLabel";
            this.processingLabel.Size = new System.Drawing.Size(64, 13);
            this.processingLabel.TabIndex = 8;
            this.processingLabel.Text = "Not working";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(12, 311);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(33, 13);
            this.sizeLabel.TabIndex = 9;
            this.sizeLabel.Text = "Size: ";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(501, 241);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 10;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Enabled = false;
            this.deleteBtn.Location = new System.Drawing.Point(582, 241);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(109, 23);
            this.deleteBtn.TabIndex = 11;
            this.deleteBtn.Text = "Delete Duplicates";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // DuplicationFinderForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(705, 332);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.processingLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ProcessBtn);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.lstFilesFound);
            this.Controls.Add(this.cboDirectory);
            this.Name = "DuplicationFinderForm";
            this.Text = "Duplicate File Finder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new DuplicationFinderForm());
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            lstFilesFound.Items.Clear();
            fileList.Clear();
            cboDirectory.Enabled = false;
            btnSearch.Text = "Searching...";
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            DirSearch(cboDirectory.Text, false);
            btnSearch.Text = "Search";
            this.Cursor = Cursors.Default;
            btnSearch.Enabled = false;
            ProcessBtn.Enabled = true;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            cboDirectory.Items.Clear();
            foreach (string s in Directory.GetLogicalDrives())
            {
                cboDirectory.Items.Add(s);
            }
            cboDirectory.Text = "C:\\";

        }
        void searchFiles(string sDir)
        {
            foreach (string f in Directory.GetFiles(sDir, "*.*"))
            {
                try
                {
                    fileList.Add(f);
                }
                catch (System.Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                }

            }

            DirectoryInfo di = new DirectoryInfo(sDir);
            FileInfo[] fiArr = di.GetFiles();

            foreach (FileInfo f in fiArr)
                totalSize += f.Length / 8192;

        }
        void DirSearch(string sDir, bool baseSearched)
        {

            if (!baseSearched)
            {
                searchFiles(sDir);
            }

            foreach (string d in Directory.GetDirectories(sDir))
            {
                try
                {
                    searchFiles(d);
                    DirSearch(d, true);
                }
                catch (System.Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                }

            }
        }

        private void ProcessBtn_Click(object sender, EventArgs e)
        {
            ProcessBtn.Text = "Processing...";
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            progressBar1.Maximum = (int)totalSize;
            

            foreach (string filename in fileList)
            {
                // modified code from: http://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file
                using (var md5 = MD5.Create())
                {
                    try
                    {
                        using (var stream = File.OpenRead(filename))
                        {
                            HashInfo temp = new HashInfo();
                            processingLabel.Text = "Currently processing file: " + filename;
                            sizeLabel.Text = "Size: " + Math.Round(new FileInfo(filename).Length / 1048576.0, 2).ToString() + " MB  Total size of duplicates found: " + Math.Round(bst.totalDuplicateSize / 1024, 2) + " MB  Duplicates Found: " + bst.duplicateCount;
                            Application.DoEvents();
                            temp.hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                            temp.filename = filename;
                            bst.Add(temp);
                            progressBar1.Increment((int)new FileInfo(filename).Length / 8192);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        //ignore file
                    }
                }
            }
            foreach (string dupliacte in bst.duplicatesMessage)
            {
                lstFilesFound.Items.Add(dupliacte);
            }

            ProcessBtn.Text = "Process";
            this.Cursor = Cursors.Default;
            cboDirectory.Enabled = true;
            ProcessBtn.Enabled = false;
            btnSearch.Enabled = true;
            fileList.Clear();
            bst.Clear();
            progressBar1.Value = 0;
            deleteBtn.Enabled = true;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            cboDirectory.Items.Clear();
            cboDirectory.Items.Add(folderBrowserDialog1.SelectedPath);
            cboDirectory.Text = folderBrowserDialog1.SelectedPath;

        }

        // code used from: http://stackoverflow.com/questions/3282418/send-a-file-to-the-recycle-bin
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to send every second file to the Recycling Bin?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (string filename in bst.duplicateB)
                {
                    var shf = new SHFILEOPSTRUCT();
                    shf.wFunc = FO_DELETE;
                    shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
                    shf.pFrom = @filename;
                    SHFileOperation(ref shf);
                }
            }
            else if (result == DialogResult.No)
            {
            
            }
            deleteBtn.Enabled = false;
            lstFilesFound.Items.Clear();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        public const int FO_DELETE = 3;
        public const int FOF_ALLOWUNDO = 0x40;
        public const int FOF_NOCONFIRMATION = 0x10; // Don't prompt the user
    
    }
}
