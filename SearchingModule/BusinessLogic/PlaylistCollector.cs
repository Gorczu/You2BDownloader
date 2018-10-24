using CommonControls.Helpers;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.BusinessLogic
{
    public class PlaylistCollector : IPlaylistCollector
    {
        private string _pattern;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;

        List<YoutubeItem> _result = new List<YoutubeItem>();
        private List<YoutubeItem> results;
        private List<YoutubeItem> channels;
        private List<YoutubeItem> playlists;

        public PlaylistCollector()
        {
        }

        public void Execute(object parameter)
        {
            var vm = (SearchingViewModel)parameter;
            this._pattern = vm.SearchText;
            Run();
            vm.Result = results;
        }


        public IList<YoutubeItem> GetCollection(string pattern)
        {
            this._pattern = pattern;
            Run();
            return _result;
        }

        private void Run()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = YouTubeAPIServiceHelper.API_KEY,
                ApplicationName = YouTubeAPIServiceHelper.APP_NAME
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = _pattern; // Replace with your search term.
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            this.results = new List<YoutubeItem>();
            string baseURI = @"https://www.youtube.com/watch?v={0}";

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        results.Add(new YoutubeMovie()
                        {
                            Name = searchResult.Snippet.Title,
                            ImgSrc = searchResult.Snippet.Thumbnails.Default__.Url,
                            Source = string.Format(baseURI, searchResult.Id)
                        });
                        break;

                    case "youtube#channel":
                        
                        results.Add(new YoutubeChanel()
                        {
                            ChanelId = searchResult.Id.ChannelId,
                            Name = searchResult.Snippet.ChannelTitle,
                            ImgSrc = searchResult.Snippet.Thumbnails.Default__.Url,
                            Source = string.Format(baseURI, searchResult.Snippet.ChannelId)
                        });
                        break;

                    case "youtube#playlist":

                        results.Add(new YoutubePlaylist()
                        {
                            PlaylistId = searchResult.Id.PlaylistId,
                            Name = searchResult.Snippet.ChannelTitle,
                            ImgSrc = searchResult.Snippet.Thumbnails?.Default__?.Url,
                            Source = string.Format(baseURI, searchResult.Snippet.Title)
                        });
                        break;
                }
            }
        }
    }
}
