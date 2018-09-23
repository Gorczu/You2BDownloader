using SearchingModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingModule.ViewModels
{
    public class YoutubeChanel : YoutubeItem, IContainerForYTubeItems
    {
        public IList<YoutubeMovie> YoutubeMovies { get; set; }

        public IList<YoutubeMovie> GetItems()
        {
            return YoutubeMovies;
        }
    }
}
