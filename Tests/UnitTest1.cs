using System;
using System.Diagnostics;
using CommonControls.Download;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            //IDownloader downloader = new Downloader();
            string url = "https://www.youtube.com/watch?v=3BnfiJnHXvk";
            string path = "tests/test";
            

            //Action<int> ac = a => Debug.WriteLine(a);
            //downloader.Download(url, ac, path);
        }

        [TestMethod]
        public void TestMethod2()
        {
            IDownloader downloader = new Downloader();
            string url = "https://r5---sn-oxup5-fgvl.googlevideo.com/videoplayback?key=yt6&pl=17&ipbits=0&initcwndbps=1360000&txp=5533332&ip=90.156.60.245&expire=1543368042&lmt=1537982141883116&aitags=133,134,135,136,137,160,242,243,244,247,248,278,394,395,396,397&mime=video/mp4&mn=sn-oxup5-fgvl,sn-f5f7lne6&c=WEB&gir=yes&mm=31,29&dur=366.824&id=o-AFrd7EcOvV3jUzAWf4WtLlMNCnx3nAE2_KqMxIvgknu5&sparams=aitags,clen,dur,ei,gir,id,initcwndbps,ip,ipbits,itag,keepalive,lmt,mime,mm,mn,ms,mv,pl,requiressl,source,expire&mv=m&mt=1543346309&ms=au,rdu&source=youtube&clen=14144172&keepalive=yes&itag=134&fvip=5&requiressl=yes&ei=Cpn9W5bML4joyQWYu4mwCg&signature=A39D39EEE8198024CE12F9DD9B924D7A93D4DF13C58.661984CA1B662BF34B7F328F4EAB9605DF2BCF7CF73&ratebypass=yes";
            string path = "tests/test";
            Action<int> ac = a => Debug.WriteLine(a);
            downloader.Download(url, ac, path);
        }
    }
}
