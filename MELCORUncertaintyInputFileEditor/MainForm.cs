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

        public void ViewSelectedFile(string selectedFile)
        {
            var frmTextViewer = new TextViewerForm(selectedFile);
            frmTextViewer.TabText = Path.GetFileName(selectedFile);
            frmTextViewer.Show(this.dockPnlMain, DockState.Document);
        }

    }
}
