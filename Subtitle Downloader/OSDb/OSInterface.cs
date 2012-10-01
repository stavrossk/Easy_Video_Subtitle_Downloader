

using CookComputing.XmlRpc;

// ReSharper disable CheckNamespace
namespace VideoSubtitleDownloader
// ReSharper restore CheckNamespace
{


    public struct LoginResult
    {
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public string Token;
        public string Status;
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public double Seconds;
    }

    public struct SearchParams
    {
        public string Sublanguageid;
        //public string moviehash;
        //public double moviesize;
        public string Imdbid;
    }

    public interface IOpenSubtitlesRemoteFunctions : IXmlRpcProxy
    {
        [XmlRpcMethod("LogIn")]
         LoginResult LogIn(string username, string password, string language, string useragent);

        [XmlRpcMethod("ServerInfo")]
        XmlRpcStruct ServerInfo();

        [XmlRpcMethod("CheckMovieHash")]
        XmlRpcStruct CheckMovieHash(string moviehash);

        [XmlRpcMethod("CheckMovieHash2")]
        XmlRpcStruct CheckMovieHash2(string moviehash);

        [XmlRpcMethod("GetIMDBMovieDetails")]
        XmlRpcStruct GetImdbMovieDetails(string token, string imdbid);

        [XmlRpcMethod("SearchSubtitles")]
        XmlRpcStruct SearchSubtitles(string token, SearchParams[] ms);

        [XmlRpcMethod("CheckMovieHash")]
        XmlRpcStruct CheckMovieHash(string token, string[] moviehash);

        //[XmlRpcMethod("InsertMovieHash")]
        //XmlRpcStruct InsertMovieHash(string token, HashUploadParams[] uploadParams);

    }
 

}
