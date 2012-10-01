

namespace VideoSubtitleDownloader
{


    public class Settings
    {



        #region Subtitles
        
        public static bool EnableSubtitleDownloader;
        public static string PrimaryLanguage = "eng";
        
        //public static string SecondaryLanguage = "eng";
        
   
        public static string Zipfilepath = "";

        
        #endregion


        #region Diagnostics
        public static bool ConnectionDiagnosticsEnabled;
        public static bool WantOSdbDiagnostics = true;
        public static bool WantFileserverDiagnostics = true;
        #endregion

 

        #region Current Session Variables

        public static bool UserCancels;
        public static bool ImportingStarted;
        public static bool ImportingCompleted;
        public static bool AlwaysPromptForImages;
        public static bool WriteDebugLog = true;
        
        //public static bool AutomaticUpdating;

        public static bool FileInUse;
        public static int KBytesPerSec;
        public static bool SubDloadLimitExceeded;
        #endregion









    }



}
