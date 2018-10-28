using CommonControls.Helpers;
using Newtonsoft.Json.Linq;
using SearchingModule.BusinessLogic;
using SearchingModule.BusinessLogic.YouTubeAPI;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchingModule.ViewModels
{
    public class YoutubePlaylist : YoutubeItem, IContainerForYTubeItems
    {
        private readonly WebClient client = new WebClient();
        public IList<YoutubeMovie> YoutubeMovies { get; set; }
        public string PlaylistId { get; internal set; }

        private static readonly string PLALIST_ITEMS_URL = "https://www.googleapis.com/youtube/v3/playlistItems";

        public override IList<YoutubeItem> GetAllElements()
        {
            var result = GetPLItems();
            return result;
        }

        private IList<YoutubeItem> GetPLItems()
        {
            IList<YoutubeItem> res = new List<YoutubeItem>();

            string parameters = $"?part=snippet%2CcontentDetails&maxResults=25&playlistId={this.PlaylistId}&key={YouTubeAPIServiceHelper.API_KEY}";

            string uri = PLALIST_ITEMS_URL + parameters;
            ProgressHelper.SetProgress("Requesting Youtube API...", 0);

            StringBuilder resBuilder = new StringBuilder();

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse myResp = myReq.GetResponse();
            var decoder = Encoding.UTF8.GetDecoder();
            using (var reader = myResp.GetResponseStream())
            {
                byte[] buffer = new byte[254];
                int idx = 0;
                int recevedBytes = 0;
                
                while ((recevedBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                  
                    double percent = ((double)(++idx * buffer.Length) / (double)myResp.ContentLength) * 100.0;
                    ProgressHelper.SetProgress($"Loading data from API {idx * buffer.Length} of " +
                                               $"{myResp.ContentLength}",
                                               (int)percent);
                    
                    
                    resBuilder.Append(Encoding.UTF8.GetString(buffer, 0, recevedBytes));
                    
                }
            }
            var text = resBuilder.ToString();
            //var response = client.DownloadString(uri);
            PlaylistItemsResponse r = JObject.Parse(text).ToObject<PlaylistItemsResponse>();
            foreach (var item in r.items)
            {
                res.Add(new YoutubeMovie()
                {
                    Name = item.snippet.title,
                    ImgSrc = item.snippet.thumbnails?.@default.url,
                    Id = item.id,
                });
            }

            return res;
        }

        public override ITaskToDbInserter<YoutubeItem> GetInserter()
        {
            ITaskToDbInserter<YoutubeItem> result = new PlaylistToDbInserter<YoutubeItem>();
            return result;
        }

        public IList<YoutubeMovie> GetItems()
        {
            return YoutubeMovies;
        }

    }
}
