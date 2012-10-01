using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using Subtitle_Downloader;


// ReSharper disable CheckNamespace
namespace VideoSubtitleDownloader.OSDb
// ReSharper restore CheckNamespace
{

    class VideoSubtitleDownloaderHelpers
    {




        internal static void DeleteSubtitleZipFile(string itemName, string parentPath)
        {
            string subtitleZip = parentPath + "\\" + itemName + ".zip";

            try
            {
                File.Delete(subtitleZip);
            }
            catch (Exception e)
            {
                Debugger.LogMessageToFile(e.ToString());
            }

        }

        internal static string ConstructSubtitlePath(string location, bool useSameFolder, string subtitlesFolder,
                                                    out string subfilePathSrt, out string subfilePathSub, out string parentPath)
        {

            FileInfo locationFI = 
                new FileInfo(location);

            string videoFilename = locationFI.Name;
            parentPath = locationFI.DirectoryName;

            if (useSameFolder)
            {
                if (!String.IsNullOrEmpty(subtitlesFolder))
                    parentPath = subtitlesFolder;
            }


            string subfile = 
                videoFilename.Remove(videoFilename.Length - 3);
            
            string subfileSrt = subfile + "srt";
            string subfileSub = subfile + "sub";
            
            subfilePathSrt = parentPath + "\\" + subfileSrt;
            subfilePathSub = parentPath + "\\" + subfileSub;


            return videoFilename;
        }

        internal static bool CheckForExistingSubtitleSetHasSubtitleFlag
            (string itemName, string parentPath,
             string subfilePathSrt, string subfilePathSub)
        {


            if (File.Exists(subfilePathSrt) || File.Exists(subfilePathSub))
            {

                DeleteSubtitleZipFile(itemName, parentPath);
                return false; 
            
            }



            return true;
       
        }

        internal static string RecognizeCorrectSubtitle(string sFile)
        {
            try
            {

                ZipFile zip = 
                    new ZipFile(File.OpenRead(sFile));

                string subFilename = String.Empty;

                foreach (ZipEntry entry in zip)
                {

                    if (!entry.IsFile) 
                        continue;

                    FileInfo fi = new FileInfo(entry.Name);
                    
                    string ext = fi.Extension;
                    string name = fi.Name;


                    if (name.Contains("cd") || name.Contains("CD"))
                    {
                        if (name.Contains("1"))
                        {
                            if (ext == ".srt" || ext == ".sub")
                                subFilename = entry.Name;
                        }



                    }
                    else if (ext == ".srt" || ext == ".sub")
                        subFilename = entry.Name;
                }

                return subFilename;

            }
            catch (Exception)
            {

                return String.Empty;
            }


        }

        internal static bool DownloadSubtitleForPrimaryOrSecondaryLanguage
            (string moviehash, bool isMovie,
            string videoFilename, string parentPath,
            MainForm mainForm)
        {


            bool subsDownloadResult = VideoSusbtitleDownloader.GetSubtitleForVideo
                (moviehash, mainForm.LanguageSelectionComboBox.Text,
              parentPath, videoFilename, isMovie, mainForm);


            //if (!subsDownloadResult)
            //    subsDownloadResult = VideoSusbtitleDownloader.GetSubtitleForVideo
            //        (moviehash, Settings.SecondaryLanguage,
            //         parentPath, videoFilename, isMovie, mainForm);


            return subsDownloadResult;
        
        
        }

        internal static bool MakeOsDbRequest
            (string useragent, string searchstring,
            out string subtitleData, out WebResponse webResponse)
        {

            HttpWebRequest request = 
                (HttpWebRequest) WebRequest.Create(searchstring);
            
            request.UserAgent = useragent;
            request.Timeout = 10000;

            try
            {
                webResponse = request.GetResponse();
            }
            catch (Exception)
            {

                //Helpers.UpdateProgress("", "OSdb did not respond. Retrying...", null);
                Thread.Sleep(2000);

                try
                {
                    webResponse = request.GetResponse();
                }
                catch (Exception)
                {
                    //Helpers.UpdateProgress("", "OSdb did not respond. Retrying...", null);
                    Thread.Sleep(2000);

                    try
                    {
                        webResponse = request.GetResponse();
                    }
                    catch (Exception)
                    {

                        //Helpers.UpdateProgress("", "Unable to communicate with OSdb.", null);
                        Thread.Sleep(700);
                        {
                            subtitleData = String.Empty;
                            webResponse = null;
                            return true;
                        }

                    }

                }

            }


            subtitleData = null;
            return false;
        
        }


        internal static bool LoadOSDbResponseXmlDocument
            (XmlDocument xDoc,
            WebResponse webResponse, 
            out string subtitleData)
        {

            Stream stream = webResponse.GetResponseStream();

            try
            {
                if (stream != null)
                    xDoc.Load(stream);
            }
            catch (Exception)
            {

                //Helpers.UpdateProgress("", "Unable to download subtitle. " +
                //                           "The response from OSdb was invalid.", null);

                Thread.Sleep(2000);

                {
                    subtitleData = String.Empty;
                    return true;
                }
            }


            subtitleData = String.Empty;
            return false;
        
        }



        internal static bool MakeOsDbRequestGetResponse
            (string searchstring,
            XmlDocument xDoc,
            string useragent,
             out string subtitleData)
        {
            WebResponse webResponse;

            string subtitleDataA;

            if (MakeOsDbRequest(useragent, searchstring, out subtitleDataA, out webResponse))
            {
                subtitleData = subtitleDataA;
                return true;
            }


            string subtitleDataB;

            if (LoadOSDbResponseXmlDocument(xDoc, webResponse, out subtitleDataB))
            {
                subtitleData = subtitleDataB;
                return true;
            }

            subtitleData = null;
            return false;


        }


        internal static decimal GetSubtitleRating(int i, XmlNodeList subrating)
        {

            decimal rating;
            
            if (!String.IsNullOrEmpty(subrating[i].InnerText))
            {

                try
                {
                    rating = Convert.ToDecimal(subrating[i].InnerText)/10;
                }
                catch (Exception)
                {
                    rating = 0;
                }

            }
            else rating = 0;

            Debugger.LogMessageToFile("Subtitle rating: " + rating);
            
            return rating;
        }




        internal static XmlNodeList DefineSubtitleXmlNodeElements(XmlDocument xDoc, 
            out XmlNodeList subrating, out XmlNodeList download)
        {
            XmlNodeList subtitle = xDoc.GetElementsByTagName("subtitle");
            download = xDoc.GetElementsByTagName("download");
            subrating = xDoc.GetElementsByTagName("subrating");

            return subtitle;
        }




        internal static void RenameSubtitleAccordingToVideoFilename
            ( string parentPath, string videoFilename,
             string subtitleFilename, string subtitleExtension)
        {

            //Construct subtitle path
            string subtitleFullPath = parentPath + "\\" + subtitleFilename;

            //Rename subtitle according to video file's name
            string source = subtitleFullPath;
            string destination = parentPath + "\\" + videoFilename + subtitleExtension;


            if (File.Exists(destination))
                return;
            
            try
            {
                File.Move(source, destination);
            }
            catch (Exception e)
            {

                //MainImportingEngine.ThisProgress.Progress
                //    (MainImportingEngine.CurrentProgress,
                //     "Error occured while moving/renaming " +
                //     source + ": " + e.Message);

                Thread.Sleep(2000);

                Debugger.LogMessageToFile("[Video Subtitle Downloader] An unexpecter error occured" +
                                          " while trying to rename subtitle " + source + ". The error was: " + e);
             
                
            }
        }



        internal static bool PerformSubtitleDownload( string zipfilePath, WebClient client, string firstsub)
        {

            //MainImportingEngine.ThisProgress.Progress(MainImportingEngine.CurrentProgress,
            //                                          "Downloading Subtitle for " + item.Name + " in 5 seconds...");

            Thread.Sleep(10000);

            try
            {
                client.DownloadFile(firstsub, zipfilePath);
            }
            catch (Exception)
            {
                try
                {
                    client.DownloadFile(firstsub, zipfilePath);
                }
                catch (Exception e)
                {


                    //MainImportingEngine.ThisProgress.Progress(MainImportingEngine.CurrentProgress,
                    //    "Unable to download a subtitle for this video due to a network issue.");

                    Thread.Sleep(1300);

                    Debugger.LogMessageToFile(
                        "MediaFairy was unable to download a subtitle for this video" +
                        " because an unexpected error occurred in the downloading method. " +
                        "The error was: " + e);

                    return false;
                }
            }
            return true;

        }




        internal static string ConstructExtractedSubtitleFilename
            (string zipfilePath, out string subtitleExtension, string videoFileName)
        {

            string subtitleFilename =
                RecognizeCorrectSubtitle(zipfilePath);


            if (String.IsNullOrEmpty(subtitleFilename))
            {

                Debugger.LogMessageToFile("Could not extract subtitle for "
                            + videoFileName + ". The archive is corrupt.");
                
                Thread.Sleep(5000);
                
                subtitleExtension = null;
                
                return subtitleFilename;
            
            }


            subtitleExtension = subtitleFilename.Remove(0, subtitleFilename.Length - 4);


            return subtitleFilename;

        }

        internal static string ConstructSubtitleFilenameAndExtractSubtitle
            (string parentPath, string videoFilename,
             FastZip fz, string zipfilePath, out string subtitleFilename,
             out string subtitleExtension)
        {

            subtitleFilename = ConstructExtractedSubtitleFilename
                (zipfilePath, out subtitleExtension, videoFilename);

            videoFilename = videoFilename.Substring(0, videoFilename.Length - 4);


            fz.ExtractZip(zipfilePath, parentPath, @"(?i)^.*(?:(?:.srt)|(?:.sub))$");


            Settings.Zipfilepath = zipfilePath;


            return videoFilename;
        }

        internal static bool ValidateDownloadedDataAndRetry(string language, 
        string firstsub, WebClient client, string zipfilePath)
        {

            StreamReader sr = File.OpenText(zipfilePath);
            string zipfileLine = sr.ReadLine();

            if (zipfileLine != null && zipfileLine.Contains("DOCTYPE html"))
            {

                //MainImportingEngine.ThisProgress.Progress
                //    (MainImportingEngine.CurrentProgress,
                //     "The subtitle archive was corrupt." +
                //     " Retrying in 10 seconds...");

                sr.Close();
                Thread.Sleep(10000);

                File.Delete(zipfilePath);

                client.DownloadFile(firstsub, zipfilePath);


                sr = File.OpenText(zipfilePath);

                zipfileLine = sr.ReadLine();

                if (zipfileLine != null && zipfileLine.Contains("DOCTYPE html"))
                {

                    //(MainImportingEngine.CurrentProgress,
                    //"Unable to extract subtitle. The downloaded archive was corrupt.");

                    sr.Close();
                    Thread.Sleep(1500);
                    File.Delete(zipfilePath);

                    return false;
                }

                //MainImportingEngine.ThisProgress.Progress
                //    (MainImportingEngine.CurrentProgress,
                //     "Subtitle for " + item.Name + " was downloaded succesfully.");

                //Thread.Sleep(1500);

                sr.Close();
                sr.Dispose();

            }

            return true;

        }


        internal static string SearchForSubtitleByVideoHash(string moviehash, string language)
        {

            #region local variables

            const string prefix = "http://www.opensubtitles.org/search/sublanguageid-";
            string searchstring = prefix + language + "/moviehash-" + moviehash + "/simplexml";

            const string useragent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.4) Gecko/20070515 Firefox/2.0.0.4";
            XmlDocument xDoc = new XmlDocument();
            #endregion


            string subtitleData;

            if (MakeOsDbRequestGetResponse(searchstring, xDoc, useragent, out subtitleData))
                return subtitleData;

            XmlNodeList subrating;
            XmlNodeList download;

            var subtitle = DefineSubtitleXmlNodeElements
                (xDoc, out subrating, out download);


            #region decide which subtitle to download

            decimal maxRating = 0;
            int preferred = 0;

            for (int i = 0; i < subtitle.Count; i++)
            {
                string sublink = download[i].InnerText;
                Debugger.LogMessageToFile("Subtitle link: " + sublink);

                decimal rating = GetSubtitleRating(i, subrating);

                if (rating <= maxRating)
                    continue;
                
                preferred = i;
                maxRating = rating;
            
            }
            #endregion

            if (download.Count > 0)
            {
                string downloadlink = download[preferred].InnerText;

                if (downloadlink.StartsWith("/download"))
                    downloadlink = "http://www.opensubtitles.org" + downloadlink;

                return downloadlink;

            }

            return null;
        
        }





        internal static void ExtractAndRenameSubtitle(string language,
            string parentPath, string videoFilename, string zipfilePath, FastZip fz)
        {

            //MainImportingEngine.ThisProgress.Progress(MainImportingEngine.CurrentProgress,
            //        "Subtitle for " + item.Name + " was downloaded succesfully.");

            Thread.Sleep(1500);

            string subtitleFilename;
            string subtitleExtension;


            videoFilename
                = ConstructSubtitleFilenameAndExtractSubtitle
                (parentPath, videoFilename, fz, zipfilePath,
                 out subtitleFilename, out subtitleExtension);


            RenameSubtitleAccordingToVideoFilename
                (parentPath, videoFilename,
                subtitleFilename, subtitleExtension);


        }



        internal static string SearchForSubtitleByVideoHashParent
            (string videoHash, string language)
        {

            string firstsub
                = String.Empty;


            if (String.IsNullOrEmpty(videoHash))
            {
                //MainImportingEngine.ThisProgress.Progress(MainImportingEngine.CurrentProgress,
                //   "Unable to search for video subtitle. Video fingerprint is unknown.");
                Thread.Sleep(2000);

                return firstsub;
            }


            firstsub = SearchForSubtitleByVideoHash
                (videoHash, language);

            return firstsub;
        }



    }



}
