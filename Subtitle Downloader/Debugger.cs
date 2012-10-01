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
