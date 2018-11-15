using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControls.Download
{
    public interface IDownloader
    {
        void Download(string path, Action<int> progressCallback, string newPath);
    }
}
