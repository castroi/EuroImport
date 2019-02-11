namespace EuroImport
{
    partial class FrmMain
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
            this.btnExcelBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtImagesFolder = new System.Windows.Forms.TextBox();
            this.btnImageFolderBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCreate = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExcelBrowse
            // 
            this.btnExcelBrowse.Location = new System.Drawing.Point(370, 37);
            this.btnExcelBrowse.Name = "btnExcelBrowse";
            this.btnExcelBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnExcelBrowse.TabIndex = 0;
            this.btnExcelBrowse.Text = "בחר...";
            this.btnExcelBrowse.UseVisualStyleBackColor = true;
            this.btnExcelBrowse.Click += new System.EventHandler(this.btnExcelBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(15, 39);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFilePath.Size = new System.Drawing.Size(349, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.btnExcelBrowse);
            this.groupBox1.Location = new System.Drawing.Point(32, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "קובץ אקסל";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtImagesFolder);
            this.groupBox2.Controls.Add(this.btnImageFolderBrowse);
            this.groupBox2.Location = new System.Drawing.Point(32, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 76);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ספריית תמונות";
            // 
            // txtImagesFolder
            // 
            this.txtImagesFolder.Location = new System.Drawing.Point(15, 39);
            this.txtImagesFolder.Name = "txtImagesFolder";
            this.txtImagesFolder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtImagesFolder.Size = new System.Drawing.Size(349, 20);
            this.txtImagesFolder.TabIndex = 1;
            // 
            // btnImageFolderBrowse
            // 
            this.btnImageFolderBrowse.Location = new System.Drawing.Point(370, 37);
            this.btnImageFolderBrowse.Name = "btnImageFolderBrowse";
            this.btnImageFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnImageFolderBrowse.TabIndex = 0;
            this.btnImageFolderBrowse.Text = "בחר...";
            this.btnImageFolderBrowse.UseVisualStyleBackColor = true;
            this.btnImageFolderBrowse.Click += new System.EventHandler(this.btnImageFolderBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(199, 202);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(197, 69);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "צור נתונים";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 395);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExcelBrowse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtImagesFolder;
        private System.Windows.Forms.Button btnImageFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

