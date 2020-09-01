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
    public partial class ExplorerForm : DockContent
    {
        private MainForm frmMain;

        public ExplorerForm(MainForm frmMain)
        {
            InitializeComponent();

            this.frmMain = frmMain;
        }

        public void AddDATFiles(List<DATFile> files)
        {
            foreach (var file in files)
            {
                var fileNodeText = Path.GetFileNameWithoutExtension(file.name);
                var fileNode = new TreeNode(fileNodeText);
                fileNode.Nodes.Add(file.path, file.name);
                this.tvwFiles.Nodes.Add(fileNode);
            }
        }

        private void TvwFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TvwFiles_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<DATFile> datFiles = new List<DATFile>();

            foreach (var file in files)
            {
                if (Path.GetExtension(file).Equals(".dat"))
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
            }
            this.AddDATFiles(datFiles);
        }

        private void TvwFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                this.frmMain.ViewSelectedFile(e.Node.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
