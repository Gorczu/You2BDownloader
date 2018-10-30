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
        private SearchingViewModel vm;

        public PlaylistCollector()
        {
        }

        public void Execute(object parameter)
        {
            var vm = (SearchingViewModel)parameter;
            Task.Run(() =>
            {
                this.vm = vm;
                this._pattern = vm.SearchText;
                this.vm.Result = this.Run();
                this.vm.UpdateCollection();
            });
        }

        private List<YoutubeItem> Run()
        {
            List<YoutubeItem> result = new List<YoutubeItem>();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = YouTubeAPIServiceHelper.API_KEY,
                ApplicationName = YouTubeAPIServiceHelper.APP_NAME
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = _pattern; // Replace with your search term.
            searchListRequest.MaxResults = 25;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();
            string baseURI = @"https://www.youtube.com/watch?v={0}";

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            ProgressHelper.SetProgress("Getting data from Youtube API", 0);
            int idx = 0;
            foreach (var searchResult in searchListResponse.Items)
            {
                double percent = ((double)idx / (double)searchListRequest.MaxResults) * 100.0;
                ProgressHelper.SetProgress($"Getting data {++idx} of {searchListRequest.MaxResults}", (int)percent);

                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        result.Add(new YoutubeMovie()
                        {
                            Name = searchResult.Snippet.Title,
                            ImgSrc = searchResult.Snippet.Thumbnails.Default__.Url,
                            Source = string.Format(baseURI, searchResult.Id)
                        });
                        break;

                    case "youtube#channel":

                        result.Add(new YoutubeChanel()
                        {
                            ChanelId = searchResult.Id.ChannelId,
                            Name = searchResult.Snippet.ChannelTitle,
                            ImgSrc = searchResult.Snippet.Thumbnails.Default__.Url,
                            Source = string.Format(baseURI, searchResult.Snippet.ChannelId)
                        });
                        break;

                    case "youtube#playlist":

                        result.Add(new YoutubePlaylist()
                        {
                            PlaylistId = searchResult.Id.PlaylistId,
                            Name = searchResult.Snippet.ChannelTitle,
                            ImgSrc = searchResult.Snippet.Thumbnails?.Default__?.Url,
                            Source = string.Format(baseURI, searchResult.Snippet.Title)
                        });
                        break;
                }


            }
            ProgressHelper.SetProgress($"Finished.", 0);
            return result;
        }
    }
}
