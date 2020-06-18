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

namespace MELCORUncertaintyInputFileEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TsmiNewWindow_Click(object sender, EventArgs e)
        {
            var frm = new MainForm();
            frm.Show();
        }

        private void TsmiOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            foreach (var fileName in openFileDialog1.FileNames)
            {
                try
                {
                    var lvi = new ListViewItem(Path.GetFileName(fileName));
                    lvi.SubItems.Add(Path.GetFileNameWithoutExtension(fileName));
                    lvi.SubItems.Add(fileName);
                    lvwFiles.Items.Add(lvi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void TsmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LvwFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void LvwFiles_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var fileName in fileNames)
            {
                if (Path.GetExtension(fileName).Equals(".dat"))
                {
                    try
                    {
                        var lvi = new ListViewItem(Path.GetFileName(fileName));
                        lvi.SubItems.Add(Path.GetFileNameWithoutExtension(fileName));
                        lvi.SubItems.Add(fileName);
                        lvwFiles.Items.Add(lvi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void LvwFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in lvwFiles.SelectedItems)
            {
                tabControl1.TabPages.Add(item.SubItems[0].Text);
                var textBox = new TextBox()
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Both,
                    Font = new Font("Tahoma", 12),
                    Text = ReadFiles(item.SubItems[2].Text.ToString()),
                };
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textBox);
                tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
            }
        }

        private string ReadFiles(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    while (!streamReader.EndOfStream)
                    {
                        stringBuilder.AppendLine(streamReader.ReadLine());
                    }
                }
            }
            return stringBuilder.ToString();
        }

        private void TsmiRun_Click(object sender, EventArgs e)
        {
            if (lvwFiles.Items.Count == 0)
            {
                return;
            }
            foreach (ListViewItem item in lvwFiles.Items)
            {
                WriteFiles(item.SubItems[2].Text.ToString());
            }
            MessageBox.Show("Done!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

