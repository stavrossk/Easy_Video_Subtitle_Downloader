


using System;


// ReSharper disable CheckNamespace
namespace VideoSubtitleDownloader
// ReSharper restore CheckNamespace
{

    public class Debugger
    {

        public static void LogMessageToFile(string msg)
        {

            if ( !Settings.WriteDebugLog)
                return;

            System.IO.StreamWriter sw
                = System.IO.File.
                AppendText("Debug.log");


            try
            {
                string logLine = String.Format(
                    "{0:G}: {1}", DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }


    }//endof class


}//endof namespace
