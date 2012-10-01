using System;
using System.Windows.Forms;
using VideoSubtitleDownloader;

namespace Subtitle_Downloader
{



    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, EventArgs e)
        {
            openVideoFileDialog.ShowDialog();


            ProgressLabel.Text = "Calculating Video Fingerprint...";
            Update();
            Application.DoEvents();

            string fileHash
                = Hasher.ComputeHash
                (openVideoFileDialog.FileName);


            VideoSubtitleDownloader.OSDb
                .VideoSusbtitleDownloader
                .DownloadSubtitleForVideoParent
                (fileHash, openVideoFileDialog.FileName,
                false, "", true, this);

        }


        internal void UpdateProgress(string progressText)
        {

            ProgressLabel.Text = progressText;
            Update();
            Application.DoEvents();

        }



    }



}
