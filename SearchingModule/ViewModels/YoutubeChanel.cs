using SearchingModule.BusinessLogic;
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
        public string ChanelId { get; internal set; }

        public override IList<YoutubeItem> GetAllElements()
        {
            IList<YoutubeItem> result = new List<YoutubeItem>();
            
            return result;
        }

        public override ITaskToDbInserter<YoutubeItem> GetInserter()
        {
            ITaskToDbInserter<YoutubeItem> result  = new ChanelToDbInserter<YoutubeItem>();
            return result;
        }

        public IList<YoutubeMovie> GetItems()
        {
            return YoutubeMovies;
        }
    }
}
