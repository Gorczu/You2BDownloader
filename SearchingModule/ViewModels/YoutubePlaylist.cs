using CommonControls.Helpers;
using Newtonsoft.Json.Linq;
using SearchingModule.BusinessLogic;
using SearchingModule.BusinessLogic.YouTubeAPI;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
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
            var response = client.DownloadString(uri);

            PlaylistItemsResponse r = JObject.Parse(response).ToObject<PlaylistItemsResponse>();
            foreach(var item in r.items)
            {
                res.Add(new YoutubeMovie() {
                    Name = item.snippet.title,
                    ImgSrc = item.snippet.thumbnails.@default.url,
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
