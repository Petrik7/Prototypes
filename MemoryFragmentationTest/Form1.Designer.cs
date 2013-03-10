namespace TestOutOfMemory
{
    partial class Form1
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
            this.buttonStreamTester = new System.Windows.Forms.Button();
            this.buttonRunTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonStreamTester
            // 
            this.buttonStreamTester.Location = new System.Drawing.Point(12, 12);
            this.buttonStreamTester.Name = "buttonStreamTester";
            this.buttonStreamTester.Size = new System.Drawing.Size(173, 23);
            this.buttonStreamTester.TabIndex = 0;
            this.buttonStreamTester.Text = "Create StreamTester";
            this.buttonStreamTester.UseVisualStyleBackColor = true;
            this.buttonStreamTester.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRunTest
            // 
            this.buttonRunTest.Enabled = false;
            this.buttonRunTest.Location = new System.Drawing.Point(12, 51);
            this.buttonRunTest.Name = "buttonRunTest";
            this.buttonRunTest.Size = new System.Drawing.Size(173, 23);
            this.buttonRunTest.TabIndex = 0;
            this.buttonRunTest.Text = "RunTest";
            this.buttonRunTest.UseVisualStyleBackColor = true;
            this.buttonRunTest.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonRunTest);
            this.Controls.Add(this.buttonStreamTester);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStreamTester;
        private System.Windows.Forms.Button buttonRunTest;
    }
}

