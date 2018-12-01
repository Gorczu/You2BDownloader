using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControls.Download
{
    public class YouTubeURL
    {
        private String quality = null; // quality text from html source code
        private String stereo3D = null; // format text from html source code
        private String youtubeId = null; // unique youtube video ID (11 alphanum letters) from html source code
        private String htmlTagId = null; // unique ID format from html source code
        private String htmlType = null; // type from html source code
        private String url = null; // URL to video of certain format (mpg, flv, webm, mp4, ..?)
        private String size = null; // dimension of video provided in url
        private String respart = null; // e.g. "LD" or "3D" for filename suffix
        private String audioStreamUrl = null; // if != null URL to audio stream to downlaod in another thread (applies to itag>102)
        private String title = null; // the video title
        private int percentage = -1; // % of download finished
        private bool isDownloading = false; // is currently beeing downloaded 

        public string Quality { get => quality; set => quality = value; }
        public string Stereo3D { get => stereo3D; set => stereo3D = value; }
        public string YoutubeId { get => youtubeId; set => youtubeId = value; }
        public string HtmlTagId { get => htmlTagId; set => htmlTagId = value; }
        public string HtmlType { get => htmlType; set => htmlType = value; }
        public string Url { get => url; set => url = value; }
        public string Size { get => size; set => size = value; }
        public string Respart { get => respart; set => respart = value; }
        public string AudioStreamUrl { get => audioStreamUrl; set => audioStreamUrl = value; }
        public string Title { get => title; set => title = value; }

        public YouTubeURL(String url)
        {
            this.Title = "";
            this.Url = url;
        }

        public YouTubeURL(String url, String videoUrl)
        {
            this.Title = "";
            this.Url = url;
            this.extractUrlParameters(url, videoUrl);
        }

        public YouTubeURL(String url, String videoUrl, String respart)
        {
            this.Url = url;
            this.Title = "";

            this.Respart = respart;
            this.extractUrlParameters(url, videoUrl);
        }

        public YouTubeURL(String url, String videoURL, String respart, String audioStreamUrl)
        {
            this.Title = "";
            this.Url = url;
            this.Respart = respart;
            this.AudioStreamUrl = audioStreamUrl;
            this.extractUrlParameters(url, videoURL);
        }

        private void extractUrlParameters(string url, string videoUrl)
        {
            try
            {
                this.YoutubeId = videoUrl.Substring(videoUrl.IndexOf("v=") + 2);
                String[] tempSplitedUrl = url.Split('&');

                for (int i = 0; i < tempSplitedUrl.Length; i++)
                {
                    if (tempSplitedUrl[i].StartsWith("quality"))
                    {
                        this.Quality = tempSplitedUrl[i].Substring(tempSplitedUrl[i].IndexOf('=') + 1);
                    }
                    if (tempSplitedUrl[i].StartsWith("itag"))
                    {
                        this.HtmlTagId = tempSplitedUrl[i].Substring(tempSplitedUrl[i].IndexOf('=') + 1);
                    }
                    if (tempSplitedUrl[i].StartsWith("type"))
                    {
                        this.HtmlType = tempSplitedUrl[i].Substring(tempSplitedUrl[i].IndexOf('=') + 1);
                    }
                    if (tempSplitedUrl[i].StartsWith("stereo3d"))
                    {
                        this.Stereo3D = tempSplitedUrl[i].Substring(tempSplitedUrl[i].IndexOf('=') + 1);
                    }
                    if (tempSplitedUrl[i].StartsWith("size"))
                    {
                        this.Size = tempSplitedUrl[i].Substring(tempSplitedUrl[i].IndexOf('=') + 1);
                    }
                }

                //JFCMainClient.debugOutput(String.format("video url stored for later use with key: %s - type: %s  quality: %s size:
                //%s   - %s", this.getHtmlTagId(), this.getHtmlType(), this.getQuality(), this.getSize(), this.getUrl()));
            }
            catch (Exception npe)
            {
                //TODO catch must not be empty - happens if URL for selection resolution could not be found
            }
        }

        public void setPercentage(int percentage)
        {
            this.percentage = percentage;
        }

        public void setDownloading()
        {
            isDownloading = true;
        }

        public void setDownloadingFinished()
        {
            this.isDownloading = false;
            this.percentage = -1;
        }

      
        // to use in GUI URL List
        public String toString()
        {
            // change to title sometime
            return (this.isDownloading ? "downloading " : "") + (this.percentage > -1 ? "(" + this.percentage + "%) " : "") + (this.Title.Length > 0 ? this.Title : this.Url);
        }
        
    }
}
