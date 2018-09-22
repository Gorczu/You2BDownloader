using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadModule.BusinessLogic
{
    interface IDownloader
    {
        void Download(string path);
    }
}
