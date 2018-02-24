namespace uPDB2ePubChs
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripSplitButtonConvert = new System.Windows.Forms.ToolStripSplitButton();
            this.ToolStripMenuItemCompatibility = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIdeal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ListViewUPdb = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BackgroundWorkerProcess = new System.ComponentModel.BackgroundWorker();
            this.FolderBrowserDialogSave = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripSplitButtonConvert,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(354, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolStripSplitButtonConvert
            // 
            this.ToolStripSplitButtonConvert.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripSplitButtonConvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripSplitButtonConvert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCompatibility,
            this.ToolStripMenuItemIdeal});
            this.ToolStripSplitButtonConvert.Enabled = false;
            this.ToolStripSplitButtonConvert.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripSplitButtonConvert.Image")));
            this.ToolStripSplitButtonConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripSplitButtonConvert.Name = "ToolStripSplitButtonConvert";
            this.ToolStripSplitButtonConvert.Size = new System.Drawing.Size(48, 22);
            this.ToolStripSplitButtonConvert.Text = "转换";
            this.ToolStripSplitButtonConvert.ToolTipText = "将列表中的文件转换为简体ePub格式。";
            this.ToolStripSplitButtonConvert.ButtonClick += new System.EventHandler(this.ToolStripSplitButtonConvert_ButtonClick);
            // 
            // ToolStripMenuItemCompatibility
            // 
            this.ToolStripMenuItemCompatibility.Checked = true;
            this.ToolStripMenuItemCompatibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItemCompatibility.Name = "ToolStripMenuItemCompatibility";
            this.ToolStripMenuItemCompatibility.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemCompatibility.Text = "兼容模式";
            this.ToolStripMenuItemCompatibility.ToolTipText = "转换速度略慢，转换后的ePub文件稍大、加载速度略慢，但在所有ePub阅读器中均能够正常显示。";
            this.ToolStripMenuItemCompatibility.Click += new System.EventHandler(this.ToolStripMenuItemCompatibility_Click);
            // 
            // ToolStripMenuItemIdeal
            // 
            this.ToolStripMenuItemIdeal.Name = "ToolStripMenuItemIdeal";
            this.ToolStripMenuItemIdeal.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemIdeal.Text = "理想模式";
            this.ToolStripMenuItemIdeal.ToolTipText = "转换速度快，转换后的ePub文件稍小、加载速度快，但在在没有完全实现ePub标准的阅读器中可能显示异常。";
            this.ToolStripMenuItemIdeal.Click += new System.EventHandler(this.ToolStripMenuItemIdeal_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(231, 22);
            this.toolStripLabel1.Text = "拖拽uPDB文件或文件夹到下面的列表框中";
            // 
            // ListViewUPdb
            // 
            this.ListViewUPdb.AllowDrop = true;
            this.ListViewUPdb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListViewUPdb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.ListViewUPdb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewUPdb.FullRowSelect = true;
            this.ListViewUPdb.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewUPdb.Location = new System.Drawing.Point(0, 25);
            this.ListViewUPdb.MultiSelect = false;
            this.ListViewUPdb.Name = "ListViewUPdb";
            this.ListViewUPdb.ShowItemToolTips = true;
            this.ListViewUPdb.Size = new System.Drawing.Size(354, 131);
            this.ListViewUPdb.TabIndex = 4;
            this.ListViewUPdb.UseCompatibleStateImageBehavior = false;
            this.ListViewUPdb.View = System.Windows.Forms.View.Details;
            this.ListViewUPdb.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewUPdb_DragDrop);
            this.ListViewUPdb.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewUPdb_DragEnter);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "书名";
            this.columnHeader2.Width = 118;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "作者";
            this.columnHeader3.Width = 109;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "完成状态";
            this.columnHeader4.Width = 114;
            // 
            // BackgroundWorkerProcess
            // 
            this.BackgroundWorkerProcess.WorkerReportsProgress = true;
            this.BackgroundWorkerProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerProcess_DoWork);
            this.BackgroundWorkerProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProcess_ProgressChanged);
            this.BackgroundWorkerProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerProcess_RunWorkerCompleted);
            // 
            // FolderBrowserDialogSave
            // 
            this.FolderBrowserDialogSave.Description = "选择输出目录";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 156);
            this.Controls.Add(this.ListViewUPdb);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(370, 110);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "uPDB2ePubChs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton ToolStripSplitButtonConvert;
        private System.Windows.Forms.ListView ListViewUPdb;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCompatibility;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIdeal;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.ComponentModel.BackgroundWorker BackgroundWorkerProcess;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialogSave;
    }
}

