namespace DownloadPicturesGUI
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTop = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbTop = new System.Windows.Forms.GroupBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.gbInfomations = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.btnGetPictures = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.gbProgress = new System.Windows.Forms.GroupBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.bwDownload = new System.ComponentModel.BackgroundWorker();
            this.menuStrip.SuspendLayout();
            this.gbTop.SuspendLayout();
            this.gbInfomations.SuspendLayout();
            this.gbProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(6, 15);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(35, 12);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "label1";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // gbTop
            // 
            this.gbTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTop.Controls.Add(this.lblTop);
            this.gbTop.Location = new System.Drawing.Point(12, 27);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(560, 100);
            this.gbTop.TabIndex = 2;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "DownloadPictures";
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gbInfomations
            // 
            this.gbInfomations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfomations.Controls.Add(this.btnSelectFolder);
            this.gbInfomations.Controls.Add(this.tbFolder);
            this.gbInfomations.Controls.Add(this.tbUrl);
            this.gbInfomations.Controls.Add(this.lblFolder);
            this.gbInfomations.Controls.Add(this.lblUrl);
            this.gbInfomations.Location = new System.Drawing.Point(278, 133);
            this.gbInfomations.Name = "gbInfomations";
            this.gbInfomations.Size = new System.Drawing.Size(294, 80);
            this.gbInfomations.TabIndex = 4;
            this.gbInfomations.TabStop = false;
            this.gbInfomations.Text = "Imformations";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(265, 37);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(23, 19);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(88, 37);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(171, 19);
            this.tbFolder.TabIndex = 3;
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(39, 12);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(249, 19);
            this.tbUrl.TabIndex = 2;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 40);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(76, 12);
            this.lblFolder.TabIndex = 1;
            this.lblFolder.Text = "保存先フォルダ";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(6, 15);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(27, 12);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(8, 301);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "全選択";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Location = new System.Drawing.Point(158, 301);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(130, 23);
            this.btnStartDownload.TabIndex = 6;
            this.btnStartDownload.Text = "ダウンロードする";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // btnGetPictures
            // 
            this.btnGetPictures.Location = new System.Drawing.Point(158, 272);
            this.btnGetPictures.Name = "btnGetPictures";
            this.btnGetPictures.Size = new System.Drawing.Size(130, 23);
            this.btnGetPictures.TabIndex = 5;
            this.btnGetPictures.Text = "URLから画像を取得する";
            this.btnGetPictures.UseVisualStyleBackColor = true;
            this.btnGetPictures.Click += new System.EventHandler(this.btnGetPictures_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Location = new System.Drawing.Point(12, 133);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(260, 416);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // gbProgress
            // 
            this.gbProgress.Controls.Add(this.lblProgress);
            this.gbProgress.Controls.Add(this.btnStartDownload);
            this.gbProgress.Controls.Add(this.btnSelectAll);
            this.gbProgress.Controls.Add(this.btnGetPictures);
            this.gbProgress.Location = new System.Drawing.Point(278, 219);
            this.gbProgress.Name = "gbProgress";
            this.gbProgress.Size = new System.Drawing.Size(294, 330);
            this.gbProgress.TabIndex = 5;
            this.gbProgress.TabStop = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(6, 15);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(50, 12);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.Text = "Progress";
            // 
            // bwDownload
            // 
            this.bwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownload_DoWork);
            this.bwDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDownload_RunWorkerCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.gbProgress);
            this.Controls.Add(this.gbInfomations);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.gbTop);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.gbInfomations.ResumeLayout(false);
            this.gbInfomations.PerformLayout();
            this.gbProgress.ResumeLayout(false);
            this.gbProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbTop;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox gbInfomations;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Button btnGetPictures;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.GroupBox gbProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.ComponentModel.BackgroundWorker bwDownload;
    }
}

