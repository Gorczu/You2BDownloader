using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlaylistModule.BusinessLogic
{
    public interface IPlaylistCollector : ICommand
    {
        IList<YouTubeItem> GetCollection(string pattern);
    }
}
