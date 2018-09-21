﻿using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using PlaylistModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistModule.BusinessLogic
{
    public class PlaylistCollector : IPlaylistCollector
    {
        private string _pattern;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;

        List<YoutubeItem>  _result = new List<YoutubeItem>();
        private List<YoutubeItem> videos;
        private List<YoutubeItem> channels;
        private List<YoutubeItem> playlists;

        public PlaylistCollector()
        {
        }

        public void Execute(object parameter)
        {
            var vm = (PlaylistViewModel)parameter;
            this._pattern = vm.SearchText;
            Run();
            vm.Result = videos;
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
                ApiKey = "AIzaSyD0PZiNzYdahSXU3QSN3YgiVsB45w5MQWA",
                ApplicationName = "second-flame-190610"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = _pattern; // Replace with your search term.
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            this.videos = new List<YoutubeItem>();
            this.channels = new List<YoutubeItem>();
            this.playlists = new List<YoutubeItem>();

            string baseURI = @"https://www.youtube.com/watch?v={0}";

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(new YoutubeItem()
                        {
                            Name = searchResult.Snippet.Title,
                            Source = string.Format(baseURI, searchResult.Id)
                        });
                        break;

                    case "youtube#channel":
                        channels.Add(new YoutubeItem()
                        {
                            Name = searchResult.Snippet.Title,
                            Source = string.Format(baseURI, searchResult.Id)
                        });
                        break;

                    case "youtube#playlist":
                        playlists.Add(new YoutubeItem()
                        {
                            Name = searchResult.Snippet.Title,
                            Source = string.Format(baseURI, searchResult.Id)
                        });
                        break;
                }
            }

            
        }        
    }
}