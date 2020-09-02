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

        public void Run()
        {
            if (this.tvwFiles.Nodes.Count <= 0)
            {
                return;
            }
            CallRecursive(this.tvwFiles);
            MessageBox.Show("It's done.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CallRecursive(TreeView treeView)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode node in nodes)
            {
                foreach (TreeNode elem in node.Nodes)
                {
                    this.WriteFiles(elem.Name);
                }
            }
        }

        private void WriteFiles(string filePath)
        {
            double pdslp = 0.0, pfdb = 0.0, tmsrp = 0.0, teca = 0.0, idfoe = 0.0, mcscr = 0.0;
            StringBuilder stringBuilder = new StringBuilder();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var lineVal = streamReader.ReadLine();

                        // 값 가져오기
                        if (lineVal.Contains("@PDSLP@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            pdslp = Convert.ToDouble(tmp[tmp.IndexOf("@PDSLP@") + 1]);
                        }
                        if (lineVal.Contains("@PFDB@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            pfdb = Convert.ToDouble(tmp[tmp.IndexOf("@PFDB@") + 1]);
                        }
                        if (lineVal.Contains("@TMSRP@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            tmsrp = Convert.ToDouble(tmp[tmp.IndexOf("@TMSRP@") + 1]);
                        }
                        if (lineVal.Contains("@TECA@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            teca = Convert.ToDouble(tmp[tmp.IndexOf("@TECA@") + 1]);
                        }
                        if (lineVal.Contains("@IDFOE@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            idfoe = Convert.ToDouble(tmp[tmp.IndexOf("@IDFOE@") + 1]);
                        }
                        if (lineVal.Contains("@MCSCR@"))
                        {
                            var tmp = lineVal.Split(' ').ToList();
                            mcscr = Convert.ToDouble(tmp[tmp.IndexOf("@MCSCR@") + 1]);
                        }


                        // 값 치환하기
                        if (lineVal.Contains("&#DIRNAME#&"))
                        {
                            lineVal = lineVal.Replace("&#DIRNAME#&", Path.GetFileNameWithoutExtension(filePath));
                        }
                        if (lineVal.Contains("&#PDSLP#&"))
                        {
                            lineVal = lineVal.Replace("&#PDSLP#&", string.Format("{0:0.00E+0}", pdslp));
                        }
                        if (lineVal.Contains("&#PFDB#&"))
                        {
                            lineVal = lineVal.Replace("&#PFDB#&", string.Format("{0:0.00E+0}", pfdb));
                        }
                        if (lineVal.Contains("&#TMSRP#&"))
                        {
                            lineVal = lineVal.Replace("&#TMSRP#&", string.Format("{0:0.00E+0}", tmsrp));
                        }
                        if (lineVal.Contains("&#TECA#&"))
                        {
                            lineVal = lineVal.Replace("&#TECA#&", string.Format("{0:0.00E+0}", teca));
                        }
                        if (lineVal.Contains("&#IDFOE#&"))
                        {
                            lineVal = lineVal.Replace("&#IDFOE#&", string.Format("{0:0.00E+0}", idfoe));
                        }
                        if (lineVal.Contains("&#MCSCR#&"))
                        {
                            lineVal = lineVal.Replace("&#MCSCR#&", string.Format("{0:0.00E+0}", mcscr));
                        }

                        stringBuilder.AppendLine(lineVal);
                    }
                }
            }
            File.WriteAllText(filePath, stringBuilder.ToString(), Encoding.UTF8);
        }

    }
}
