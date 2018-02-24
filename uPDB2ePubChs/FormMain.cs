using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uPDB2ePubChs
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private List<UnicodePDB> Books = new List<UnicodePDB>();

        private void ToolStripMenuItemCompatibility_Click(object sender, EventArgs e)
        {
            if (!ToolStripMenuItemCompatibility.Checked)
            {
                ToolStripMenuItemCompatibility.Checked = true;
                ToolStripMenuItemIdeal.Checked = false;
            }
        }

        private void ToolStripMenuItemIdeal_Click(object sender, EventArgs e)
        {
            if (!ToolStripMenuItemIdeal.Checked)
            {
                ToolStripMenuItemCompatibility.Checked = false;
                ToolStripMenuItemIdeal.Checked = true;
            }
        }

        private String OutDir;
        private void ToolStripSplitButtonConvert_ButtonClick(object sender, EventArgs e)
        {
            if (BackgroundWorkerProcess.IsBusy)
            {
                if (cts != null)
                {
                    cts.Cancel();
                }
            }
            else if (FolderBrowserDialogSave.ShowDialog(this) == DialogResult.OK)
            {
                OutDir = FolderBrowserDialogSave.SelectedPath;
                ListViewUPdb.AllowDrop = false;
                ToolStripSplitButtonConvert.Text = "取消";
                ToolStripMenuItemCompatibility.Enabled = false;
                ToolStripMenuItemIdeal.Enabled = false;
                ToolStripSplitButtonConvert.ToolTipText = "取消转换任务。";
                BackgroundWorkerProcess.RunWorkerAsync();
            }
        }

        private void ListViewUPdb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;

        }


        private void ListViewUPdb_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ListViewUPdb.Items.Clear();
                ClearBooks();
                foreach (var item in e.Data.GetData(DataFormats.FileDrop) as String[])
                {
                    try
                    {
                        var fi = new FileInfo(item);
                        if (fi.Attributes.HasFlag(FileAttributes.Directory))
                        {
                            TraverseDir(new DirectoryInfo(item));
                        }
                        else
                        {
                            AddFile(fi);
                        }
                    }
                    catch { }//不处理。

                }
                ToolStripSplitButtonConvert.Enabled = ListViewUPdb.Items.Count > 0;
            }
        }

        private void TraverseDir(DirectoryInfo di)
        {
            foreach (var file in di.GetFiles())
            {
                AddFile(file);
            }

            foreach (var dir in di.GetDirectories())
            {
                TraverseDir(dir);
            }
        }


        private void AddFile(FileInfo fi)
        {
            if (fi.Extension.Equals(".updb", StringComparison.OrdinalIgnoreCase))
            {
                var book = new UnicodePDB(fi.FullName);
                if (book.MD5 == null)
                {
                    book = null;
                    return;
                }
                foreach (var item in Books)
                {
                    if (item.MD5.SequenceEqual(book.MD5))
                    {
                        return;
                    }
                }
                Books.Add(book);
                var li = new ListViewItem(book.Title);
                li.SubItems.Add(book.Author);
                li.SubItems.Add(String.Empty);
                li.ToolTipText = fi.FullName;
                ListViewUPdb.Items.Add(li);
            }
        }

        private void ClearBooks()
        {
            foreach (var item in Books)
            {
                item.Close();
            }
            Books.Clear();
        }


        private CancellationTokenSource cts = null;

        private void BackgroundWorkerProcess_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            cts = new CancellationTokenSource();

            var po = new ParallelOptions
            {
                CancellationToken = cts.Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            try
            {
                Parallel.For(0, Books.Count, po, i =>
                {
                    if (Books[i].ToEPub($"{OutDir}\\《{Books[i].Title}》{Books[i].Author}_{DateTime.Now.ToString("yyMMddHHmmssffff")}.epub", ToolStripMenuItemIdeal.Checked))
                    {
                        BackgroundWorkerProcess.ReportProgress(i, "完成");
                    }
                    else
                    {
                        BackgroundWorkerProcess.ReportProgress(i, "失败");
                    }
                });
            }
            catch(Exception ex)
            {
                var q = ex;
            }
            finally
            {
                if (cts != null)
                {
                    cts.Dispose(); cts = null;
                }
            }
        }

        private void BackgroundWorkerProcess_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ListViewUPdb.Items[e.ProgressPercentage].SubItems[2].Text = e.UserState as String;
        }

        private void BackgroundWorkerProcess_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ToolStripSplitButtonConvert.Text = "转换";
            ToolStripSplitButtonConvert.ToolTipText = "将列表中的文件转换为简体ePub格式。";
            ToolStripMenuItemCompatibility.Enabled = true;
            ToolStripMenuItemIdeal.Enabled = true;
            ListViewUPdb.AllowDrop = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorkerProcess.IsBusy)
            {
                if (cts != null)
                {
                    cts.Cancel();
                }
            }
        }
    }
}
