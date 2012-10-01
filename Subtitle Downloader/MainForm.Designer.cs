namespace Subtitle_Downloader
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
            this.openVideoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btn = new System.Windows.Forms.Button();
            this.LanguageSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openVideoFileDialog
            // 
            this.openVideoFileDialog.FileName = "openFileDialog1";
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(12, 12);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(159, 83);
            this.btn.TabIndex = 0;
            this.btn.Text = "Open Video File";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.BtnClick);
            // 
            // LanguageSelectionComboBox
            // 
            this.LanguageSelectionComboBox.FormattingEnabled = true;
            this.LanguageSelectionComboBox.Items.AddRange(new object[] {
            "afr",
            "alb",
            "ara",
            "arm",
            "bul",
            "chi",
            "scr",
            "cze",
            "dan",
            "nld",
            "eng",
            "est",
            "fin",
            "fra",
            "deu",
            "ell",
            "hun",
            "isl",
            "ind",
            "gle",
            "jpn",
            "kor",
            "lat",
            "lit",
            "ltz",
            "nor",
            "pol",
            "por",
            "ron",
            "rus",
            "srp",
            "slk",
            "slv",
            "spa",
            "swe",
            "tur",
            "ukr"});
            this.LanguageSelectionComboBox.Location = new System.Drawing.Point(199, 28);
            this.LanguageSelectionComboBox.Name = "LanguageSelectionComboBox";
            this.LanguageSelectionComboBox.Size = new System.Drawing.Size(66, 21);
            this.LanguageSelectionComboBox.TabIndex = 2;
            this.LanguageSelectionComboBox.Text = "eng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label3.Location = new System.Drawing.Point(184, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Primary language:";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Location = new System.Drawing.Point(177, 56);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(112, 28);
            this.ProgressLabel.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 111);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LanguageSelectionComboBox);
            this.Controls.Add(this.btn);
            this.Name = "MainForm";
            this.Text = "Easy Video Subtitle Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openVideoFileDialog;
        private System.Windows.Forms.Button btn;
        internal System.Windows.Forms.ComboBox LanguageSelectionComboBox;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label ProgressLabel;
    }
}

