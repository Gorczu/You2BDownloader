using Newtonsoft.Json.Linq;
using SearchingModule.BusinessLogic;
using SearchingModule.BusinessLogic.YouTubeAPI;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchingModule.ViewModels
{
    public class YoutubePlaylist : YoutubeItem, IContainerForYTubeItems
    {
        private readonly HttpClient client = new HttpClient();
        public IList<YoutubeMovie> YoutubeMovies { get; set; }
        private static readonly string PLALIST_ITEMS_URL = "https://www.googleapis.com/youtube/v3/playlistItems";

        public override IList<YoutubeItem> GetAllElements()
        {
            IList<YoutubeItem> result = GetPLItems();
            return result;
        }

        private async IList<YoutubeItem> GetPLItems()
        {
            IList<YoutubeItem> res = new List<YoutubeItem>();
            var query = HttpUtility.ParseQueryString(PLALIST_ITEMS_URL);
            query["playlistId"] = this.Source;
            query["maxResults"] = "25";
            query["part"] = "snippet,contentDetails";
            string queryString = query.ToString();
            var response = await client.GetAsync(queryString);
            string content = await response.Content.ReadAsStringAsync();
            PlaylistItemsResponse r = JObject.Parse(content).ToObject<PlaylistItemsResponse>();
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
