using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace CommonControls.Download
{
    public class Downloader : IDownloader
    {
        class DownladProgress : IProgress<double>
        {
            private Action<int> callBack;

            public DownladProgress(Action<int> progressCallback)
            {
                this.callBack = progressCallback;
            }

            public void Report(double value)
            {
                callBack((int)(value * 100.0));
            }
        }

        IYoutubeClient client = new YoutubeClient();
        public async Task<string> Download(string url, Action<int> progressCallback, string newPath, bool onlyMusic)
        {
            string result = null;
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(url);
            MediaStreamInfo stream = null;
            if(onlyMusic)
            {
                stream = streamInfoSet.Audio.WithHighestBitrate();
            }
            else
            {
                stream = streamInfoSet.Muxed.WithHighestVideoQuality();
            }

            var fileExtension = stream.Container.GetFileExtension();
            result = newPath + "." + fileExtension;
            var fileStream = File.Create(result);

            var progress = new DownladProgress(progressCallback);

            await client.DownloadMediaStreamAsync(stream, fileStream, progress);

            return result;
        }
    }
}
