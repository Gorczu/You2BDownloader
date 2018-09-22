using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DownloadModule.BusinessLogic
{
    public class Downloader : IDownloader
    {
        public void Download(string url)
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

                        /*Update the progress bar 
                        if (progressBar1.Value + bytesRead <= progressBar1.Maximum)
                        {
                            progressBar1.Value += bytesRead;
                            lbProgress.Text = progressBar1.Value.ToString() + "/" + dataLength.ToString();
                            progressBar1.Refresh();
                        }
                        */
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedDataStream = memStream.ToArray();

                //Clean up 
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                //May not be connected to the internet 
                //Or the URL might not exist 
                MessageBox.Show("There was an error accessing the URL.");
            }
            finally
            {

            }
        }

    }

}
