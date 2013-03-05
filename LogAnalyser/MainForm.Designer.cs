namespace LogAnalizer
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
            this.tbLogFolder = new System.Windows.Forms.TextBox();
            this.btSelectLogFolder = new System.Windows.Forms.Button();
            this.btRun = new System.Windows.Forms.Button();
            this.LogFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lbIPs = new System.Windows.Forms.ListBox();
            this.gbNumberOfIPs = new System.Windows.Forms.GroupBox();
            this.lblNumberOfIPs = new System.Windows.Forms.Label();
            this.gbTotalRequests = new System.Windows.Forms.GroupBox();
            this.lblNumberOfTotalRequests = new System.Windows.Forms.Label();
            this.lbLogFiles = new System.Windows.Forms.ListBox();
            this.gbNumberOfIPs.SuspendLayout();
            this.gbTotalRequests.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLogFolder
            // 
            this.tbLogFolder.Location = new System.Drawing.Point(12, 12);
            this.tbLogFolder.Name = "tbLogFolder";
            this.tbLogFolder.Size = new System.Drawing.Size(886, 20);
            this.tbLogFolder.TabIndex = 0;
            // 
            // btSelectLogFolder
            // 
            this.btSelectLogFolder.Location = new System.Drawing.Point(905, 12);
            this.btSelectLogFolder.Name = "btSelectLogFolder";
            this.btSelectLogFolder.Size = new System.Drawing.Size(113, 23);
            this.btSelectLogFolder.TabIndex = 1;
            this.btSelectLogFolder.Text = "Select Log Folder";
            this.btSelectLogFolder.UseVisualStyleBackColor = true;
            this.btSelectLogFolder.Click += new System.EventHandler(this.btSelectLogFolder_Click);
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(1026, 12);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(75, 23);
            this.btRun.TabIndex = 2;
            this.btRun.Text = "Run";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // lbIPs
            // 
            this.lbIPs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbIPs.FormattingEnabled = true;
            this.lbIPs.Location = new System.Drawing.Point(12, 38);
            this.lbIPs.Name = "lbIPs";
            this.lbIPs.Size = new System.Drawing.Size(588, 420);
            this.lbIPs.TabIndex = 3;
            // 
            // gbNumberOfIPs
            // 
            this.gbNumberOfIPs.Controls.Add(this.lblNumberOfIPs);
            this.gbNumberOfIPs.Location = new System.Drawing.Point(606, 38);
            this.gbNumberOfIPs.Name = "gbNumberOfIPs";
            this.gbNumberOfIPs.Size = new System.Drawing.Size(146, 41);
            this.gbNumberOfIPs.TabIndex = 4;
            this.gbNumberOfIPs.TabStop = false;
            this.gbNumberOfIPs.Text = "Number of IPs";
            // 
            // lblNumberOfIPs
            // 
            this.lblNumberOfIPs.AutoSize = true;
            this.lblNumberOfIPs.Location = new System.Drawing.Point(7, 20);
            this.lblNumberOfIPs.Name = "lblNumberOfIPs";
            this.lblNumberOfIPs.Size = new System.Drawing.Size(37, 13);
            this.lblNumberOfIPs.TabIndex = 0;
            this.lblNumberOfIPs.Text = "00000";
            // 
            // gbTotalRequests
            // 
            this.gbTotalRequests.Controls.Add(this.lblNumberOfTotalRequests);
            this.gbTotalRequests.Location = new System.Drawing.Point(606, 85);
            this.gbTotalRequests.Name = "gbTotalRequests";
            this.gbTotalRequests.Size = new System.Drawing.Size(146, 41);
            this.gbTotalRequests.TabIndex = 5;
            this.gbTotalRequests.TabStop = false;
            this.gbTotalRequests.Text = "Number of total requests";
            // 
            // lblNumberOfTotalRequests
            // 
            this.lblNumberOfTotalRequests.AutoSize = true;
            this.lblNumberOfTotalRequests.Location = new System.Drawing.Point(7, 20);
            this.lblNumberOfTotalRequests.Name = "lblNumberOfTotalRequests";
            this.lblNumberOfTotalRequests.Size = new System.Drawing.Size(37, 13);
            this.lblNumberOfTotalRequests.TabIndex = 0;
            this.lblNumberOfTotalRequests.Text = "00000";
            // 
            // lbLogFiles
            // 
            this.lbLogFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLogFiles.FormattingEnabled = true;
            this.lbLogFiles.Location = new System.Drawing.Point(606, 155);
            this.lbLogFiles.Name = "lbLogFiles";
            this.lbLogFiles.Size = new System.Drawing.Size(503, 303);
            this.lbLogFiles.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 464);
            this.Controls.Add(this.lbLogFiles);
            this.Controls.Add(this.gbTotalRequests);
            this.Controls.Add(this.gbNumberOfIPs);
            this.Controls.Add(this.lbIPs);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.btSelectLogFolder);
            this.Controls.Add(this.tbLogFolder);
            this.Name = "Form1";
            this.Text = "LogAnalizer";
            this.gbNumberOfIPs.ResumeLayout(false);
            this.gbNumberOfIPs.PerformLayout();
            this.gbTotalRequests.ResumeLayout(false);
            this.gbTotalRequests.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLogFolder;
        private System.Windows.Forms.Button btSelectLogFolder;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.FolderBrowserDialog LogFolderBrowserDialog;
        private System.Windows.Forms.ListBox lbIPs;
        private System.Windows.Forms.GroupBox gbNumberOfIPs;
        private System.Windows.Forms.Label lblNumberOfIPs;
        private System.Windows.Forms.GroupBox gbTotalRequests;
        private System.Windows.Forms.Label lblNumberOfTotalRequests;
        private System.Windows.Forms.ListBox lbLogFiles;
    }
}

