using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using Subtitle_Downloader;


namespace VideoSubtitleDownloader.OSDb
{


    class VideoSusbtitleDownloader
    {





        internal static bool GetSubtitleForVideo
            (string videoHash, string language,
            string parentPath, string videoFilename,
            bool isMovie, MainForm mainForm)
        {


            #region  vars

            var webClient
                = new WebClient();

            webClient.Headers.Add
                ("user-agent",
                "Mozilla/4.0" +
                " (compatible; MSIE 6.0;" +
                " Windows NT 5.2;" +
                " .NET CLR 1.0.3705;)");
           

            string firstsub
                = string.Empty;
            
            var fz = new FastZip();

            #endregion


            if (string.IsNullOrEmpty(parentPath))
                return false;

            if (!parentPath.EndsWith(@"\"))
                parentPath += @"\";

            string zipfilePath
                = parentPath
                + videoHash 
                + ".zip";


            try
            {

                mainForm.UpdateProgress
                    ("Searching OSDb for subtitle...");
            
                firstsub = VideoSubtitleDownloaderHelpers
                    .SearchForSubtitleByVideoHashParent
                    (videoHash, language);



                mainForm.UpdateProgress
                    ("Downloading subtitle...");


                if (!VideoSubtitleDownloaderHelpers
                    .PerformSubtitleDownload
                    (zipfilePath, webClient, firstsub))
                    return false;


            }
            catch (Exception e)
            {
                MessageBox.Show
               (@"An error occured while trying to download
                the subtitle on online address: "
                + firstsub + @" to local location: "
                + zipfilePath + @". The error was: " + e);

                return false;
            
            }


            mainForm.UpdateProgress
                ("Validating downloaded data...");

            if (!VideoSubtitleDownloaderHelpers
                .ValidateDownloadedDataAndRetry
                (language, firstsub,
                webClient, zipfilePath)) 
                return false;


            mainForm.UpdateProgress
                ("Extracting subtitle file...");

            VideoSubtitleDownloaderHelpers
                .ExtractAndRenameSubtitle
                (language, parentPath,
                videoFilename, zipfilePath, fz);


            mainForm.UpdateProgress
                ("All Done! Bye bye...");

            Thread.Sleep(2000);

            Application.Exit();


            return true;


        }




        internal static bool DownloadSubtitleForVideoParent
            (string videoHash, string location,
            bool useSameFolder, string subtitlesFolder,
            bool isMovie, MainForm mainForm)
        {


            string subfilePathSrt;
            string subfilePathSub;
            string parentPath;



            mainForm.UpdateProgress
                ("Constructing subtitle path...");
           
            var videoFilename
                = VideoSubtitleDownloaderHelpers
                .ConstructSubtitlePath
                (location, useSameFolder,
                subtitlesFolder, out subfilePathSrt,
                out subfilePathSub, out parentPath);


            mainForm.UpdateProgress
                ("Checking for existing subtitle...");

            if (!VideoSubtitleDownloaderHelpers
                .CheckForExistingSubtitleSetHasSubtitleFlag
                (videoHash, parentPath,
                subfilePathSrt, subfilePathSub))
                return false;


            var subsDownloadResult
                = VideoSubtitleDownloaderHelpers
                .DownloadSubtitleForPrimaryOrSecondaryLanguage
                (videoHash, isMovie,
                videoFilename, parentPath, mainForm);


            VideoSubtitleDownloaderHelpers
                .DeleteSubtitleZipFile
                (videoHash, parentPath);



            return subsDownloadResult;


        }
    
    
    }



}
