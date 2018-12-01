using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonControls.Download
{
    public class Downloader : IDownloader
    {
        public void Download(string url, Action<int> progressCallback, string newPath)
        {

            var downloadedDataStream = new byte[0];
            try
            {
                //Get a data stream from the url 
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks 
                byte[] buffer = new byte[1024];

                //Get Total Size 
                int dataLength = (int)response.ContentLength;
                
                //Download to memory //Note: adjust the streams here to download directly to the hard drive 
                MemoryStream memStream = new MemoryStream();
                int step = 1;
                while (true)
                {
                    //Try to read the data 
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        //Finished downloading 
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                        //Update the progress bar 
                        double m = step * buffer.Length * 100.0;
                        double percent = m / (double)dataLength;
                        progressCallback((int)percent);
                    }
                    step++;
                }

                //Convert the downloaded stream to a byte array
                downloadedDataStream = memStream.ToArray();
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                File.WriteAllBytes(newPath, downloadedDataStream);

                //Clean up 
                stream.Close();
                memStream.Close();
            }
            catch (Exception ex)
            {
                Debug.Write($"There was an error accessing the URL:{Environment.NewLine}{url}");
                Debug.Write(ex.Message);
            }
            finally
            {
                progressCallback(100);
            }
        }

    }

}
