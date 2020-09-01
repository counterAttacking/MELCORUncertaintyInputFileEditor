namespace MELCORUncertaintyInputFileEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainFormRibbon = new System.Windows.Forms.Ribbon();
            this.ribbonTabFile = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonBtnOpenFolder = new System.Windows.Forms.RibbonButton();
            this.ribbonBtnOpenFile = new System.Windows.Forms.RibbonButton();
            this.dockPnlMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.SuspendLayout();
            // 
            // mainFormRibbon
            // 
            this.mainFormRibbon.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.mainFormRibbon.Location = new System.Drawing.Point(0, 0);
            this.mainFormRibbon.Minimized = false;
            this.mainFormRibbon.Name = "mainFormRibbon";
            // 
            // 
            // 
            this.mainFormRibbon.OrbDropDown.BorderRoundness = 8;
            this.mainFormRibbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.mainFormRibbon.OrbDropDown.Name = "";
            this.mainFormRibbon.OrbDropDown.Size = new System.Drawing.Size(527, 447);
            this.mainFormRibbon.OrbDropDown.TabIndex = 0;
            this.mainFormRibbon.OrbDropDown.Visible = false;
            this.mainFormRibbon.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.mainFormRibbon.OrbVisible = false;
            // 
            // 
            // 
            this.mainFormRibbon.QuickAccessToolbar.Visible = false;
            this.mainFormRibbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.mainFormRibbon.Size = new System.Drawing.Size(784, 141);
            this.mainFormRibbon.TabIndex = 0;
            this.mainFormRibbon.Tabs.Add(this.ribbonTabFile);
            this.mainFormRibbon.TabSpacing = 4;
            // 
            // ribbonTabFile
            // 
            this.ribbonTabFile.Name = "ribbonTabFile";
            this.ribbonTabFile.Panels.Add(this.ribbonPanel1);
            this.ribbonTabFile.Text = "File";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonBtnOpenFolder);
            this.ribbonPanel1.Items.Add(this.ribbonBtnOpenFile);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "Open";
            // 
            // ribbonBtnOpenFolder
            // 
            this.ribbonBtnOpenFolder.Image = global::MELCORUncertaintyInputFileEditor.Properties.Resources.newFolder_48;
            this.ribbonBtnOpenFolder.LargeImage = global::MELCORUncertaintyInputFileEditor.Properties.Resources.newFolder_48;
            this.ribbonBtnOpenFolder.Name = "ribbonBtnOpenFolder";
            this.ribbonBtnOpenFolder.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonBtnOpenFolder.SmallImage")));
            this.ribbonBtnOpenFolder.Text = "Folder";
            this.ribbonBtnOpenFolder.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbonBtnOpenFolder.Click += new System.EventHandler(this.RibbonBtnOpenFolder_Click);
            // 
            // ribbonBtnOpenFile
            // 
            this.ribbonBtnOpenFile.Image = global::MELCORUncertaintyInputFileEditor.Properties.Resources.newFile_48;
            this.ribbonBtnOpenFile.LargeImage = global::MELCORUncertaintyInputFileEditor.Properties.Resources.newFile_48;
            this.ribbonBtnOpenFile.Name = "ribbonBtnOpenFile";
            this.ribbonBtnOpenFile.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonBtnOpenFile.SmallImage")));
            this.ribbonBtnOpenFile.Text = "File";
            this.ribbonBtnOpenFile.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbonBtnOpenFile.Click += new System.EventHandler(this.RibbonBtnOpenFile_Click);
            // 
            // dockPnlMain
            // 
            this.dockPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPnlMain.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dockPnlMain.Location = new System.Drawing.Point(0, 141);
            this.dockPnlMain.Name = "dockPnlMain";
            this.dockPnlMain.Padding = new System.Windows.Forms.Padding(6);
            this.dockPnlMain.ShowAutoHideContentOnHover = false;
            this.dockPnlMain.Size = new System.Drawing.Size(784, 420);
            this.dockPnlMain.TabIndex = 1;
            this.dockPnlMain.Theme = this.vS2015DarkTheme1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dockPnlMain);
            this.Controls.Add(this.mainFormRibbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MELCOR Uncertainty Input File Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon mainFormRibbon;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPnlMain;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.RibbonTab ribbonTabFile;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFolder;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFile;
    }
}