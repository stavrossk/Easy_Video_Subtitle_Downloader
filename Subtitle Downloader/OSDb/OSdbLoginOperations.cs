


using System;
using CookComputing.XmlRpc;
using VideoSubtitleDownloader;


//#if USE_MEEDIO 
//using Meedio; 
//#elif USE_MEEDIOS
//using MediaFairy.ImportingEngine;
//using MeediOS;
//#endif


namespace Subtitle_Downloader
{

    public class OSdbLoginOperations
    {

        private const string TxtUrl 
            = "http://api.opensubtitles.org/xml-rpc";


        internal static IOpenSubtitlesRemoteFunctions Proxy;

        public static string Token 
            = "4782378472834782378372";


        public bool ServerInfo()
        {

            CreateProxy();

            try
            {
                Proxy.ServerInfo();
                return true;
            }
            catch (Exception ex)
            {

                Debugger.LogMessageToFile
                    (ex.Message);

                return false;

            }

        }


        public LoginResult SiteLogin()
        {

            CreateProxy();

            const string userAgent
                = "OS Test User Agent";

            LoginResult loginresult
                = Proxy.LogIn
                (string.Empty,
                 string.Empty,
                 "eng",
                 userAgent);

            return loginresult;
        }



        private static void CreateProxy()
        {

            Proxy = XmlRpcProxyGen
                .Create<IOpenSubtitlesRemoteFunctions>();

            Proxy.Timeout = 5000;
            
            Proxy.UserAgent
                = "Mozilla/5.0" +
                  " (Windows; U; Windows NT 5.1;" +
                  " en-US; rv:1.8.1.4)" +
                  " Gecko/20070515 Firefox/2.0.0.4";


            Proxy.UserAgent
                = "OS Test User Agent";
            
            Proxy.Url = TxtUrl;
        
        }






    }


}
