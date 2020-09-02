using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MELCORUncertaintyInputFileEditor
{
    public partial class MainForm : RibbonForm
    {
        private ExplorerForm frmExplorer;

        public MainForm()
        {
            InitializeComponent();

            this.frmExplorer = new ExplorerForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.frmExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void RibbonBtnOpenFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog();
            openFolderDialog.IsFolderPicker = true;
            openFolderDialog.Multiselect = true;
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Cancel)
            {
                return;
            }

            List<DATFile> datFiles = new List<DATFile>();
            DirectoryInfo directoryInfo = new DirectoryInfo(openFolderDialog.FileName);
            if (directoryInfo.GetDirectories().Length > 0)
            {
                foreach (var dir in directoryInfo.GetDirectories())
                {
                    this.DirFileSearch(dir.FullName);
                }
            }
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(file.Name).Equals(".dat"))
                {
                    try
                    {
                        var datFile = new DATFile();
                        datFile.name = Path.GetFileName(file.Name);
                        datFile.path = file.FullName;
                        datFiles.Add(datFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            this.frmExplorer.AddDATFiles(datFiles);
        }

        private void RibbonBtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DAT File|*.dat";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            List<DATFile> datFiles = new List<DATFile>();
            foreach (var file in openFileDialog.FileNames)
            {
                try
                {
                    var datFile = new DATFile();
                    datFile.name = Path.GetFileName(file);
                    datFile.path = file;
                    datFiles.Add(datFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            this.frmExplorer.AddDATFiles(datFiles);
        }

        private void RibbonBtnRun_Click(object sender, EventArgs e)
        {
            this.frmExplorer.Run();
        }

        public void ViewSelectedFile(string selectedFile)
        {
            var frmTextViewer = new TextViewerForm(selectedFile);
            frmTextViewer.TabText = Path.GetFileName(selectedFile);
            frmTextViewer.Show(this.dockPnlMain, DockState.Document);
        }

        private void DirFileSearch(string dirPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
            foreach (var dir in Directory.GetDirectories(dirPath))
            {
                this.DirFileSearch(dir);
            }

            List<DATFile> datFiles = new List<DATFile>();
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(file.Name).Equals(".dat"))
                {
                    try
                    {
                        var datFile = new DATFile();
                        datFile.name = Path.GetFileName(file.Name);
                        datFile.path = file.FullName;
                        datFiles.Add(datFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            this.frmExplorer.AddDATFiles(datFiles);
        }

    }
}
