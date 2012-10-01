//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
//''    MediaFairy                                                               ''
//''    Copyright (C) 2008-2010  Stavros Skamagkis                               ''
//''                                                                             ''
//''    This program is free software: you can redistribute it and/or modify     ''
//''    it under the terms of the GNU General Public License as published by     ''
//''    the Free Software Foundation, either version 3 of the License, or        ''
//''    (at your option) any later version.                                      ''
//''                                                                             ''
//''    This program is distributed in the hope that it will be useful,          ''
//''    but WITHOUT ANY WARRANTY; without even the implied warranty of           ''
//''    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            ''
//''    GNU General Public License for more details.                             ''
//''                                                                             ''
//''    You should have received a copy of the GNU General Public License        ''
//''    along with this program.  If not, see <http://www.gnu.org/licenses/>.    ''
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


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

    public class OSoperations
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
